using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LeftButton : MonoBehaviour
{
    // Start is called before the first frame update
    private bool expanded;
    public string state;
    public GameObject leftPanel;
    Button leftButton;
    public Sprite hideSprite;
    public Sprite displaySprite;

    private Vector3 startposition = new Vector3(-1316, 0, 0);
    private Vector3 endposition = new Vector3(-606, 0, 0);
    void Start()
    {
        expanded = false;
       
        leftButton = GetComponent<Button>();
        leftButton.onClick.AddListener(TaskOnClick);

    }
    void TaskOnClick()
    {
        expanded = !expanded;
        
        if (expanded)
        {
            leftPanel.transform.DOMove(endposition,0.3f).SetEase(Ease.InOutBack);
            Debug.Log(leftPanel.transform.position);

            
        }
        else 
        {
            leftPanel.transform.DOMove(startposition, 0.3f).SetEase(Ease.InOutBack);
            Debug.Log(leftPanel.transform.position);

        }

    }

}
