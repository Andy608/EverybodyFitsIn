using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeUniformScaleScript : ShapeScaleScript
{
    public float scale = 1.0f;

    protected override void updateScale()
    {
        mShapeScale.x = scale;
        mShapeScale.y = scale;

        Vector3 newScale = gameObject.transform.localScale;
        newScale.x = mShapeScale.x;
        newScale.y = mShapeScale.y;
        gameObject.transform.localScale = newScale;
    }
}
