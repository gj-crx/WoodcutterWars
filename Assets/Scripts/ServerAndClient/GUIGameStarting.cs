using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using ClientSideLogic;

public class GUIGameStarting : MonoBehaviour
{

    private void Awake()
    {
        
    }
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
       // GameObject.Find("ClientGameController").SetActive(true);
        ClientGameController.Singleton.InitializeClient();
    }
    public void StartServer(bool StartHost = false)
    {
        GameObject.Find("GameCoordinator").GetComponent<GameNetCoordinator>().Init();
        if (StartHost)
        {
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            NetworkManager.Singleton.StartServer();
        }
    }
}
