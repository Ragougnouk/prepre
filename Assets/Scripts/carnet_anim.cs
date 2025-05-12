using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carnet_anim : MonoBehaviour
{
    public GameObject carnet;
    public GameObject iconOpen;
    public GameObject iconClose;
    public bool actif = false;
    public Animator anim;

    public carnet_fill cf;

    public AudioSource openClose;
    public AudioClip[] bookSound;

    public bool launched = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && launched)
        {
            if(actif)
            {
                anim.SetTrigger("close");
            }
            else
            {
                open();
            }
        }
    }

    public void open()
    {
        //cf.newLine();
        carnet.SetActive(true);
        openClose.PlayOneShot(bookSound[0]);
        actif = true;
        iconClose.SetActive(true);
        iconOpen.SetActive(false);
    }

    public void close()
    {
        actif = false;
        carnet.SetActive(false);
        openClose.PlayOneShot(bookSound[1]);
        iconOpen.SetActive(true);
        iconClose.SetActive(false);
    }
}
