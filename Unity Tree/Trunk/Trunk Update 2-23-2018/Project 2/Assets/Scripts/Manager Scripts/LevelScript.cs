using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    private List<GameObject> shapeList = new List<GameObject>();
    private List<GameObject> rainbowPieces = new List<GameObject>();

    private List<GameObject> snappedShapeList = new List<GameObject>();

    public Material endStateMaterial;

	void Start ()
    {
        //shapeList.Clear();
        //rainbowPieces.Clear();
	}
	
    public void addShape(GameObject shape)
    {
        shapeList.Add(shape);
    }

    public void addRainbowShape(GameObject rainbowShape)
    {
        rainbowPieces.Add(rainbowShape);
    }

    public void addSnappedShape(GameObject shape)
    {
        snappedShapeList.Add(shape);

        if (isLevelComplete())
        {
            preformWin();
        }
    }

    public void removeSnappedShape(GameObject shape)
    {
        snappedShapeList.Remove(shape);
    }

    private bool isLevelComplete()
    {
        Debug.Log("LEVEL COMPLETE: " + shapeList.Count + " " + snappedShapeList.Count);
        return shapeList.Count == snappedShapeList.Count;
    }
    
    private void preformWin()
    {
        showRainbow();
    }

    private void showRainbow()
    {
        foreach (GameObject shape in snappedShapeList)
        {
            Debug.Log(shape.name + " is now disabled.");
            //shape.GetComponent<PieceScript>().isEnabled = false; //Sets the pieces to disabled
            shape.GetComponent<PieceScript>().isAnchor = true; //Set all to anchors so they can't move.
        }

        foreach (GameObject piece in rainbowPieces)
        {
            Debug.Log(piece.name + " is now a rainbow!");
            piece.GetComponent<Renderer>().material = endStateMaterial; //Applies the rainbow effect
        }
    }
}
