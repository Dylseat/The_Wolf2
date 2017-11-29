using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField]
    GameObject Begin;
    [SerializeField]
    GameObject host;
    [SerializeField]
    GameObject client;
    [SerializeField]
    Text inputFieldText;
    [SerializeField]
    Text nameOfServer;
    LobbyMenu menu;

    private void Start()
    {
        menu = gameObject.GetComponent<LobbyMenu>();
    }

    public string GetUserServerName()
    {
        return inputFieldText.text;
    }

    public void SetServerName(string newName)
    {
        nameOfServer.text = newName;
    }

    public void CreateGame()
    {
        Begin.SetActive(false);
        host.SetActive(true);
    }

    public void JoinGame()
    {
        Begin.SetActive(false);
        client.SetActive(true);
        menu.JoinServer();
    }

    public void Return()
    {
        client.SetActive(false);
        host.SetActive(false);
        Begin.SetActive(true);
    }
}
