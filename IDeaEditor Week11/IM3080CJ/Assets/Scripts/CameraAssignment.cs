using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTG;

public class CameraAssignment : MonoBehaviour
{
    private Camera playerCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCam = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Camera>();
        transform.GetComponent<RTFocusCamera>().setFocusCamera(playerCam);
    }
}
