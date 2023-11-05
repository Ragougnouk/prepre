using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mod3test : MonoBehaviour
{
    public TMP_Text txt;
    public string[] messages;
    public GameObject cases;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        txt.SetText(messages[0]);
        cases.SetActive(true);
    }
}
