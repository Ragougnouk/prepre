using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class moduleSequencer : MonoBehaviour
{

    public MessagesList messages;

    public module1Controller mod1;
    public module2Controller mod2;
    public mod3test mod3;
    public Module3Controller mod3b;
    public module4Controller mod4;
    public boxNumber boxNb;


    public GameObject goScreen;
    public GameObject lit1,lit2,lit3;

    public bool step1, step2, step3;

    public int loopNumber = 0;


    // Start is called before the first frame update
    public TMP_Text playerNameText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print("loop number "+loopNumber);
        if(mod1.enabled && step1)
        {
            if (!lit1.activeSelf)
            {
                lit1.SetActive(true);
            }

            if (mod1.nextStep)
            {
                step1 = false;
                mod1.nextStep = false;
                StartCoroutine(mod1mod2());
            }
        }

        if(mod2.enabled && step2)
        {
            if (!lit2.activeSelf)
            {
                lit2.SetActive(true);
            }

            if (mod2.nextStep)
            {
                step2 = false;
                mod2.nextStep = false;
                StartCoroutine(mod2mod3());
            }
        }

        if(mod3.enabled && step3)
        {

            if (!lit3.activeSelf)
            {
                lit3.SetActive(true);
            }

            if (mod3b.nextStep)
            {
                step3 = false;
                mod3b.nextStep = false;
                StartCoroutine(mod3mod4());
            }

        }


    }

    private IEnumerator mod1mod2()
    {
        step2 = true;
        mod1.enabled = false;
        lit1.SetActive(false);
        yield return new WaitForSeconds(1);
        mod1.reInit();
        mod2.enabled = true;
        lit2.SetActive(true);
    }

    private IEnumerator mod2mod3()
    {
        step3 = true;
        mod2.enabled = false;
        lit2.SetActive(false);
        yield return new WaitForSeconds(1);
        mod2.reInit();
        boxNb.enabled = true;
        mod3.enabled = true;
        lit3.SetActive(true);
    }
    private IEnumerator mod3mod4()
    {
        //step1 = true;
        mod3.enabled = false;
        mod3b.enabled = false;
        boxNb.enabled = false;
        lit3.SetActive(false);
        yield return new WaitForSeconds(1);
        
        boxNb.reInit();
        mod3.reInit();
        mod3b.reInit();
        mod4.updateText();
        StartCoroutine(mod4mod1());
        //loopNumber += 1;
        //mod1.enabled = true;

    }

    private IEnumerator mod4mod1()
    {
        step1 = true;
        //mod3.enabled = false;
        //mod3b.enabled = false;
        //boxNb.enabled = false;
        //lit3.SetActive(false);
        yield return new WaitForSeconds(1);
        //boxNb.reInit();
        //mod3.reInit();
        //mod3b.reInit();
        //mod4.updateText();
        loopNumber += 1;
        gameOver();
        //mod1.enabled = true;
    }

    void gameOver()
    {
        if(loopNumber < messages.stringList.Length)
        {
            mod1.enabled = true;
        }
        else
        {
            goScreen.SetActive(true);
        }
    }
}