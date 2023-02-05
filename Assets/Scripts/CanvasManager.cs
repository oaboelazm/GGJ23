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



    [SerializeField] public GameObject dialogText;
    [SerializeField] public GameObject seedCounter;
    [SerializeField] public GameObject scoreCounter;

    [SerializeField] Image[] treeImages ;
    


    [SerializeField]Sprite treeHealthFull;
    [SerializeField]Sprite treeHealthLow;
    [SerializeField]Sprite treeHealthDead;


    [SerializeField] GameObject HP;

    void Start()
    {
        LevelManagerScript = LevelManagerObject.GetComponent<LevelManager>();
        LevelManager.onSeedsObtained += CanvasUpdateSeed;
        LevelManager.onScoreChanged += CanvasUpdateScore;
       //LevelManager.onDifficltyLevelChange += incDiffLevel;
        //LevelManager.onHavingEnoughSeeds+= showDialog;

        LevelManager.onTreeAttacked+= updateTreeHealthSprite;
        LevelManager.onTreeSpawned+= updateTreeHealthSprite;
        LevelManager.onTreeKilled+=updateTreeHealthSprite; 
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
        
    }
    public void CanvasUpdateSeed (object sender, EventArgs e){
        seedCounter.GetComponent<TMP_InputField>().text = string.Format("##", LevelManagerScript.seedNumber);
    }
    public void CanvasUpdateScore(object sender, EventArgs e)
    {
        scoreCounter.GetComponent<TMP_InputField>().text = string.Format("######", LevelManagerScript.score);
    }
    public void endLevel(){
        LevelManagerScript.restart_level_public();
    }
    public void updateTreeHealthSprite(object sender, LevelManager.onTreeDamagedArgs e){
        int MHOT = LevelManagerScript.MaxHealthOfTrees;
        int TreeHealth= LevelManagerScript.treesHealth[e.treeIndex];
        float heathPersentage = ((float)TreeHealth)/((float)(MHOT));
        if(heathPersentage>0.5f){
            treeImages[e.treeIndex].sprite =treeHealthFull;
        }
        else if(heathPersentage>0){
            treeImages[e.treeIndex].sprite =treeHealthLow;
        }
        else {
            treeImages[e.treeIndex].sprite =treeHealthDead;  
        }
        HP.GetComponent<Image>().fillAmount= ((float)LevelManagerScript.treeNum) / 4.0f;
    }
}
