using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private GameObject selectedObject;

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if(!hit.collider.CompareTag("Drag")) {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;

                }

            } else {

                setObjPos(selectedObject);
                selectedObject = null;
                Cursor.visible = true;
            }

        }

        if(selectedObject!=null)
        {
            setObjPos(selectedObject);

            if(Input.GetMouseButtonDown(1))
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x,
                    selectedObject.transform.rotation.eulerAngles.y + 90,
                    selectedObject.transform.rotation.eulerAngles.z));
            }
        }
        
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

    public void setObject(GameObject obj)
    {
        this.selectedObject = obj;
    }

    private void setObjPos(GameObject obj)
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(obj.transform.position).z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);

        obj.transform.position = new Vector3(worldPos.x, 10, worldPos.z);
    }

}
