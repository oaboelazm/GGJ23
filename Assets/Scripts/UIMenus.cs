using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMenus : MonoBehaviour
{
   public void ChangeToScene(string sceneToChangeTo) 
    {
		Application.LoadLevel(sceneToChangeTo);
    }

	public void QuitGame()
	{
		Application.Quit();
	}

    //************************************* Mute sound of the game. *********************************************//

	public GameObject SoundON; //Button of the Sound On
	public GameObject SoundOFF; //Button of the sound Off
	public AudioSource Audio;
	public AudioSource Effect;
                
    void Start() {
       // _Sound();
        StartCoroutine(Type());
    }
	public void Mute ()
	{
		Effect.mute = false;
		Audio.mute = false;
        SoundON.SetActive(false);
        SoundOFF.SetActive(true);
        PlayerPrefs.SetInt("Muted",0);
	}

	public void startsound ()
	{
		Effect.mute = true;
		Audio.mute = true;
        SoundON.SetActive(true);
        SoundOFF.SetActive(false);
        PlayerPrefs.SetInt("Muted",1);
	}
    
    void _Sound()
    {
        if(PlayerPrefs.GetInt("Muted") == 0)
        {
            Mute();
        }
        else if(PlayerPrefs.GetInt("Muted") == 1)
        {
            startsound();
        }
    }

    //********************************************** Write the game name *******************************************//

    public TextMeshProUGUI Text;

    public string GameName = "Game Name!";

    public float typingSpeed;

   IEnumerator Type(){
    	foreach(char letter in GameName.ToCharArray()){
    		Text.text += letter;
    		yield return new WaitForSeconds(typingSpeed);
    	}
    }

}
