using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScaleScript : MonoBehaviour
{
    public Vector2 shapeScale = new Vector2(1.0f, 1.0f);

    public void OnValidate()
    {
        updateScale();
    }

    private void updateScale()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x = shapeScale.x;
        newScale.y = shapeScale.y;
        gameObject.transform.localScale = newScale;
    }

    public Vector2 getScale()
    {
        return shapeScale;
    }
}
