using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mod3test : MonoBehaviour
{
    public TMP_Text txt;
    private string message;
    public MessagesList messageFile;
    public moduleSequencer modSeq;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable()
    {
        message = messageFile.stringList[modSeq.loopNumber];
        txt.SetText(message);
    }

    /*void OnDisable()
    {
        txt.SetText("");
    }*/

    public void reInit()
    {
        txt.SetText("");
    }
}
