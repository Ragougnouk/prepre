using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class module2Controller : MonoBehaviour
{
    public GameObject cadre;
    public GameObject caseDet;
    public GameObject sig1,sig2,sig3;
    public GameObject point1,point2, point3;
    public GameObject signal;
    public GameObject ecran;
    public GameObject led1,led2,led3;
    public Transform[] signalPos; 

    private bool p1,p2,p3 = false;

    private Vector2 coordC;
    private Color origColor;

    public float speed = 1.0f;
    public float distDet = 0.1f;
    public float size = 1.0f;

    public bool nextStep = false;

    private int litLed = 0;

    // Start is called before the first frame update
    void Start()
    {
        coordC = cadre.transform.position;
        origColor = point1.GetComponent<SpriteRenderer>().color;

    }

    private void OnEnable()
    {
        randomSignalPosition();
        ecran.SetActive(true);
        signal.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        float dirX = caseDet.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime;
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
        ledTest();

    }

    void testP1()
    {
        if(Input.GetKeyDown("space") && Vector3.Distance(point1.transform.position,caseDet.transform.position) < distDet)
        {
            p1 = true;
            SpriteRenderer point1C = point1.GetComponent<SpriteRenderer>();
            point1C.color = new Color(0,250,0);
            litLed += 1;
        }
    }

    void testP2()
    {
        if(Input.GetKeyDown("space") && Vector3.Distance(point2.transform.position,caseDet.transform.position) < distDet)
        {
            p2 = true;
            SpriteRenderer point2C = point2.GetComponent<SpriteRenderer>();
            point2C.color = new Color(0,250,0);
            litLed += 1;
        }
    }

    void testP3()
    {
        if(Input.GetKeyDown("space") && Vector3.Distance(point3.transform.position,caseDet.transform.position) < distDet)
        {
            p3 = true;
            SpriteRenderer point3C = point3.GetComponent<SpriteRenderer>();
            point3C.color = new Color(0,250,0);
            litLed += 1;
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
        int[] usedIndices = new int[3];
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
}
