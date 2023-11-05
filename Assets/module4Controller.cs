using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class module4Controller : MonoBehaviour
{

    public TMP_Text txt;
    public string[] messages;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnEnable()
    {
        txt.SetText(messages[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
