using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shining_stars : MonoBehaviour
{

    public Transform starCache;
    public List<GameObject> stars;
    public int rdmNb;

    public LeanTweenType easeType;
    public AnimationCurve curve1;

    public float bounceTime = 0.5f;
    private bool[] onSpark;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.init(800);
        starList(starCache,stars);
        //print(stars.Count);
        onSpark = new bool[stars.Count];
        for(int i = 0; i<stars.Count ; i++)
        {
            onSpark[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        rdmNb = Random.Range(0, stars.Count);

        //if(!onSpark){ StartCoroutine(waitSpark()); }
        //sparkleSec(rdmNb);

        if(!onSpark[rdmNb]){ StartCoroutine(sparkle(rdmNb)); }
        
    }

    private void starList(Transform parent, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            list.Add(child.gameObject);
            //starList(child, list);
        }
    }

    IEnumerator sparkle(int index)
    {
        /*Color starA = stars[index].GetComponent<Image>().color;
        starA.a = 0.0f;
        stars[index].GetComponent<Image>().color = starA;
        yield return new WaitForSeconds(0.5f);
        starA.a = 0.8f;
        stars[index].GetComponent<Image>().color = starA;*/

        onSpark[index] = true;

        Color starA = stars[index].GetComponent<Image>().color;
        starA.a = 0.2f;
        stars[index].GetComponent<Image>().color = starA;
        yield return new WaitForSeconds(bounceTime/10);
        starA = stars[index].GetComponent<Image>().color;
        starA.a = 0.4f;
        stars[index].GetComponent<Image>().color = starA;
        yield return new WaitForSeconds(bounceTime/10);
        starA = stars[index].GetComponent<Image>().color;
        starA.a = 0.6f;
        stars[index].GetComponent<Image>().color = starA;
        yield return new WaitForSeconds(bounceTime/10);
        starA = stars[index].GetComponent<Image>().color;
        starA.a = 0.8f;
        stars[index].GetComponent<Image>().color = starA;
        yield return new WaitForSeconds(bounceTime/15);
        starA = stars[index].GetComponent<Image>().color;
        starA.a = 0.6f;
        stars[index].GetComponent<Image>().color = starA;
        yield return new WaitForSeconds(bounceTime/10);
        starA = stars[index].GetComponent<Image>().color;
        starA.a = 0.4f;
        stars[index].GetComponent<Image>().color = starA;
        yield return new WaitForSeconds(bounceTime/10);
        starA = stars[index].GetComponent<Image>().color;
        starA.a = 0.2f;
        stars[index].GetComponent<Image>().color = starA;
        yield return new WaitForSeconds(bounceTime/10);
        starA = stars[index].GetComponent<Image>().color;
        starA.a = 0.0f;
        stars[index].GetComponent<Image>().color = starA;
        yield return new WaitForSeconds(bounceTime/5);

        onSpark[index] = false;

    }

    private void sparkleSec(int index)
    {
        if(LeanTween.isTweening(stars[index]))
        {
            return;
        }
        else
        {
            LeanTween.alpha(stars[index],0.0f, bounceTime).setEase(easeType).setLoopPingPong();

        }
    }

    private IEnumerator waitSpark()
    {
        sparkleSec(rdmNb);
        //onSpark = true;
        yield return new WaitForSeconds(bounceTime/10); 
        //onSpark = false;

    }

}
