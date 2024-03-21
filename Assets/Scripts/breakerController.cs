using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class breakerController : MonoBehaviour
{
    public GameObject mainSwitchOn;
    public GameObject mainSwitchOff;
    public module1Controller mod1;
    public module2Controller mod2;
    public Module3Controller mod3;
    public mod3test mod3b;
    public module4Controller mod4;
    public boxNumber boxNb;
    public moduleSequencer ms;

    public carnet_fill cf;
    public carnet_anim ca;

    private bool first = true;

    public life_system ls;

    public GameObject[] leds1;
    public GameObject[] leds2;

    public Color lGreen;
    public Color dGreen;
    public Color lRed;
    public Color dRed;

    public bool[] modOn = {true,true,true,true};

    private IEnumerator onDelay;

    public bool mod1Delay = true;

    //private IEnumerator lLeds;

    // Start is called before the first frame update
    void Start()
    {
        /*lGreen = new Color(255,75,255);
        dGreen = new Color(32,103,51);
        lRed = new Color(255,0,45);
        dRed = new Color(115,25,30);*/
        onDelay = powerOnDelay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void powerOn()
    {
        mainSwitchOff.SetActive(false);
        mainSwitchOn.SetActive(true);
        if(ms.inLoop)
        {
            return;
        }
        //mainSwitchOn.GetComponent<Button>().interactable = false;
        ls.lightsOn();
        StartCoroutine(littleLeds(true));
        StartCoroutine(onDelay);
    }

    public void powerOff()
    {
        mainSwitchOff.SetActive(true);
        mainSwitchOn.SetActive(false);

        if(ms.inLoop)
        {
            StartCoroutine(afterLoop());
            return;
        }

        ms.backStep();
        mod1Delay = false;
        StopCoroutine(onDelay);
        onDelay = powerOnDelay();
        
        ls.lightsOut();
        ls.healthPoints = 10;
        mod1.turnOff();
        mod2.turnOff();
        mod3.turnOff();
        mod3b.reInit();
        mod3b.turnOff();
        mod4.turnOff();
        boxNb.reInit();
        StartCoroutine(littleLeds(false));
    }

    private IEnumerator powerOnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        mod1.turnOn(mod1Delay);
        yield return new WaitForSeconds(0.1f);
        mod2.turnOn();
        yield return new WaitForSeconds(0.15f);
        mod3.turnOn();
        mod3b.turnOn();
        yield return new WaitForSeconds(0.1f);
        mod4.turnOn();
        //mainSwitchOn.GetComponent<Button>().interactable = true;
        if (first)
        {
            yield return new WaitForSeconds(0.5f);
            cf.etape = 1;
            cf.newLine();
            //ca.open();
            first = false;
        }
    }

    public void reset()
    {
        mod1.turnOff();
        //mod1.reInit();
        mod2.turnOff();
        //mod2.reInit();
        mod3.turnOff();
        mod3b.reInit();
        mod3b.turnOff();
        mod4.turnOff();
        boxNb.reInit();
        ls.healthPoints = 10;
        ls.lightsOn();
        mod1.turnOn(true);  
        mod2.turnOn();
        mod3.turnOn();
        mod3b.turnOn();
        mod4.turnOn();
    }

    public void turnLedRed(int ledNb)
    {
        leds1[ledNb].GetComponent<SpriteRenderer>().color = lRed;
        leds2[ledNb].GetComponent<SpriteRenderer>().color = dRed;
        modOn[ledNb] = false;
    }

    public void turnLedGreen(int ledNb)
    {
        leds1[ledNb].GetComponent<SpriteRenderer>().color = lGreen;
        leds2[ledNb].GetComponent<SpriteRenderer>().color = dGreen;
        modOn[ledNb] = true;
    }

    private IEnumerator littleLeds(bool activate)
    {
        if(activate)
        {
            yield return new WaitForSeconds(1.0f);
            foreach(var led in leds1)
            {
                led.SetActive(true);
            }

            foreach(var led in leds2)
            {
                led.SetActive(true);
            }
        }

        if(!activate)
        {
            yield return new WaitForSeconds(1.0f);
            foreach(var led in leds1)
            {
                led.SetActive(false);
            }

            foreach(var led in leds2)
            {
                led.SetActive(false);
            }
        }
    }

    private IEnumerator afterLoop()
    {
        ms.backStep();
        mod1Delay = false;
        StopCoroutine(onDelay);
        onDelay = powerOnDelay();
        
        ls.lightsOut();
        ls.healthPoints = 10;
        mod1.turnOff();
        mod2.turnOff();
        mod3.turnOff();
        mod3b.reInit();
        mod3b.turnOff();
        mod4.turnOff();
        boxNb.reInit();
        StartCoroutine(littleLeds(false));
        yield return new WaitForSeconds(5.0f);
        ls.lightsOnRed();
    }

    //public void 
}
