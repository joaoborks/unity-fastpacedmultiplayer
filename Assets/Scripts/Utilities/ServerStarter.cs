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
            Screen.SetResolution(800, 600, false);
            NetworkManager.singleton.StartServer();
        }
        else
            Destroy(gameObject);
    }
}