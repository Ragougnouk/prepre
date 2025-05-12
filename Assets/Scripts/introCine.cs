using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;

public class introCine : MonoBehaviour
{
    public GameObject main;
    public carnet_anim carnet;
    public carnet_fill cf;
    public blinking_backlight bbl;
    public float speed1 = 1.0f;
    public float speed2 = 1.0f;
    public float waintingTime = 0.0f;

    public LeanTweenType easeType;
    public LeanTweenType easeType1;

    public flicker flckr;

    private bool first = true;

    private bool inBloomAnim = true;

    public float bloomIntensity = 50.0f;
    public float bloomTarget = 20.0f;
    public float speedBloom = 5.0f;
    public float delay = 0.0f;

    public float bloomDiffusion = 10.0f;
    public float bloomDiffTarget = 7.0f;


    public float bloomInt;
    public float bloomDiff;

    public LeanTweenType easeBloom;

    public PostProcessVolume v;
    public PostProcessProfile profile;
    public GameObject vol;

    public UnityEngine.Rendering.PostProcessing.Bloom bloomValue;


    // Start is called before the first frame update
    void Start()
    {
        profile = v.profile;
        bloomValue = profile.GetSetting<UnityEngine.Rendering.PostProcessing.Bloom>();
        LeanTween.moveY(main,0.0f,speed1)/*.setOnComplete(startGame)*/.setEase(easeType);
        LeanTween.value(vol, bloomIntensity,bloomTarget,speedBloom).setOnUpdate( (float val) => { bloomInt = val ;} ).setDelay(delay).setEase(easeType);
        //LeanTween.value(vol, bloomDiffusion,bloomDiffTarget,speedBloom).setOnUpdate( (float val) => { bloomDiff = val ;} ).setDelay(delay).setEase(easeType);
        lightOn();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && first)
        {
            startGame();
            first = false;
        }

        if(inBloomAnim)
        {
            bloomValue.intensity.Override(bloomInt);
            //bloomValue.diffusion.Override(bloomDiff);
        }
    }

    private void firstLaunch()
    {
        carnet.launched = true;
        carnet.open();
        cf.writeSound();
        //enabled = false;
    }

    public void startGame()
    {
        LeanTween.moveX(main,-0.36f,speed2).setDelay(waintingTime).setOnComplete(pauseFirst).setEase(easeType1);
    }

    private void pauseFirst()
    {
        StartCoroutine(pauseLaunch());
    }

    private IEnumerator pauseLaunch()
    {
        yield return new WaitForSeconds(0.5f);
        firstLaunch();
    }

    async void lightOn()
    {
        await Task.Delay(5000);
        flckr.enabled = true;
        bbl.alarmOn = true;
    }
}

