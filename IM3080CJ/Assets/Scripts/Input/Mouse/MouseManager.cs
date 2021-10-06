using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : InputManager
{
    Vector2Int screen;
    float mousePositionOnRotateStart;

    //EVENTS
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    private void Awake()
    {
        screen = new Vector2Int(Screen.width, Screen.height);
    }

    private void Update()
    {
        Vector3 mp = Input.mousePosition;
        

        //movement


        //rotation
        if (Input.GetMouseButtonDown(1))
        {
            mousePositionOnRotateStart = mp.x;
        }
        else if (Input.GetMouseButton(1))
        {
            if(mp.x<mousePositionOnRotateStart)
            {
                OnRotateInput?.Invoke(-1f);
            }
            else if(mp.x>mousePositionOnRotateStart)
            {
                OnRotateInput?.Invoke(1f);
            }
        }

        //zoom
        if(Input.mouseScrollDelta.y > 0)
        {
            OnZoomInput?.Invoke(-3f);
        }
        else if (Input.mouseScrollDelta.y<0)
        {
            OnZoomInput?.Invoke(3f);
        }
    }

    
}
