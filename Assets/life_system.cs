using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class life_system : MonoBehaviour
{
    public breakerController bc;
    public GameObject lightScreen;
    public GameObject[] lights;
    public carnet_fill cf;

    public int healthPoints;
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lightsOn()
    {
        /*for(int i=0; i < healthPoints; i++)
        {
            //lights[i].SetActive(true);
            StartCoroutine(lightRythm(lights[i]));
        }*/
        lightScreen.SetActive(true);
        StartCoroutine(lightRythm());
    }

    private IEnumerator lightRythm()
    {
        for(int i=0; i < healthPoints; i++)
        {
            lights[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void full_life()
    {
        healthPoints = 10;
    }

    public void loseHP()
    {
        lights[healthPoints-1].SetActive(false);
        if(healthPoints>1)
        {
            healthPoints -= 1;

            if (healthPoints == 5)
            {
                cf.backseat();
            }
        }
        else
        {
            perdu();
        }
    }

    public void lightsOut()
    {
        lightScreen.SetActive(false);
    }

    private void perdu()
    {
        //cf.etape -= 2;
        bc.mod1Delay = true;
        bc.powerOff();
        //healthPoints =10;
    }
}
