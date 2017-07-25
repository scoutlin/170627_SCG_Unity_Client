using EgamingPacketStructModel;
using SCG_Unity_Client_API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryView : MonoBehaviour {

    public Text ButtonText;


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
        //Exchange Key when initial
        if (RegistTable.CommonDate.Flags.isNeedEncrypt == true)
        {
            string plaintextToken = string.Empty;
            string encryptTextToken = string.Empty;
            RSAParameters mRSAPublicKey;
            string mRSAPublicKeyString = string.Empty;
            string jsonReqGetKey = string.Empty;
            string jsonReqMainPacket = string.Empty;
            PacketStructModel.ReqMainPacket mReqMainPacket = null;
            PacketStructModel.EGS_Router.ReqGetRSAKey mReqGetKey = null;

            Debug.Log("Create RSA Start - DateTime: " + DateTime.Now.ToString());
            Console.WriteLine("Create RSA Start - DateTime: " + DateTime.Now.ToString());
            ButtonText.text = "Create RSA Start - DateTime: " + DateTime.Now.ToString();
            //Create Local RSAKey
            Cryptography.Instance.CreateRSAKey();
            ButtonText.text = "Create RSA End - DateTime: " + DateTime.Now.ToString();
            Debug.Log("Create RSA End - DateTime: " + DateTime.Now.ToString());
            Console.WriteLine("Create RSA End - DateTime: " + DateTime.Now.ToString());

            //Initial NetWork
            //--------------------------GetRSAKey----------------------------------------
            //Payload
            mRSAPublicKey = Cryptography.Instance.GetRSAPublicKey("local");
            mRSAPublicKeyString = Cryptography.Instance.TranslateRSAKeyToRSAKeyString(mRSAPublicKey);

            mReqGetKey = new PacketStructModel.EGS_Router.ReqGetRSAKey();
            mReqGetKey.mRSAPublicKeyString = mRSAPublicKeyString;
            jsonReqGetKey = JsonUtility.ToJson(mReqGetKey);

            //MainPacket
            //string stringGuid = Guid.NewGuid().ToString();
            mReqMainPacket = new PacketStructModel.ReqMainPacket();
            mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_GetRSAKey.ToString();
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

            mReqMainPacket = new PacketStructModel.ReqMainPacket();
            mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_GetAESKey.ToString();
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
