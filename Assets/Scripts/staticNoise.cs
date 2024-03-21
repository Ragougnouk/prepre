using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staticNoise : MonoBehaviour
{
    //public Sprite[] staticSprite;
    //public GameObject[] staticObj;

    public Image screen;

    public bool active = false;


    public Color[] screenColor; // Define colors for each screen
    public float noiseUpdateInterval = 1f; // Interval to update noise
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {   
            timer += Time.deltaTime;

            // Check if it's time to update noise
            if (timer >= noiseUpdateInterval)
            {
                // Update noise
                UpdateNoise();
                if(!screen.enabled)
                {
                    screen.enabled = true;
                }
                // Reset timer
                timer = 0f;
            }

            //whiteNoise();
            //UpdateNoise();
        }
        else if (!active && screen.enabled)
        {
            screen.enabled = false;
        }
    }

    /*private void whiteNoise()
    {
        foreach(GameObject obj in staticObj)
        {
            int ind = Random.Range(0,staticSprite.Length);
            obj.GetComponent<Image>().sprite = staticSprite[ind];
        }
    }*/




    private void UpdateNoise()
    {
        // Randomly select a color from the defined array
        //Color selectedColor = screenColor;

        // Generate pixel-perfect noise texture
        Texture2D noiseTexture = GenerateNoiseTexture();

        // Apply the texture to the screen object's material
        screen.sprite = Sprite.Create(noiseTexture, new Rect(0, 0, noiseTexture.width, noiseTexture.height), Vector2.zero);
    }

    private Texture2D GenerateNoiseTexture()
    {
        int width = (int)screen.rectTransform.rect.width/4;
        int height = (int)screen.rectTransform.rect.height/4;

        // Create a new texture
        Texture2D texture = new Texture2D(width, height);

        // Fill the texture with pixel-perfect noise
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            // Set each pixel to the selected color with slight variations
            //pixels[i] = color * Random.Range(0.9f, 1.1f);
            int ind = Random.Range(0, screenColor.Length);
            pixels[i] = screenColor[ind];
        }
        texture.SetPixels(pixels);
        texture.Apply();

        texture.filterMode = FilterMode.Point;

        return texture;
    }

}
