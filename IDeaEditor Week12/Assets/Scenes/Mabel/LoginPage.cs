using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginPage : MonoBehaviour

{
    public void MainPage ()
    {
        SceneManager.LoadScene("MainPage", LoadSceneMode.Single);
        
    }


    public void Quit()
    {
        Debug.Log("Quit!!");
        Application.Quit();
    }

    
}
