using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FurniturePlacer : NetworkBehaviour
{
    /*Declare Variables*/
    [Header("PlacementIndicator")]
    private bool currentlyPlacing;                            //Status of placing
    private Vector3 curPlacementPos;                         //Store an instance of current placement position
    private float lastUpdateTime;                   
    private float placementIndicatorUpdateRate = 0.05f;
    public GameObject placementIndicator;                   //Point to placement indicator object

    //Furniture Preset 
    //private FurniturePreset curFurniturePreset;             //Store Furniture Preset components
    public static FurniturePlacer inst;                     //Instantiate FurniturePlacer
    private GameObject furnitureObj;                         //Store selected furniture object
    private GameObject curFurniture;
    private GameObject curPosition;

    //Camera Controllers
    //public Camera m_Camera;                                //Instance of Main Camera
    //public GameObject selectorCam;                          //Points to the SelectorCam_Anchor //Not needed

    //Player Controllers
    private GameObject[] playerObjectArr;                   //Store player camera controllers to be used
    
    //public GameObject closeTab;                             //Point to the CloseTab button

    public UIBedGrey stateChanger;

    /*Instiate FurniturePlacer. Start at Program Start*/
    void Awake ()
    {
        inst = this;
    }

    /*Update is called once per frame*/
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

    /*Function called when clicking on the UI*/
    public void BeginNewFurniturePlacement (GameObject furniturePreset, GameObject position)
    {
        //Uncomment this when Camera Controller is in project (Gideon Part)
        //m_Camera = Camera.main;     //Store an instance of Main Camera
        //m_Camera.enabled = false;
        //selectorCam.SetActive(true);    //Activate Selector Camera used to find grid placement
        //*/

        //Activate Placement Indicator Controls
        currentlyPlacing = true;
        curFurniture = furniturePreset;       //Store an instance of the Furniture Preset 
        curPosition = position;
        placementIndicator.SetActive(true);         //Activate PlacementIndicator Object

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
    }

    /*Cancel Furniture Placement when escape is pressed*/
    public void CancelFurniturePlacement ()
    {
        GameObject grab = GameObject.Find("BedGrey");
        stateChanger = grab.GetComponent<UIBedGrey>();
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
        //closeTab.SetActive(true);
        //m_Camera.enabled = true;
        //selectorCam.SetActive(false);

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
        }

        stateChanger.FinishPlacement();
    }

    /*Called when user clicks on grid to place furniture down*/
    void PlaceFurniture3D ()
    {
        //Instantiate furniture preset prefab, current position and preset rotation
        furnitureObj = Instantiate(curFurniture, curPlacementPos, curPosition.transform.rotation);
        furnitureObj.tag = "Drag";
        CancelFurniturePlacement();
    }

    #region Server

    #endregion

    #region Client

    #endregion
}
