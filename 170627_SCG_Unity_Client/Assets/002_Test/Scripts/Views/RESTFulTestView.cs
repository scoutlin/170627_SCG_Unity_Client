using SCG_Unity_Client_API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class RESTFulTestView : MonoBehaviour
{
    string mainRespPacketJson = string.Empty;
    private int bundle = 0;

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
        for (int i = 0; i < 10000; i++)
        {
            OnGetServerVersionButtonClick();
        }
        Debug.Log("End Time: " + DateTime.Now.ToString());
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

    public void RESTFulThreadTest(object nameOfThread)
    {
        //Test Use Thead
        long tikcs;
        DateTime dt1;
        DateTime dt2;
        dt1 = DateTime.Now;
        for (int i = 0; i <= 10000; i++)
        {
            PacketStruct.ServerVersionPacket serverVersion = new PacketStruct.ServerVersionPacket();
            serverVersion.version = "1." + i.ToString();
            serverVersion.bundle = i.ToString();

            string serverVersionJson = JsonUtility.ToJson(serverVersion);

            PacketStruct.ReqMainPacket reqMainPacket = new PacketStruct.ReqMainPacket();
            reqMainPacket.enumCmd = PacketStruct.EnumCmd.PackageVersion;
            reqMainPacket.payload = serverVersionJson;

            string reqMainPacketJson = JsonUtility.ToJson(reqMainPacket);

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["HEADER"] = "ServerVersion";
                values["JSON"] = reqMainPacketJson;

                var response = client.UploadValues("http://localhost:3000/ServerVersion", values);

                //var responseString = Encoding.Default.GetString(response);
            }
        }
        dt2 = DateTime.Now;

        tikcs = dt2.Ticks - dt1.Ticks;

        double double_ticks = (double)tikcs;
        double_ticks = double_ticks * 0.0000001;

        Debug.Log(nameOfThread.ToString() + " double_ticks: " + double_ticks.ToString());
    }

    public void OnRESTFulThreadTest()
    {
        RESTFulThreadTest("thread1");
    }

        public void OnRESRFulThreadPressureTest()
    {
        Thread th1 = new Thread(RESTFulThreadTest);
        Thread th2 = new Thread(RESTFulThreadTest);
        Thread th3 = new Thread(RESTFulThreadTest);
        Thread th4 = new Thread(RESTFulThreadTest);
        Thread th5 = new Thread(RESTFulThreadTest);
        Thread th6 = new Thread(RESTFulThreadTest);
        Thread th7 = new Thread(RESTFulThreadTest);
        Thread th8 = new Thread(RESTFulThreadTest);
        Thread th9 = new Thread(RESTFulThreadTest);
        Thread th10 = new Thread(RESTFulThreadTest);

        th1.Start("Thread1");
        th2.Start("Thread2");
        th3.Start("Thread3");
        th4.Start("Thread4");
        th5.Start("Thread5");
        th6.Start("Thread6");
        th7.Start("Thread7");
        th8.Start("Thread8");
        th9.Start("Thread9");
        th10.Start("Thread10");
    }
}
