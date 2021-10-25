using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SideToggleMenu : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float expandDuration;
    [SerializeField] private float collapseDuration;
    [SerializeField] private Ease expandEase;
    [SerializeField] private Ease collapseEase;

    [Header("Rotation")]
    [SerializeField] private float rotationDuration;
    [SerializeField] private Ease rotationEase;


    [SerializeField] private Vector3 rotstart; 
    [SerializeField]private Vector3 rotend; 

    [SerializeField]private Vector3 EndPosition;
    [SerializeField]private Vector3 StartPosition;
    private Button toggle;
    private GameObject container;
    private GameObject tri;
    private bool isexpanded = false;
    // Start is called before the first frame update
    void Start()
    {
        toggle = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        toggle.onClick.AddListener(togglemenu);

        tri = toggle.transform.GetChild(0).gameObject;

        container = transform.GetChild(0).gameObject;
    }

    private void togglemenu()
    {
        isexpanded = !isexpanded;
        
        if(isexpanded)
        {
            container.transform.DOMove(EndPosition, expandDuration).From(StartPosition).SetEase(expandEase);
            tri.transform.DORotate(rotend, rotationDuration).From(rotstart).SetEase(rotationEase);
        }
        else
        {
            container.transform.DOMove(StartPosition, collapseDuration).From(EndPosition).SetEase(collapseEase);
            tri.transform.DORotate(rotstart, rotationDuration).From(rotend).SetEase(rotationEase);
        }
    }

}
