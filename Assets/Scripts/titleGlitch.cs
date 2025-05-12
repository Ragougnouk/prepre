using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleGlitch : MonoBehaviour
{
    public Color[] col;

    public GameObject titleTri;
    public GameObject titleShift;
    public GameObject titleUp;
    public GameObject titleMid;
    public GameObject titleDown;
    public GameObject titleColor;

    public GameObject eye;
    public GameObject[] eyes;

    public AudioSource glitchSource;
    public AudioClip[] glitchSounds;

    public GameObject startButton;
    public GameObject optionsButton;
    public GameObject creditsButton;

    private float minDelay = 0.01f;
    private float maxDelay = 0.1f;

    private float randomMainTime = 2.0f;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(GlitchEffect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GlitchEffect()
    {
        /*yield return new WaitForSeconds(1.0f);

        Color titleImageColor = titleTri.GetComponent<Image>().color;
        titleImageColor.a = 0.05f;
        titleTri.GetComponent<Image>().color = titleImageColor;

        yield return new WaitForSeconds(1.0f);

        titleImageColor.a = 0.1f;
        titleTri.GetComponent<Image>().color = titleImageColor;

        yield return new WaitForSeconds(1.0f);

        titleImageColor.a = 0.3f;
        titleTri.GetComponent<Image>().color = titleImageColor;

        yield return new WaitForSeconds(1.0f);

        titleImageColor.a = 0.7f;
        titleTri.GetComponent<Image>().color = titleImageColor;

        yield return new WaitForSeconds(1.0f);

        titleImageColor.a = 1f;
        titleTri.GetComponent<Image>().color = titleImageColor;*/

        Color titleImageColor = titleTri.GetComponent<Image>().color;
        float stepAlpha = 0.5f;
        float timeAlpha = 0.0f;

        while (titleImageColor.a < 1.0f)
        {
            yield return new WaitForSeconds(stepAlpha);
            timeAlpha += (stepAlpha/5);
            titleImageColor.a = Mathf.Pow(timeAlpha,5);
            titleTri.GetComponent<Image>().color = titleImageColor;

        }
        yield return new WaitForSeconds(0.2f);

        startButton.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        optionsButton.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        creditsButton.SetActive(true);

        yield return new WaitForSeconds(0.4f);
        // Repeat infinitely
        while (true)
        {
            randomMainTime = Random.Range(0.5f,3.0f);
            // Get a random glitch sprite index
            int randomIndex = Random.Range(0, 100);
            float randomDelay = 0.0f;

            // Set the sprite to the random glitch sprite
            if(randomIndex<10)
            {
                titleMain();
                randomDelay = randomMainTime;
            }
            else if (randomIndex >= 10 && randomIndex < 50)
            {
                titleColorOn();
                randomDelay = Random.Range(minDelay, maxDelay);
            }
            else if (randomIndex >= 50 && randomIndex < 95)
            {
                titleShiftOn();
                randomDelay = Random.Range(minDelay, maxDelay);
            }
            else if (randomIndex >= 95)
            {
                eyeOn();
                randomDelay = Random.Range(minDelay, maxDelay);
            }
            // Wait for a random delay before the next glitch swap
            
            yield return new WaitForSeconds(randomDelay);
        }
    }

    private void titleMain()
    {
        glitchSource.Stop();
        allOff();
        titleTri.SetActive(true);
        StartCoroutine(mainSec());
    }

    private void titleShiftOn()
    {
        glitchSource.PlayOneShot(glitchSounds[Random.Range(0,glitchSounds.Length)]);
        allOff();
        titleShift.SetActive(true);
        float rX = Random.Range(-45,45);
        titleUp.transform.localPosition = new Vector3(rX,titleUp.transform.localPosition.y,0);
        rX = Random.Range(-45,45);
        titleMid.transform.localPosition = new Vector3(rX,titleMid.transform.localPosition.y,0);
        rX = Random.Range(-45,45);
        titleDown.transform.localPosition = new Vector3(rX,titleDown.transform.localPosition.y,0);
    }

    private void titleColorOn()
    {
        glitchSource.PlayOneShot(glitchSounds[Random.Range(0,glitchSounds.Length)]);
        allOff();
        int randomIndex = Random.Range(0, col.Length);
        float rX = Random.Range(-45,45);
        float rY = Random.Range(-20,20);
        titleColor.transform.localPosition = new Vector3(rX,rY,0.0f);
        titleColor.GetComponent<Image>().color = col[randomIndex];
        titleColor.SetActive(true);
    }

    private void eyeOn()
    {
        glitchSource.PlayOneShot(glitchSounds[Random.Range(0,glitchSounds.Length)]);
        allOff();
        foreach(GameObject eyePiece in eyes)
        {
            float rX = Random.Range(166,186);
            eyePiece.transform.localPosition = new Vector3(rX,eyePiece.transform.localPosition.y,0);
        }
        eye.SetActive(true);

    }

    private void allOff()
    {
        titleTri.SetActive(false);
        titleShift.SetActive(false);
        titleColor.SetActive(false);
        eye.SetActive(false);
    }

    private IEnumerator mainSec()
    {
        Vector3 basePos = titleTri.transform.localPosition;
        float rX = Random.Range(-10,10);
        float rY = Random.Range(-10,10);
        yield return new WaitForSeconds(randomMainTime/4);
        int rI = Random.Range(0,4);
        for(int i = 0; i < rI; i++)
        {
            titleTri.transform.localPosition = new Vector3(rX, rY,0);
            rX = Random.Range(-10,10);
            rY = Random.Range(-10,10);
            yield return new WaitForSeconds(randomMainTime/20);
        }
        titleTri.transform.localPosition = basePos;

        
    }
}
