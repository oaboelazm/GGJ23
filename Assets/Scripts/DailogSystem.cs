using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailogSystem : MonoBehaviour
{
	public TextMeshProUGUI Text;
	public string[] Sentences;
	public float typingSpeed;
	private int _index;

	public GameObject Panel;

	public GameObject Btn;

	Animator panelAnim;
    // Start is called before the first frame update
    void Start()
    {
        panelAnim = Panel.GetComponent<Animator>();

        MakeDailog(0);
    }

    // Update is called once per frame
    void Update()
    {
    	if(Text.text == Sentences[_index]){
    		Btn.SetActive(true);
    	}
      
    }

    public void MakeDailog(int indexOfSen)
    {
    	panelAnim.SetBool("Show", true);
    	panelAnim.SetBool("Hide", false);
    	StartCoroutine(Type(indexOfSen));
    }

    IEnumerator Type(int index){
    	_index = index;
    	foreach(char letter in Sentences[index].ToCharArray()){
    		Text.text += letter;
    		yield return new WaitForSeconds(typingSpeed);
    	}
    }

    public void OK(){
    	Btn.SetActive(false);
    	Text.text = "";
    	panelAnim.SetBool("Show", false);
    	panelAnim.SetBool("Hide", true);
    }

}
