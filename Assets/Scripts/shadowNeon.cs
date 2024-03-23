using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowNeon : MonoBehaviour
{
    public GameObject mask;
    public SpriteRenderer neon2;

    public GameObject pos1G;
    public GameObject pos2G;
    public LeanTweenType easeType;
    public float speed1;

    public float minWait;
    public float maxWait;

    private bool ongoing;

    private Vector3 pos1;
    private Vector3 pos2;

        
    // Start is called before the first frame update
    void Start()
    {
        pos1 = pos1G.transform.position;
        pos2 = pos2G.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(!ongoing)
        {
            StartCoroutine(randomWait());
        }
    }

    private void shadowPass1()
    {
        mask.transform.position = pos1;
        LeanTween.moveX(mask,pos2.x,speed1).setEase(easeType);
        LeanTween.moveY(mask,pos2.y,speed1).setOnComplete(neonAction).setEase(easeType);
    }

    private void shadowPass2()
    {
        mask.transform.position = pos2;
        LeanTween.moveX(mask,pos1.x,speed1).setEase(easeType);
        LeanTween.moveY(mask,pos1.y,speed1).setOnComplete(neonAction).setEase(easeType);
    }

    private IEnumerator randomWait()
    {
        ongoing = true;
        float randomWaitTime = Random.Range(minWait,maxWait);
        yield return new WaitForSeconds(randomWaitTime);
        neon2.enabled = false;      
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
        {
            shadowPass1();
        }
        else
        {
            shadowPass2();
        }
        ongoing = false;
    }

    void neonAction()
    {
        neon2.enabled = true;
    }
}
