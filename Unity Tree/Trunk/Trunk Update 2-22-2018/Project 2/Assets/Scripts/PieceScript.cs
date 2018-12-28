using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    private bool isMouseDown = false; //if the mouse is clicked on the object
    public bool isEnabled = true; //If the peice is in the correct spot, will disable the unit
    private bool isSnapped = false;
    //private bool isUnuseallShape = false; //Used for L pieces, possibly z pieces if those get implemented

    const string SNAP_SPOT_TAG = "SnapSpot"; //Tag for the snap aoe spot
    //const string ANCHOR_TAG = "AnchorSpot";
    int checkCount = 0; //I'm using this to see if the trigger is constatnly being called
                        //GameObject objInsideOf = null; //The object this is inside of, used for snaping

    private GameObject closestSnapObj;
    private GameObject currentOccupingAOE;

    //Diffrent gameobjects associated with this object
    private GameObject parent; //Parent gameobject of the peice
    private GameObject art; //Art of the piece
    //private List<GameObject> children = new List<GameObject>();

    private Vector2 previousPosition;

    public List<GameObject> AOEsInside = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        GameControllerScript.getInstance().addNumOfPieces(gameObject);
        closestSnapObj = null;
        previousPosition = transform.parent.position;

        parent = gameObject.transform.parent.gameObject;
        art = parent.transform.GetChild(0).gameObject; //This will work as long as the art is the first gameobject in the children

        //Not sure if totaly nessary, will comment back in if is
        //if (gameObject.tag == "L Piece") //NOTE: Will need to be adjuseted once more pieces are implemented
        //{
        //    isUnuseallShape = true;
            
        //    //Adds all the children to the list
        //    for(int i = 0; i < gameObject.transform.childCount; i++)
        //    {
        //        children.Add(gameObject.transform.GetChild(i).gameObject);
        //    }
        //}
            
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

        //Snaps all the children to the parent's position
        gameObject.transform.position = parent.transform.position;
        art.transform.position = parent.transform.position;

        //Checks to see if the piece is snapped or not
        if(isSnapped)
        {
            GameControllerScript.getInstance().removeNumOfCorrect();
            currentOccupingAOE.GetComponent<SnapSpotScript>().setUnOccupided(false);
            isEnabled = true;
        }
        isSnapped = false;
        closestSnapObj = null;
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
        if(snapTo.GetComponent<SnapSpotScript>().isCurrentlyOccupied() != true)
        {
            Debug.Log("DISABLING PIECE AND SNAPPING");
            Vector3 snapPos = snapTo.transform.position;
            snapPos.z = transform.position.z;
            transform.position = snapPos;
            snapTo.GetComponent<SnapSpotScript>().setOccupied(true);
            //isEnabled = false;
            isSnapped = true;
            GameControllerScript.getInstance().addNumOfCorrectPieces();
            currentOccupingAOE = snapTo;
        }
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
        //else if (AOEsInside.Count > 0)
        //{
        //    transform.parent.position = previousPosition;
        //}
        //else
        //{
        //    previousPosition = transform.parent.position;
        //}
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
