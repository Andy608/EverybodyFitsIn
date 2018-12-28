using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShapeScaleScript : MonoBehaviour
{
    protected Vector2 mShapeScale;

    public void OnValidate()
    {
        updateScale();
    }

    protected abstract void updateScale();

    public Vector2 getScale()
    {
        return mShapeScale;
    }
}
