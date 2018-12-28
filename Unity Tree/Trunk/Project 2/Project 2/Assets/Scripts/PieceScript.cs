using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    private bool isMouseDown = false; //if the mouse is clicked on the object
    private bool isEnabled = true; //If the peice is in the correct spot, will disable the unit

    const string SNAP_SPOT_TAG = "SnapSpot"; //Tag for the snap aoe spot
    //const string ANCHOR_TAG = "AnchorSpot";
    int checkCount = 0; //I'm using this to see if the trigger is constatnly being called
                        //GameObject objInsideOf = null; //The object this is inside of, used for snaping

    private GameObject closestSnapObj;

    private Vector2 previousPosition;

    public List<GameObject> AOEsInside = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        GameControllerScript.getInstance().addNumOfPeices();
        closestSnapObj = null;
        previousPosition = transform.parent.position;
    }

    // Update is called once per frame
    void Update ()
    {
        moveObject();
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
        Debug.Log(gameObject + ": " + isEnabled);
    }

    private void OnMouseUp()
    {
        isMouseDown = false;

        if (isEnabled)
        {
            checkForSnap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(gameObject.name + " is in " + collision.gameObject.name);

        //Debug.Log("Total: " + AOEsInside.Count);
        AOEsInside.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(gameObject.name + " just left " + collision.gameObject.name);
        AOEsInside.Remove(findAOE(collision.gameObject));
        //Debug.Log("Total: " + AOEsInside.Count);
    }

    //Moves the object with the mouse if it is enabled && the mouse is over it
    void moveObject()
    {
        if (isMouseDown && isEnabled)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.parent.position = mousePos;
        }
    }

    void moveToSnap(GameObject snapTo)
    {
        Debug.Log("DISABLING PIECE AND SNAPPING");
        Vector3 snapPos = snapTo.transform.position;
        snapPos.z = transform.position.z;
        transform.position = snapPos;
        snapTo.GetComponent<SnapSpotScript>().setOccupied(true);
        isEnabled = false;
        GameControllerScript.getInstance().addNumOfCorrectPieces();
    }

    void checkForSnap()
    {
        //What this convoluted mess basicly does is run through each of the AOE's the peice is currently inside of,
        // then checks it against all the checks for snaping
        foreach (GameObject objInsideOf in AOEsInside)
        {
            if(objInsideOf != null && isEnabled && !isMouseDown)
            {
                //Debug.Log("Checking " + gameObject.name + " for a snap for the " + checkCount + "th time...");
                checkCount++;
                //Checks if it is the correct gameobject type and is no longer held
                if (objInsideOf.gameObject.tag == SNAP_SPOT_TAG)
                {
                    //Debug.Log("Checking if snapable...");

                    //Runs the check within the snapAoe object, if true snaps
                    SnapSpotScript snapScript = objInsideOf.gameObject.GetComponent<SnapSpotScript>();
                    if (snapScript.isCorrectPiece(gameObject))
                    {
                        Debug.Log("SNAP SPOT: " + objInsideOf.gameObject);
                        updateClosestSnap(objInsideOf.gameObject);
                    }
                }
            }
        }

        if (closestSnapObj != null)
        {
            Debug.Log("CLOSEST SNAP SPOT: " + closestSnapObj.gameObject);
            moveToSnap(closestSnapObj);
        }
        else if (AOEsInside.Count > 0)
        {
            transform.parent.position = previousPosition;
        }
        else
        {
            previousPosition = transform.parent.position;
        }
    }

    GameObject findAOE(GameObject searchKey)
    {
        foreach(GameObject index in AOEsInside)
        {
            if (index == searchKey)
            {
                return index;
            }
        }
        return null;
    }

    private void updateClosestSnap(GameObject otherSnapSpotObj)
    {
        if (closestSnapObj == null)
        {
            closestSnapObj = otherSnapSpotObj;
        }
        else if (Vector3.Magnitude(closestSnapObj.transform.position - gameObject.transform.position) >
            Vector3.Magnitude(otherSnapSpotObj.transform.position - gameObject.transform.position))
        {
            closestSnapObj = otherSnapSpotObj;
        }
    }

    public bool isMouseHoldingPiece()
    {
        return isMouseDown;
    }

}
