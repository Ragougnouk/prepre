using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mod3test : MonoBehaviour
{
    public TMP_Text txt;
    private string message;
    private string tempMessage;
    public MessagesList messageFile;
    public moduleSequencer ms;

    public GameObject canvasMod3Down;

    public AudioSource beepSource;
    public AudioClip beepSound;

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
        StartCoroutine(progText(message));
        //txt.SetText(message);
    }

    private IEnumerator progText(string msg)
    {
        for(int i=0; i < msg.Length; i++)
        {
            tempMessage = msg.Substring(0,i+1);
            //print(tempMessage);
            txt.SetText(tempMessage);
            beepSource.PlayOneShot(beepSound);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
