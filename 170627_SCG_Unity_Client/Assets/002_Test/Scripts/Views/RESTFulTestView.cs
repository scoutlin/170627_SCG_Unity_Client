﻿using SCG_Unity_Client_API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RESTFulTestView : MonoBehaviour
{
    public Text mRESTFulStartEndTimeText;

    string mainRespPacketJson = string.Empty;

    private int bundle = 0;
    private int count = 0;
    private long tickTimeStart = 0;
    private long tickTimeEnd = 0;
    private double totalTime = 0;



    // Use this for initialization
    void Start()
    {
        RegistTable.Instance.mView.mRESTFulTestView = this;
        Debug.Log("Yeah");
    }

    // Update is called once per frame
    void Update()
    {
        //Send
        StartCoroutine(NetAPIModel.Instance.ProcessSend());
        //Receive
        if (NetAPIModel.Instance.HasReceive == true)
        {
            mainRespPacketJson = NetAPIModel.Instance.ProcessReceive();
            ProcessMainRespPacket(mainRespPacketJson);
        }
    }

    private void ProcessMainRespPacket(string mainRespPacketJson)
    {
        mRESTFulStartEndTimeText.text = count.ToString();
        count++;

        if(count > 9999)
        {
            Debug.Log("End Time: " + DateTime.Now.ToString());
            tickTimeEnd = DateTime.Now.Ticks;
            totalTime = tickTimeEnd - tickTimeStart;
            totalTime = totalTime * 0.0000001;
            Debug.Log("totalTime: " + totalTime.ToString());
        }
    }

    public void OnHelloWorldButtonClick()
    {
        //Original
        PacketStruct.HelloWorldPacket helloWorld = new PacketStruct.HelloWorldPacket();
        helloWorld.count = 0;
        helloWorld.text = "0";

        string helloWorldJson = JsonUtility.ToJson(helloWorld);

        PacketStruct.ReqMainPacket reqMainPacket = new PacketStruct.ReqMainPacket();
        reqMainPacket.enumCmd = PacketStruct.EnumCmd.HelloWorld;
        reqMainPacket.payload = helloWorldJson;

        string reqMainPacketJson = JsonUtility.ToJson(reqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/HelloWorld", reqMainPacketJson);
    }

    public void OnServerVersionLoopTestButtonClick()
    {
        Debug.Log("Start Time: " + DateTime.Now.ToString());
        tickTimeStart = DateTime.Now.Ticks;
        for (int i = 0; i < 10000; i++)
        {
            OnGetServerVersionButtonClick();
        }
    }


    public void OnGetServerVersionButtonClick()
    {
        PacketStruct.ServerVersionPacket serverVersion = new PacketStruct.ServerVersionPacket();
        serverVersion.version = "1." + bundle.ToString();
        serverVersion.bundle = bundle.ToString();
        bundle++;

        string serverVersionJson = JsonUtility.ToJson(serverVersion);

        PacketStruct.ReqMainPacket reqMainPacket = new PacketStruct.ReqMainPacket();
        reqMainPacket.enumCmd = PacketStruct.EnumCmd.PackageVersion;
        reqMainPacket.payload = serverVersionJson;

        string reqMainPacketJson = JsonUtility.ToJson(reqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/ServerVersion", reqMainPacketJson);
    }
}
