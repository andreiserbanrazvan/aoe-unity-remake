using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager me;

    [SerializeField]
    GameObject currentlySelected;

    [SerializeField]
    TileMasterClass[] objectsWithSuperClass;

    private void Awake()
    {
        findAllSuperClassExamples();
    }

    // Update is called once per frame
    void Update()
    {
        checkForLeftMouseClick();
    }

    public void setSelected(GameObject toSet)
    {
        currentlySelected = toSet;
        currentlySelected.GetComponent<TileMasterClass>().OnSelect();
    }

    public GameObject getSelected()
    {
        return currentlySelected;
    }

    void clearSelected()
    {
        currentlySelected = null;
    }

    void checkForLeftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicking, looking for raycast hits");
            selectionRaycast();
        }
    }

    void selectionRaycast()
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

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

    void findAllSuperClassExamples()
    {
        objectsWithSuperClass = FindObjectsOfType<TileMasterClass>();
    }

}
