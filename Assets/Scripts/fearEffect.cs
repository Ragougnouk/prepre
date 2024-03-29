using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class fearEffect : MonoBehaviour
{

    public PostProcessVolume v;
    public PostProcessProfile profile;
    public UnityEngine.Rendering.PostProcessing.ChromaticAberration ca;
    public ColorGrading clrGr;

    public GameObject house;

    public float caInt;
    public int hs;
    //public float increaseTime = 1.0f;

    public bool fearOn;
    public fearEffect fe;

    // Start is called before the first frame update
    void Start()
    {
        profile = v.profile;
        ca = profile.GetSetting<UnityEngine.Rendering.PostProcessing.ChromaticAberration>();
        clrGr = profile.GetSetting<ColorGrading>();
        //ca.enabled.Override(true);
    }

    // Update is called once per frame
    void Update()
    {
        ca.intensity.Override(caInt);
        clrGr.hueShift.Override(hs);
        if (fearOn)
        {
            hs = Random.Range(-180,180);
        }
    }

    public void raiseFear(float increaseTime)
    {
        LeanTween.value(house, 0.0f, 1.0f, increaseTime ).setOnUpdate( (float val) => { caInt = val ;} ).setEase(LeanTweenType.easeOutQuad);
        fearOn = true;
    }

    public void stopFear(float downTime)
    {
        fearOn = false;
        LeanTween.value(house, 1.0f, 0.0f, downTime ).setOnUpdate( (float val) => { caInt = val ;} ).setEase(LeanTweenType.easeOutQuad).setOnComplete(endFear);
        hs = 0;
    }

    private void endFear()
    {
        fe.enabled = false;
    }
}
