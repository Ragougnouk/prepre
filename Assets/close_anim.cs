using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_anim : MonoBehaviour
{
    public GameObject carnet;
    public GameObject icon;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void close()
    {
        carnet.SetActive(false);
        icon.SetActive(true);
    }
}
