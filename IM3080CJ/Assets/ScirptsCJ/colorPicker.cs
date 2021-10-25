using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorPicker : MonoBehaviour
{
    [SerializeField] GameObject furniture;
    FlexibleColorPicker fcp;
    public Material newmat;
    [SerializeField] private ModelManager modelManager;
    
    // Update is called once per frame
    void Update()
    {
        if (modelManager.selectedModel != null)
        {
            fcp = GameObject.FindWithTag("colorPicker").GetComponent<FlexibleColorPicker>();
            furniture = modelManager.selectedModel;
            var mats = furniture.GetComponent<MeshRenderer>().materials;
            
            if (furniture.name == "bed1" || furniture.name == "bed1(Clone)")
            {
                newmat = mats[2];
            }
            else if(furniture.name == "chair" || furniture.name == "chair(Clone)")
            {
                newmat = mats[1];
                Debug.Log("get chair");
            }
            else
            {
                newmat = mats[1];
            }
            newmat.color = fcp.color;
        }
    }
}
