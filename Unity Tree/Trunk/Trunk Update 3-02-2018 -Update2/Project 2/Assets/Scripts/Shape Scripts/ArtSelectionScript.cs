using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtSelectionScript : MonoBehaviour {

    public string tileType; //This should match the test against string within the LevelScript
    private Sprite neutralSprite;
    private Sprite happySprite;

	// Use this for initialization
	void Start () {
        setRandomSprite();
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
        GameControllerScript.getInstance().GetComponent<LevelScript>().returnRandomSquareSprite(tileType, gameObject);

        if (neutralSprite != null)
            setSprite(neutralSprite);
    }

    public void setHappy(Sprite spriteToSet)
    {
        happySprite = spriteToSet;
    }

    public void applyHappy()
    {
        if(happySprite != null)
            setSprite(happySprite);
    }

    public void setNeutral(Sprite spriteToSet)
    {
        neutralSprite = spriteToSet;
    }

    public void appyNeutral()
    {
        if (happySprite != null)
            setSprite(neutralSprite);
    }
}
