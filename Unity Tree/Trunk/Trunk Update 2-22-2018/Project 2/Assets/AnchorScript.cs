using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
        //This exists purly to give the anchors the rainbow effect at the end of the puzzle
        GameControllerScript.getInstance().addToRainbowList(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
