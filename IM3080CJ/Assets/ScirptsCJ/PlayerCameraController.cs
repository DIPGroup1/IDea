using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);        //Limits of Cinemachine Camera
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);       //Values of Camera X& Y velocity
    [SerializeField] private Transform playerTransform = null;                      //Camera Rotation
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;         //Cinemachine virtual reference

    private CinemachineTransposer transposer;                                       //Cinemachine component

    private Controls controls;                                                      //Reference to Control asset (Inout Manager Assest)
        
    private Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    /*Called on the object that has authority over attached game object*/
    public override void OnStartAuthority()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        virtualCamera.gameObject.SetActive(true);   

        enabled = true;     //enable this component unless its Local Player     

        //When Look performed on Input Manager call method "Look" and read value Vector2
        Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());      
    }

    /*ClietnCallback to run on client only. Enable and Disable Client Control when needed*/
    [ClientCallback]
    private void OnEnable() => Controls.Enable();
        
    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    /*Logic for Camera Movement*/
    private void Look(Vector2 lookAxis)
    {
        //delatTime stored as usage in 2 places
        float deltaTime = Time.deltaTime;

        /*Explanation of usages*/
        //transposer.mFollowOffset.y -> Sets how far away Camera is from Player 
        //Mathf.Clamp -> Camera cannot go beyong maxFollowOffset.x and maxFollowOffset.y
        transposer.m_FollowOffset.y = Mathf.Clamp(
            transposer.m_FollowOffset.y - (lookAxis.y * cameraVelocity.y * deltaTime),  //Calculation based on movement of mouse
            maxFollowOffset.x,
            maxFollowOffset.y);

        //X-axis rotates player
        playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);
    }
}
