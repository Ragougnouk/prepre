using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boxNumber : MonoBehaviour
{
    public GameObject letterBox;
    public Canvas parentCanvas;
    public MessagesList messageFile;
    public boxNumber boxNb;
    public Module3Controller mod3;
    public moduleSequencer modSeq;

    private int boxWidth = 36;
    private int boxHeight = 52;
    private int rowX = 0;
    private int rowY = 0;

    public List<TMP_InputField> inputFields = new List<TMP_InputField>();


    private string message;
    //private GameObject[] letters;
    public List<GameObject> letters = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //messages = messageFile.stringList;
    }

    void OnEnable()
    {

        float canvasWidth = parentCanvas.GetComponent<RectTransform>().rect.width;
        message = messageFile.stringList[modSeq.loopNumber];
        message += " ";
        int lastSpace = -1;
        int returnValue = 0;
        for (int i = 0; i < message.Length; i++)
        {
            //print("row = "+ rowX +", i = "+ i + ", letter = " +message[i]);
            if(message[i] == ' ')
            {

                if (rowX * boxWidth > canvasWidth)
                {
                    //print("rowX = "+ rowX);
                    
                    returnValue += lastSpace + 1;
                    rowX = i - returnValue;
                    lastSpace = -1;
                    rowY -= 1;
                }


                for (int j = lastSpace + 1; j < rowX; j++)
                {
                    GameObject letter = Instantiate(letterBox, parentCanvas.transform);
                    letters.Add(letter);
                    
                    int offsetX = j * boxWidth;
                    int offsetY = rowY * boxHeight ;

                    RectTransform boxTransform = letter.GetComponent<RectTransform>();
                    boxTransform.SetParent(parentCanvas.transform, false);

                    boxTransform.anchoredPosition = new Vector2(offsetX, offsetY);

                }
                lastSpace = i - returnValue;


            }
            rowX += 1;
        }

        populateList();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnDisable()
    {
        rowX = 0;
        rowY = 0;
        foreach (GameObject letter in letters)
        {
            Destroy(letter);
        }
    }*/

    void populateList()
    {
        TMP_InputField newIF;
        foreach(GameObject letter in letters)
        {
            
            newIF = letter.GetComponent<TMP_InputField>();
            inputFields.Add(newIF);

        }
        mod3.IFlistFill(inputFields);
        mod3.enabled = true;
    }

    public void reInit()
    {
        rowX = 0;
        rowY = 0;
        foreach (GameObject letter in letters)
        {
            Destroy(letter);
        }
        letters.Clear();
        inputFields.Clear();
    }

}
