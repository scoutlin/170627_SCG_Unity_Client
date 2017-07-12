using SCG_Unity_Client_API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class RESTFulTestView : MonoBehaviour
{
    string mainRespPacketJson = string.Empty;

	// Use this for initialization
	void Start ()
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
        //Debug.Log("mainRespPacketJson: " + mainRespPacketJson);
    }

    public void OnHelloWorldButtonClick()
    {
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
        for (int i = 0; i < 10000; i++)
        {
            OnGetServerVersionButtonClick();
        }
        Debug.Log("End Time: " + DateTime.Now.ToString());
    }

    int bundle = 0;
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
