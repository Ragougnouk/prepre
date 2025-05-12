using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigationCarnet : MonoBehaviour
{
    public GameObject[] pagesG;
    public GameObject[] pagesD;
    public int authLvl = 1;
    public int currentLvl = 1;
    public GameObject flDroite;
    public GameObject flGauche;

    public bool carnetOp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flGauche.activeSelf || flDroite.activeSelf)
        {
            if(Input.GetKeyDown("left"))
            {
                gaucheNav();
            }
            else if (Input.GetKeyDown("right"))
            {
                droiteNav();
            }
        }
    }

    public void droiteNav()
    {
        if (currentLvl < authLvl)
        {
            pagesG[currentLvl-1].SetActive(false);
            pagesD[currentLvl-1].SetActive(false);
            pagesG[currentLvl].SetActive(true);
            pagesD[currentLvl].SetActive(true);
            currentLvl += 1;
        }

        if (currentLvl == authLvl)
        {
            flDroite.SetActive(false);
        }

        if(currentLvl > 1)
        {
            flGauche.SetActive(true);
        }
    }

    public void gaucheNav()
    {
        if (currentLvl > 1)
        {
            pagesG[currentLvl-1].SetActive(false);
            pagesD[currentLvl-1].SetActive(false);
            pagesG[currentLvl-2].SetActive(true);
            pagesD[currentLvl-2].SetActive(true);
            currentLvl -= 1;
        }

        if (currentLvl == 1)
        {
            flGauche.SetActive(false);
        }

        if (currentLvl < authLvl)
        {
            flDroite.SetActive(true);
        }
    }

    public void newPage(int page)
    {
        authLvl = page;
        pagesG[currentLvl-1].SetActive(false);
        pagesD[currentLvl-1].SetActive(false);
        currentLvl = authLvl;
        pagesG[currentLvl-1].SetActive(true);
        pagesD[currentLvl-1].SetActive(true);
        flGauche.SetActive(true);
    }

}
