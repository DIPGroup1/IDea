using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.MultiplayerAgent.Model;
using System;
using Mirror;


public class ServerStart : NetworkManager
{

    private List<PlayerConnection> playerConnections = new List<PlayerConnection>();
    private List<ConnectedPlayer> connectedPlayers = new List<ConnectedPlayer>();

    private string playfabId;
    private void Start()
    {
        StartPlayFabAPI();
        this.StartServer(); 
    }

    private void StartPlayFabAPI()
    {
        PlayFabMultiplayerAgentAPI.Start();
        StartCoroutine(ReadyForPlayers());
    }

    private IEnumerator ReadyForPlayers()
    {
        yield return new WaitForSeconds(.5f);
        PlayFabMultiplayerAgentAPI.ReadyForPlayers();
    }

    //server overrides
    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<PlayerInfo>(OnReceivePlayerInfo);
        NetworkServer.Listen(100);
    }

    private void OnReceivePlayerInfo(PlayerInfo netMsg)
    {
        var playerConnection = playerConnections.Find(x => x.ConnectionId == netMsg.ConnectionId);
        playerConnection.ConnectedPlayer = new ConnectedPlayer(netMsg.PlayFabId);
        connectedPlayers.Add(playerConnection.ConnectedPlayer);
        PlayFabMultiplayerAgentAPI.UpdateConnectedPlayers(connectedPlayers);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        Debug.Log("Connected client to server, ConnectionId: " + conn.connectionId);

        playerConnections.Add(new PlayerConnection
        {
            ConnectionId=conn.connectionId,
        });

        conn.Send<PlayerInfo>(new PlayerInfo
        {
            ConnectionId = conn.connectionId,
        });
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        Debug.Log("Client disconnected from server, ConnectionId: " + conn.connectionId);

        var playerConnection = playerConnections.Find(x => x.ConnectionId == conn.connectionId);

        connectedPlayers.Remove(playerConnection.ConnectedPlayer);
        playerConnections.Remove(playerConnection);

        PlayFabMultiplayerAgentAPI.UpdateConnectedPlayers(connectedPlayers);

        if(playerConnections.Count==0)
        {
            StartCoroutine(shutdown());
        }
    }

    private IEnumerator shutdown()
    {
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
    
}

public class PlayerConnection
{
    public ConnectedPlayer ConnectedPlayer;
    public int ConnectionId;
}
//public class ServerStart : MonoBehaviour
//{
//    [SerializeField] Configuration config;
//    [SerializeField] UnityNetworkServer nm;
//    private List<ConnectedPlayer> _connectedPlayers;
//    public bool Debugging = true;

//    // Start is called before the first frame update
//    void Start()
//    {   
//        if(config.buildType == BuildType.REMOTE_SERVER)
//        {
//            StartRemoteServer();
//        }
//    }

//    private void StartRemoteServer()
//    {
//        _connectedPlayers = new List<ConnectedPlayer>();
//        PlayFabMultiplayerAgentAPI.Start();
//        PlayFabMultiplayerAgentAPI.OnServerActiveCallback += OnServerActive;
//        PlayFabMultiplayerAgentAPI.OnShutDownCallback += OnShutDown;
//        PlayFabMultiplayerAgentAPI.OnMaintenanceCallback += OnMaintenance;
//        PlayFabMultiplayerAgentAPI.IsDebugging = Debugging;
//        PlayFabMultiplayerAgentAPI.OnAgentErrorCallback += OnAgentError;

//        nm.OnPlayerAdd.AddListener(OnPlayerAdded);
//        nm.OnPlayerRemoved.AddListener(OnPlayerRemoved);

//        StartCoroutine(ReadyForPlayers());
//        //...
//    }


//    private void OnPlayerRemoved(string playfabId)
//    {
//        ConnectedPlayer player = _connectedPlayers.Find(x => x.PlayerId.Equals(playfabId, StringComparison.OrdinalIgnoreCase));
//        _connectedPlayers.Remove(player);
//        PlayFabMultiplayerAgentAPI.UpdateConnectedPlayers(_connectedPlayers);
//        if (_connectedPlayers.Count == 0)
//        {
//            OnShutDown();
//        }
//    }

//    private void OnPlayerAdded(string playfabId)
//    {
//        _connectedPlayers.Add(new ConnectedPlayer(playfabId));
//        PlayFabMultiplayerAgentAPI.UpdateConnectedPlayers(_connectedPlayers);
//    }

//    private IEnumerator ReadyForPlayers()
//    {
//        yield return new WaitForSeconds(.5f);
//        PlayFabMultiplayerAgentAPI.ReadyForPlayers();
//    }

//    private void OnServerActive()
//    {
//        nm.StartListen();
//        Debug.Log("Server Started From Agent Activation");
//        // players can now connect to the server
//    }

//    private void OnShutDown()
//    {
//        Debug.Log("Shutting Down...");
//        foreach(var conn in nm.Connections)
//        {
//            conn.Connection.Send<ShutdownMessage>(new ShutdownMessage());
//        }

//        StartCoroutine(Shutdown());
//    }

//    IEnumerator Shutdown()
//    {
//        yield return new WaitForSeconds(5f);
//        Application.Quit();
//    }

//    private void OnAgentError(string error)
//    {
//        Debug.Log(error);
//    }

//    private void OnMaintenance(DateTime? NextScheduledMaintenanceUtc)
//    {
//        Debug.LogFormat("Maintenance scheduled for: {0}", NextScheduledMaintenanceUtc.Value.ToLongDateString());
//        foreach (var conn in nm.Connections)
//        {
//            conn.Connection.Send<MaintenanceMessage>(new MaintenanceMessage()
//            {
//                ScheduledMaintenanceUTC = (DateTime)NextScheduledMaintenanceUtc
//            });
//        }
//    }

//}
