using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using System;
using PlayFab.ClientModels;
using Mirror;

public class ClientStart : MonoBehaviour
{
    PlayFabAuthService _authService;
    ClientNetworkManager _nm;

    private void StartRemoteClient()
    {
        _authService = PlayFabAuthService.Instance;
        PlayFabAuthService.OnDisplayAuthentication += OnDisplayAuth;
        PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;


        _nm = ClientNetworkManager.Instance;
        _nm.OnDisconnected.AddListener(OnDisconnected);
        _nm.OnConnected.AddListener(OnConnected);
        NetworkClient.RegisterHandler<ShutdownMessage>(OnServerShutdown);
        NetworkClient.RegisterHandler<MaintenanceMessage>(OnMaintenanceMessage);
    }

    private void OnLoginSuccess(LoginResult success)
    {
        NetworkClient.connection.Send<ReceiveAuthenticateMessage>(new ReceiveAuthenticateMessage()
        {
            PlayFabId = success.PlayFabId
        });
    }

    private void OnDisplayAuth()
    {
        _authService.Authenticate(Authtypes.Silent);
    }

    private void OnServerShutdown(ShutdownMessage msg)
    {
        Debug.Log("Server has issued a shutdown.");
        NetworkClient.Disconnect();
    }

    private void OnMaintenanceMessage(MaintenanceMessage msg)
    {
        var message = msg;
        Debug.LogFormat("Maintenance scheduled for: {0}", message.ScheduledMaintenanceUTC.ToString("MM-DD-YYYY hh:mm:ss"));
    }

    private void OnConnected()
    {
        _authService.Authenticate();
    }

    private void OnDisconnected(int? arg0)
    {
        Debug.Log("You were disconnected from the server");
    }


    //Implement Authentication
    public void OnClickLogin ()
    {
        StartRemoteClient();
    }

}
