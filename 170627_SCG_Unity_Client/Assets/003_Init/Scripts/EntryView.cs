using SCG_Unity_Client_API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryView : MonoBehaviour {

    // Use this for initialization
    void Start()
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
        if (RegistTable.CommonDate.Flags.isNeedEncrypt == true)
        {
            string plaintextToken = string.Empty;
            string encryptTextToken = string.Empty;
            RSAParameters mRSAPublicKey;
            string mRSAPublicKeyString = string.Empty;
            string jsonReqGetKey = string.Empty;
            string jsonReqMainPacket = string.Empty;
            PacketStruct.ReqMainPacket mReqMainPacket = null;
            PacketStruct.EGS_Router.ReqGetRSAKey mReqGetKey = null;

            //Create Local RSAKey
            Cryptography.Instance.CreateRSAKey();

            //Initial NetWork
            //--------------------------GetRSAKey----------------------------------------
            //Payload
            mRSAPublicKey = Cryptography.Instance.GetRSAPublicKey("local");
            mRSAPublicKeyString = Cryptography.Instance.TranslateRSAKeyToRSAKeyString(mRSAPublicKey);

            mReqGetKey = new PacketStruct.EGS_Router.ReqGetRSAKey();
            mReqGetKey.mRSAPublicKeyString = mRSAPublicKeyString;
            jsonReqGetKey = JsonUtility.ToJson(mReqGetKey);

            //MainPacket
            //string stringGuid = Guid.NewGuid().ToString();
            mReqMainPacket = new PacketStruct.ReqMainPacket();
            mReqMainPacket.cmd = PacketStruct.EnumCmd.EGS_Router_GetRSAKey.ToString();
            mReqMainPacket.token = string.Empty;
            mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
            mReqMainPacket.payload = jsonReqGetKey;
            jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);


            //Debug.Log("Start RESTFul: " + "/n" +
            //          "url: " + "http://localhost:3000/egs-router/" + "/n" +
            //          "json: " + mReqMainPacketJson);

            NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);



            while (RegistTable.CommonDate.Flags.reqRSAKeyComplete == false)
            {
                yield return null;
            }
            //------------------------------------------------------------------------------------------------

            //-------------------------------Send Packet EGS_Router_GetAESKey------------------------------
            mRSAPublicKey = Cryptography.Instance.GetRSAPublicKey("server");
            plaintextToken = Cryptography.Instance.GetToken();
            encryptTextToken = Cryptography.Instance.RSAEncrypt(mRSAPublicKey, plaintextToken);
            Debug.Log("cryptTextToken send to server use ServerRSAPublicKey: " + encryptTextToken);

            mReqMainPacket = new PacketStruct.ReqMainPacket();
            mReqMainPacket.cmd = PacketStruct.EnumCmd.EGS_Router_GetAESKey.ToString();
            mReqMainPacket.token = encryptTextToken;
            mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
            mReqMainPacket.payload = string.Empty;
            jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

            NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);

            while (RegistTable.CommonDate.Flags.reqAESKeyComplete == false)
            {
                yield return null;
            }
        }

        SceneManager.LoadScene("Login", LoadSceneMode.Additive);

        yield return null;
    }
}
