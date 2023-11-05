using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleSequencer : MonoBehaviour
{
    public module1Controller mod1;
    public module2Controller mod2;
    public mod3test mod3;
    public GameObject lit1,lit2,lit3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mod1.enabled)
        {
            if (!lit1.activeSelf)
            {
                lit1.SetActive(true);
            }

            if (mod1.nextStep)
            {
                StartCoroutine(mod1mod2());
            }
        }

        if(mod2.enabled)
        {
            if (!lit2.activeSelf)
            {
                lit2.SetActive(true);
            }

            if (mod2.nextStep)
            {
                StartCoroutine(mod2mod3());
            }
        }


    }

    private IEnumerator mod1mod2()
    {
        mod1.enabled = false;
        lit1.SetActive(false);
        yield return new WaitForSeconds(1);
        mod2.enabled = true;
        lit2.SetActive(true);
    }

    private IEnumerator mod2mod3()
    {
        mod2.enabled = false;
        lit2.SetActive(false);
        yield return new WaitForSeconds(1);
        mod3.enabled = true;
        lit3.SetActive(true);
    }
}