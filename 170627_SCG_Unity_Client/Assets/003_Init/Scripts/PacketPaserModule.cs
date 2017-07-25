using EgamingPacketStructModel;
using SCG_Unity_Client_API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
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
        //bool isTokenExist = false;
        PacketStructModel.RespMainPacket mRespMainPacket;
        string errorMessage = string.Empty;
        PacketStructModel.EnumCmd enumCmd;
        string payload = string.Empty;
        string plaintextToken = string.Empty;
        string cryptotextToken = string.Empty;
        string timeStamp = string.Empty;

        mRespMainPacket = JsonUtility.FromJson<PacketStructModel.RespMainPacket>(jsonRespMainPacket);
        errorMessage = mRespMainPacket.errorMessage;
        enumCmd = (PacketStructModel.EnumCmd)Enum.Parse(typeof(PacketStructModel.EnumCmd), mRespMainPacket.cmd);     
        cryptotextToken = mRespMainPacket.token;
        timeStamp = mRespMainPacket.timeStamp;
        payload = mRespMainPacket.payload;

        //Debug.Log("mRespMainPacket.errorMessage: " + mRespMainPacket.errorMessage);
        //Debug.Log("mRespMainPacket.cmd: " + mRespMainPacket.cmd);
        //Debug.Log("mRespMainPacket.token: " + mRespMainPacket.token);
        //Debug.Log("mRespMainPacket.timeStamp: " + mRespMainPacket.timeStamp);
        //Debug.Log("mRespMainPacket.payload: " + mRespMainPacket.payload);

        if (mRespMainPacket.errorMessage != string.Empty)
        {
            throw new Exception("RespMainPacket Error!!");
        }
        else
        {
            //if (enumCmd == PacketStruct.EnumCmd.EGS_Router_GetRSAKey)
            //{
            //    Debug.Log("cryptotextToken receive from server : " + cryptotextToken);
            //    plaintextToken = Cryptography.Instance.RSADecrypt(cryptotextToken);
            //    Debug.Log("plaintextToken receive from server : : " + plaintextToken);
            //    isTokenExist = Cryptography.Instance.CheckTokenExist(plaintextToken);
            //    isTokenExist = true;
            //}
            //else
            //{
            //    Debug.Log("cryptotextToken receive from server : " + cryptotextToken);
            //    plaintextToken = Cryptography.Instance.RSADecrypt(cryptotextToken);
            //    Debug.Log("plaintextToken receive from server : " + plaintextToken);
            //    isTokenExist = Cryptography.Instance.CheckTokenExist(plaintextToken);               
            //}

            //Debug.Log("isTokenExist: " + isTokenExist);

            //if (isTokenExist == true)
            if (true)
                {
                switch (enumCmd)
                {
                    default:
                        {

                        }
                        break;

                    case PacketStructModel.EnumCmd.EGS_Router_GetRSAKey:
                        {
                            Debug.Log("PacketParserModule - EGS_Router_GetRSAKey");

                            //Receive
                            string mRSAPublicKeyString = string.Empty;
                            RSAParameters mRSAPublicKey;
                            cryptotextToken = mRespMainPacket.token;
                            plaintextToken = Cryptography.Instance.RSADecrypt(mRespMainPacket.token);
                            Debug.Log("!!!!!!!!!!!!!!!!!!!!!plaintextToken2: " + plaintextToken);
                            Cryptography.Instance.AddingToken(plaintextToken);
                            
                            Debug.Log("cryptTextToken receive from server use ClinetRSAPublicKey: " + cryptotextToken);
                            //Debug.Log("plainTextToken: " + plainTextToken);

                            var mRespGetRSAKey = JsonUtility.FromJson<PacketStructModel.EGS_Router.RespGetRSAKey>(mRespMainPacket.payload);
                            mRSAPublicKeyString = mRespGetRSAKey.mRSAPublicKeyString;
                            mRSAPublicKey = Cryptography.Instance.TranslateRSAKeyStringToRSAKey(mRSAPublicKeyString);
                            Cryptography.Instance.SetRSAPublicKey("server", mRSAPublicKey);

                            //Set Flag
                            RegistTable.CommonDate.Flags.reqRSAKeyComplete = true;
                        }
                        break;

                    case PacketStructModel.EnumCmd.EGS_Router_GetAESKey:
                        {
                            Debug.Log("PacketParserModule - EGS_Router_GetToken");

                            //Get AES Key, IV Decrypt if and save
                            PacketStructModel.EGS_Router.RespGetAESKey mRespGetAESKey = new PacketStructModel.EGS_Router.RespGetAESKey();
                            mRespGetAESKey = JsonUtility.FromJson<PacketStructModel.EGS_Router.RespGetAESKey>(payload);
                            string encryptAESKey = mRespGetAESKey.mAESKey;
                            string encryptAESIV = mRespGetAESKey.mAESIV;
                            string plainAESKey = Cryptography.Instance.RSADecrypt(encryptAESKey);
                            string plainAESIV = Cryptography.Instance.RSADecrypt(encryptAESIV);
                            byte[] mAESKey = null;
                            byte[] mAESIV = null;
                            Cryptography.AESKeyPaire mAESKeyPaire = new Cryptography.AESKeyPaire();
                            //mAESKey = Encoding.UTF8.GetBytes(plainAESKey);
                            //mAESIV = Encoding.UTF8.GetBytes(plainAESIV);
                            //var bytesCypherText = Convert.FromBase64String(cyphertext);
                            //var encryptJson = Convert.ToBase64String(bytesCypherText);
                            mAESKey = Convert.FromBase64String(plainAESKey);
                            mAESIV = Convert.FromBase64String(plainAESIV);

                            mAESKeyPaire.mAesKey = mAESKey;
                            mAESKeyPaire.mAesIV = mAESIV;

                            Cryptography.Instance.SetAESKeyPair("local", mAESKeyPaire);

                            //Debug.Log("encryptAESKey: " + encryptAESKey);
                            //Debug.Log("encryptAESIV: " + encryptAESIV);
                            //Debug.Log("plainAESKey: " + plainAESKey);
                            //Debug.Log("plainAESIV: " + plainAESIV);
                            string byteStringmAESKey = string.Empty;
                            string byteStringmAESIV = string.Empty;
                            for (int i = 0; i < mAESKey.Length; i++)
                            {
                                byteStringmAESKey += mAESKey[i].ToString() + ", ";
                            }
                            for (int i = 0; i < mAESIV.Length; i++)
                            {
                                byteStringmAESIV += mAESIV[i].ToString() + ", ";
                            }
                            Debug.Log("length: " + mAESKey .Length + "/" + "byteStringmAESKey: " + byteStringmAESKey);
                            Debug.Log("length: " + mAESIV.Length + "/" + "byteStringmAESIV: " + byteStringmAESIV);

                            RegistTable.CommonDate.Flags.reqAESKeyComplete = true;
                        }
                        break;



                    case PacketStructModel.EnumCmd.EGS_Router_AdminRegist:
                        {
                            PacketStructModel.EGS_Router.RespAdminRegist mRespAdminRegist = new PacketStructModel.EGS_Router.RespAdminRegist();
                            mRespAdminRegist = JsonUtility.FromJson<PacketStructModel.EGS_Router.RespAdminRegist>(payload);

                            //Debug.Log("respAccount: " + mRespRegistMember.respAccount);
                            RegistTable.Instance.mView.mLoginView.TestText.text += mRespAdminRegist.isSucess.ToString();
                            RegistTable.CommonDate.Flags.reqRegistMemberComplete = true;
                        }
                        break;

                    case PacketStructModel.EnumCmd.EGS_Router_AdminEdit:
                        {

                        }
                        break;

                    case PacketStructModel.EnumCmd.EGS_Router_AdminDelete:
                        {

                        }
                        break;

                    case PacketStructModel.EnumCmd.EGS_Router_AdminLogin:
                        {

                        }
                        break;

                    case PacketStructModel.EnumCmd.EGS_Router_AdminLogout:
                        {

                        }
                        break;



                    case PacketStructModel.EnumCmd.EGS_Router_MemberRegist:
                        {
                            //Debug.Log("PacketParserModule - EGS_Router_GetToken");

                            PacketStructModel.EGS_Router.RespMemberRegist mRespMemberRegist = new PacketStructModel.EGS_Router.RespMemberRegist();
                            mRespMemberRegist = JsonUtility.FromJson<PacketStructModel.EGS_Router.RespMemberRegist>(payload);

                            //Debug.Log("respAccount: " + mRespRegistMember.respAccount);
                            RegistTable.Instance.mView.mLoginView.TestText.text += mRespMemberRegist.isSuccess.ToString();
                            RegistTable.CommonDate.Flags.reqRegistMemberComplete = true;
                        }
                        break;

                    case PacketStructModel.EnumCmd.EGS_Router_MemberEdit:
                        {

                        }
                        break;

                    case PacketStructModel.EnumCmd.EGS_Router_MemberDelete:
                        {

                        }
                        break;

                    case PacketStructModel.EnumCmd.EGS_Router_MemberLogin:
                        {

                        }
                        break;

                    case PacketStructModel.EnumCmd.EGS_Router_MemberLogout:
                        {

                        }
                        break;
                }
            }
            else
            {
                //throw new Exception("Token Identify Fail!!");
            }
        }
    }
}
