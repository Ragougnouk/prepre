using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class glitchy_start : MonoBehaviour
{
    public GameObject[] glitchPiece;
    public float frequence;
    public Vector3[] basePos;

    public GameObject[] buttonPieces;

    public Color[] colorChoice;

    public float glitchLong = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        basePos[0] = glitchPiece[0].transform.localPosition;
        basePos[1] = glitchPiece[1].transform.localPosition;
        basePos[2] = glitchPiece[2].transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float rdmNb = Random.Range(0,100);
        if (rdmNb < frequence)
        {
            StartCoroutine(glitchy());
        }
    }

    IEnumerator glitchy()
    {
        int rdmPiece = Random.Range(0,3);
        int[] offsetList = {2,4,-2,-4,6,-6,8,-8 };
        int offset = offsetList[Random.Range(0, offsetList.Length)];
        //Vector3 basePos = glitchPiece[rdmPiece].transform.localPosition;
        glitchPiece[rdmPiece].transform.localPosition = new Vector3(glitchPiece[rdmPiece].transform.localPosition.x + offset,glitchPiece[rdmPiece].transform.localPosition.y,0);
        float glitchTime = Random.Range(0,glitchLong);
        yield return new WaitForSeconds(glitchTime);
        /*print("rdm "+ rdmPiece  );
        print("1 " + glitchPiece[rdmPiece].transform.localPosition);
        print("2 " + basePos[rdmPiece]);*/
        glitchPiece[rdmPiece].transform.localPosition = basePos[rdmPiece];
    }

    public void colorSwapOn()
    {
        Color rdmColor = colorChoice[Random.Range(0, colorChoice.Length)];
        foreach (GameObject piece in buttonPieces)
        {
            piece.GetComponent<Image>().color = rdmColor;
        }
    }

    public void colorSwapOff()
    {
        Color rdmColor = Color.white;
        foreach (GameObject piece in buttonPieces)
        {
            piece.GetComponent<Image>().color = rdmColor;
        }
    }
}
