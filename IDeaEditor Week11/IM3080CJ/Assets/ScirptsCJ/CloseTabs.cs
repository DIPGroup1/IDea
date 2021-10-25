using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTabs : MonoBehaviour
{
    public LeftButton leftPanel;
    public topbar topBar;

    void Start()
    {
        Button closeTab = GetComponent<Button>();
        closeTab.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        topBar.setInactive();
        Debug.Log("Close Panels");
    }
    
}
