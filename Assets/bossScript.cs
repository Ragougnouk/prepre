using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
	public staticNoise[] statNoise;
	public breakerController bc;

    /*public module1Controller mod1;
    public module2Controller mod2;
    public Module3Controller mod3;
    public module4Controller mod4;*/

    public GameObject[] eyes;
    public screenShake ss;

    public bool inBoss = false;
    private bool eyeOpened = false;

    private bool[] modEyeOn = {false,false,false,false};

    private float lastTime;

    // Start is called before the first frame update
    void Start()
    {
       lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    	if (inBoss)
        {
        	if(Time.time - lastTime > 1.0f && !eyeOpened)
                {
                    int i = Random.Range(0,4);
                    StartCoroutine(eyeAppear(i));
                    //lastTime = Time.time;
                }
        }
    }

    public void bossSequence()
    {
    	inBoss = true;
    }

    private IEnumerator eyeAppear(int mod)
    {
    	eyeOpened = true;
    	if(mod == 0 || mod == 1)
    	{
    		eyes[mod].SetActive(true);
    		modEyeOn[mod] = true;
    	}
    	else if(mod == 2)
    	{
    		eyes[2].SetActive(true);
    		eyes[3].SetActive(true);
    		modEyeOn[2] = true;
    	}
    	else if(mod == 3)
    	{
    		eyes[4].SetActive(true);
    		modEyeOn[3] = true;
    	}

    	yield return new WaitForSeconds(3.0f);

    	if(mod == 0 || mod == 1)
    	{
    		eyes[mod].SetActive(false);
    		modEyeOn[mod] = false;
    	}

    	else if(mod == 2)
    	{
    		eyes[2].SetActive(false);
    		eyes[3].SetActive(false);
    		modEyeOn[2] = false;
    	}
    	else if(mod == 3)
    	{
    		eyes[4].SetActive(false);

    		modEyeOn[3] = false;
    	}
    	eyeOpened = false;
    	lastTime = Time.time;
    }

    public void killMod(int i)
    {
    	if(i == 0 && modEyeOn[0])
    	{
    		print("hit1");
    		eyes[0].SetActive(false);
    		StartCoroutine(damage());
    		modEyeOn[0] = false;
    		eyeOpened = false;
    	}

    	if(i == 1 && modEyeOn[1])
    	{
    		print("hit2");
    		eyes[1].SetActive(false);
    		StartCoroutine(damage());
    		modEyeOn[1] = false;
    		eyeOpened = false;
    	}

    	if(i == 2 && modEyeOn[2])
    	{
    		print("hit3");
    		eyes[2].SetActive(false);
    		eyes[3].SetActive(false);
    		StartCoroutine(damage());
    		modEyeOn[2] = false;
    		eyeOpened = false;
    	}

    	if(i == 3 && modEyeOn[3])
    	{
    		print("hit4");
    		eyes[4].SetActive(false);
    		StartCoroutine(damage());
    		modEyeOn[3] = false;
    		eyeOpened = false;
    	}
    }

    private IEnumerator damage()
    {
    	ss.shakeOn();
    	yield return new WaitForSeconds(0.2f);
    	ss.shakeOff();
    }

}
