using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ongletSwitch : MonoBehaviour
{
    public GameObject coms;
    public GameObject trad;

    public GameObject iconCom;
    public GameObject iconTrad;

    public Sprite spriteComOn;
    public Sprite spriteComOff;
    public Sprite spriteTradOn;
    public Sprite spriteTradOff;

    private Image imTrad;
    private Image imCom;

    // Start is called before the first frame update
    void Start()
    {
        imTrad = iconTrad.GetComponent<Image>();
        imCom = iconCom.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tradOn()
    {
        coms.SetActive(false);
        trad.SetActive(true);
        imTrad.sprite = spriteTradOn;
        imCom.sprite = spriteComOff;

    }

    public void tradOff()
    {
        coms.SetActive(true);
        trad.SetActive(false);
        imTrad.sprite = spriteTradOff;
        imCom.sprite = spriteComOn;
    }
    
}
