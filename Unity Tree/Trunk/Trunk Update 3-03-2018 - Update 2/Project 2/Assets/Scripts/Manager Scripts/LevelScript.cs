using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class LevelScript : MonoBehaviour
{
    private SoundManagerScript soundManager;

    private List<GameObject> shapeList = new List<GameObject>();
    private List<GameObject> rainbowPieces = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> snappedShapeList = new List<GameObject>();

    //Variables for art stuff
    public string squareTestAgainstString = "Square";
    private string squareSpritePath = "Sprites/TileSprites/1x1Tiles/Neutral";
    private string squareSpritePathHappy = "Sprites/TileSprites/1x1Tiles/Happy";
    private Object[] squareSprites; //Holds the sprites for the 1x1 squares
    private Object[] squareSpritesHappy; //Holds the sprites for the 1x1 happy sprites

    //No art yet, just a placeholder for now
    public string bigSquareTestAgainstString = "2x2Square";
    private string bigSquareSpritePath = "Sprites/TileSprites/2x2Tiles";
    private Object[] bigSquareSprites; //Holds the sprites for the 2x2 squares

    //No art yet, just a placeholder for now
    public string LTestAgainstString = "LSpot";
    private string LSpritePath = "Sprites/TileSprites/LTiles";
    private Object[] LSprites; //Holds the sprites for the 2x2 squares

    
    public string anchorTestAgainsString = "Anchor";
    private string anchorSpritePath = "Sprites/TileSprites/AnchorSprites/Level1/Sad";
    private string anchorSpritePathHappy = "Sprites/TileSprites/AnchorSprites/Level1/Happy";
    private Object[] anchorSprites; //Holds the sprites for the 2x2 squares
    private Object[] anchorSpritesHappy; //Holds the sprites for the 2x2 squares

    public Material endStateMaterial;

    private void Awake() //Will ensure that any required reasorces are loaded before anything else
    {

        populateSpritesFromReasorceFolder();
        soundManager = SoundManagerScript.getInstance();
    }

    private void OnLevelWasLoaded(int level)
    {
        shapeList.Clear();
        rainbowPieces.Clear();
        snappedShapeList.Clear();

    }

    void Start ()
    {
        
        //shapeList.Clear();
        //rainbowPieces.Clear();
    }
	
    public void returnRandomSquareSprite(string pieceType, GameObject piece) //Maby hold this and the other sprite loader in it's own script?
    {
        int randIndexSquare = Random.Range(0, squareSprites.Length);
        int randIndexAnchor = Random.Range(0, anchorSprites.Length);

        if (pieceType == squareTestAgainstString)
        {
            piece.GetComponent<ArtSelectionScript>().setNeutral((Sprite)squareSprites[randIndexSquare]);
            piece.GetComponent<ArtSelectionScript>().setHappy((Sprite)squareSpritesHappy[randIndexSquare]);
        }

        if (pieceType == anchorTestAgainsString)
        {
            piece.GetComponent<ArtSelectionScript>().setNeutral((Sprite)anchorSprites[randIndexAnchor]);
            piece.GetComponent<ArtSelectionScript>().setHappy((Sprite)anchorSpritesHappy[randIndexAnchor]);
        }

        if (pieceType == bigSquareTestAgainstString) //No art yet, placeholder for later
            Debug.Log("Not implemented");

        if (pieceType == LTestAgainstString) //No art yet, placeholder for later
            Debug.Log("Not implemented");

        //return null;
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
        soundManager.playFx("victory-cry-reverb-2");
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
            piece.GetComponent<ArtSelectionScript>().applyHappy();
        }
    }

    private void populateSpritesFromReasorceFolder() //Populates the arrays with all the appopriate sprites from the reasorce folder
    {
        squareSprites = Resources.LoadAll(squareSpritePath, typeof(Sprite)); //Loads the entire folder specifiyed to the array
        squareSpritesHappy = Resources.LoadAll(squareSpritePathHappy, typeof(Sprite)); //Loads the entire folder specifiyed to the array

        anchorSprites = Resources.LoadAll(anchorSpritePath, typeof(Sprite)); //Loads the entire folder specifiyed to the array
        anchorSpritesHappy = Resources.LoadAll(anchorSpritePathHappy, typeof(Sprite)); //Loads the entire folder specifiyed to the array

        //bigSquareSprites = Resources.LoadAll(bigSquareSpritePath, typeof(Sprite)); 
        //LSprites = Resources.LoadAll(LSpritePath, typeof(Sprite)); //Loads the entire folder specifiyed to the array
    }
}
