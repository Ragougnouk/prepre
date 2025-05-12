using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class syncLineEffect : MonoBehaviour
{

    private SpriteRenderer spriteLine;
    public float minPosY = 0.56f;
    public float maxPosY = 5.56f;

    public float minOpacity = 0.01f;
    public float maxopacity = 0.2f;

    public float flickerInterval = 0.1f; // Adjust this to change the flicker speed
    private float nextFlickerTime;

    public bool rdmFlick = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteLine = GetComponent<SpriteRenderer>();

    }

    private void OnEnable()
    {
        //firstTime = Time.time;
        nextFlickerTime = Time.time + flickerInterval;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFlickerTime)
        {
            randomizeSyncLineParams();

            // Set the time for the next flicker
            if(rdmFlick)
            {
                nextFlickerTime = Time.time + Random.Range(0.05f, 0.3f);
            }
            else
            {
                nextFlickerTime = Time.time + flickerInterval;
            }
            
        }
    }

    void randomizeSyncLineParams()
    {
        Color lineColor = spriteLine.color;
        lineColor.a = Random.Range(minOpacity,maxopacity);
        spriteLine.color = lineColor;

        //int rdmPosY = Random.Range(0,125);

        

        int currentStepPos = Mathf.RoundToInt(transform.localPosition.y/0.04f);


        int lineRdmDistance = Random.Range(0,100);
        float newPosY = minPosY;

        if (lineRdmDistance < 60)
        {
            newPosY = Mathf.Clamp(0.04f * (Random.Range(currentStepPos - 5, currentStepPos + 5)),minPosY,maxPosY);
        }
        else if (lineRdmDistance < 90 && lineRdmDistance > 60)
        {
            newPosY = Mathf.Clamp(0.04f * (Random.Range(currentStepPos - 20, currentStepPos + 20)),minPosY,maxPosY);
        }
        else if (lineRdmDistance > 90)
        {
            newPosY = Mathf.Clamp(0.04f * (Random.Range(0, 125)),minPosY,maxPosY);
        }


        transform.localPosition = new Vector3(transform.localPosition.x, newPosY, transform.localPosition.z);
    }
}
