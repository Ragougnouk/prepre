using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class module4Controller : MonoBehaviour
{

    public TMP_Text txt;
    public TMP_Text hintText;
    private string messages;
    public MessagesList messageFile;

    public moduleSequencer modSeq;
    public breakerController bc;

    public GameObject light;
    public GameObject screen;

    public GameObject winMessage;
    public Sprite[] decSprite;
    public GameObject[] lines1;
    public GameObject[] lines2;
    public GameObject linesObj;

    private IEnumerator inProgress;

    public bool on = false;

    public bool randomTarget = false;

    public bool testWin = false;

    public bool hintDisplay = false;
    private string hintWait ="";
    public GameObject hint;
    public GameObject loading;
    public GameObject loading_bar;      
    //public GameObject stopSign;
    //public GameObject lineHint;

    // Start is called before the first frame update
    void Start()
    {
        inProgress = decInProgress();
    }


    void OnEnable()
    {
        //txt.SetText(messages[modSeq]);
    }

    // Update is called once per frame
    void Update()
    {
        if(on && !randomTarget)
        {
            randomTarget = true;
        }

        if(randomTarget && !on)
        {
            randomTarget = false;
        }

        /*if (testWin == true)
        {
            launchWinTest();
            testWin = false;
        }*/
    }

    public void updateText()
    {
        if(!on)
        {
            bc.flickOn(6,7);
        }
        messages = messageFile.stringList[modSeq.loopNumber-1];
        StartCoroutine(progText(messages));
        //txt.SetText(messages);
    }

    public void turnOn()
    {
        on = true;
        txt.enabled = true;
        light.SetActive(true);
        screen.SetActive(true);
        //stopSign.SetActive(true);
        //hint.SetActive(true);
    }

    public void turnOff()
    {
        on = false;
        txt.enabled = false;
        light.SetActive(false);
        screen.SetActive(false);
        //stopSign.SetActive(false);
        hint.SetActive(false);
        //lineHint.SetActive(false);
    }

    public void active()
    {

    }

    public void inactive()
    {

    }



    public void loopWin()
    {
        //hintDisplay = false;

        StopCoroutine(inProgress);
        inProgress = decInProgress();
        txt.enabled = false;
        StartCoroutine(winSeq());

        StartCoroutine(lightFlicker());
    }

    private IEnumerator winSeq()
    {
        //stopSign.SetActive(false);
        
        hint.SetActive(false);
        loading.SetActive(true);
        loadingBarSize(0);

        //lineHint.SetActive(false);
        linesObj.SetActive(true);
        for(int i = 0; i < lines1.Length; i++)
        {
            lines1[i].SetActive(true);
            yield return new WaitForSeconds(0.05f);
            lines1[i].SetActive(false);
        }

        Image Im = winMessage.GetComponent<Image>();
        winMessage.SetActive(true);
        
        for(int i = 0; i < decSprite.Length; i++)
        {
            Im.sprite = decSprite[i];
            lines2[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            lines2[i].SetActive(false);
        }
         lines2[lines2.Length - 1].SetActive(true);
        for(int i = 0; i < 6; i++)
        {
            Im.enabled = !Im.enabled;
            if(lines2[lines2.Length - 1].activeSelf)
            {
                lines2[lines2.Length - 1].SetActive(false);
            }
            else
            {
                lines2[lines2.Length - 1].SetActive(true);
            }
            //screen.GetComponent<Image>().enabled = !screen.GetComponent<Image>().enabled; 
            yield return new WaitForSeconds(0.2f);
        }
        lines2[lines2.Length - 1].SetActive(false);
        screen.GetComponent<Image>().enabled = true;
        Im.enabled = true;
        winMessage.SetActive(false);
        linesObj.SetActive(false);
        updateText();
        txt.enabled = true;

        //hintText.SetText("EXCLUDING");
    }

    private IEnumerator lightFlicker()
    {
        for(int i = 0; i < 10; i++)
        {
            light.GetComponent<Image>().enabled = !light.GetComponent<Image>().enabled;
            yield return new WaitForSeconds(0.1f);
        }
        light.GetComponent<Image>().enabled = true;
    }


    public void startDec()
    {
        StartCoroutine("decInProgress");
    }

    private IEnumerator decInProgress()
    {
        if(!hintDisplay)
        {
            hintWait = "EXCLUDING.";
            hintText.SetText(messages);
            yield return new WaitForSeconds(1.0f);
            hintWait = "EXCLUDING..";
            hintText.SetText(messages);
            yield return new WaitForSeconds(1.0f);
            hintWait = "EXCLUDING...";
            hintText.SetText(messages);
            yield return new WaitForSeconds(1.0f); 
        } 
    }

    public void launchWinTest()
    {
        StartCoroutine(winSeq());
    }

    public void loadingBarSize(int taille)
    {
        loading_bar.GetComponent<RectTransform>().sizeDelta = new Vector2(taille,3);
    }

    private IEnumerator progText(string msg)
    {
        string tempMessage = "";
        for(int i=0; i < msg.Length; i++)
        {
            tempMessage = msg.Substring(0,i+1);
            //print(tempMessage);
            txt.SetText(tempMessage);
            yield return new WaitForSeconds(0.05f);
        }

    }
}
