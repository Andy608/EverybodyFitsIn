using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class LevelScript : MonoBehaviour
{
    private List<GameObject> shapeList = new List<GameObject>();
    private List<GameObject> rainbowPieces = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> snappedShapeList = new List<GameObject>();

    //Variables for art stuff
    public string squareTestAgainstString = "Square";
    public string squareSpritePath = "Sprites/TileSprites/1x1Tiles";
    private Object[] squareSprites; //Holds the sprites for the 1x1 squares

    //No art yet, just a placeholder for now
    public string bigSquareTestAgainstString = "2x2Square";
    public string bigSquareSpritePath = "Sprites/TileSprites/2x2Tiles";
    private Object[] bigSquareSprites; //Holds the sprites for the 2x2 squares

    //No art yet, just a placeholder for now
    public string LTestAgainstString = "LSpot";
    public string LSpritePath = "Sprites/TileSprites/LTiles";
    private Object[] LSprites; //Holds the sprites for the 2x2 squares

    public Material endStateMaterial;

    private void Awake() //Will ensure that any required reasorces are loaded before anything else
    {
        populateSpritesFromReasorceFolder();
    }

    void Start ()
    {
        //shapeList.Clear();
        //rainbowPieces.Clear();
        

    }
	
    public Sprite returnRandomSquareSprite(string pieceType) //Maby hold this and the other sprite loader in it's own script?
    {
        if(pieceType == squareTestAgainstString) 
            return (Sprite)squareSprites[Random.Range(0, squareSprites.Length)]; //Will test further once I know this work

        if (pieceType == bigSquareTestAgainstString) //No art yet, placeholder for later
            return null;

        if (pieceType == LTestAgainstString) //No art yet, placeholder for later
            return null;

        return null;
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

    private void populateSpritesFromReasorceFolder() //Populates the arrays with all the appopriate sprites from the reasorce folder
    {
        squareSprites = Resources.LoadAll(squareSpritePath, typeof(Sprite)); //Loads the entire folder specifiyed to the array
        //bigSquareSprites = Resources.LoadAll(bigSquareSpritePath, typeof(Sprite)); 
        //LSprites = Resources.LoadAll(LSpritePath, typeof(Sprite)); //Loads the entire folder specifiyed to the array
    }
}
