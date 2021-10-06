using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string state;
    public GameObject leftPanel;
    Button leftButton;
    public Sprite hideSprite;
    public Sprite displaySprite;
    void Start()
    {
        leftButton = GetComponent<Button>();
        leftButton.onClick.AddListener(TaskOnClick);
        state = "hide";

    }
    void TaskOnClick()
    {
        
        if (state == "hide")
        {
            leftPanel.transform.localPosition = new Vector3(-606, 0, 0);
            Debug.Log(leftPanel.transform.position);
            Debug.Log(state);
            state = "display";
            GetComponent<Image>().sprite = displaySprite;
        }
        else 
        {
            leftPanel.transform.localPosition = new Vector3(-1323, 0, 0);
            Debug.Log(leftPanel.transform.position);
            Debug.Log(state);
            state = "hide";
            GetComponent<Image>().sprite = hideSprite;

        }

    }

    /*When function called, hide side panel. For when click outside panel*/
    public void HidePanel()
    {
        leftPanel.transform.localPosition = new Vector3(-1311, 0, 0);
        Debug.Log(leftPanel.transform.position);
        Debug.Log(state);
        state = "hide";
        GetComponent<Image>().sprite = hideSprite;
    }
}
