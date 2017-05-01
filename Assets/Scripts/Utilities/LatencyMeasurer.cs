/**
 * LatencyMeasurer.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LatencyMeasurer : MonoBehaviour
{
    public Gradient valueGradient;
    [Range(200, 500)]
    public int badLatency = 300;

    NetworkManager net;
    Text textValue;

    void Start()
    {
        net = NetworkManager.singleton;
        textValue = GetComponent<Text>();
    }

    void Update()
    {
        var ping = -1;
        if (net.client != null)
            ping = net.client.GetRTT();
        if (ping >= 0)
        {
            textValue.text = ping.ToString();
            textValue.color = valueGradient.Evaluate(ping / badLatency);
        }
        else
        {
            textValue.text = "Off";
            textValue.color = valueGradient.Evaluate(1);
        }
    }
}