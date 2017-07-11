using SCG_Unity_Client_API;
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
	void Update ()
    {
        mainRespPacketJson = NetAPIModel.Instance.GetDataFromReceiveQueue();
        if(mainRespPacketJson != string.Empty)
        {
            ProcessMainRespPacket(mainRespPacketJson);
        }
        else
        {
            //Do Nothing
        }
	}

    private void ProcessMainRespPacket(string mainRespPacketJson)
    {
        Debug.Log("mainRespPacketJson: " + mainRespPacketJson);
    }

    public void OnHelloWorldButtonClick()
    {
        PacketStruct.HelloWorldPacket helloWorld = new PacketStruct.HelloWorldPacket();
        helloWorld.count = 1;
        helloWorld.text = "1";

        string helloWorldJson = JsonUtility.ToJson(helloWorld);

        PacketStruct.ReqMainPacket reqMainPacket = new PacketStruct.ReqMainPacket();
        reqMainPacket.enumCmd = PacketStruct.EnumCmd.HelloWorld;
        reqMainPacket.payload = helloWorldJson;

        string reqMainPacketJson = JsonUtility.ToJson(reqMainPacket);

        StartCoroutine(NetAPIModel.Instance.Send("http://192.168.0.103:3000/HelloWorld", reqMainPacketJson));
    }

    public void OnGetServerVersionButtonClick()
    {
        PacketStruct.ServerVersionPacket serverVersion = new PacketStruct.ServerVersionPacket();
        serverVersion.version = "1.0";
        serverVersion.bundle = "1";

        string serverVersionJson = JsonUtility.ToJson(serverVersion);

        PacketStruct.ReqMainPacket reqMainPacket = new PacketStruct.ReqMainPacket();
        reqMainPacket.enumCmd = PacketStruct.EnumCmd.PackageVersion;
        reqMainPacket.payload = serverVersionJson;

        string reqMainPacketJson = JsonUtility.ToJson(reqMainPacket);

        StartCoroutine(NetAPIModel.Instance.Send("http://192.168.0.103:3000/ServerVersion", reqMainPacketJson));
    }
}
