using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introCine : MonoBehaviour
{
    public GameObject title;
    public GameObject main;
    public GameObject screen1;
    public GameObject reticule;
    public module1Controller mod1;
    public moduleSequencer modSeq;
    public float speed = 1.0f;

    private bool ceparti = false;
    public GameObject introScene;

    public LeanTweenType easeType;
    public LeanTweenType easeType1;


    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveY(main,0.0f,3.0f).setOnComplete(startGame).setEase(easeType);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            startGame();
        }
        //StartCoroutine(introLaunch());
        if (ceparti)
        {
            main.transform.position = Vector3.MoveTowards(main.transform.position, new Vector3(0,0,-10), speed * Time.deltaTime);
        }*/
    }

    /*private IEnumerator introLaunch()
    {
        yield return new WaitForSeconds(2);
        //screen1.SetActive(true);
        //reticule.SetActive(true);
        modSeq.step1 = true;
        mod1.enabled = true;

        introScene.SetActive(false);
        enabled = false;
    }*/

    private void firstLaunch()
    {
        modSeq.step1 = true;
        mod1.enabled = true;

        //introScene.SetActive(false);
        enabled = false;
    }

    public void startGame()
    {
        //StartCoroutine(introLaunch());
        title.SetActive(false);
        LeanTween.moveX(main,0.0f,1.5f).setDelay(5.0f).setOnComplete(firstLaunch).setEase(easeType1);
        //ceparti = true;
        //mod1.enabled = true;
    }
}

