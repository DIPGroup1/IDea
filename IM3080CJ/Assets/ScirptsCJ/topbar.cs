using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topbar : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    public void caller ()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void setInactive()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
