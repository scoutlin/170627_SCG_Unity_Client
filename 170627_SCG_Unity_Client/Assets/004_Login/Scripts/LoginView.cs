using SCG_Unity_Client_API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    public InputField mAccountInputField;
    public InputField mPasswordInputField;
    public Button mMessageBoxButton;
    public Text mMessageBoxButtonText;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public IEnumerator CreateButtonClick()
    {
        Debug.Log("OnCreateButtonClick");

        string mAccountInputFieldText = mAccountInputField.text;
        string mPasswordInputFieldText = mPasswordInputField.text;
        Cryptography.AESKeyPaire mAESKeyPaire;
        string jsonReqRegistNewNumber = string.Empty;
        PacketStruct.EGS_Router.ReqRegistMember mReqRegistMember = null;
        PacketStruct.ReqMainPacket mReqMainPacket = null;
        string jsonMainReqPacket = string.Empty;
        RSAParameters mRSAParameters;
        string plaintextToken = string.Empty;
        string cryptotextToken = string.Empty;
        string cryptoPayload = string.Empty;

        mReqRegistMember = new PacketStruct.EGS_Router.ReqRegistMember();
        mReqRegistMember.account = mAccountInputFieldText;
        mReqRegistMember.password = mPasswordInputFieldText;
        jsonReqRegistNewNumber = JsonUtility.ToJson(mReqRegistMember);

        //----------------------Encrypt Token-------------------------------------------------
        //Get Token
        plaintextToken = Cryptography.Instance.GetToken();
        //Get server RSAPublicKey
        mRSAParameters = Cryptography.Instance.GetRSAPublicKey("server");
        //Encrypt Token
        Debug.Log("plaintextToken: " + plaintextToken);
        cryptotextToken = Cryptography.Instance.RSAEncrypt(mRSAParameters, plaintextToken);

        //----------------------Encrypt payload-----------------------------------------------
        //Get AESKeyPair
        mAESKeyPaire = Cryptography.Instance.GetAESKeyPair("local");
        //Encrypt jsonMainReqPacket
        Debug.Log("jsonReqRegistNewNumber: " + jsonReqRegistNewNumber);
        Debug.Log("length: " + mAESKeyPaire.mAesKey.Length.ToString());
        Debug.Log("length: " + mAESKeyPaire.mAesIV.Length.ToString());
        cryptoPayload = Cryptography.Instance.AESEncrypt(jsonReqRegistNewNumber, mAESKeyPaire.mAesKey, mAESKeyPaire.mAesIV);
        Debug.Log("cryptoPayload: " + cryptoPayload);

        //Set ReqMainPacket
        mReqMainPacket = new PacketStruct.ReqMainPacket();
        mReqMainPacket.cmd = PacketStruct.EnumCmd.EGS_Router_RegistMember.ToString();
        mReqMainPacket.token = cryptotextToken;
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = cryptoPayload;
        jsonMainReqPacket = JsonUtility.ToJson(mReqMainPacket);

        Debug.Log("Start RESTFul: " + "\n" +
                  "url: " + "http://localhost:3000/egs-router/" + "\n" +
                  "json: " + jsonMainReqPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonMainReqPacket);
        //-----------------------------------------------------------------------------------------------

        while (RegistTable.CommonDate.Flags.reqRegistMemberComplete == false)
        {
            yield return null;
        }

        //Reset Flag
        //This version can multy regist for one token 
        RegistTable.CommonDate.Flags.reqRegistMemberComplete = false;

        mMessageBoxButtonText.text = "Regist Member Success!!";
        mMessageBoxButton.gameObject.SetActive(true);

        mAccountInputField.text = string.Empty;
        mPasswordInputField.text = string.Empty;
    }

    public void OnCreateButtonClick()
    {
        StartCoroutine(CreateButtonClick());
    }



    public void OnLoginButtonClick()
    {
        Debug.Log("OnLoginButtonClick");
    }

    public void OnFacebookLoginButtonClick()
    {
        Debug.Log("OnFacebookLoginButtonClick");

        Debug.Log("----------------RSA-------------------");

        Cryptography.Instance.CreateRSAKey();


        var mRSAString = "TestString";
        Debug.Log("mRSAString: " + mRSAString);
        var byteTest = Encoding.Unicode.GetBytes(mRSAString);
        var byteTestString = "byteTest ";
        for (int i = 0; i < byteTest.Length; i++)
        {
            byteTestString += byteTest[i].ToString() + ",";
        }
        Debug.Log("byteTestString: " + byteTestString);
        var testStringEncrypt = "testStringEncrypt"; // Cryptography.Instance.RSAEncrypt(byteTest);
        Debug.Log("testStringEncrypt: " + testStringEncrypt);
        var testStringDecrypt = "testStringDecrypt"; // Cryptography.Instance.RSADecrypt(testStringEncrypt);
        //Debug.Log("testStringDecrypt: " + testStringDecrypt);


        Debug.Log("---------------AES------------------");

        Cryptography.Instance.CreateAESKey();

        var mAESPair = Cryptography.Instance.GetAESKeyPair("Local");

        var mAESString = "TestString";
        Debug.Log("mAESString: " + mAESString);
        var mAESEncryptString = Cryptography.Instance.AESEncrypt(mAESString, mAESPair.mAesKey, mAESPair.mAesIV);
        Debug.Log("mAESEncryptString: " + mAESEncryptString);
        var mAESDecryptString = Cryptography.Instance.AESDecrypte(mAESEncryptString, mAESPair.mAesKey, mAESPair.mAesIV);
        Debug.Log("mAESDecryptString: " + mAESDecryptString);
    }

    public void OnMessageBoxButtonClick()
    {
        mMessageBoxButton.gameObject.SetActive(false);

        Debug.Log("OnMessageBoxButtonClick");
    }
}
