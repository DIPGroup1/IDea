using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Editors : MonoBehaviour

{
    public void LoadClient ()
    {
        SceneManager.LoadScene("Client", LoadSceneMode.Single);
        Debug.Log("client scene!!");
        
    }


 
}
