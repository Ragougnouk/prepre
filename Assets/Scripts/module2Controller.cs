using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random=UnityEngine.Random;

public class module2Controller : MonoBehaviour
{
    public carnet_fill cf;
    public carnet_anim ca;
    public moduleSequencer ms;
    public flicker flckr;
    public breakerController bc;

    public GameObject light;
    public GameObject blankScreen;

    public GameObject cadre;
    public GameObject caseDet;
    public GameObject sig1,sig2,sig3;
    public GameObject[] sigs;

    public GameObject point1,point2, point3;
    public GameObject signal;
    public GameObject ecran;
    public GameObject led1,led2,led3;
    public Transform[] signalPos; 

    private bool p1,p2,p3 = false;

    private Vector2 coordC;
    private Color origColor;

    public float speed = 1.0f;
    public float pxSize = 0.04f;

    public float distDet = 0.1f;
    public float size = 1.0f;

    public bool nextStep = false;
    public bool actif = false;

    public bool on = false;

    private int litLed = 0;

    // Start is called before the first frame update
    void Start()
    {
        coordC = cadre.transform.position;
        origColor = point1.GetComponent<SpriteRenderer>().color;

    }

    private void OnEnable()
    {
        /*randomSignalPosition();
        ecran.SetActive(true);
        signal.SetActive(true);*/

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (actif && !ca.actif && on)
        {
            viseurMove();
        }
        /*float dirX = caseDet.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        dirX = Mathf.Clamp(dirX,coordC.x - size, coordC.x + size);
        caseDet.transform.position = new Vector3(dirX,caseDet.transform.position.y,0);
        if (!p1)
        {
            testP1();
        }
        if (!p2)
        {
            testP2();
        }
        if (!p3)
        {
            testP3();
        }
        if (p1 && p2 && p3)
        {
            //reInit();
            nextStep = true;

        }
        ledTest();*/

    }

    void Update()
    {
        if (actif && !ca.actif && Input.GetKeyDown("space"))
        {
            signalPinPoint();
        }
    }

    void viseurMove()
    {
        float dirX = caseDet.transform.position.x + Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        dirX = Mathf.Round(dirX/pxSize) * pxSize;
        dirX = Mathf.Clamp(dirX,coordC.x, coordC.x + size);
        caseDet.transform.position = new Vector3(dirX,caseDet.transform.position.y,0);
    }

    void signalPinPoint()
    {
            if (!p1)
            {
                testP1();
            }
            if (!p2)
            {
                testP2();
            }
            if (!p3)
            {
                testP3();
            }
            if (p1 && p2 && p3)
            {
                //reInit();
                nextStep = true;
                //cf.etape += 1;
                StartCoroutine(success());

            }
    }

    void testP1()
    {
        if(point1.transform.position.x > caseDet.transform.position.x && point1.transform.position.x < (caseDet.transform.position.x +0.28f)) //Vector3.Distance(point1.transform.position,caseDet.transform.position) < distDet)
        {
            p1 = true;
            SpriteRenderer point1C = point1.GetComponent<SpriteRenderer>();
            point1C.color = new Color(0,250,0);
            litLed += 1;
            ledTest();
        }
    }

    void testP2()
    {
        if(point2.transform.position.x > caseDet.transform.position.x && point2.transform.position.x < (caseDet.transform.position.x +0.28f))
        {
            p2 = true;
            SpriteRenderer point2C = point2.GetComponent<SpriteRenderer>();
            point2C.color = new Color(0,250,0);
            litLed += 1;
            ledTest();
        }
    }

    void testP3()
    {
        if(point3.transform.position.x > caseDet.transform.position.x && point3.transform.position.x < (caseDet.transform.position.x +0.28f))
        {
            p3 = true;
            SpriteRenderer point3C = point3.GetComponent<SpriteRenderer>();
            point3C.color = new Color(0,250,0);
            litLed += 1;
            ledTest();
        }
    }

    void ledTest()
    {
        if(litLed > 0)
        {
            if(litLed == 1)
            {
                led1.SetActive(true);
            }
            else if(litLed == 2)
            {
                led2.SetActive(true);
            }
            else if(litLed == 3)
            {
                led3.SetActive(true);
            }
        }
    }

    public void reInit()
    {
        p1 = false;
        p2 = false;
        p3 = false;
        litLed=0;
        led1.SetActive(false);
        led2.SetActive(false);
        led3.SetActive(false);
        SpriteRenderer pointColorReset = point1.GetComponent<SpriteRenderer>();
        pointColorReset.color = origColor;
        pointColorReset = point2.GetComponent<SpriteRenderer>();
        pointColorReset.color = origColor;
        pointColorReset = point3.GetComponent<SpriteRenderer>();
        pointColorReset.color = origColor;
        ecran.SetActive(false);
        signal.SetActive(false);
    }

    void randomSignalPosition()
    {
        int[] usedIndices = new int[7];
        int i1 = Random.Range(0,signalPos.Length);
        sig1.transform.position = signalPos[i1].position;
        usedIndices[0]= i1;

        int i2 = Random.Range(0,signalPos.Length);
        if(usedIndices.Contains(i2))
        {
            i2 = Random.Range(0,signalPos.Length);
        }
        sig2.transform.position = signalPos[i2].position;
        usedIndices[1]= i2;

        int i3 = Random.Range(0,signalPos.Length);
        if(usedIndices.Contains(i3))
        {
            i3 = Random.Range(0,signalPos.Length);
        }
        sig3.transform.position = signalPos[i3].position;
        usedIndices[2]= i3;

        int i4 = Random.Range(0,signalPos.Length);
        if(usedIndices.Contains(i4))
        {
            i4 = Random.Range(0,signalPos.Length);
        }
        sigs[0].transform.position = signalPos[i4].position;
        usedIndices[3]= i4;

        int i5 = Random.Range(0,signalPos.Length);
        if(usedIndices.Contains(i5))
        {
            i5 = Random.Range(0,signalPos.Length);
        }
        sigs[1].transform.position = signalPos[i5].position;
        usedIndices[4]= i5;

        int i6 = Random.Range(0,signalPos.Length);
        if(usedIndices.Contains(i6))
        {
            i6 = Random.Range(0,signalPos.Length);
        }
        sigs[2].transform.position = signalPos[i6].position;
        usedIndices[5]= i6;

        int i7 = Random.Range(0,signalPos.Length);
        if(usedIndices.Contains(i7))
        {
            i7 = Random.Range(0,signalPos.Length);
        }
        sigs[3].transform.position = signalPos[i7].position;
        
    }

    /*void OnDisable()
    {
        p1 = false;
        p2 = false;
        p3 = false;
        litLed=0;
        led1.SetActive(false);
        led2.SetActive(false);
        led3.SetActive(false);
        SpriteRenderer pointColorReset = point1.GetComponent<SpriteRenderer>();
        pointColorReset.color = origColor;
        pointColorReset = point2.GetComponent<SpriteRenderer>();
        pointColorReset.color = origColor;
        pointColorReset = point3.GetComponent<SpriteRenderer>();
        pointColorReset.color = origColor;
        ecran.SetActive(false);
        signal.SetActive(false);
    }*/

    public void turnOn()
    {
        on = true;
        blankScreen.SetActive(true);
        light.SetActive(true);
    }

    public void turnOff()
    {
        on = false;
        //actif = false;
        flckr.enabled = false;
        ecran.SetActive(false);
        signal.SetActive(false);
        blankScreen.SetActive(false);
        light.SetActive(false);
        //reInit();
    }

    public void turnOffReset()
    {
        flckr.enabled = false;
        ecran.SetActive(false);
        signal.SetActive(false);
        blankScreen.SetActive(false);
        light.SetActive(false);
        reInit();
    }

    public void active()
    {
        actif = true;
        flckr.enabled = true;
        randomSignalPosition();
        ecran.SetActive(true);
        signal.SetActive(true);
        blankScreen.SetActive(false);
        if(!on)
        {
            bc.flickOn(2,3);
        }
    }

    public void inactive()
    {
        actif = false;
        flckr.enabled = false;
        light.SetActive(true);
        //ecran.SetActive(false);
        //signal.SetActive(false);
    }

    private IEnumerator success()
    {
        actif = false;
        //cf.etape += 1;
        yield return new WaitForSeconds(1);
        /*if(Array.Exists(cf.etapeList, element => element == cf.etape))
        {
            cf.newLine();
            ca.open();
        }*/
        StartCoroutine(ms.winMod2());
    }
}
