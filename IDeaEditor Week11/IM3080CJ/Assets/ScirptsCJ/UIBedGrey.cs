using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBedGrey : MonoBehaviour
{
    public string state;
    public GameObject bed;
    public GameObject position;
    public Sprite hideSprite;
    public Sprite displaySprite;

    private SetFurnitureScript fpInst = null;          //For assesing the methods of FurniturePlacer

    void Start()
    {
        //respawn = GameObject.FindWithTag("BedGrey");
        Button bedButton = transform.GetComponent<Button>();
        bedButton.onClick.AddListener(TaskOnClick);
        state = "hide";

    }
    void TaskOnClick()
    {
        GameObject grab = GameObject.FindGameObjectWithTag("Player");
        fpInst = grab.GetComponent<SetFurnitureScript>();
        Debug.Log("You have create a bed");
        fpInst.BeginNewFurniturePlacement(bed, position); //Send furniture prefab and position object to Function
            //Instantiate(bed, position.transform.position, position.transform.rotation); // can edit with Shen An
        state = "display";
        GetComponent<Image>().sprite = displaySprite;
        /*
        else
        {
            
            state = "hide";
            GetComponent<Image>().sprite = hideSprite;

        }*/
        Debug.Log("You have clicked the button!");
        //Instantiate(bed, position.transform.position, position.transform.rotation);

    }

    /*Function call after finish successful placement*/
    public void FinishPlacement()
    {
        state = "hide";
        GetComponent<Image>().sprite = hideSprite;
        Debug.Log("State: hide");
    }
}
