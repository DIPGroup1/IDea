using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    /*Variables as well as Singleton*/
    public Camera cam;                          //Camera used to identify position
    public static Selector inst;                //Used to instiate script 
    //public GameObject selectorCamAnchor;

    /*Instantiate Script at Program Start*/
    void Awake()
    {
        inst = this;
        cam = Camera.main;
    }

    /*Return Position of tile user is hovering over*/
    public Vector3 GetCurTilePosition ()
    {
        //Checks if pointer is over a gameobject
        if(EventSystem.current.IsPointerOverGameObject())
            return new Vector3(0, -99, 0);
    
        //Get position from Ray Cast of Camera
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float rayOut = 0.0f;

        //If there is a RayCast Captured, take position of the tile mouse is over
        if(plane.Raycast(cam.ScreenPointToRay(Input.mousePosition), out rayOut))
        {
            Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(0.5f, 0.0f, 0.5f);
            return new Vector3(Mathf.CeilToInt(newPos.x), 0, Mathf.CeilToInt(newPos.z));
        }
        return new Vector3(0, -99, 0);
    }
}
