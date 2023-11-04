using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class module1Controller : MonoBehaviour
{
    public GameObject cadre;
    public GameObject pointeur;
    public GameObject sonar;
    private Vector2 coordC;
    private AudioSource sound;

    public float distVal = 0.1f;
    public bool nextStep = false;

    public float size = 1.0f;

    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        coordC = cadre.transform.position;
        sound = sonar.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        float dirX = pointeur.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float dirY = pointeur.transform.position.y + Input.GetAxis("Vertical") * speed * Time.deltaTime;
        dirX = Mathf.Clamp(dirX,coordC.x-size,coordC.x + size);
        dirY = Mathf.Clamp(dirY,coordC.y-size,coordC.y + size);

        pointeur.transform.position = new Vector3(dirX,dirY,0);

        soundUpdate();
    }

    void soundUpdate()
    {
        if (Vector3.Distance(sonar.transform.position,pointeur.transform.position) < sound.maxDistance)
        {
            sound.volume = Mathf.Pow(1 - (Vector3.Distance(sonar.transform.position,pointeur.transform.position) / sound.maxDistance),4.0f);
        }

        if(Vector3.Distance(sonar.transform.position,pointeur.transform.position)< distVal)
        {
            nextStep = true;
            //print("yes");
        }
    }
}
