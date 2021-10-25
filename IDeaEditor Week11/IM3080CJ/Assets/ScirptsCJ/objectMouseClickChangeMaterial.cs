using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMouseClickChangeMaterial : MonoBehaviour
{
    public GameObject camObject;
    const string TAG = "topbar";
    topbar _topbar;
    public string state;

    //Player Controllers
    private GameObject[] playerObjectArr;                   //Store player camera controllers to be used

    // Start is called before the first frame update
    void Start()
    {
        _topbar = GameObject.FindWithTag(TAG).GetComponent<topbar>(); //Brandon: must reserve
        state = "hide";

    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        if (state == "hide")
        {
            state = "display";
            _topbar.caller();//Brandon: must reserve

            Debug.Log("topbar display");
        }
        else
        {

            state = "hide";
            _topbar.setInactive();//Brandon: must reserve
            Debug.Log("topbar fold");
        }
        
    }

    
}
