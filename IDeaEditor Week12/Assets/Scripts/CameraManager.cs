using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Positioning")]
    public Vector2 cameraOffset = new Vector2(10, 14);
    public float lookAtOffset = 2f;

    [Header("Move Controls")]
    public float inOutSpeed = 5;
    public float lateralSpeed = 5f;
    public float rotateSpeed = 45f;

    [Header("Move Bounds")]
    public Vector2 minBounds, maxBounds;

    [Header("ZoomControls")]
    public float zoomSpeed = 4f;
    public float nearZoomLimit = 2f;
    public float farZoomLimit = 16f;
    public float startingZoom = 5f;

    ZoomStrategy zoomStrategy;
    Vector3 frameMove;
    float frameRotate;
    float frameZoom;
    Camera cam;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        cam.transform.localPosition = new Vector3(0f, Mathf.Abs(cameraOffset.y),-Mathf.Abs(cameraOffset.x));
        zoomStrategy = cam.orthographic ? (ZoomStrategy)new OrthographicZoomStrategy(cam, startingZoom) : new PerspectiveZoomStrategy(cam, cameraOffset, startingZoom);
        //zoomStrategy = new OrthographicZoomStrategy(cam, startingZoom);
        cam.transform.LookAt(transform.position + Vector3.up * lookAtOffset);

    }

    private void OnEnable()
    {
        KeyboardManager.OnMoveInput += UpdateFrameMove;
        KeyboardManager.OnRotateInput += UpdateFrameRotate;
        KeyboardManager.OnZoomInput += UpdateFramZoom;
        MouseManager.OnRotateInput += UpdateFrameRotate;
        MouseManager.OnZoomInput += UpdateFramZoom;
    }

    private void OnDisable()
    {
        KeyboardManager.OnMoveInput -= UpdateFrameMove;
        KeyboardManager.OnRotateInput -= UpdateFrameRotate;
        KeyboardManager.OnZoomInput -= UpdateFramZoom;
        MouseManager.OnRotateInput -= UpdateFrameRotate;
        MouseManager.OnZoomInput -= UpdateFramZoom;
    }

    private void UpdateFrameMove(Vector3 moveVector)
    {
        frameMove += moveVector;
    }

    private void UpdateFrameRotate(float rotateAmount)
    {
        frameRotate += rotateAmount;
    }
    private void UpdateFramZoom(float zoomAmount)
    {
        frameZoom += zoomAmount;
    }

    private void LateUpdate()
    {
        if (frameMove != Vector3.zero)
        {
            Vector3 speedModFrameMove = new Vector3(frameMove.x * lateralSpeed, frameMove.y, frameMove.z * inOutSpeed);
            transform.position += transform.TransformDirection(speedModFrameMove) * Time.deltaTime;
            LockPositionInBounds();
            frameMove = Vector3.zero;
        }

        if(frameRotate != 0f)
        {
            transform.Rotate(Vector3.up, frameRotate * Time.deltaTime * rotateSpeed);
            frameRotate = 0f;
        }

        if(frameZoom < 0f)
        {
            zoomStrategy.Zoomin(cam, Time.deltaTime * Mathf.Abs(frameZoom) * zoomSpeed, nearZoomLimit);
            frameZoom = 0;
        }
        else if(frameZoom >0f)
        {
            zoomStrategy.Zoomout(cam, Time.deltaTime * frameZoom * zoomSpeed, farZoomLimit);
            frameZoom = 0;
        }

    }

    private void LockPositionInBounds()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.y),
            transform.position.y,
            Mathf.Clamp(transform.position.z,minBounds.y,maxBounds.y)
            );
    }
}
