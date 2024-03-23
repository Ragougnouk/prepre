using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mod3test : MonoBehaviour
{
    public TMP_Text txt;
    private string message;
    public MessagesList messageFile;
    public moduleSequencer ms;

    public GameObject canvasMod3Down;

    public bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable()
    {
        //message = messageFile.stringList[modSeq.loopNumber];
        //txt.SetText(message);
    }

    /*void OnDisable()
    {
        txt.SetText("");
    }*/

    public void reInit()
    {
        txt.SetText("");
    }

    public void turnOn()
    {
        canvasMod3Down.SetActive(true);
    }

    public void turnOff()
    {
        canvasMod3Down.SetActive(false);
    }

    public void active()
    {
        message = messageFile.stringList[ms.loopNumber];
        txt.SetText(message);
    }
}
