using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinking_backlight : MonoBehaviour
{
    public GameObject[] light;

    public Color[] colorLight;

    public float blinkRate = 10.0f;

    private bool inBlink = false;

    private float lastTime;

    public AudioSource hum;
    public AudioSource blinking;
    public AudioClip[] blinkSound;
    public AudioClip alarm;

    public float alarmVol;

    public bool alarmOn = true;
    public bool muteSoundAlarm = false;
    private bool beeping = false;

    // Start is called before the first frame update
    void Start()
    {
     lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {   
        if (alarmOn && !beeping)
        {
            StartCoroutine(blinkOff());
        }

        /*if(!alarmOn)
        {
            print("yo");
            StopCoroutine(blinkOff());
        }*/
        
        if(!alarmOn /*&& ! beeping*/ && (Time.time - lastTime > 2.0f) )
        {
            
            if(Random.Range(0,100) < blinkRate && !inBlink)
            {
                inBlink = true;
                //float rdmTime = Random.Range(0.05f,0.5f);
                StartCoroutine(blinkInLoop());
                
            }
            lastTime = Time.time;
            
        }
    }

    private IEnumerator blinkInLoop()
    {
        int blinkType = Random.Range(0,3);

        if ( blinkType < 2 )
        {
            int counts = 0;
            int nb = Random.Range(1,3);
            while(counts < nb)

            {
                counts += 1;
                float blinkTime = Random.Range(0.05f,0.5f);
                blinking.PlayOneShot(blinkSound[0]);
                hum.Stop();
                foreach (GameObject gl in light)
                {
                    Color col = gl.GetComponent<SpriteRenderer>().color;
                    col = colorLight[0];
                    gl.GetComponent<SpriteRenderer>().color = col;
                }
            
                yield return new WaitForSeconds(blinkTime);

                hum.Play();
                blinking.PlayOneShot(blinkSound[1]);

                foreach (GameObject gl in light)
                {
                    Color col = gl.GetComponent<SpriteRenderer>().color;
                    col = colorLight[1];
                    gl.GetComponent<SpriteRenderer>().color = col;
                }

                yield return new WaitForSeconds(0.025f);

                foreach (GameObject gl in light)
                {
                    Color col = gl.GetComponent<SpriteRenderer>().color;
                    col = colorLight[2];
                    gl.GetComponent<SpriteRenderer>().color = col;
                }

            }

        }

        else

        {
            int counts = 0;
            int nb = Random.Range(1,3);
            while(counts < nb)

            {
                counts += 1;
                blinking.PlayOneShot(blinkSound[1]);
                foreach (GameObject gl in light)
                {
                    Color col = gl.GetComponent<SpriteRenderer>().color;
                    col = colorLight[1];
                    gl.GetComponent<SpriteRenderer>().color = col;
                }

                yield return new WaitForSeconds(0.05f);

                foreach (GameObject gl in light)
                {
                    Color col = gl.GetComponent<SpriteRenderer>().color;
                    col = colorLight[2];
                    gl.GetComponent<SpriteRenderer>().color = col;
                }

                yield return new WaitForSeconds(0.05f);
            }
        }

        inBlink = false;
    }

    private IEnumerator blinkOff()
    {
        blinking.volume = alarmVol;
        beeping = true;
        while(true)
        {
            if(!alarmOn)
            {
                yield break;
            }
            if(!muteSoundAlarm)
            {
                blinking.volume = 0.05f;
                foreach (GameObject gl in light)
                {
                    Color col = gl.GetComponent<SpriteRenderer>().color;
                    col = colorLight[3];
                    gl.GetComponent<SpriteRenderer>().color = col;
                }
            }
            

            blinking.PlayOneShot(alarm);
            yield return new WaitForSeconds(0.25f);
            blinking.PlayOneShot(alarm);
            yield return new WaitForSeconds(0.25f);

            if(!muteSoundAlarm)
            {
                foreach (GameObject gl in light)
                {
                    Color col = gl.GetComponent<SpriteRenderer>().color;
                    col = colorLight[2];
                    gl.GetComponent<SpriteRenderer>().color = col;
                }
            }

            yield return new WaitForSeconds(2.0f);
            
        }
        
        foreach (GameObject gl in light)
        {
            Color col = gl.GetComponent<SpriteRenderer>().color;
            col = colorLight[2];
            gl.GetComponent<SpriteRenderer>().color = col;
        }
        beeping = false;
        blinking.volume = 0.05f;
        
    }
}
