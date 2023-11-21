using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class module1Controller : MonoBehaviour
{
    public GameObject pointeur;
    public GameObject sonar;
    public Transform coordC;
    private AudioSource sound;
    private bool inMod;

    public GameObject screen1;
    public GameObject reticule;

    public float distVal = 0.1f;
    public bool nextStep = false;

    public float size = 1.0f;
    public float minSize = 0.5f;

    public float speed = 0.1f;


    public SpriteRenderer spriteRenderer;
    public float flickerInterval = 0.2f; // Adjust this to change the flicker speed

    private Vector2 startPos;
    private float nextFlickerTime;

    // Start is called before the first frame update
    void Start()
    {
        //coordC = cadre.transform.position;
        startPos = reticule.transform.position;
        sound = sonar.GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        screen1.SetActive(true);
        reticule.SetActive(true);
        float minX = coordC.position.x-size;
        float maxX = coordC.position.x+size;
        float minY = coordC.position.y-size;
        float maxY = coordC.position.y+size;
        float randomX = Random.Range(minX,maxX);
        float randomY = Random.Range(minY,maxY);
        while(randomX < coordC.position.x-minSize && randomY < coordC.position.y - minSize)
        {
            randomX = Random.Range(minX,maxX);
            randomY = Random.Range(minY,maxY);
        }
        //print("x "+ minX+" "+maxX+", y "+minY+" "+maxY);
        sonar.transform.position = new Vector3(randomX ,randomY,0);
        nextFlickerTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        float dirX = pointeur.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float dirY = pointeur.transform.position.y + Input.GetAxis("Vertical") * speed * Time.deltaTime;
        dirX = Mathf.Clamp(dirX,coordC.position.x-size,coordC.position.x + size);
        dirY = Mathf.Clamp(dirY,coordC.position.y-size,coordC.position.y + size);
        pointeur.transform.position = new Vector3(dirX,dirY,0);

        soundUpdate();

        if (Time.time > nextFlickerTime)
        {
            // Toggle the sprite's visibility by enabling/disabling the SpriteRenderer component
            spriteRenderer.enabled = !spriteRenderer.enabled;
            if (spriteRenderer.enabled)
            {
                sound.Play();
            }

            // Set the time for the next flicker
            nextFlickerTime = Time.time + flickerInterval;
        }
    }

    void soundUpdate()
    {
        if (Vector3.Distance(sonar.transform.position,pointeur.transform.position) < sound.maxDistance)
        {
            sound.volume = Mathf.Pow(1 - (Vector3.Distance(sonar.transform.position,pointeur.transform.position) / sound.maxDistance),4.0f);
            flickerInterval = Vector3.Distance(sonar.transform.position,pointeur.transform.position)/4.0f;
        }

        if(Vector3.Distance(sonar.transform.position,pointeur.transform.position)< distVal)
        {
            nextStep = true;
            sound.volume = 0;
        }
    }

    void OnDisable()
    {
        //screen1.SetActive(false);
        //reticule.SetActive(false);
        //reticule.transform.position = startPos;
    }

    public void reInit()
    {
        screen1.SetActive(false);
        reticule.SetActive(false);
        reticule.transform.position = startPos;
    }
}
