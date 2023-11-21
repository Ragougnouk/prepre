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
        messages = messageFile.stringList[modSeq.loopNumber];
        txt.SetText(messages);
    }
}
