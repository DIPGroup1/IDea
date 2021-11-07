using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Createmodel : MonoBehaviour
{
    private GameObject modelInstance;
    private Camera cam;

    [SerializeField]


    public void InstantiateCall(GameObject obj)
    {
        string name = obj.name;
        modelInstance = Instantiate(obj,new Vector3(0,10,0),Quaternion.identity);
        int id = Random.Range(0, 50);
        modelInstance.name = name + id;
        modelInstance.gameObject.tag = "Drag";
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);

        Vector3 screenMousePosNear = new Vector3(
           Input.mousePosition.x,
           Input.mousePosition.y,
           Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }


}
