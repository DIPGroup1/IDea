using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using Mirror;
using UnityEngine.UI;

public class ClientStartUp : NetworkManager
{   
    public Configuration config;
    private string PlayFabId;

    public Button Login;
    public Canvas Logincanvas;

    private void Start()
    {
        Login.onClick.AddListener(OnClickLogin);
    }

    public void OnClickLogin()
    {
        if (config.buildType == BuildType.REMOTE_CLIENT)
        {
            RemoteUserLogin();
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        NetworkClient.RegisterHandler<PlayerInfo>(OnReceivePlayerInfo);
    }

    public void OnReceivePlayerInfo(PlayerInfo netMsg)
    {
        NetworkClient.connection.Send<PlayerInfo>(new PlayerInfo
        {
            PlayFabId = this.PlayFabId,
            ConnectionId = netMsg.ConnectionId

        });
    }

    private void RemoteUserLogin()
    {
        var request = new LoginWithCustomIDRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            CreateAccount = true,
            CustomId = "Anotherplayer"
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnplayFabLoginSuccess, OnLoginFail);
    }

    private void OnLoginFail(PlayFabError error)
    {
        Debug.LogError(error);
    }

    private void OnplayFabLoginSuccess(LoginResult response)
    {
        Debug.Log("Login with CustomID succeed. Your PlayFabID is: " + response.PlayFabId);

        this.PlayFabId = response.PlayFabId;

        RequestMultiplayerServer();
    }

    private void RequestMultiplayerServer()
    {
        Debug.Log("Request Multiplayer Server");
        var request = new RequestMultiplayerServerRequest();

        request.BuildId = config.buildId;
        request.SessionId = "C92D1B27-239D-4EFC-B880-196FC8A7FF2B";
        request.PreferredRegions = new List<string>() { AzureRegion.EastUs.ToString() };
        PlayFabMultiplayerAPI.RequestMultiplayerServer(request, OnRequestSuccess, OnRequestFail);


    }

    private void OnRequestFail(PlayFabError error)
    {
        Debug.LogError(error);
    }

    private void OnRequestSuccess(RequestMultiplayerServerResponse res)
    {
        Debug.Log("port: "+ (ushort)res.Ports[0].Num+" ip: "+ res.IPV4Address);
        this.networkAddress = res.IPV4Address;
        this.GetComponent<TelepathyTransport>().port = (ushort)res.Ports[0].Num;
        this.StartClient();
        Logincanvas.enabled = false;
    }


    
}

internal class GUIDUtility
{
    public static string getUniqueID(bool generateNewIDState = false)
    {
        string uniqueID;

        if (PlayerPrefs.HasKey("guid") && !generateNewIDState)
        {
            uniqueID = PlayerPrefs.GetString("guid");
        }
        else
        {
            uniqueID = generateGUID();
            PlayerPrefs.SetString("guid", uniqueID);
        }

        return uniqueID;
    }

    public static string generateGUID()
    {
        var random = new System.Random();
        DateTime epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
        double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;

        string uniqueID = String.Format("{0:X}", Convert.ToInt32(timestamp))                //Time
                        + "-" + String.Format("{0:X}", random.Next(1000000000))                   //Random Number
                        + "-" + String.Format("{0:X}", random.Next(1000000000))                 //Random Number
                        + "-" + String.Format("{0:X}", random.Next(1000000000))                  //Random Number
                        + "-" + String.Format("{0:X}", random.Next(1000000000));                  //Random Number

        Debug.Log(uniqueID);

        return uniqueID;
    }
}

public struct PlayerInfo : NetworkMessage
{
    public string PlayFabId;
    public int ConnectionId;
}