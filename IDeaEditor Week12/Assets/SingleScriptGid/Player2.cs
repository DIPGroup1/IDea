using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player2 : MonoBehaviour
{
	public LayerMask whatCanBeClickedOn;

//    public bool ispaused = false;

	private NavMeshAgent myAgent;

/*
	CODE IS STANDARD CONTROLS: CLICK TO MOVE. UTILISES NAVMESH WHICH DETECTS AREAS WHERE THE CHARACTER(CAPSULE) CAN MOVE
	EVERYTHING IN THE Update() SECTION I DON'T UNDERSTAND EXCEPT GetMouseButtonDown LOL
*/

	void Start(){
		myAgent = GetComponent <NavMeshAgent> ();
/*        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
*/
	}
/*
	public void ControllerPause(){
        ispaused = !ispaused;

            Cursor.lockState = ispaused? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = ispaused;
    	
    }
*/
	void Update(){
		if (Input.GetMouseButtonDown (1)) {
			Ray myRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;

			if (Physics.Raycast (myRay, out hitInfo, 100, whatCanBeClickedOn)){
				myAgent.SetDestination (hitInfo.point);
			}

		}
//        if(Input.GetButtonDown("Cancel")){ControllerPause();}
	}


}
