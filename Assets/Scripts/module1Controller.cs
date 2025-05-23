using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;


public class module1Controller : MonoBehaviour
{

    public carnet_fill cf;
    public carnet_anim ca;
    public moduleSequencer ms;
    public blinking_backlight bbl;
    public ucomNumber ucomNb;

    public GameObject anim;

    public GameObject pointeur;
    public GameObject sonar;
    public Transform coordC;
    private AudioSource sound;
    public AudioSource reticuleSource;
    public AudioSource newSignal;
    public AudioClip retMoveSound;
    public AudioClip successSound;
    //public AudioClip newSignalSound;
    private bool inMod;

    public GameObject emptyScreen;
    public GameObject screenDark;
    public GameObject screen1;
    public GameObject reticule;
    public GameObject words;
    public GameObject wordsLoc;

    public float distVal = 0.1f;
    public bool nextStep = false;

    public float size = 4.92f;
    public float minSize = 0.8f;
    public float margin = 0.4f;

    public float speed = 0.1f;
    public float pxSize = 0.04f;


    public GameObject light;
    public float flickerInterval = 0.2f; // Adjust this to change the flicker speed

    //private Vector2 startPos;
    private float nextFlickerTime;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    public bool actif = false;

    public bool on = false;

    private Vector3 initPos;

    public breakerController bc;

    public bool randomTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        //coordC = cadre.transform.position;
        //startPos = reticule.transform.position;
        sound = sonar.GetComponent<AudioSource>();
        minX = coordC.position.x;
        maxX = coordC.position.x+size;
        minY = coordC.position.y;
        maxY = coordC.position.y+size;
    }

    void OnEnable()
    {
        /*screen1.SetActive(true);
        reticule.SetActive(true);
        float minX = coordC.position.x-size;
        float maxX = coordC.position.x+size;
        float minY = coordC.position.y-size;
        float maxY = coordC.position.y+size;
        float randomX = Random.Range(minX,maxX);
        float randomY = Random.Range(minY,maxY);
        while(randomX < coordC.position.x-minSize && randomY < coordC.position.y - minSize)
        {
            randomX = Random.Range(minX,maxX);
            randomY = Random.Range(minY,maxY);
        }
        //print("x "+ minX+" "+maxX+", y "+minY+" "+maxY);
        sonar.transform.position = new Vector3(randomX ,randomY,0);
        nextFlickerTime = Time.time;*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (actif && !ca.actif && on)
        {
            //bbl.muteSoundAlarm = true;

            reticuleSource.clip = retMoveSound;
            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                //bbl.alarmOn = false;
                //StopCoroutine(newSignalLoop());
                words.SetActive(false);
                if(!reticuleSource.isPlaying)
                {
                    reticuleSource.Play();
                }
            }
            else if (reticuleSource.isPlaying)
            {
                reticuleSource.Stop();
            }
            float dirX = pointeur.transform.position.x + Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
            float dirY = pointeur.transform.position.y + Input.GetAxisRaw("Vertical") * speed * Time.deltaTime ;
            dirX = Mathf.Round(dirX/pxSize) * pxSize;
            dirY = Mathf.Round(dirY/pxSize) * pxSize;
            dirX = Mathf.Clamp(dirX,minX,maxX);
            dirY = Mathf.Clamp(dirY,minY,maxY);
            pointeur.transform.position = new Vector3(dirX,dirY,0);

            soundUpdate();

            if (Time.time > nextFlickerTime)
            {
                // Toggle the sprite's visibility by enabling/disabling the SpriteRenderer component
                light.GetComponent<SpriteRenderer>().enabled = !light.GetComponent<SpriteRenderer>().enabled;
                if (light.GetComponent<SpriteRenderer>().enabled)
                {
                    sound.Play();
                }

                // Set the time for the next flicker
                nextFlickerTime = Time.time + flickerInterval;
            }
        }

        if(!actif && on && !randomTarget)
        {
            randomTarget = true;
        }

        if(randomTarget && (actif || !on))
        {
            randomTarget = false;
        }
        
    }

    void soundUpdate()
    {
        if (Vector3.Distance(sonar.transform.position,pointeur.transform.position) < sound.maxDistance)
        {
            sound.volume = Mathf.Clamp(Mathf.Pow(1 - (Vector3.Distance(sonar.transform.position,pointeur.transform.position) / sound.maxDistance),4.0f),0.04f, 0.4f);
            flickerInterval = Vector3.Distance(sonar.transform.position,pointeur.transform.position)/4.0f;
        }

        if(Vector3.Distance(sonar.transform.position,pointeur.transform.position)< distVal)
        {
            //cf.etape += 1;
            actif = false;
            //ca.open();
            StartCoroutine(success());
            nextStep = true;
            
            sound.volume = 0;
        }
    }

    void OnDisable()
    {
        //screen1.SetActive(false);
        //reticule.SetActive(false);
        //reticule.transform.position = startPos;
    }

    public void reInit()
    {
        //screen1.SetActive(false);
        //reticule.SetActive(false);
        float randomX = Random.Range(minX + margin,maxX - margin);
        float randomY = Random.Range(minY + margin,maxY - margin);
        while(Vector3.Distance(pointeur.transform.position, new Vector3(randomX, randomY,0)) <minSize)
        {
            randomX = Random.Range(minX + margin,maxX - margin);
            randomY = Random.Range(minY + margin,maxY - margin);
        }
        //print("x "+ minX+" "+maxX+", y "+minY+" "+maxY);
        sonar.transform.position = new Vector3(randomX ,randomY,0);
        nextFlickerTime = Time.time;
        //reticule.transform.position = startPos;
    }

    public void turnOn(bool delay)
    {
        initPos = pointeur.transform.position;
        
        words.SetActive(true);
        emptyScreen.SetActive(true);
        light.SetActive(true);
        screenDark.SetActive(true);
        if(delay)
        {
            StartCoroutine(turnOnDelay());
        }
        else
        {
            reticule.SetActive(true);
            screenDark.SetActive(false);
            emptyScreen.SetActive(false);
            screen1.SetActive(true);
            //words.SetActive(true);
            //actif = true;
            on = true;
        }
        ucomNb.updateNumber(ms.loopNumber, ms.lastLoopCount);
    }

    public void turnOff()
    {
        words.SetActive(false);
        wordsLoc.SetActive(false);
        on = false;
        //actif = false;
        light.SetActive(false);
        screen1.SetActive(false);
        reticule.SetActive(false);
        anim.SetActive(false);
        pointeur.transform.position = initPos;
        //reInit();
    }

    public void turnOffReset()
    {
        words.SetActive(false);
        wordsLoc.SetActive(false);
        light.SetActive(false);
        screen1.SetActive(false);
        reticule.SetActive(false);
        anim.SetActive(false);
        pointeur.transform.position = initPos;
        reInit();
    }

    private IEnumerator turnOnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        words.SetActive(true);
        //StartCoroutine(newSignalLoop());
        yield return new WaitForSeconds(1.5f);
        reticule.SetActive(true);
        screenDark.SetActive(false);
        emptyScreen.SetActive(false);
        screen1.SetActive(true);
        //words.SetActive(true);
        //actif = true;
        on = true;
    }

    private IEnumerator success()
    {
        reticuleSource.Stop();
        reticuleSource.PlayOneShot(successSound);

        wordsLoc.SetActive(true);

        //initPos = pointeur.transform.position;
        anim.SetActive(true);
        yield return new WaitForSeconds(1);
        /*if(Array.Exists(cf.etapeList, element => element == cf.etape))
        {
            cf.newLine();
            ca.open();
        }*/
        //on = false;
        StartCoroutine(ms.winMod1());
    }

    public void active()
    {
        actif = true;
        if(!on)
        {
            bc.flickOn(0,1);
        }
    }

    public void inactive()
    {
        actif= false;
        light.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void retPos()
    {
        initPos = pointeur.transform.position;
    }

    /*private IEnumerator newSignalLoop()
    {
        while(true)
        {
            newSignal.PlayOneShot(newSignalSound);
            yield return new WaitForSeconds(0.25f);
            newSignal.PlayOneShot(newSignalSound);
            yield return new WaitForSeconds(2.0f);
        }      
    }*/
}
