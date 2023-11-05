using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class module2Controller : MonoBehaviour
{
    public GameObject cadre;
    public GameObject caseDet;
    public GameObject point1,point2, point3;
    public GameObject signal;
    public GameObject ecran;

    private bool p1,p2,p3 = false;

    private Vector2 coordC;

    public float speed = 1.0f;
    public float distDet = 0.1f;
    public float size = 1.0f;

    public bool nextStep = false;

    // Start is called before the first frame update
    void Start()
    {
        coordC = cadre.transform.position;

    }

    private void OnEnable()
    {
        ecran.SetActive(false);
        signal.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        float dirX = caseDet.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        dirX = Mathf.Clamp(dirX,coordC.x - size, coordC.x + size);
        caseDet.transform.position = new Vector3(dirX,caseDet.transform.position.y,0);
        if (!p1)
        {
            testP1();
        }
        if (!p2)
        {
            testP2();
        }
        if (!p3)
        {
            testP3();
        }
        if (p1 && p2 && p3)
        {
            nextStep = true;
        }

    }

    void testP1()
    {
        if(Input.GetKey("space") && Vector3.Distance(point1.transform.position,caseDet.transform.position) < distDet)
        {
            p1 = true;
            SpriteRenderer point1C = point1.GetComponent<SpriteRenderer>();
            point1C.color = new Color(0,250,0);
        }
    }

    void testP2()
    {
        if(Input.GetKey("space") && Vector3.Distance(point2.transform.position,caseDet.transform.position) < distDet)
        {
            p2 = true;
            SpriteRenderer point2C = point2.GetComponent<SpriteRenderer>();
            point2C.color = new Color(0,250,0);
        }
    }

    void testP3()
    {
        if(Input.GetKey("space") && Vector3.Distance(point3.transform.position,caseDet.transform.position) < distDet)
        {
            p3 = true;
            SpriteRenderer point3C = point3.GetComponent<SpriteRenderer>();
            point3C.color = new Color(0,250,0);
        }
    }
}
