using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMasterClass : MonoBehaviour
{
    public virtual void OnSelect()
    {
        Debug.Log("Tile Master Class " + this.gameObject.transform.position);
    }
}
