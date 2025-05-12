using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeAnim : MonoBehaviour
{

    public flicker flckr;
    public bool startAnim;

    public GameObject smoke;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startAnim)
        {
            flckr.enabled = false;
            smoke.SetActive(true);
        }
    }
}
