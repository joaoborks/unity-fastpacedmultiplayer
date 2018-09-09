/**
 * ServerStarter.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;

public class ServerStarter : MonoBehaviour
{
    void Start()
    {
        var args = Environment.GetCommandLineArgs();
        if (args.Any((arg) => arg == "servermode"))
        {
            Application.targetFrameRate = 60;
            NetworkManager.singleton.StartServer();
        }
        else if (args.Any((arg) => arg == "gamemode"))
        {
            NetworkManager.singleton.StartClient();
            AuthCharInput.simulated = true;
        }
        Time.fixedDeltaTime = 1 / 60f;
        Destroy(gameObject);
    }
}