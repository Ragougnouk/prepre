using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carnet_fill : MonoBehaviour
{ 
    public int etape = 0;
    public int[] etapeList = {1,2,3,4};

    public carnet_anim ca;
    public navigationCarnet nc;

    public GameObject tuto1Schema;
    public GameObject tuto1Text;
    public GameObject tuto2Schema;
    public GameObject tuto2Text;
    public GameObject tuto3Schema;
    public GameObject tuto3Text;
    public GameObject tuto3Textb;
    public GameObject[] carnetItem;
    private int fillStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Etape 1 = avant le premier module de la première boucle

    public void newLine()
    {
        if (etape==1)
        {
            tuto1Text.SetActive(true);
            tuto1Schema.SetActive(true);
        }
        else if (etape==2)
        {
            tuto2Text.SetActive(true);
            tuto2Schema.SetActive(true);
        }
        else if (etape==3)
        {
            tuto3Text.SetActive(true);
            tuto3Schema.SetActive(true);
        }
        else if (etape==4)
        {
            carnetItem[fillStep].SetActive(true);
            nc.newPage(2);
        }
    }

    public void backseat()
    {
        if(etape == 3)
        {
            tuto3Textb.SetActive(true);
            ca.open();
        }
    }
}
