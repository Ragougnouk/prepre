using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleSequencer : MonoBehaviour
{
    public module1Controller mod1;
    public module2Controller mod2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mod1.enabled)
        {
            if (mod1.nextStep)
            {
                StartCoroutine(mod1mod2());
            }
        }


    }

    private IEnumerator mod1mod2()
    {
        mod1.enabled = false;
        yield return new WaitForSeconds(1);
        mod2.enabled = true;
    }
}
