using SCG_Unity_Client_API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PacketPaserModule
{
    private static PacketPaserModule mPacketPaserModule;

    public static PacketPaserModule Instance
    {
        get
        {
            if(mPacketPaserModule == null)
            {
                mPacketPaserModule = new PacketPaserModule();
            }

            return mPacketPaserModule;
        }
    }

    public void ProcessParser(string jsonRespMainPacket)
    {
        Debug.Log("jsonRespMainPacket: " + jsonRespMainPacket);

        var mRespMainPacket = JsonUtility.FromJson<PacketStruct.RespMainPacket>(jsonRespMainPacket);
        var payload = mRespMainPacket.payload;

        PacketStruct.EnumCmd enumCmd = (PacketStruct.EnumCmd)Enum.Parse(typeof(PacketStruct.EnumCmd), mRespMainPacket.cmd);

        Debug.Log("mRespMainPacket.cmd: " + mRespMainPacket.cmd);
        Debug.Log("mRespMainPacket.token: " + mRespMainPacket.token);
        Debug.Log("mRespMainPacket.timeStamp: " + mRespMainPacket.timeStamp);
        Debug.Log("mRespMainPacket.payload: " + mRespMainPacket.payload);
        Debug.Log("mRespMainPacket.errorMessage: " + mRespMainPacket.errorMessage);

        if(mRespMainPacket.errorMessage != string.Empty)
        {
            throw new Exception("RespMainPacket Error!!");
        }

        if(RegistTable.CommonDate.reqRSAKeyComplete == true)
        {

        }


        switch (enumCmd)
        {
            default:
                {

                }
                break;

            case PacketStruct.EnumCmd.EGS_Router_GetRSAKey:
                {
                    Debug.Log("PacketParserModule - EGS_Router_GetRSAKey");

                    string mRSAPublicKeyString = string.Empty;
                    RSAParameters mRSAPublicKey;
                    string cryptTextToken = mRespMainPacket.token;
                    string plainTextToken = Cryptography.Instance.RSADecrypt(mRespMainPacket.token);
                    Debug.Log("cryptTextToken: " + cryptTextToken);
                    Debug.Log("plainTextToken: " + plainTextToken);

                    var mRespGetRSAKey = JsonUtility.FromJson<PacketStruct.EGS_Router.RespGetRSAKey>(mRespMainPacket.payload);
                    mRSAPublicKeyString = mRespGetRSAKey.mRSAPublicKeyString;
                    mRSAPublicKey = Cryptography.Instance.TranslateRSAKeyStringToRSAKey(mRSAPublicKeyString);
                    Cryptography.Instance.SetRSAPublicKey("server", mRSAPublicKey);

                    mRSAPublicKey = Cryptography.Instance.GetRSAPublicKey("server");
                    cryptTextToken = Cryptography.Instance.RSAEncrypt(mRSAPublicKey, plainTextToken);
                    Debug.Log("cryptTextToken: " + mRespMainPacket.token);

                    PacketStruct.ReqMainPacket mReqMainPacket = new PacketStruct.ReqMainPacket();
                    mReqMainPacket.cmd = PacketStruct.EnumCmd.EGS_Router_GetAESKey.ToString();
                    mReqMainPacket.token = cryptTextToken;
                    mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
                    mReqMainPacket.payload = string.Empty;

                    RegistTable.CommonDate.reqRSAKeyComplete = true;
                }
                break;

            case PacketStruct.EnumCmd.EGS_Router_GetAESKey:
                {
                    Debug.Log("PacketParserModule - EGS_Router_GetToken");
                }
                break;
        }
    }


}
