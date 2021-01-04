using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager me;

    [SerializeField]
    List<GameObject> currentlySelected;

    bool drawMultiSelectBox = false;

    private void Awake()
    {
        currentlySelected = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        checkForLeftMouseClick();
    }

    public void setSelected(GameObject toSet)
    {
        clearSelected();
        currentlySelected.Add(toSet);
        toSet.GetComponent<TileMasterClass>().OnSelect();
    }

    public void setSelected(List<GameObject> toSet,bool clearExisting)
    {
        if(clearExisting == true)
        {
            clearSelected();
        }
       
        currentlySelected = toSet;
        foreach( GameObject obj in getSelected())
        {
            obj.GetComponent<TileMasterClass>().OnSelect();
        }
    }

    public List<GameObject> getSelected()
    {
        return currentlySelected;
    }

    void clearSelected()
    {
        firstTileSelected = null;
        lastTileSelected = null;

        foreach(GameObject obj in getSelected())
        {
            obj.GetComponent<TileMasterClass>().OnDeselect();
        }
        currentlySelected = new List<GameObject>();
    }

    GameObject firstTileSelected = null;
    GameObject lastTileSelected = null;

    void checkForLeftMouseClick()
    {
        if (Input.GetKey(KeyCode.LeftControl) == true)
        {
            Debug.Log("leftCtrl");
            if (Input.GetMouseButtonDown(0) == true)
            {
                Debug.Log("Multi select");
                if (firstTileSelected == null)
                {
                    Debug.Log("Selecting first tile");
                    selectionRaycast(ref firstTileSelected);
                }
            }
            else if (firstTileSelected != null && lastTileSelected != null)
            {
                Vector2 startCoords = firstTileSelected.GetComponent<TileMasterClass>().getGridCoords();
                Vector2 endCoords = lastTileSelected.GetComponent<TileMasterClass>().getGridCoords();
                Debug.Log("Start " + startCoords);
                Debug.Log("End " + endCoords);
                if (firstTileSelected != null && lastTileSelected != null)
                {
                    List<GameObject> selectedTiles = GridGenerator.me.getTiles(startCoords, endCoords);
                    setSelected(selectedTiles, true);
                    firstTileSelected = null;
                    lastTileSelected = null;
                }
            }


            if (Input.GetMouseButton(0) == false && firstTileSelected != null)
            {
                Debug.Log("Selecting second tile");
                drawMultiSelectBox = true;

                selectionRaycast(ref lastTileSelected);
            }
            else
            {
                drawMultiSelectBox = false;
            }

        }else if(Input.GetMouseButton(0) == true)
        {
            firstTileSelected = null;
            lastTileSelected = null;
            Debug.Log("Clicking looking for raycast hits");
            selectionRaycast();
        }
        else
        {
            drawMultiSelectBox = false;
        }
    }

    bool areWeSelecting()
    {
        if(Input.GetMouseButtonDown (0) && Input.GetKey(KeyCode.LeftControl))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void selectionRaycast()
    {
        var coord_x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        var coord_y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        Vector2 mousePos = new Vector2(coord_x, coord_y);

        RaycastHit2D raycast = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        try
        {
            GameObject hitObject = raycast.collider.gameObject;

            Debug.Log(hitObject.name);

            setSelected(hitObject);
        }
        catch
        {
            Debug.Log("No valid object selected");
        }
    }

    void selectionRaycast(ref GameObject objToSet)
    {
        var coord_x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        var coord_y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        Vector2 mousePos = new Vector2(coord_x, coord_y);

        RaycastHit2D raycast = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        try
        {
            GameObject hitObject = raycast.collider.gameObject;

            Debug.Log(hitObject.name);

            objToSet = hitObject;
        }
        catch
        {
            Debug.Log("No valid object selected");
        }
    }

    private void OnGUI()
    {
        if(drawMultiSelectBox == true)
        {
            Vector3 startScreenPos = Camera.main.WorldToScreenPoint(firstTileSelected.transform.position);

            Vector3 endScreenPos = Input.mousePosition;

            float width, height;
            if (startScreenPos.x > endScreenPos.x)
            {
                width = startScreenPos.x - endScreenPos.x;
            }
            else
            {
                width = endScreenPos.x - startScreenPos.x;
            }

            if(startScreenPos.y > endScreenPos.y)
            {
                height = startScreenPos.y - endScreenPos.y;
            }
            else
            {
                height = endScreenPos.y - startScreenPos.y;
            }

            Rect posToDrawBox;

            if(endScreenPos.x> startScreenPos.x)
            {
                if(endScreenPos.y > startScreenPos.y)
                {
                    posToDrawBox = new Rect(startScreenPos.x, Screen.height - endScreenPos.y, width, height);
                }else
                {
                    posToDrawBox = new Rect(startScreenPos.x, Screen.height -startScreenPos.y, width, height);
                }
            }else
            {
                if(endScreenPos.y > startScreenPos.y)
                {
                    posToDrawBox = new Rect(endScreenPos.x, Screen.height - endScreenPos.y, width, height);
                }
                else
                {
                    posToDrawBox = new Rect(endScreenPos.x, Screen.height - startScreenPos.y, width, height);
                }
            }

            GUI.DrawTexture(posToDrawBox, GUIManager.me.getBlackBoxSemiTrans());

        }
    }

}
