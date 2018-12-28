using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSpotScript : MonoBehaviour
{
    bool isOccupied = false;
    //string occupingPeice = ""; //What peice is currently occuping the snap
    //public string requiredPieceTag; //What peice is required for the peticular spot
    public GameObject requiredObject;
    //public bool isMouseOver = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    /*private void OnMouseOver()
    {
        Debug.Log("OMG THE MOUSE IS IN SHAPE: " + gameObject);
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("OMG THE MOUSE IS NOT IN SHAPE: " + gameObject);
        isMouseOver = false;
    }*/

    //Checks & returns if the piece over is the correct one
    public bool isCorrectPiece(GameObject selectedObj)
    {
        //Debug.Log("Tag on gameobject is: " + selectedObj.tag + " Is occupied = " + isOccupied);
        if (!isOccupied 
            /*&& isMouseOver*/
            && isCorrectTaggedPiece(selectedObj) 
            && isCorrectRotatedPiece(selectedObj)
            && isCorrectScaledPiece(selectedObj.GetComponent<PieceScript>().getShapeBounds().gameObject))
        {
            //occupingPeice = collisionName;

            return true;
        }

        return false;
    }

    private bool isCorrectTaggedPiece(GameObject selectedObj)
    {
        return (selectedObj.tag == requiredObject.tag);
    }

    private bool isCorrectRotatedPiece(GameObject selectedObj)
    {
        //Debug.Log(selectedObj.tag + " | " + requiredObject.tag + " | " + selectedObj.transform.rotation.z + " | " + gameObject.transform.rotation.z);
        return (selectedObj.transform.localRotation.eulerAngles.z == gameObject.transform.localRotation.eulerAngles.z);
    }

    private bool isCorrectScaledPiece(GameObject selectedObj)
    {
        //Debug.Log("Snap Scale: " + gameObject.transform.localScale + " | Piece Scale: " + selectedObj.transform.localScale);
        return (gameObject.transform.localScale == selectedObj.transform.localScale);
    }

    public bool isCurrentlyOccupied()
    {
        if (isOccupied)
            return true;
        return false;
    }

    public void setOccupied(bool occupied)
    {
        isOccupied = occupied;
        gameObject.GetComponent<SpriteRenderer>().enabled = !isOccupied;
        Debug.Log("Enabled: " + gameObject.name);

        //RaycastHit2D[] hits;
        //hits = Physics2D.RaycastAll(gameObject.transform.position/*Camera.main.ScreenToWorldPoint(Input.mousePosition)*/, Vector2.zero);

        //for (int i = 0; i < hits.Length; i++)
        //{
        //    RaycastHit2D hit = hits[i];
        //    GameObject potentialSnapSpot = hit.transform.gameObject;

        //    if (potentialSnapSpot.GetComponent<SnapSpotScript>() != null)
        //    {
        //        //Debug.Log("ADDING POTENTIAL SNAPSPOT: " + potentialSnapSpot);
        //        if (occupied)
        //        {
        //            //potentialSnapSpot.GetComponent<SnapSpotScript>().setEnabled(false);
        //        }
        //        else
        //        {
        //            //potentialSnapSpot.GetComponent<SnapSpotScript>().setEnabled(true);
        //        }
        //    }
        //}
    }
}
