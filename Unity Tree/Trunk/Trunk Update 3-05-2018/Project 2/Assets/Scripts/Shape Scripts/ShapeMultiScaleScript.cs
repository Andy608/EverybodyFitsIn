using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMultiScaleScript : ShapeScaleScript
{
    public Vector2 shapeScale = new Vector2(1.0f, 1.0f);

    protected override void updateScale()
    {
        mShapeScale.x = shapeScale.x;
        mShapeScale.y = shapeScale.y;

        Vector3 newScale = gameObject.transform.localScale;
        newScale.x = shapeScale.x;
        newScale.y = shapeScale.y;
        gameObject.transform.localScale = newScale;
    }
}
