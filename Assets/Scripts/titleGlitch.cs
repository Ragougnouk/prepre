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
        allOff();
        titleTri.SetActive(true);
        StartCoroutine(mainSec());
    }

    private void titleShiftOn()
    {
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
