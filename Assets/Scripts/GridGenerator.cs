using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public static GridGenerator me;

    TileMasterClass[,] gridOfTiles;
    public GameObject prefabTile;

    public Vector2 gridDimensions;
    // Start is called before the first frame update

    private void Awake()
    {
        me = this;
        gridOfTiles = new TileMasterClass[(int)gridDimensions.x, (int)gridDimensions.y];
    }

    void Start()
    {
        generateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateGrid()
    {
        for (int x = 0; x < gridDimensions.x; x++){
            for(int y = 0; y < gridDimensions.y; y++){
                Vector3 posToCreateTile = new Vector3(x, y, 0);
                GameObject mostRecentTile = (GameObject)Instantiate(prefabTile, posToCreateTile, Quaternion.Euler(0, 0, 0));
                mostRecentTile.GetComponent<TileMasterClass>().setGridCoords(new Vector2(x, y));
                mostRecentTile.transform.parent = this.gameObject.transform;
                mostRecentTile.name = "Tile" + mostRecentTile.GetComponent<TileMasterClass>().getGridCoords().ToString();
                gridOfTiles[x, y] = mostRecentTile.GetComponent<TileMasterClass>();
            }    
        }
    }

    public List<GameObject> getTiles(Vector2 startPos,Vector2 endPos)
    {
        Debug.Log("Getting Tiles");
        int lowestX, lowestY, highestX, highestY;
        List<GameObject> retVal = new List<GameObject>();
        if (startPos.x <= endPos.x)
        {
            lowestX = (int)startPos.x;
            highestX = (int)endPos.x;
        }
        else
        {
            lowestX = (int)endPos.x;
            highestX = (int)startPos.y;
        }

        if(startPos.y <= endPos.y)
        {
            lowestY = (int)startPos.y;
            highestY = (int)endPos.x;
        }
        else
        {
            lowestY = (int)endPos.y;
            highestY = (int)startPos.y;
        }

        for(int x = (int)lowestX; x <= (int)highestX; x++)
        {
            for(int y = (int)lowestY; y<=(int)highestY; y++)
            {
                retVal.Add(gridOfTiles[x, y].gameObject);
            }
        }
        Debug.Log(retVal.Count);
        return retVal;
    }
}
