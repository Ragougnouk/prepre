using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class module4Controller : MonoBehaviour
{

    public TMP_Text txt;
    private string messages;
    public MessagesList messageFile;

    public moduleSequencer modSeq;
    public breakerController bc;

    public GameObject light;
    public GameObject screen;

    public GameObject winMessage;
    public Sprite[] decSprite;

    private IEnumerator inProgress;

    public bool on = true;

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
        
    }

    public void updateText()
    {
        if(!on)
        {
            bc.flickOn(6,7);
        }
        messages = messageFile.stringList[modSeq.loopNumber-1];
        txt.SetText(messages);
    }

    public void turnOn()
    {
        txt.enabled = true;
        light.SetActive(true);
        screen.SetActive(true);
    }

    public void turnOff()
    {
        txt.enabled = false;
        light.SetActive(false);
        screen.SetActive(false);
    }

    public void active()
    {

    }

    public void inactive()
    {

    }



    public void loopWin()
    {
        StopCoroutine(inProgress);
        inProgress = decInProgress();
        txt.enabled = false;
        StartCoroutine(winSeq());
        StartCoroutine(lightFlicker());
    }

    private IEnumerator winSeq()
    {
        Image Im = winMessage.GetComponent<Image>();
        winMessage.SetActive(true);
        for(int i = 0; i < decSprite.Length; i++)
        {
            Im.sprite = decSprite[i];
            yield return new WaitForSeconds(0.1f);
        }
        for(int i = 0; i < 6; i++)
        {
            Im.enabled = !Im.enabled;
            screen.GetComponent<Image>().enabled = !screen.GetComponent<Image>().enabled; 
            yield return new WaitForSeconds(0.2f);
        }
        screen.GetComponent<Image>().enabled = true;
        Im.enabled = true;
        winMessage.SetActive(false);
        updateText();
        txt.enabled = true;
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
        StartCoroutine(inProgress);
    }

    private IEnumerator decInProgress()
    {
        while(true)
        {
            messages = "DECIPHERING.";
            txt.SetText(messages);
            yield return new WaitForSeconds(1.0f);
            messages = "DECIPHERING..";
            txt.SetText(messages);
            yield return new WaitForSeconds(1.0f);
            messages = "DECIPHERING...";
            txt.SetText(messages);
            yield return new WaitForSeconds(1.0f); 
        } 
    }
}
