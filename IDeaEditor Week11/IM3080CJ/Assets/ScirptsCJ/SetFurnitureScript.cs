using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SetFurnitureScript : NetworkBehaviour
{
    /*Comments/decommissioned features

    //Furniture Preset 
    //private FurniturePreset curFurniturePreset;             //Store Furniture Preset components
    
    //Camera Controllers
    //public Camera m_Camera;                                //Instance of Main Camera
    //public GameObject selectorCam;                          //Points to the SelectorCam_Anchor //Not needed

    //Player Controllers
    //private GameObject[] playerObjectArr;                   //Store player camera controllers to be used

    //public GameObject closeTab;                             //Point to the CloseTab button


    /*Update is called once per frame
    void Update()
    {
        //Capture esc key sequence
        if(Input.GetKeyDown(KeyCode.Escape))
            CancelFurniturePlacement();

        //If we are placing a furniture down, make it follow the cursor via the placement indicator object
        if(Time.time - lastUpdateTime > placementIndicatorUpdateRate && currentlyPlacing)
        {
            //closeTab.SetActive(false);
            lastUpdateTime = Time.time;
            curPlacementPos = Selector.inst.GetCurTilePosition();
            placementIndicator.transform.position = curPlacementPos;
        }

        //When user press left mouse button – place it down.
        if(currentlyPlacing && Input.GetMouseButtonDown(0))
        {
            PlaceFurniture3D();
        }
    }

    
        //Disbale all players cameras 
        if(GameObject.FindGameObjectsWithTag("Player").Length == 0)             //Find all the player camera controllers (Player camera controller must be tag with Player)
        {
            Debug.Log("nothing");
            return;
            
        }
        else 
        {   
            //Disable all player camera controllers
            playerObjectArr = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject playerObj in playerObjectArr)
            {
                playerObj.SetActive(false);
                Debug.Log("Setting false");
            }
        }

    //Uncomment this when Camera Controller is in project (Gideon Part)
    //m_Camera = Camera.main;     //Store an instance of Main Camera
    //m_Camera.enabled = false;
    //selectorCam.SetActive(true);    //Activate Selector Camera used to find grid placement
    //

    //closeTab.SetActive(true);
    //m_Camera.enabled = true;
    //selectorCam.SetActive(false);

    /*
    //Enable all players cameras
    if (playerObjectArr == null)
    {
        return;
    }
    else 
    {
        foreach (GameObject playerObj in playerObjectArr)
        {
            playerObj.SetActive(true);
        }
    }*/

    [Header("Settings")]
    [SerializeField] private bool currentlyPlacing;                            //Status of placing

    [SerializeField] private float lastUpdateTime;
    [SerializeField] private float placementIndicatorUpdateRate = 0.05f;
    [SerializeField] public GameObject placementIndicator;                   //Point to placement indicator object
    [SerializeField] private GameObject furnitureObj;                         //Store selected furniture object

    [Header("References")]
    [SerializeField] public static SetFurnitureScript inst;                     //Instantiate FurniturePlacer
    [SerializeField]private UIBedGrey stateChanger;

    [SerializeField] private GameObject curFurniture = null;
    [SerializeField] private GameObject curPosition = null;
    [SerializeField] private Vector3 curPlacementPos;                         //Store an instance of current placement position

    #region SyncVar
    [SyncVar]
    //private int maxHealth = 100;
    [SerializeField] private GameObject servFurniture;
    [SerializeField] private GameObject servPosition;
    [SerializeField] private Vector3 servPlacementPos;                         //Store an instance of current placement position

    public delegate void AddNewFurnitureDelegate(GameObject furnitureObj, Vector3 objPlacement, Quaternion objRotation);

    public event AddNewFurnitureDelegate EventAddFurniture;

    #endregion

    #region Server
    [Server]
    /*private void SetFurnitureObject(GameObject furnitureObj, Vector3 objPlacement, GameObject defaultPos)
    {
        servFurniture = furnitureObj;
        servPlacementPos = objPlacement;
        servPosition = defaultPos;
        Debug.Log("Variables initialized");
        EventAddFurniture?.Invoke(servFurniture, servPlacementPos, servPosition.transform.rotation);
    }*/


    [Command]
    public void CmdFurniturePlaced()
    {
        GameObject serFurn = Instantiate(curFurniture, curPlacementPos, curPosition.transform.rotation);
        serFurn.tag = "Drag";
        //SetFurnitureObject(curFurniture, curPlacementPos, curPosition);
        Debug.Log("Command to server okay");
        NetworkServer.Spawn(serFurn);
        RpcCreateObject();
        //CancelFurniturePlacement();
    }

    [ClientRpc]
    private void RpcCreateObject()
    {
        Debug.Log("Creating object");
    }

    #endregion

    #region Client

    /*Instiate FurniturePlacer. Start at Program Start*/
    void Awake()
    {
        inst = this;
    }

    private void Update()
    {
        if (!hasAuthority) { return; }

        //Capture esc key sequence
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelFurniturePlacement();

        //If we are placing a furniture down, make it follow the cursor via the placement indicator object
        if (Time.time - lastUpdateTime > placementIndicatorUpdateRate && currentlyPlacing)
        { 
            lastUpdateTime = Time.time;
            curPlacementPos = Selector.inst.GetCurTilePosition();
            placementIndicator.transform.position = curPlacementPos;
            Debug.Log("Finding position");
        }

        //When user press left mouse button – place it down.
        if (currentlyPlacing && Input.GetMouseButtonDown(0))
        {
            PlaceFurniture3D();

            CmdFurniturePlaced();
            Debug.Log("Sent command to server");
            //CancelFurniturePlacement();
        }
    }

    /*Function called when clicking on the UI*/
    public void BeginNewFurniturePlacement(GameObject furniturePreset, GameObject position)
    {
        //Activate Placement Indicator Controls
        currentlyPlacing = true;
        Debug.Log("currentlyPlacing = true");
        curFurniture = furniturePreset;       //Store an instance of the Furniture Preset 
        curPosition = position;
        placementIndicator.SetActive(true);         //Activate PlacementIndicator Object
        Debug.Log("Begin placement");
    }

    /*Cancel Furniture Placement when escape is pressed*/
    public void CancelFurniturePlacement()
    {
        GameObject grab = GameObject.Find("BedGrey");
        stateChanger = grab.GetComponent<UIBedGrey>();
        currentlyPlacing = false;
        Debug.Log("currentlyPlacing = false");
        placementIndicator.SetActive(false);

        stateChanger.FinishPlacement();
        Debug.Log("Cancel placement");
    }

    //Called when user clicks on grid to place furniture down
    void PlaceFurniture3D()
    {
        //Instantiate furniture preset prefab, current position and preset rotation
        furnitureObj = Instantiate(curFurniture, curPlacementPos, curPosition.transform.rotation);
        furnitureObj.tag = "Drag";
        CancelFurniturePlacement();
    }

    #endregion
}
