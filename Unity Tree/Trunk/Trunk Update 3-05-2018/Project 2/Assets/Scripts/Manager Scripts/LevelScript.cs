using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    private SoundManagerScript soundManager;

    private List<GameObject> shapeList = new List<GameObject>();
    private List<GameObject> rainbowPieces = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> snappedShapeList = new List<GameObject>();

    public Material endStateMaterial;

    private void Awake() //Will ensure that any required reasorces are loaded before anything else
    {
        shapeList.Clear();
        rainbowPieces.Clear();
        snappedShapeList.Clear();

        soundManager = SoundManagerScript.getInstance();
    }

    void OnLevelWasLoaded(int level)
    {
        shapeList.Clear();
        rainbowPieces.Clear();
        snappedShapeList.Clear();
        //Debug.Log("HELLO");
    }
	
    public void addShape(GameObject shape)
    {
        //Debug.Log("ADDING NEW SHAPE: " + shapeList.Count);
        shapeList.Add(shape);
    }

    public void addRainbowShape(GameObject rainbowShape)
    {
        //Debug.Log("ADDING NEW RAINBOW SHAPE: " + rainbowPieces.Count);
        rainbowPieces.Add(rainbowShape);
    }

    public void addSnappedShape(GameObject shape)
    {
        //Debug.Log("ADDING NEW SNAPPED SHAPE: " + snappedShapeList.Count);
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
        //Debug.Log("LEVEL COMPLETE: " + shapeList.Count + " " + snappedShapeList.Count);
        return shapeList.Count == snappedShapeList.Count;
    }
    
    private void preformWin()
    {
        soundManager.playFx("victory-cry-reverb-2");
        showRainbow();
        GameControllerScript.compleateLevel();
    }

    private void showRainbow()
    {
        foreach (GameObject shape in snappedShapeList)
        {
            //Debug.Log(shape.name + " is now disabled.");
            //shape.GetComponent<PieceScript>().isEnabled = false; //Sets the pieces to disabled
            shape.GetComponent<PieceScript>().isAnchor = true; //Set all to anchors so they can't move.
        }

        foreach (GameObject piece in rainbowPieces)
        {
            //Debug.Log(piece.name + " is now a rainbow!");
            piece.GetComponent<Renderer>().material = endStateMaterial; //Applies the rainbow effect
            piece.GetComponent<ArtSelectionScript>().applyHappy();
        }
    }
}
