using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class avatarAnimScript : MonoBehaviour
{
    public GameObject dialog;
    Animator m_Animator;
    EventTrigger trigger;
    bool hover = false;
    const int dialogTime = 5;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        //trigger = GetComponent<EventTrigger>();
        timer = dialogTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(hover == true)
        {
            m_Animator.SetTrigger("hover");
            m_Animator.ResetTrigger("unhover");
            
        }
        if(hover == false)
        {
            m_Animator.SetTrigger("unhover");
            m_Animator.ResetTrigger("clickToTalk");
            m_Animator.ResetTrigger("hover");
        }

        if (dialog.activeSelf == true)
        {
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                dialog.SetActive(false);
                timer = dialogTime;
            }
        }
    }

    
    public void enterHoverAvatar()
    {
        hover = true;
    }
    public void exithoverAvatar()
    {
        hover = false;
    }
    public void clickAvatar()
    {
        m_Animator.SetTrigger("clickToTalk");
        dialog.SetActive(true);
    }
    
    
    
}
