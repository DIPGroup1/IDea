using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using RTG;

public class ModelManager : MonoBehaviour
{   
    [Header("Camera")]
    [SerializeField] private Camera cam;
    public GameObject selectedModel;
    [SerializeField] private GameObject previousModel;

    private readonly Timer _MouseSingleClickTimer = new Timer();
    private GameObject floatControl;

    private ObjectTransformGizmo MoveGizmo;
    private ObjectTransformGizmo RotateGizmo;
    private ObjectTransformGizmo ScaleGizmo;
    private ObjectTransformGizmo UniversalGizmo;

    //[SerializeField] private GameObject placeCamAnchor;

    [SerializeField]
    private GameObject floatingModelControlUISlot;
    
    // Start is called before the first frame update
    void Start()
    {
        MoveGizmo = RTGizmosEngine.Get.CreateObjectMoveGizmo();
        RotateGizmo = RTGizmosEngine.Get.CreateObjectRotationGizmo();
        ScaleGizmo = RTGizmosEngine.Get.CreateObjectScaleGizmo();
        UniversalGizmo = RTGizmosEngine.Get.CreateObjectUniversalGizmo();

        MoveGizmo.Gizmo.SetEnabled(false);
        RotateGizmo.Gizmo.SetEnabled(false);
        ScaleGizmo.Gizmo.SetEnabled(false);
        UniversalGizmo.Gizmo.SetEnabled(false);

        _MouseSingleClickTimer.Interval = 400;
        _MouseSingleClickTimer.Elapsed += SingleClick;
        //placeCamAnchor.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //placeCamAnchor.SetActive(true);
            if(selectedModel==null)
            {
                grab();
            }
        }


        if (selectedModel!=null)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if(_MouseSingleClickTimer.Enabled ==false)
                {
                    _MouseSingleClickTimer.Start();
                    return;
                }
                else
                {
                    _MouseSingleClickTimer.Stop();
                    System.Diagnostics.Debug.WriteLine("Double Click");
                    drop();
                }
            }
  
        }

    }
    

    public void initializeModel(GameObject obj)
    {
        selectedModel = obj;
        selectedModel.gameObject.tag = "Drag";
    }

    public void deleteModel()
    {
        UniversalGizmo.Gizmo.SetEnabled(false);
        ScaleGizmo.Gizmo.SetEnabled(false);
        Destroy(selectedModel);
        Destroy(floatControl);
    }

    public GameObject getSelectedModel()
    {
        return selectedModel;
    }

    private Vector3 getSelectedModelScreenCoordinate()
    {
        Vector3 screenlocation = cam.WorldToScreenPoint(selectedModel.transform.position);
        screenlocation.z = 0;
        return screenlocation;
    }

    private void callModelControlUI()
    {
        //placeCamAnchor.SetActive(true);

        Vector3 instentiatelocation = getSelectedModelScreenCoordinate() + new Vector3(100, 50, 0);
        floatControl = Instantiate(floatingModelControlUISlot);
        floatControl.name = "Float Control";
        GameObject container = floatControl.transform.GetChild(1).gameObject;
        container.transform.position = instentiatelocation;

        Button delete = container.transform.Find("Delete").gameObject.GetComponent<Button>();
        delete.onClick.AddListener(delegate { deleteModel(); });

        Button Scale = container.transform.Find("Move Dropdown").Find("Scale").gameObject.GetComponent<Button>();
        Scale.onClick.AddListener(delegate { 
            UniversalGizmo.Gizmo.SetEnabled(false);
            ScaleGizmo.Gizmo.SetEnabled(true);
            ScaleGizmo.SetTargetObject(selectedModel);
        });

        Button move = container.transform.Find("Move Dropdown").Find("Move").gameObject.GetComponent<Button>();
        move.onClick.AddListener(delegate {
            ScaleGizmo.Gizmo.SetEnabled(false);
            UniversalGizmo.Gizmo.SetEnabled(true);
            UniversalGizmo.SetTargetObject(selectedModel);
        });

    }
    private void grab()
    {
        //if (previousModel != null)
        //{
        //    placeCamAnchor.SetActive(true);
        //}

        //if (selectedModel == null)
        //{
            RaycastHit hit = CastRay();
        
            if (hit.collider != null)
            {
                if (!hit.collider.CompareTag("Drag"))
                {
                    return;
                }

                selectedModel = hit.collider.gameObject;
                //previousModel = selectedModel;
                //selectedModel.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.3f);

                callModelControlUI();
                UniversalGizmo.SetTargetObject(selectedModel);
                UniversalGizmo.Gizmo.SetEnabled(true);

            }

        //}
    }

    private void drop()
    {
        //selectedModel.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        selectedModel = null;
        Destroy(floatControl);

        UniversalGizmo.Gizmo.SetEnabled(false);
        ScaleGizmo.Gizmo.SetEnabled(false);
        //placeCamAnchor.SetActive(false);
    }


    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            cam.farClipPlane);

        Vector3 screenMousePosNear = new Vector3(
           Input.mousePosition.x,
           Input.mousePosition.y,
           cam.nearClipPlane);

        Vector3 worldMousePosFar = cam.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = cam.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }

    void SingleClick(object o,System.EventArgs e)
    {
        _MouseSingleClickTimer.Stop();

        System.Diagnostics.Debug.WriteLine("Single Click");
    }
}

