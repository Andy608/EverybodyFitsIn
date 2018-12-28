using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public bool isAnchor = false;
    //private bool isEnabled = true; //If the peice is in the correct spot, will disable the unit
    private bool isSnapped = false;
    //private bool isUnuseallShape = false; //Used for L pieces, possibly z pieces if those get implemented

    const string SNAP_SPOT_TAG = "SnapSpot"; //Tag for the snap aoe spot
    //const string ANCHOR_TAG = "AnchorSpot";
    int checkCount = 0; //I'm using this to see if the trigger is constatnly being called
                        //GameObject objInsideOf = null; //The object this is inside of, used for snaping

    private GameObject closestSnapObj;
    private GameObject currentOccupingAOE;

    //Diffrent gameobjects associated with this object
    private GameObject boundsObj; //Parent gameobject of the piece
    private GameObject artObj; //Art of the piece
    //private List<GameObject> children = new List<GameObject>();

    private Vector2 previousPosition;

    [HideInInspector]
    public List<GameObject> AOEsInside = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        artObj = gameObject.transform.GetChild(0).gameObject; //This will work as long as the art is the first gameobject in the children
        boundsObj = gameObject.transform.GetChild(1).gameObject; //This will only work is bounds is the second gameobject.
        previousPosition = transform.localPosition;

        GameControllerScript.getInstance().GetComponent<LevelScript>().addRainbowShape(artObj);

        if (!isAnchor)
        {
            GameControllerScript.getInstance().GetComponent<LevelScript>().addShape(gameObject);
        }
        else
        {
            artObj.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        closestSnapObj = null;


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

    void Update ()
    {
        moveObject();
    }

    public void mouseDown(bool isMouseDown)
    {
        if (isAnchor)
        {
            return;
        }

        if (isMouseDown)
        {
            artObj.GetComponent<SpriteRenderer>().sortingOrder = 2;
            //Debug.Log(gameObject + ": " + isEnabled);

            //Snaps all the children to the parent's position
            //gameObject.transform.position = parent.transform.position;
            //art.transform.position = parent.transform.position;

            //Checks to see if the piece is snapped or not
            if (isSnapped)
            {
                GameControllerScript.getInstance().GetComponent<LevelScript>().removeSnappedShape(gameObject);
                currentOccupingAOE.GetComponent<SnapSpotScript>().setOccupied(false);
                //isEnabled = true;
            }

            isSnapped = false;
            closestSnapObj = null;
        }
        else
        {
            //if (isEnabled)
            //{
            artObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            checkForSnap();
            //}
        }
    }

    //Moves the object with the mouse if it is enabled && the mouse is over it
    void moveObject()
    {
        if (isAnchor)
        {
            return;
        }

        bool isMouseDown = getShapeBounds().isMouseDownOnShape();
        //Debug.Log("MOUSE DOWN: " + isMouseDown + " | ENABLED: " + isEnabled);
        if (isMouseDown /*&& isEnabled*/)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = mousePos;
        }
    }

    void moveToSnap(GameObject snapTo)
    {
        if (isAnchor)
        {
            return;
        }

        if (!snapTo.GetComponent<SnapSpotScript>().isCurrentlyOccupied())
        {
            //Debug.Log("DISABLING PIECE AND SNAPPING");
            Vector3 snapPos = snapTo.transform.position;
            snapPos.z = transform.position.z;
            transform.position = snapPos;//Update the position.
            snapTo.GetComponent<SnapSpotScript>().setOccupied(true);
            //isEnabled = false;
            isSnapped = true;
            GameControllerScript.getInstance().GetComponent<LevelScript>().addSnappedShape(gameObject);
            currentOccupingAOE = snapTo;
        }
    }

    void checkForSnap()
    {
        if (isAnchor)
        {
            return;
        }

        //What this convoluted mess basicly does is run through each of the AOE's the piece is currently inside of,
        // then checks it against all the checks for snapping
        foreach (GameObject objInsideOf in AOEsInside)
        {
            if(objInsideOf != null && /*isEnabled &&*/ !getShapeBounds().isMouseDownOnShape())
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
                        //Debug.Log("SNAP SPOT: " + objInsideOf.gameObject);
                        updateClosestSnap(objInsideOf.gameObject);
                    }
                }
            }
        }

        if (closestSnapObj != null)
        {
            //Debug.Log("CLOSEST SNAP SPOT: " + closestSnapObj.gameObject);
            moveToSnap(closestSnapObj);
        }
        else if (AOEsInside.Count > 0)
        {
            transform.localPosition = previousPosition;
        }
        else
        {
            previousPosition = transform.localPosition;
        }
    }

    public GameObject findAOE(GameObject searchKey)
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

    public ShapeBoundsScript getShapeBounds()
    {
        return boundsObj.GetComponent<ShapeBoundsScript>();
    }
}
