using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAmbiance : MonoBehaviour
{

    public AudioSource SoundScapeSource;
    public AudioSource SFXSource;

    public AudioClip[] SoundScape;
    public AudioClip[] SFX;

    public float minVolume = 0.05f;
    public float maxVolume = 0.5f;

    public float fadeDuration = 0.5f;

    public float StartTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        StartCoroutine(startPlay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator startPlay()
    {
        yield return new WaitForSeconds(StartTime);
        StartCoroutine(PlaySoundScape());
        StartCoroutine(PlaySFX());
    }

    IEnumerator PlaySoundScape()
    {
        while(true)
        {
            AudioClip clip = SoundScape[Random.Range(0,SoundScape.Length)];
            SoundScapeSource.clip = clip;

            float targetVolume = Random.Range(minVolume, maxVolume);

            SoundScapeSource.volume = 0;
            SoundScapeSource.Play();
            yield return StartCoroutine(FadeIn(fadeDuration, targetVolume));

            float clipTime = Random.Range(5.0f, 15.0f);

            yield return new WaitForSeconds(clipTime);

            yield return StartCoroutine(FadeOut(fadeDuration, targetVolume));

            float waitTime = Random.Range(10.0f, 20.0f);

            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator PlaySFX()
    {
        while(true)
        {
            float waitTime = Random.Range(10.0f, 20.0f);

            yield return new WaitForSeconds(waitTime);

            AudioClip clipSFX = SFX[Random.Range(0,SFX.Length)];
            SFXSource.clip = clipSFX;
            SFXSource.Play();
            yield return new WaitForSeconds(clipSFX.length);
        }
    }

    IEnumerator FadeIn(float duration, float vol)
    {
        float time = 0;
        while (time < duration)
        {
            SoundScapeSource.volume = Mathf.Lerp(0, vol, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        SoundScapeSource.volume = vol;
    }

    IEnumerator FadeOut(float duration, float vol)
    {
        float time = 0;
        while (time < duration)
        {
            SoundScapeSource.volume = Mathf.Lerp(vol, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        SoundScapeSource.volume = 0;
    }
}
