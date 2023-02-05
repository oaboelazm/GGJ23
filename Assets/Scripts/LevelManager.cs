using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /*******************************************************************
                            timer Variables                       
    *******************************************************************/
    float timer;
    float minutes;
    float seconds;
    public float lastMinutes;
    public float lastSeconds;

    /*******************************************************************
                           Gameplay events handlered                      
    *******************************************************************/

    public int score;
    int difficultyLevel;
    public int seedNumber;
    public int treeNum;




    [SerializeField] int treeMultiplier = 1;
    [SerializeField] int enemyMultiplier = 1;
    [SerializeField] int seedMutiplier = 1;


    

    public static event EventHandler onEndLevel;
    public static event EventHandler<onScoreChangedArgs> onScoreChanged;
    public class onScoreChangedArgs : EventArgs { public int eScore; }

    public static event EventHandler<onTreeDamagedArgs> onTreeSpawned;
    public static event EventHandler<onTreeDamagedArgs> onTreeKilled;
    public static event EventHandler onEnemyKilled;
    public static event EventHandler onDifficltyLevelChange;
    public static event EventHandler onSeedsObtained;
    public static event EventHandler onHavingEnoughSeeds;
    public static event EventHandler<onTreeDamagedArgs> onTreeAttacked;
    public class onTreeDamagedArgs : EventArgs { public int treeIndex; }
    [SerializeField] public int[] treesHealth;
    public int MaxHealthOfTrees;
    
    [SerializeField] public int seedNeededToPlant;
    private GameObject[] trees;


    void Awake()
    {
        treesHealth = new int[] { MaxHealthOfTrees, MaxHealthOfTrees, MaxHealthOfTrees, MaxHealthOfTrees };
        treeNum = 4;
        onScoreChanged += changeScore;
        onDifficltyLevelChange += incDiffLevel;
        onSeedsObtained+= incSeeds;
        onHavingEnoughSeeds+= showDialog;
        onTreeSpawned+= PlantTree;
        onTreeKilled+= KillTree;
        onEnemyKilled+= computeScore;
        onTreeAttacked+= dicDamage;

        

        
        


    }



    void Start()
    {
        /*******************************************************************
                                  timer part                            
        *******************************************************************/
        StartCoroutine(mainTick());
        timer = 0;
        minutes=0;
        seconds=0;
        lastMinutes = 0;
        lastSeconds = 0;


    }


    void Update()
    {

        trees = GameObject.FindGameObjectsWithTag("Tree");

         
        if(trees.Length  == 0)
        {
              GameObject Soundfx;
        Soundfx = GameObject.FindWithTag("sfx");
        Soundfx.GetComponent<SFXSystem>().MakeSound("Lose");

            // Show dialog 
           // Level_Restart(SceneManager.GetActiveScene().buildIndex);
        }
        /*******************************************************************
                                  timer part                            
        *******************************************************************/
        if (lastMinutes != minutes || lastSeconds != seconds)
        {
            lastMinutes = minutes;
            lastSeconds = seconds;
           //Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
    }

    /*******************************************************************
                            timer Methods                         
    *******************************************************************/
    IEnumerator mainTick()
    {
        //wait 1 second
        yield return new WaitForSeconds(1);
        //add 1 second to the time variable
        timer++;
        //update UI
        DisplayTime(timer);
        //start mainTick again
        StartCoroutine(mainTick());
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
    }


    /*******************************************************************
                           Scene Methods                        
    *******************************************************************/
    public void Level_Restart(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    /*******************************************************************
                           Gameplay events                      
    *******************************************************************/

    private void changeScore(object sender, onScoreChangedArgs e)
    {
        score= e.eScore;
        //TODO update Score in the UI
    }
    private void incDiffLevel(object sender, EventArgs e)
    {
        difficultyLevel++;
        //TODO difficultyLevel in the UI
        //TODO changeSpawnRate
    }

    private void incSeeds(object sender, EventArgs e)
    {
        seedNumber++;
        //TODO SeedNumber in the UI
    }

    private void showDialog(object sender, EventArgs e)
    {
        return;
        //TODO show dialog 
    }
    private void computeScore(object sender, EventArgs e)
    {
        score += (seedMutiplier*seedNumber) + (treeNum*treeMultiplier) + (enemyMultiplier);
        onScoreChanged?.Invoke(this, new onScoreChangedArgs { eScore = this.score });
    }

    private void dicDamage(object sender, onTreeDamagedArgs e)
    {
        treesHealth[e.treeIndex]=treesHealth[e.treeIndex]-1;
        if(treesHealth[e.treeIndex]<=0) {onTreeKilled?.Invoke(this, e);}
    }
    public void restart_level_public()
    {
        onEndLevel?.Invoke(this,new EventArgs());
    }
    public void KillTree(object sender, onTreeDamagedArgs e)
    {
        int mytreeNum = 0;
        for (int x = 0; x < treesHealth.Length; x++) { if (treesHealth[x]>=0) mytreeNum++; }
        treeNum=mytreeNum;
    }
   public void PlantTree(object sender, onTreeDamagedArgs e)
    {
        if(seedNumber >= seedNeededToPlant){
            seedNumber-=seedNeededToPlant;
            treesHealth[e.treeIndex]=MaxHealthOfTrees;
            treeNum++;
        }
       
    }
    public void notifySeedOptained()
    {
        onSeedsObtained?.Invoke(this, new EventArgs());
    }

    public void notifyTreeSpawned(int ntreeIndex)
    {
        onTreeSpawned?.Invoke(this, new onTreeDamagedArgs { treeIndex = ntreeIndex });
    }

    public void notifyTreeKilled(int ntreeIndex)
    {
        onTreeKilled?.Invoke(this, new onTreeDamagedArgs { treeIndex = ntreeIndex });
    }

    public void notifyTreeAttacked(int ntreeIndex)
    {
        onTreeAttacked?.Invoke(this, new onTreeDamagedArgs { treeIndex = ntreeIndex });
    }
}