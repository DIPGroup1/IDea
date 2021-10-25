using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaterial : MonoBehaviour
{
    [SerializeField] GameObject bed;
    public Material theNewMaterial;
    // Start is called before the first frame update
    void Start()
    {
        //bed = GameObject.FindWithTag("BedGrey");
        Button card = transform.GetComponent<Button>();
        card.onClick.AddListener(TaskOnClick);
        //Debug.Log(bed.GetComponent<MeshRenderer>().materials[2]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {

        bed = GameObject.Find("ModelManager").GetComponent<ModelManager>().getSelectedModel(); //find again everytime clicked (check if there is new object called bedgrey)

        var mats = bed.GetComponent<MeshRenderer>().materials;
        mats[2] = theNewMaterial;

        bed.GetComponent<MeshRenderer>().materials = mats;
        //change material

        //for (var j = 0; j < bed.Length; j++)
        //{
        //    var mats = bed[j].GetComponent<MeshRenderer>().materials;
        //    mats[2] = theNewMaterial;
        //    bed[j].gameObject.GetComponent<MeshRenderer>().materials = mats;
        //}

        //var mats = bed[0].GetComponent<MeshRenderer>().materials;
        //mats[2].color = Color.green;
        //mats[2] = theNewMaterial;


        //bed[0].gameObject.GetComponent<MeshRenderer>().materials = mats;
        //Debug.Log(bed[0].GetComponent<MeshRenderer>().materials[2]);
    }
}
