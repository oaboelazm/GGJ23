using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailogSystem : MonoBehaviour
{
	public TextMeshProUGUI Text;
	public float typingSpeed;
	public GameObject Panel; // dialog place holder
	Animator panelAnim;

	bool IsDialog = false;
    
    void Start()
    {
        //panelAnim = Panel.GetComponent<Animator>();

    }

    void Update()
    {
      if (IsDialog && Input.GetKeyDown(KeyCode.Space))
      {
            //panelAnim?.SetBool("Show", false);
            //panelAnim?.SetBool("Hide", true);
            IsDialog = false;
            Text.text = "";
        }
        if (!IsDialog)
        {
            Panel.gameObject.SetActive(false);
        }
        else
        {
            Panel.gameObject.SetActive(true);
        }

    }

    public void MakeDailog(string Sentence)
    {
    	//panelAnim?.SetBool("Show", true);
    	//panelAnim?.SetBool("Hide", false);
    	StartCoroutine(Type(Sentence));
	IsDialog = true;
    }

    IEnumerator Type(string Sentence){
    	
    	foreach(char letter in Sentence.ToCharArray()){
    		Text.text += letter;
    		yield return new WaitForSeconds(typingSpeed);
    	}
    }

}
