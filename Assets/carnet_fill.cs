using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class carnet_fill : MonoBehaviour
{ 
    public int etape = 0;
    //public int[] etapeList = {1,2,3,4};
    public static List<int> etapeList = new List<int>(){1,2,3,4,7,10};

    public carnet_anim ca;
    public navigationCarnet nc;

    public GameObject tuto0Text;
    public GameObject tuto1Schema;
    public GameObject tuto1Text;
    public GameObject tuto2Schema;
    public GameObject tuto2Text;
    public GameObject tuto3Schema;
    public GameObject tuto3Text;
    public GameObject tuto3Textb;
    public GameObject[] carnetItem;
    private int fillStep = 0;
    private TMP_Text lastText;
    private Color textColor;

    public AudioSource audioWrite;

    // Start is called before the first frame update
    void Start()
    {
        textColor = tuto1Text.GetComponent<TMP_Text>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Etape 1 = avant le premier module de la première boucle

    public void newLine()
    {
        if(etapeList.Contains(etape))
        {
        	writeSound();
            if (etape==1)
            {
                tuto0Text.GetComponent<TMP_Text>().color = textColor;
                tuto1Text.GetComponent<TMP_Text>().color = Color.black;
                tuto1Text.SetActive(true);
                tuto1Schema.SetActive(true);
                etapeList.Remove(1);
                ca.open();
            }
            else if (etape==2)
            {
                tuto1Text.GetComponent<TMP_Text>().color = textColor;
                tuto2Text.GetComponent<TMP_Text>().color = Color.black;
                tuto2Text.SetActive(true);
                tuto2Schema.SetActive(true);
                etapeList.Remove(2);
                ca.open();
            }
            else if (etape==3)
            {
                tuto2Text.GetComponent<TMP_Text>().color = textColor;
                tuto3Text.GetComponent<TMP_Text>().color = Color.black;
                tuto3Text.SetActive(true);
                tuto3Schema.SetActive(true);
                etapeList.Remove(3);
                ca.open();
            }
            else if (etape==4)
            {
                tuto3Text.GetComponent<TMP_Text>().color = textColor;
                tuto3Textb.GetComponent<TMP_Text>().color = textColor;
                carnetItem[fillStep].GetComponent<TMP_Text>().color = Color.black;
                carnetItem[fillStep].SetActive(true);
                fillStep +=1;
                nc.newPage(2);
                etapeList.Remove(4);
                ca.open();
            }
            else if (etape == 7)
            {
                carnetItem[fillStep - 1].GetComponent<TMP_Text>().color = textColor;
                carnetItem[fillStep].GetComponent<TMP_Text>().color = Color.black;
                carnetItem[fillStep].SetActive(true);
                fillStep +=1;
                etapeList.Remove(7);
                ca.open();
            }
            else if(etape == 10)
            {
                carnetItem[fillStep - 1].GetComponent<TMP_Text>().color = textColor;
                carnetItem[fillStep].GetComponent<TMP_Text>().color = Color.black;
                carnetItem[fillStep].SetActive(true);
                fillStep +=1;
                etapeList.Remove(10);
                ca.open();
            }
        }
        
    }

    public void backseat()
    {
        if(etape == 3)
        {
            tuto3Text.GetComponent<TMP_Text>().color = textColor;
            tuto3Textb.GetComponent<TMP_Text>().color = Color.black;
            tuto3Textb.SetActive(true);
            ca.open();
        }
    }

    public void writeSound()
    {
    	audioWrite.Play();
    }
}
