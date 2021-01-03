using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTile : TileMasterClass
{

    public override void OnSelect()
    {
        Debug.Log("Tile Test Class " + this.gameObject.transform.position);
    }

}
