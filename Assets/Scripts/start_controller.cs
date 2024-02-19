using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class start_controller : MonoBehaviour
{
	public GameObject startMenu;
	public GameObject background;
	public GameObject nameMenu;
	public GameObject errorM;
	public TMP_InputField nameInputField;

	public string sceneName;

	public LeanTweenType easeType;
	public AnimationCurve curve1;

	//public bool name_field = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nameMenu.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
        	NameRegister();
        }
    }

    public void NameRegister()
    {
    	string playerName = nameInputField.text;
    	if (!string.IsNullOrWhiteSpace(playerName))
    	{
	        PlayerPrefs.SetString("PlayerName", playerName);
	        PlayerPrefs.Save();
	        print(playerName);
	        //StartCoroutine(launchGame());
	        launchAnim();
	    }
	    else
	    {
	    	StartCoroutine(errorMessage());
	    }
    }

    private IEnumerator errorMessage()
    {
    	errorM.SetActive(true);
    	yield return new WaitForSeconds(3);
    	errorM.SetActive(false);
    }

    private void launchAnim()
    {
    	nameMenu.SetActive(false);
    	LeanTween.moveY(background.GetComponent<RectTransform>(),140.0f,1.0f).setDelay(0.5f).setEase(easeType);
    	LeanTween.moveY(startMenu.GetComponent<RectTransform>(),500.0f,1.0f).setDelay(0.5f).setOnComplete(changeScene).setEase(easeType);
    }


	private void changeScene()
	{
		SceneManager.LoadScene(sceneName);
	}

    /*private IEnumerator launchGame()
    {
    	nameMenu.SetActive(false);
    	LeanTween.moveY(startMenu.GetComponent<RectTransform>(),365.0f,1.0f).setEase(easeType);
    	yield return new WaitForSeconds(1);
    	SceneManager.LoadScene(sceneName);
    }*/
}
