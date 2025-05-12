using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucomNumber : MonoBehaviour
{
    public SpriteRenderer[] digits;
    public Sprite[] numbers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateNumber(int loopNb, int lastloopNb)
    {
        if(lastloopNb == 0)
        {
            if (loopNb < 10)
            {
                digits[0].sprite = numbers[loopNb];
            }
            else
            {
                digits[0].sprite = numbers[loopNb % 10];
                digits[1].sprite = numbers[loopNb/10];
            } 
        }
        else
        {
            if(lastloopNb == 1)
            {
                digits[0].sprite = numbers[11];
                digits[1].sprite = numbers[10];
                digits[2].sprite = numbers[10];
                digits[3].sprite = numbers[10];
                digits[4].sprite = numbers[10];
            }
            else if(lastloopNb == 2)
            {
                digits[0].sprite = numbers[11];
                digits[1].sprite = numbers[11];
                digits[2].sprite = numbers[10];
                digits[3].sprite = numbers[10];
                digits[4].sprite = numbers[10];
            }
            else if(lastloopNb == 3)
            {
                digits[0].sprite = numbers[11];
                digits[1].sprite = numbers[11];
                digits[2].sprite = numbers[11];
                digits[3].sprite = numbers[10];
                digits[4].sprite = numbers[10];
            }
            else if(lastloopNb == 4)
            {
                digits[0].sprite = numbers[11];
                digits[1].sprite = numbers[11];
                digits[2].sprite = numbers[11];
                digits[3].sprite = numbers[11];
                digits[4].sprite = numbers[10];
            }
            else if(lastloopNb > 4)
            {
                digits[0].sprite = numbers[11];
                digits[1].sprite = numbers[11];
                digits[2].sprite = numbers[11];
                digits[3].sprite = numbers[11];
                digits[4].sprite = numbers[11];
            }
        }
        
    }
}
