using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingMenu : MonoBehaviour
{
    private SettingItems[] menuItems;
    private Button mainButton;
    private int itemcount;
    private Vector3 mainButtonTrans;
    private bool isexpanded = false;

    [Space]
    [Header("Animation")]
    [SerializeField] private float expandDuration;
    [SerializeField] private float collapseDuration;
    [SerializeField] private Ease expandEase;
    [SerializeField] private Ease collapseEase;

    [Header("Fade")]
    [SerializeField] private float fadeDuration;
    [Header("Offset")]
    [SerializeField] private Vector3 offset;

    



    private void Start()
    {
        itemcount = transform.childCount - 1;
        menuItems = new SettingItems[itemcount];

        for (int i = 0; i < itemcount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingItems>();
        }

        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.transform.SetAsLastSibling();
        

        mainButtonTrans = mainButton.transform.position;
        mainButton.onClick.AddListener(Toggle);

        resetPotitions();
    }

    private void Update()
    {
        mainButtonTrans = mainButton.transform.position;
    }

    private void resetPotitions()
    {
        for (int i = 0; i < itemcount; i++)
        {
            menuItems[i].transform.position = mainButtonTrans;
            menuItems[i].GetComponent<Image>().color = new Color(1,1,1,0);
        }
    }

    void Toggle()
    {
        isexpanded = !isexpanded;

        if(isexpanded)
        {
            for (int i = 0; i < itemcount; i++)
            {
                menuItems[i].transform.DOMove(mainButtonTrans + offset * (i + 1), expandDuration).SetEase(expandEase);
                menuItems[i].GetComponent<Image>().DOFade(1f, fadeDuration).From(0f);

            }
        }
        else
        {
            for (int i = 0; i < itemcount; i++)
            {
                //menuItems[i].transform.position = mainButtonTrans + offset * (i + 1);
                menuItems[i].transform.DOMove(mainButtonTrans, collapseDuration).SetEase(collapseEase);
                menuItems[i].GetComponent<Image>().DOFade(0f, fadeDuration);

            }
        }
    }
}
