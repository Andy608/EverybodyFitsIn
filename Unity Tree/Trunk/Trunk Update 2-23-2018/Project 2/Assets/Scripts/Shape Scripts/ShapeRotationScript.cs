using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeRotationScript : MonoBehaviour
{
    /*public GameObject[] rotateShapes;
    private GameObject currentShape;
    public int currentShapeIndex;*/

    public EnumRotation currentRotation;
    public EnumRotation[] validRotations;

    public enum EnumRotation
    {
        NORTH = 0,
        WEST = 1,
        SOUTH = 2,
        EAST = 3,
    }

    private static int RIGHT_CLICK = 1;

	// Use this for initialization
	void Start ()
    {
        /*if (rotateShapes.Length == 0)
        {
            Debug.Log("There are no shapes to choose from!");
        }
        else
        {
            currentShapeIndex %= rotateShapes.Length;
            rotateShape();
        }*/
    }
	
	//Update is called once per frame
	void Update ()
    {
        //Check the input to see if the shape should rotate
        checkInput();
	}

    private void checkInput()
    {
        if (GetComponent<PieceScript>() != null && GetComponent<PieceScript>().getShapeBounds().isMouseDownOnShape())
        {
            if (Input.GetMouseButtonDown(RIGHT_CLICK) || Input.GetKeyDown(KeyCode.Space))
            {
                rotateShape();
            }
        }
    }

    private void rotateShape()
    {
        currentRotation = validRotations[(((int)currentRotation + 1) % validRotations.Length)];
        rotateShape(currentRotation);
    }

    private void rotateShape(EnumRotation rotation)
    {
        Vector3 newRotation = gameObject.transform.localEulerAngles;
        newRotation.z = -(int)rotation * 90;
        gameObject.transform.localEulerAngles = newRotation;
    }

    public void OnValidate()
    {
        rotateShape(currentRotation);
    }
}
