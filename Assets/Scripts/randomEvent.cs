using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomEvent : MonoBehaviour
{
    public screenShake ss;

    public staticNoise[] statNoise;
    public module1Controller mod1;
    public module2Controller mod2;
    public Module3Controller mod3;
    public module4Controller mod4;

    public GameObject[] eyes;

    //public float noisePercent = 0.001f;
    public float eyePercent = 0.001f;
    private float lastTime;

    public bossScript bs;
    public fearEffect fe;
    public AudioSource source;

    public AudioSource[] sourceNoise;

    public GameObject[] canvasNoise;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastTime > 1.0f)
        {
            randomNoise(0.001f);
            lastTime = Time.time;
        }
    }

    public void canvasSwitchOn()
    {
        foreach(GameObject go in canvasNoise)
        {
            go.SetActive(true);
        }

        foreach(AudioSource audioS in sourceNoise)
        {
            audioS.UnPause();
        }
    }

    public void canvasSwitchOff()
    {
        foreach(GameObject go in canvasNoise)
        {
            go.SetActive(false);
        }

        foreach(AudioSource audioS in sourceNoise)
        {
            if(audioS.isPlaying)
            {
                audioS.Pause();
            }
        }
    }

    private void randomNoise(float noisePercent)
    {
        if(mod1.randomTarget && !statNoise[0].active)
        {
            float randomValue = Random.value;
            //print(randomValue);

            // Check if the random number is within the activation chance
            if (randomValue < noisePercent)
            {
                // Activate the noise effect
                statNoise[0].active = true;
                mod1.on = false;
            }
        }
    }


    public void startNoise(int module)
    {
        if(module == 0)
        {
            statNoise[0].active = true;
            mod1.on = false; statNoise[1].active = true;
            mod2.on = false;
            statNoise[2].active = true;
            statNoise[3].active = true;
            statNoise[4].active = true;
            mod3.on = false;
            statNoise[5].active = true;
            mod4.on = false;
        }
        else if(module == 1)
        {
            statNoise[0].active = true;
            mod1.on = false;
        }

        else if(module == 2)
        {
            statNoise[1].active = true;
            mod2.on = false;
        }

        else if(module == 3)
        {
            statNoise[2].active = true;
            statNoise[3].active = true;
            statNoise[4].active = true;
            mod3.on = false;
        }

        else if(module == 4)
        {
            statNoise[5].active = true;
            mod4.on = false;
        }
    }


    public void stopNoise(int i)
    {
        statNoise[i].stopNoise();
    }

    private void randomEye()
    {
        if(!mod1.actif && mod1.on && eyes[0].activeSelf)
        {
            StartCoroutine(randomEyeCo(0));
        }
    }

    private IEnumerator randomEyeCo(int i)
    {
        eyes[i].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        eyes[i].SetActive(false);
    }

    private void eyeOpen(int i)
    {
        eyes[i].SetActive(true);
    }

    private void eyeClose(int i)
    {
        eyes[i].SetActive(false);
    }

    public IEnumerator bossStart()
    {
        canvasSwitchOn();
        startNoise(0);
        yield return new WaitForSeconds(1);
        for(int i =0; i<6 ; i++)
        {
            stopNoise(i);
        }

        yield return new WaitForSeconds(1);

        source.Play();
        for(int i =0; i<5 ; i++)
        {
            eyeOpen(i);
        }
        ss.shakeOn();
        fe.raiseFear(1.0f);
        fe.enabled = true;

        yield return new WaitForSeconds(3);

        fe.stopFear(0.2f);

        for(int i =0; i<5 ; i++)
        {
            eyeClose(i);
        }
        ss.shakeOff();
        bs.bossSequence();
    }

}
