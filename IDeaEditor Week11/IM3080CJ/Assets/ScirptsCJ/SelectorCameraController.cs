using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;

public class SelectorCameraController : MonoBehaviour
{
	/*Variables Declaration*/ 
	public float moveSpeed;  

	//Set Rotation value 
	public float minXRot; 
	public float maxXRot;

	private float curXRot; //Get current X rotation

	//Set Zoom value
	public float minZoom;
	public float maxZoom;

	//Set Speed value
	public float zoomSpeed;
	public float rotateSpeed;

	private float curZoom;

	public Camera cam3D;
    
    // Start is called before the first frame update
    void Start()
    {
   			curZoom = cam3D.transform.localPosition.y;
   			curXRot = -50;  
    }

    // Update is called once per frame
    void Update()
    {
    	userMovement3D();
    }

	public void userMovement3D ()
	{
		//if (isLocalPlayer)
		//{
			//Zooming in and out with the scroll wheel
			curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
			curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);
			cam3D.transform.localPosition = Vector3.up * curZoom;

			//If hold down the right mouse, rotate the camera around
			if(Input.GetMouseButton(1))
			{
				float x = Input.GetAxis("Mouse X");
				float y = Input.GetAxis("Mouse Y");
				curXRot += -y * rotateSpeed;
				curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);
				transform.eulerAngles = new Vector3(curXRot, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);
			}

			//Allow user to move forwards, back, left and right relative to where user is facing
			Vector3 forward = cam3D.transform.forward;
			forward.y = 0.0f;
			forward.Normalize(); //Normalize vector keeps the same direction
			Vector3 right = cam3D.transform.right.normalized;
			float moveX = Input.GetAxisRaw("Horizontal");
			float moveZ = Input.GetAxisRaw("Vertical");
			Vector3 dir = forward * moveZ + right * moveX;
			dir.Normalize();
			dir *= moveSpeed * Time.deltaTime;
			transform.position += dir;
		//}
		
	}

}
