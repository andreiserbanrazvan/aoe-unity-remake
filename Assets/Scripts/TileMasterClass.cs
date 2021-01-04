using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMasterClass : MonoBehaviour
{
    float gridX, gridY;
    public virtual void OnSelect()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public virtual void OnDeselect()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public Vector2 getGridCoords()
    {
        return new Vector2(gridX, gridY);
    }

    public void setGridCoords(Vector2 coords)
    {
        gridX = coords.x;
        gridY = coords.y;
    }
}
