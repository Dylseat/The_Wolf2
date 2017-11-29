using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LobbyMenu : NetworkDiscovery
{
    NetworkManager networkManager;
    Lobby lobby;
    string ipAdress = null;
    string serverName;

    private void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        lobby = gameObject.GetComponent<Lobby>();
    }


    public void CreateServeur()
    {
        Initialize();
        serverName = lobby.GetUserServerName();
        broadcastData = serverName;
        StartAsServer();
        networkManager.StartHost();
    }

    public void JoinServer()
    {
        Initialize();
        StartAsClient();
    }

    public void Disconnect()
    {
        if (isServer)
        {
            networkManager.StopHost();
        }
        else
        {
            networkManager.StopClient();
        }
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        base.OnReceivedBroadcast(fromAddress, data);
        ipAdress = fromAddress;
        lobby.SetServerName(data);
    }

    public void Connect()
    {
        networkManager.networkAddress = ipAdress;
        networkManager.StartClient();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}