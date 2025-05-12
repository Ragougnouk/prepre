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
    public randomEvent re;
    public blinking_backlight bbl;

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

    public bool[] modOn = {false,false,false,false};

    private IEnumerator onDelay;

    public bool mod1Delay = true;

    public GameObject[] mods;

    public flicker[] lilFlick;

    private bool inBoss1 = false;
    private bool inBoss2 = false;

    public AudioSource[] breakerSource;
    public AudioClip[] breakerSounds;
    public Button[] switchesButton;

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
        bbl.alarmOn = false;
        mainSwitchOff.SetActive(false);
        mainSwitchOn.SetActive(true);
        foreach (Button btn in switchesButton)
        {
            btn.interactable = false;
        }
        if(ms.inLoop)
        {
            foreach (Button btn in switchesButton)
            {
                btn.interactable = true;
            }
            return;
        }
        //mainSwitchOn.GetComponent<Button>().interactable = false;
        re.canvasSwitchOn();
        ls.lightsOn();
        StartCoroutine(littleLeds(true));
        StartCoroutine(onDelay);
    }

    public void powerOff()
    {
        /*for(int i =0; i<6 ; i++)
        {
            re.stopNoise(i);
        }*/

        StartCoroutine(breakerPoweringDown());
        foreach (Button btn in switchesButton)
        {
            btn.interactable = false;
        }
        mainSwitchOff.SetActive(true);
        mainSwitchOn.SetActive(false);

        if(ms.inLoop)
        {
            foreach (Button btn in switchesButton)
            {
                btn.interactable = true;
            }
            StartCoroutine(afterLoop());
            return;
        }

        ms.backStep();
        mod1Delay = false;
        StopCoroutine(onDelay);
        onDelay = powerOnDelay();
        re.canvasSwitchOff();
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

        StartCoroutine(breakerPoweringUp(1));
        yield return new WaitForSeconds(0.25f);
        mod1.turnOn(mod1Delay);
        yield return new WaitForSeconds(0.35f);
        mod2.turnOn();
        yield return new WaitForSeconds(0.45f);
        mod3.turnOn();
        mod3b.turnOn();
        yield return new WaitForSeconds(0.55f);
        mod4.turnOn();
        //mainSwitchOn.GetComponent<Button>().interactable = true;
        if (first)
        {
            yield return new WaitForSeconds(0.5f);
            cf.etape = 1;
            cf.newLine();
            //ca.open();
            mod1.on = true;
            first = false;
        }
        mod1.active();
    }

    public void reset()
    {
        mod1.turnOffReset();
        //mod1.reInit();
        mod2.turnOffReset();
        //mod2.reInit();
        mod3.turnOff();
        //mod3b.reInit();
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

        mod1.active();
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
        if(inBoss1 || inBoss2)
        {
            yield break;
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
        yield return new WaitForSeconds(5.0f);
        ls.lightsOnRed();
        if(ms.lastLoopCount > 4)
        {
            inBoss1 = true;
        }
        else
        {
            StartCoroutine(re.bossStart());
            inBoss2 = true;
        }
        
    }

    public void mod1Switch(bool off)
    {
        if(off)
        {
            if(mod1.actif)
            {
                flickOn(0,1);
            }
            //mod1.turnOff();
            mods[0].SetActive(false);
            mod1.on = false;
            re.stopNoise(0);
        }
        else
        {
            //mod1.turnOn(false);
            mods[0].SetActive(true);
            mod1.on = true;
            flickOff(0,1);
            leds1[0].GetComponent<SpriteRenderer>().enabled = true;
            leds2[0].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void mod2Switch(bool off)
    {
        if(off)
        {
            if(mod2.actif)
            {
                flickOn(2,3);
            }
            //mod2.turnOff();
            mods[1].SetActive(false);
            mod2.on = false;

            re.stopNoise(1);
        }
        else
        {
            //mod2.turnOn();
            mods[1].SetActive(true);
            mod2.on = true;
            flickOff(2,3);
            leds1[1].GetComponent<SpriteRenderer>().enabled = true;
            leds2[1].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void mod3Switch(bool off)
    {
        if(off)
        {
            if(mod3.actif)
            {
                flickOn(4,5);
            }
            //mod2.turnOff();
            mods[2].SetActive(false);
            mod3.on = false;
            mods[3].SetActive(false);
            mod3b.on = false;

            re.stopNoise(2);
            re.stopNoise(3);
            re.stopNoise(4);
        }
        else
        {
            //mod2.turnOn();
            mods[2].SetActive(true);
            mod3.on = true;
            mods[3].SetActive(true);
            mod3b.on = true;
            flickOff(4,5);
            leds1[2].GetComponent<SpriteRenderer>().enabled = true;
            leds2[2].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void mod4Switch(bool off)
    {
        if(off)
        {
            //mod2.turnOff();
            mods[4].SetActive(false);
            mod4.on = false;
            re.stopNoise(5);

        }
        else
        {
            //mod2.turnOn();
            mods[4].SetActive(true);
            mod4.on = true;
            flickOff(6,7);
            leds1[3].GetComponent<SpriteRenderer>().enabled = true;
            leds2[3].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void flickOn(int i, int j)
    {
        lilFlick[i].enabled = true;
        lilFlick[j].enabled = true;
    }

    public void flickOff(int i, int j)
    {
        lilFlick[i].enabled = false;
        lilFlick[j].enabled = false;
    }

    private IEnumerator breakerPoweringUp(int version)
    {
        if(version == 1)
        {

            breakerSource[0].PlayOneShot(breakerSounds[0]);
            breakerSource[1].PlayOneShot(breakerSounds[1]);
            breakerSource[2].PlayOneShot(breakerSounds[2]);
            while(breakerSource[0].isPlaying)
            {
                yield return null;
            }
            //breakerSource[0].clip = breakerSounds[2];

            breakerSource[0].Play();
            breakerSource[1].Play();
            breakerSource[2].Play();
            foreach (Button btn in switchesButton)
            {
                btn.interactable = true;
            }
        }
        else
        {
            //breakerSource[0].Play();
        }
        
        yield return null;
    }

    private IEnumerator breakerPoweringDown()
    {
        breakerSource[0].Stop();
        breakerSource[1].Stop();
        breakerSource[2].Stop();

        breakerSource[0].PlayOneShot(breakerSounds[3]);
        breakerSource[1].PlayOneShot(breakerSounds[4]);
        breakerSource[2].PlayOneShot(breakerSounds[5]);

        while(breakerSource[0].isPlaying)
        {
            yield return null;
        }

        foreach (Button btn in switchesButton)
        {
            btn.interactable = true;
        }
        yield return null;
    }
}
