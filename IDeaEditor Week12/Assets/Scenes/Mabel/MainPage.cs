using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPage : MonoBehaviour

{
    public void Client()
    {
        SceneManager.LoadScene("Client", LoadSceneMode.Single);

    }


    public void Quit()
    {
        Debug.Log("Quit!!");
        Application.Quit();
    }


}