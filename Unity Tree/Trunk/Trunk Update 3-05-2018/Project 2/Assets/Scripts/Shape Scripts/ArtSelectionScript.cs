using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtSelectionScript : MonoBehaviour
{
    public bool useRandomSprite = true;

    static string squarePath = "Sprites/TileSprites/1x1_Square_Tiles";
    static Object[] squareHappy;
    static Object[] squareNeutral;

    static string rectanglePath = "Sprites/TileSprites/2x1_Rectangle_Tiles";
    static Object[] rectangleHappy;
    static Object[] rectangleNeutral;

    static string LPath = "Sprites/TileSprites/2x2_L_Tiles";
    static Object[] LHappy;
    static Object[] LNeutral;

    static string happyPath = "/Happy";
    static string neturalPath = "/Neutral";

    public Sprite neutralSprite;
    public Sprite happySprite;

    private static bool isLoaded;

	// Use this for initialization
	void Start ()
    {
        if (!isLoaded)
        {
            populateSpritesFromResourceFolder();
        }

        setRandomSprite();
        applyNeutral();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setSprite(Sprite spriteToSet)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteToSet;
    }

    private void setRandomSprite()
    {
        PieceScript pieceScript = transform.parent.GetComponent<PieceScript>();
        if (pieceScript != null && useRandomSprite)
        {
            //Debug.Log("YO YO YO");
            switch (gameObject.tag)
            {
                case "Square Piece":
                    //Debug.Log("LOAD SQUARE STUFF");
                    setRandomImage(squareHappy, squareNeutral);
                    break;
                case "Rectangle Piece":
                    setRandomImage(rectangleHappy, rectangleNeutral);
                    break;
                case "L Piece":
                    setRandomImage(LHappy, LNeutral);
                    break;
                case "Triangle Piece":
                    //setRandomImage(TriangleHappy, TriangleNeutral);
                    break;
            }
        }
    }

    private void setRandomImage(Object[] happyArray, Object[] neutralArray)
    {
        int index = Random.Range(0, happyArray.Length);

        setHappy((Sprite)happyArray[index]);
        setNeutral((Sprite)neutralArray[index]);
    }

    public void setHappy(Sprite spriteToSet)
    {
        happySprite = spriteToSet;
    }

    public void applyHappy()
    {
        if(happySprite != null)
        {
            setSprite(happySprite);
        }
    }

    public void setNeutral(Sprite spriteToSet)
    {
        neutralSprite = spriteToSet;
    }

    public void applyNeutral()
    {
        if (happySprite != null)
        {
            setSprite(neutralSprite);
        }
    }

    public static void populateSpritesFromResourceFolder() //Populates the arrays with all the appopriate sprites from the reasorce folder
    {
        squareHappy = Resources.LoadAll(squarePath + happyPath, typeof(Sprite)); //Loads the entire folder specifiyed to the array
        squareNeutral = Resources.LoadAll(squarePath + neturalPath, typeof(Sprite)); //Loads the entire folder specifiyed to the array

        rectangleHappy = Resources.LoadAll(rectanglePath + happyPath, typeof(Sprite)); //Loads the entire folder specifiyed to the array
        rectangleNeutral = Resources.LoadAll(rectanglePath + neturalPath, typeof(Sprite)); //Loads the entire folder specifiyed to the array

        LHappy = Resources.LoadAll(LPath + happyPath, typeof(Sprite)); //Loads the entire folder specifiyed to the array
        LNeutral = Resources.LoadAll(LPath + neturalPath, typeof(Sprite)); //Loads the entire folder specifiyed to the array

        isLoaded = true;
    }
}
