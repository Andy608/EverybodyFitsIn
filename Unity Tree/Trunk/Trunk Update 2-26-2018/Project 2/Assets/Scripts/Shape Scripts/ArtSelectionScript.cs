using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtSelectionScript : MonoBehaviour {

    public string tileType; //This should match the test against string within the LevelScript

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
        Sprite temp = GameControllerScript.getInstance().GetComponent<LevelScript>().returnRandomSquareSprite(tileType);

        if (temp != null)
            setSprite(temp);
    }
}
