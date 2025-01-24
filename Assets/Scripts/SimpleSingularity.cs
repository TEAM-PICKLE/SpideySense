using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine.Events;
using Oculus.Interaction;
using System.Net.Sockets;

public class SimpleSingularity : MonoBehaviour
{
    // UI elements for entering the server's IP address and port

    [SerializeField]
    string ip;
    int port = 80;

    private TcpClient tcpClient;
    private NetworkStream tcpStream;

    // TCP client and stream for communication
    System.Net.Sockets.TcpClient client;
    System.Net.Sockets.NetworkStream stream;

    // Task that handles data exchange with the server
    private Task exchangeTask;

    // FUNCTIONs START
    private void Awake()
    {

    }

    private void Start()
    {
        Connect();
    }

    // Connect using data entered in the UI
    public async Task Connect()
    {
        await ConnectTask(ip, port);
    }

    // Connection function for the UWP platform
    private async Task ConnectTask(string host, int port)
    {
        try
        {
            Debug.Log("Attempting to connect...");
            // Ensure previous connections are closed
            if (client != null)
            {
                client.Close();
                client = null;
            }

            client = new System.Net.Sockets.TcpClient();
            Debug.Log($"Attempting to connect to {host}:{port}");

            //configure socket
            client.ReceiveBufferSize = 16384;
            client.SendBufferSize = 16384;

            await client.ConnectAsync(host, port);
            stream = client.GetStream();
    //        onConnectionEstablished.Invoke();
            Debug.Log("Connected successfully and stream created!");

            // Start exchanging data with the server
            RestartExchange();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    // Send an initial message once connected
    public void SendInitialMessage()
    {
        if (client != null && client.Connected && stream != null)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes("Hello from Unity!");
            stream.Write(messageBytes, 0, messageBytes.Length);
            Debug.Log("Sent initial message: Hello from Unity!");
        }
    }

    public void SendMyMessage(string message)
    {
        if (client != null && client.Connected && stream != null)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            stream.Write(messageBytes, 0, messageBytes.Length);
            Debug.Log("message sent");
        }
    }


    public void receiveMetadata()
    {
        byte[] bytes = new byte[client.ReceiveBufferSize];
        int recv = stream.Read(bytes, 0, client.ReceiveBufferSize);

        string received = Encoding.UTF8.GetString(bytes, 0, recv);
    }

    private bool exchangeStopRequested = false;
    private string lastPacket = null;

    // Function to initiate data exchange
    public void RestartExchange()
    {
        // Ensure previous exchange tasks are terminated
        if (exchangeTask != null)
        {
            exchangeStopRequested = true;
            try
            {
                exchangeTask.Wait();
            }
            catch { }
            exchangeTask = null;
        }

        exchangeStopRequested = false;
        exchangeTask = Task.Run(() => ExchangePackets());
    }

    // Continuous function to receive packets from the server
    public void ExchangePackets()
    {
        //receive metadata for drawing graph before looping
        receiveMetadata();
        while (!exchangeStopRequested)
        {
           // Debug.Log("Started exchanging packets");
            string received = null;
            byte[] bytes = new byte[client.ReceiveBufferSize];
            int recv = 0;

            while (true)
            {
                recv = stream.Read(bytes, 0, client.ReceiveBufferSize);
                received = Encoding.UTF8.GetString(bytes, 0, recv);

                if (!string.IsNullOrEmpty(received))
                {
                    Debug.Log($"Received from server: {received}");
                    lastPacket = received;
                    break;
                }
            }
        }
    }


    private float[] StringToFloatArray(string data)
    {
        // Using regex to find all numbers in the string
        MatchCollection matches = Regex.Matches(data, @"-?\d+(\.\d+)?");
        return matches.Cast<Match>().Select(m => float.Parse(m.Value)).ToArray();
    }


    // Function to terminate data exchange
    public async Task StopExchange()
    {
        exchangeStopRequested = true;
        if (exchangeTask != null)
        {
            await exchangeTask;
            Debug.Log("Socket closed successfully");
            client.Dispose();
            exchangeTask = null;
        }
    }

    // Function to close the connection
    public void CloseConnection()
    {
        if (client != null)
        {
            Task.Run(() => StopExchange());
        }
       // onConnectionClosed.Invoke();
    }

    // Close the connection when the Unity application is quit
    void OnApplicationQuit()
    {
        CloseConnection();
    }
}