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
    public mod3test mod3b;
    public module4Controller mod4;

    public GameObject[] eyes;

    public float noisePercent = 0.001f;
    public float eyePercent = 0.001f;
    private float lastTime;

    public bossScript bs;
    public fearEffect fe;
    public AudioSource source;

    public AudioSource[] sourceNoise;

    public GameObject[] canvasNoise;

    public bool eyeOn = false;



    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastTime > 1.0f )
        {
            randomNoise(noisePercent);

            if (eyeOn) {randomEye(eyePercent);}
            
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

    private void randomNoise(float noiseP)
    {
        /*if(mod1.randomTarget && !statNoise[0].active)
        {
            float randomValue = Random.value;
            //print(randomValue);

            // Check if the random number is within the activation chance
            if (randomValue < noiseP)
            {
                // Activate the noise effect
                statNoise[0].active = true;
                mod1.on = false;
            }
        }*/
        float randomValue = Random.value;
        if (randomValue < noiseP)
        {

            List<int> validGameObjects = new List<int>();

            if(mod1.randomTarget && !statNoise[0].active)
            {
                validGameObjects.Add(1);
            }

            if(mod2.randomTarget && !statNoise[1].active)
            {
                validGameObjects.Add(2);
            }

            if(mod3.randomTarget && !statNoise[2].active)
            {
                validGameObjects.Add(3);
            }

            if(mod4.randomTarget && !statNoise[5].active)
            {
                validGameObjects.Add(4);
            }

            if (validGameObjects.Count > 0)
            {
                int randomGameObject = validGameObjects[Random.Range(0, validGameObjects.Count)];
                startNoise(randomGameObject);
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
            mod3b.on = false;
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

    private void randomEye(float eyeP)
    {
        /*if(!mod1.actif && mod1.on && !eyes[0].activeSelf)
        {
            StartCoroutine(randomEyeCo(0));
        }*/

        float randomValue = Random.value;
        if (randomValue < eyeP)
        {

            List<int> validGameObjects = new List<int>();

            if(mod1.on)
            {
                validGameObjects.Add(1);
            }

            if(mod2.on)
            {
                validGameObjects.Add(2);
            }

            if(mod3.on)
            {
                validGameObjects.Add(3);
            }

            if(mod4.on)
            {
                validGameObjects.Add(4);
            }

            if (validGameObjects.Count > 0)
            {
                int randomGameObject = validGameObjects[Random.Range(0, validGameObjects.Count)];
                StartCoroutine(randomEyeCo(randomGameObject));
            }
        }
    }

    private IEnumerator randomEyeCo(int i)
    {
        float duration = Random.Range(0.0f,0.5f);
        if (i == 3)
        {
            eyes[i-1].SetActive(true);
            eyes[i].SetActive(true);
            yield return new WaitForSeconds(duration);
            eyes[i-1].SetActive(false);
            eyes[i].SetActive(false);
        }
        else if (i == 4)
        {
            eyes[i].SetActive(true);
            yield return new WaitForSeconds(duration);
            eyes[i].SetActive(false);
        }
        else
        {
            eyes[i-1].SetActive(true);
            yield return new WaitForSeconds(duration);
            eyes[i-1].SetActive(false);
        }
        
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
