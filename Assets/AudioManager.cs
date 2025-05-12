using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public GameObject manager;

    public bool resetPrefs = true;

    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider ambientSlider;
    public Slider crtAlphaSlider;

    private const string MASTER_VOLUME = "MasterVolume";
    private const string SFX_VOLUME = "SFXVolume";
    private const string AMBIENT_VOLUME = "AmbientVolume";
    private const string CRT_ALPHA = "CRT_Alpha";

    public Image crtImage;

    void Awake()
    {
        //DontDestroyOnLoad(manager);
    }

    public void SetMasterVolume(float volume)
    {
        //print((Mathf.Log10((volume/96f)*99f +1f)/Mathf.Log10(100f))*85f - 80f);
        audioMixer.SetFloat(MASTER_VOLUME, (Mathf.Log10((volume/96f)*99f +1f)/Mathf.Log10(100f))*85f - 80f);
        PlayerPrefs.SetFloat(MASTER_VOLUME, volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SFX_VOLUME, (Mathf.Log10((volume/96f)*99f +1f)/Mathf.Log10(100f))*85f - 80f);
        PlayerPrefs.SetFloat(SFX_VOLUME, volume);
    }

    public void SetAmbientVolume(float volume)
    {
        audioMixer.SetFloat(AMBIENT_VOLUME, (Mathf.Log10((volume/96f)*99f +1f)/Mathf.Log10(100f))*85f - 80f);
        PlayerPrefs.SetFloat(AMBIENT_VOLUME, volume);
    }

    public void SetAlphaCRT(float alphaValue)
    {
        float alpha = alphaValue / 96f;

        Color currentColor = crtImage.color;
        crtImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        PlayerPrefs.SetFloat(CRT_ALPHA, alphaValue);
    }

    void Start()
    {
        if (resetPrefs)
        {
            ResetPlayerPrefs();
        }
        float masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME, 74.0f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME, 74.0f);
        float ambientVolume = PlayerPrefs.GetFloat(AMBIENT_VOLUME, 74.0f);
        float crtAlpha = PlayerPrefs.GetFloat(CRT_ALPHA, 19.0f);

        masterSlider.value = masterVolume;
        sfxSlider.value = sfxVolume;
        ambientSlider.value = ambientVolume;
        crtAlphaSlider.value = crtAlpha;

        SetMasterVolume(masterVolume);
        SetSFXVolume(sfxVolume);
        SetAmbientVolume(ambientVolume);
        SetAlphaCRT(crtAlpha);
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey(MASTER_VOLUME);
        PlayerPrefs.DeleteKey(SFX_VOLUME);
        PlayerPrefs.DeleteKey(AMBIENT_VOLUME);
        PlayerPrefs.DeleteKey(CRT_ALPHA);
    }
}
