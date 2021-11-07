using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITableGrey : MonoBehaviour
{
    public string state;
    public GameObject table;
    public GameObject position;
    public Sprite hideSprite;
    public Sprite displaySprite;

    public SetFurnitureScript fpInst;          //For assesing the methods of FurniturePlacer
    public SceneReference sceneReference;

    void Start()
    {
        //respawn = GameObject.FindWithTag("TableGrey");
        Button tableButton = transform.GetComponent<Button>();
        tableButton.onClick.AddListener(TaskOnClick);
        state = "hide";

    }
    void TaskOnClick()
    {
        if (state == "hide")
        {
            if (fpInst != null)
            {
                Debug.Log("You have create a bed");
                fpInst.BeginNewFurniturePlacement(5); //Send furniture prefab and position object to Function
                Debug.Log("Grabbing Cmd");
                state = "display";
                GetComponent<Image>().sprite = displaySprite;
            }
        }

        Debug.Log("You have clicked the button!");

    }

    /*Function call after finish successful placement*/
    public void FinishPlacement()
    {
        state = "hide";
        GetComponent<Image>().sprite = hideSprite;
        Debug.Log("State: hide");
    }
}