using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InstFurnitureScript : NetworkBehaviour
{
    [Header("Settings")]
    [SerializeField] private SetFurnitureScript furniture;
    [SerializeField] private GameObject objFurniture = null;

    /*
    private void OnEnable()
    {
        furniture.EventAddFurniture += Furniture_EventAddFurniture;
    }

    private void OnDisable()
    {
        furniture.EventAddFurniture -= Furniture_EventAddFurniture;
    }

    [ClientRpc]
    private void Furniture_EventAddFurniture(GameObject furnitureObj, Vector3 objPlacement, Quaternion objRotation)
    {
        objFurniture = Instantiate(furnitureObj, objPlacement, objRotation);
        objFurniture.tag = "Drag";
        Debug.Log("Created Object on other Clients");
    }
    */
    
}
