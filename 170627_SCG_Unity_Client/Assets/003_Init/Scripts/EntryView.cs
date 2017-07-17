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

    private void ProcessMainRespPacket(string jsonRespMainPacket)
    {
        PacketPaserModule.Instance.ProcessParser(jsonRespMainPacket);
    }

    private IEnumerator InitialProcess()
    {
        Cryptography.Instance.CreateRSAKey();
        Cryptography.Instance.CreateAESKey();

        SceneManager.LoadScene("Login", LoadSceneMode.Additive);

        yield return null;
    }
}
