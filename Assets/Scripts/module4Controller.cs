using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class module4Controller : MonoBehaviour
{

    public TMP_Text txt;
    private string messages;
    public MessagesList messageFile;

    public moduleSequencer modSeq;

    public GameObject light;
    public GameObject screen;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
