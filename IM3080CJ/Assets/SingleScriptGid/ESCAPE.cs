using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCAPE : MonoBehaviour
{

    public bool isout = true;
    public bool ispause = false;
    public GameObject cameraobject;

/*
	THIS CODE IS TO REMOVE SCRIPT FUNCTIONS FOR BOTH CAMERA AND MOVEMENT. THIS IS TO ALLOW USERS TO MOVE CURSOR WITHOUT GETTING CONFUSED BY THE ROTATION OF CAMERA.
	THIS CODE FIXES THE CAMERA IN PLACE AND PREVENTS CHARACTER(CAPSULE) FROM MOVING. (Escape())
	THIS CODE ALSO HIDES THE CURSOR WHEN THE GAME STARTS FOR VISUAL PURPOSES WHEN MOVING THE CAMERA. (Start())
*/    

    // Start is called before the first frame update
    void Start()
    {
        isout = true;
        /*ispause = false;
        Cursor.lockState = CursorLockMode.Locked;												REMOVE COMMENTS AFTER UI IMPLEMENTED
        Cursor.visible = false;
        */
    }

	//This is to disable camera and player motion when esc is pressed
	public void Escape(){
        isout = !isout;
        /*ispause = !ispause;
        		    Cursor.lockState = ispause? CursorLockMode.None : CursorLockMode.Locked;	REMOVE COMMENTS AFTER UI IMPLEMENTED
        		    Cursor.visible = ispause;*/
		GetComponent<Player2>().enabled = isout;
		cameraobject.GetComponent<Cinemachine.CinemachineFreeLook>().enabled = isout;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel")){Escape();}
    }
}
