using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;


public class CanvasManager : MonoBehaviour
{
    [SerializeField] public GameObject LevelManagerObject;
    LevelManager LevelManagerScript;

    private IEnumerator coroutine;

    [SerializeField] public GameObject dialogText;
    [SerializeField] public GameObject seedCounter;
    [SerializeField] public GameObject scoreCounter;

    [SerializeField] Image[] treeImages;



    [SerializeField] Sprite treeHealthFull;
    [SerializeField] Sprite treeHealthLow;
    [SerializeField] Sprite treeHealthDead;


    [SerializeField] GameObject HP;

    [SerializeField] DailogSystem dailogSystem;
    public string[] dialog_string;
    public bool[] dialog_appeared;


    int DlogStart = 0;
    int DlogEnemiesSpawned = 1;
    int DLogEnemyKilled = 2;
    int DlogEnoughSeeds = 3;


    public TextMeshProUGUI Timer;
    void Start()
    {
        LevelManagerObject = GameObject.Find("LevelManagerObject");
        LevelManagerScript = LevelManagerObject.GetComponent<LevelManager>();
        LevelManager.onSeedsObtained += CanvasUpdateSeed;
        LevelManager.onScoreChanged += CanvasUpdateScore;
        //LevelManager.onDifficltyLevelChange += incDiffLevel;
        //LevelManager.onHavingEnoughSeeds+= showDialog;

        LevelManager.onTreeAttacked+= updateTreeHealthSprite;
        LevelManager.onTreeSpawned+= updateTreeHealthSprite;
        LevelManager.onTreeKilled+=updateTreeHealthSprite;


        dialog_string = new string[] {
            "hi help us protect our trees",
            "kill the enemies before they get to the trees",
            "each enemy will drop a seed which can be used to plant a tree",
            "if you have enough seeds you can plant a tree"
        };
        dialog_appeared = new bool[dialog_string.Length];

        for (int i = 0; i< dialog_appeared.Length; i++)
        {
            dialog_appeared[i] = false;
        }


        StartCoroutine(WaitAndPrint(0.0f, DlogStart));
        StartCoroutine(WaitAndPrint(4.0f, DlogEnemiesSpawned));
    }

    private void LevelManager_onTreeKilled(object sender, LevelManager.onTreeDamagedArgs e)
    {
        throw new NotImplementedException();
    }

    private void LevelManager_onTreeSpawned(object sender, LevelManager.onTreeDamagedArgs e)
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        if(Timer is null)
        {
            return;
        }
        
        Timer.text = string.Format("{0:00}:{1:00}", LevelManagerScript.lastMinutes, LevelManagerScript.lastSeconds);
    }
    public void CanvasUpdateSeed(object sender, EventArgs e)
    {
        seedCounter.GetComponent<TextMeshProUGUI>().text =  LevelManagerScript.seedNumber.ToString("D2");
        if (LevelManagerScript.seedNumber >= LevelManagerScript.seedNeededToPlant)
        {
            StartCoroutine(WaitAndPrint(0.3f, DLogEnemyKilled));
        }
    }
    public void CanvasUpdateScore(object sender, EventArgs e)
    {
        //scoreCounter.GetComponent<TextMeshProUGUI>().text = string.Format("###,###", LevelManagerScript.score);
        scoreCounter.GetComponent<TextMeshProUGUI>().text =LevelManagerScript.score.ToString("D6");
        StartCoroutine(WaitAndPrint(0.3f, DlogEnoughSeeds));
    }
    public void endLevel()
    {
        LevelManagerScript.restart_level_public();
    }
    public void updateTreeHealthSprite(object sender, LevelManager.onTreeDamagedArgs e)
    {
        int MHOT = LevelManagerScript.MaxHealthOfTrees;
        float TreeHealth = LevelManagerScript.treesHealth[e.treeIndex];
        float heathPersentage = ((float)TreeHealth)/((float)(MHOT));
        if (heathPersentage>0.5f)
        {
            treeImages[e.treeIndex].sprite =treeHealthFull;
        }
        else if (heathPersentage>0)
        {
            treeImages[e.treeIndex].sprite =treeHealthLow;
        }
        else
        {
            treeImages[e.treeIndex].sprite =treeHealthDead;
        }
        HP.GetComponent<Image>().fillAmount= ((float)LevelManagerScript.treeNum) / 4.0f;
        //Debug.Log(LevelManagerScript.treeNum.ToString());
    }
    private IEnumerator WaitAndPrint(float waitTime, int dialogIndex)
    {

        yield return new WaitForSeconds(waitTime);
        dailogSystem.MakeDailog(dialog_string[dialogIndex]);
        dialog_appeared[dialogIndex] = true;

    }
}