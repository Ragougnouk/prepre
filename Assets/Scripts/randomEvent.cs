using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomEvent : MonoBehaviour
{

    public staticNoise[] statNoise;
    public module1Controller mod1;
    public module2Controller mod2;
    public Module3Controller mod3;
    public module4Controller mod4;

    public GameObject[] eyes;

    public float noisePercent = 0.001f;
    public float eyePercent = 0.001f;
    private float lastTime;

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
            randomNoise();
            lastTime = Time.time;
        }
    }

    private void randomNoise()
    {
        if(!mod1.actif && mod1.on && !statNoise[0].active)
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

    public void stopNoise(int i)
    {
        statNoise[i].stopNoise();
    }

    private void randomEye()
    {
        if(!mod1.actif && mod1.on && eyes[0].activeSelf)
        {
            StartCoroutine(randomEyeCo());
        }
    }

    private IEnumerator randomEyeCo()
    {
        eyes[0].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        eyes[0].SetActive(false);
    }

}
