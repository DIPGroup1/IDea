using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatControlManager : MonoBehaviour
{
    private Camera cam;
    private Vector3 delta;

    [SerializeField]
    private  GameObject floatUI;
    private GameObject container;
    private void Start()
    {
        cam = Camera.main;
        container = floatUI.transform.parent.gameObject;
    }

    private Vector3 getMousePos()
    {
        Vector3 worldmousePos = Input.mousePosition;
        worldmousePos.z = 0;
        return worldmousePos;
    }
    public void getDelta()
    {
        delta = container.transform.position - getMousePos();
    }
    public void DragFloat()
    {
        container.transform.position = getMousePos() + delta;
    }
}
