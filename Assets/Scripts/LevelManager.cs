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
    float lastMinutes;
    float lastSeconds;

    /*******************************************************************
                           Gameplay events handlered                      
    *******************************************************************/

    int score;
    int difficultyLevel;
    int seedNumber;
    int treeNum;

    [SerializeField] int treeMultiplier = 1;
    [SerializeField] int enemyMultiplier = 1;
    [SerializeField] int seedMutiplier = 1;

    public static event EventHandler onEndLevel;
    public static event EventHandler<onScoreChangedArgs> onScoreChanged;
    public class onScoreChangedArgs : EventArgs
    {
        public int eScore;
    }

    public static event EventHandler onTreeSpawned;
    public static event EventHandler onTreeKilled;
    public static event EventHandler onEnemyKilled;
    public static event EventHandler onDifficltyLevelChange;
    public static event EventHandler onSeedsObtained;
    public static event EventHandler onHavingEnoughSeeds;

    void Awake()
    {
        onEndLevel += Level_Restart;
        onScoreChanged += changeScore;
        onDifficltyLevelChange += incDiffLevel;
        onSeedsObtained+= incSeeds;
        onHavingEnoughSeeds+= showDialog;
        onTreeSpawned+= (object sender, EventArgs e) => treeNum++;
        onTreeKilled+= (object sender, EventArgs e) => treeNum--;
        onEnemyKilled+= computeScore;
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

        /*******************************************************************
                                  timer part                            
        *******************************************************************/
        if (lastMinutes != minutes || lastSeconds != seconds)
        {
            lastMinutes = minutes;
            lastSeconds = seconds;
            Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));
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
    private void Level_Restart(object sender, System.EventArgs e)
    {
        SceneManager.LoadScene(0);
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
        int x = 0;
        //TODO show dialog 
    }
    private void computeScore(object sender, EventArgs e)
    {
        score += (seedMutiplier*seedNumber) + (treeNum*treeMultiplier) + (enemyMultiplier);
        onScoreChanged?.Invoke(this, new onScoreChangedArgs { eScore = this.score });
    }
}