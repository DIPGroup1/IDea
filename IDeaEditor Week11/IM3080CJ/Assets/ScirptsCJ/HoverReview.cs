using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverReview : MonoBehaviour
{
    
    [SerializeField] private Camera cam;
    public GameObject reviewPanel;
    bool state = false;
    Vector3 outsidePos;
    // Start is called before the first frame update
    void Start()
    {
        outsidePos = new Vector3(0, 2000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 getSelectedModelScreenCoordinate()
    {
        Vector3 screenlocation = cam.WorldToScreenPoint(gameObject.transform.position);
        screenlocation.z = 0;
        return screenlocation;
    }
    private void OnMouseOver()
    {
        //Debug.Log("hovermodel");
        reviewPanel.transform.position = getSelectedModelScreenCoordinate() + new Vector3(200, 400, 0);
    }
    private void OnMouseExit()
    {
        reviewPanel.transform.position = outsidePos;
    }
}
