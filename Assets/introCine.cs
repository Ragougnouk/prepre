using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introCine : MonoBehaviour
{
    public GameObject title;
    public GameObject main;
    public module1Controller mod1;
    public float speed = 1.0f;

    private bool ceparti =false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(introLaunch());
        if (ceparti)
        {
            main.transform.position = Vector3.MoveTowards(main.transform.position, new Vector3(0,0,-10), speed * Time.deltaTime);
        }
    }

    private IEnumerator introLaunch()
    {
        yield return new WaitForSeconds(2);
        title.SetActive(false);
        ceparti = true;
        mod1.enabled = true;
    }
}

