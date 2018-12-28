using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBoundsScript : MonoBehaviour
{
    private bool mouseInShape;
    private bool mouseDown;

    private void OnMouseDown()
    {
        mouseDown = true;
        transform.parent.GetComponent<PieceScript>().mouseDown(mouseDown);
    }

    private void OnMouseUp()
    {
        mouseDown = false;
        transform.parent.GetComponent<PieceScript>().mouseDown(mouseDown);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(gameObject.name + " is in " + collision.gameObject.name);

        //Debug.Log("Total: " + AOEsInside.Count);
        //mouseInShape = true;
        transform.parent.GetComponent<PieceScript>().AOEsInside.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(gameObject.name + " just left " + collision.gameObject.name);
        //mouseInShape = false;
        transform.parent.GetComponent<PieceScript>().AOEsInside.Remove(transform.parent.GetComponent<PieceScript>().findAOE(collision.gameObject));
        //Debug.Log("Total: " + AOEsInside.Count);
    }

    public bool isMouseDownOnShape()
    {
        return mouseDown;
    }
}
