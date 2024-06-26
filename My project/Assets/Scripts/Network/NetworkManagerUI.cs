using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField]
    private Button hostBtn;
    [SerializeField]
    private Button serverBtn;
    [SerializeField]
    private Button clientBtn;
    [SerializeField]
    private Button room1;
    [SerializeField] 
    private Button room2;
    [SerializeField]
    UnityTransport unityTransport;
    void Start()
    {
        var args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i ++ )
        {
            if (args[i] == "-port")
            {
                ushort port = ushort.Parse(args[i + 1]);
                unityTransport.ConnectionData.Address = "60.205.206.147";
                unityTransport.ConnectionData.Port = port;
                unityTransport.ConnectionData.ServerListenAddress = "0.0.0.0";
            }
        }

        for (int i = 0;i < args.Length;i ++ )
        {
            if (args[i] == "-lauch-as-server")
            {
                NetworkManager.Singleton.StartServer();
                DestroyAllButtons();
            }
        }

        room1.onClick.AddListener(() =>
        {
            unityTransport.ConnectionData.Port = 7777;
            NetworkManager.Singleton.StartClient();
            DestroyAllButtons();
        });
        room2.onClick.AddListener(() =>
        {
            unityTransport.ConnectionData.Port = 7778;
            NetworkManager.Singleton.StartClient();
            DestroyAllButtons();
        });

        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            DestroyAllButtons();
        });

        serverBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            DestroyAllButtons();
        });
        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            DestroyAllButtons();
        });
    }

    private void DestroyAllButtons()
    {
        Destroy(hostBtn.gameObject);
        Destroy(serverBtn.gameObject);
        Destroy(clientBtn.gameObject);
        Destroy(room1.gameObject);
        Destroy(room2.gameObject);
    }
}
