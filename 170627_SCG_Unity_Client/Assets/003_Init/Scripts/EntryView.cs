using SCG_Unity_Client_API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryView : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(InitialProcess());
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Send
        StartCoroutine(NetAPIModel.Instance.ProcessSend());
        //Receive
        if (NetAPIModel.Instance.HasReceive == true)
        {
            string mainRespPacketJson = NetAPIModel.Instance.ProcessReceive();
            ProcessMainRespPacket(mainRespPacketJson);
        }
    }

    private void ProcessMainRespPacket(string mainRespPacketJson)
    {
        var mRespMainPacket = JsonUtility.FromJson<PacketStruct.RespMainPacket>(mainRespPacketJson);

        PacketStruct.EnumCmd enumCmd = (PacketStruct.EnumCmd)Enum.Parse(typeof(PacketStruct.EnumCmd), mRespMainPacket.cmd);

        Debug.Log("mRespMainPacket.cmd: " + mRespMainPacket.cmd);
        Debug.Log("mRespMainPacket.token: " + mRespMainPacket.token);
        Debug.Log("mRespMainPacket.timeStamp: " + mRespMainPacket.timeStamp);
        Debug.Log("mRespMainPacket.payload: " + mRespMainPacket.payload);
        Debug.Log("mRespMainPacket.errorMessage: " + mRespMainPacket.errorMessage);

        PacketPaserModule.Instance.ProcessParser(enumCmd, mRespMainPacket.payload);
    }

    private IEnumerator InitialProcess()
    {        
        SceneManager.LoadScene("Login", LoadSceneMode.Additive);

        yield return null;
    }
}
