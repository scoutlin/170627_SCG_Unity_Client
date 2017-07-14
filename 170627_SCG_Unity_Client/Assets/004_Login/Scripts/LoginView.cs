using SCG_Unity_Client_API;
using System.Collections;
using System.Collections.Generic;
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

        //--------------------------GetRSAKey----------------------------------------
        string mAccountInputFieldText = mAccountInputField.text;
        string mPasswordInputFieldText = mPasswordInputField.text;

        ////RSA
        //Cryptography.Instance.CreateRSAKey();
        //var publicRSAKeyString = Cryptography.Instance.GetPublicKeyString();

        //Debug.Log("publicRSAKeyString: " + publicRSAKeyString);

        //Payload
        PacketStruct.EGS_Router.ReqGetRSAKey mReqGetKey = new PacketStruct.EGS_Router.ReqGetRSAKey();
        mReqGetKey.publicRSAKeyString = mAccountInputFieldText + "@" + mPasswordInputFieldText;
        var mReqGetKeyJson = JsonUtility.ToJson(mReqGetKey);

        //MainPacket
        PacketStruct.ReqMainPacket mReqMainPacket = new PacketStruct.ReqMainPacket();
        mReqMainPacket.cmd = PacketStruct.EnumCmd.EGS_Router_GetRSAKey.ToString();
        mReqMainPacket.payload = mReqGetKeyJson;
        var mReqMainPacketJson = JsonUtility.ToJson(mReqMainPacket);


        //Debug.Log("Start RESTFul: " + "/n" +
        //          "url: " + "http://localhost:3000/egs-router/GetRSAKey" + "/n" +
        //          "json: " + mReqMainPacketJson);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/GetRSAKey", mReqMainPacketJson);



        while (RegistTable.CommonDate.reqRSAKeyComplete == false)
        {
            yield return null;
        }
        RegistTable.CommonDate.reqRSAKeyComplete = false;
        //------------------------------------------------------------------------------------------------

        //-------------------------------GetToken---------------------------------------------------------
        Cryptography.AESKeyPaire mAESKeyPaire = Cryptography.Instance.GetAESKeyPair("Local");

        var account = Encoding.Unicode.GetBytes(mAccountInputFieldText);
        var password = Encoding.Unicode.GetBytes(mPasswordInputFieldText);
        var key = mAESKeyPaire.mAesKey;
        var iv = mAESKeyPaire.mAesIV;

        PacketStruct.EGS_Router.ReqGetToken mReqGetToken = new PacketStruct.EGS_Router.ReqGetToken();
        mReqGetToken.account = Cryptography.Instance.RSAEncrypt(account);
        mReqGetToken.password = Cryptography.Instance.RSAEncrypt(password);
        mReqGetToken.key = Cryptography.Instance.RSAEncrypt(key);
        mReqGetToken.iv = Cryptography.Instance.RSAEncrypt(iv);
        var ReqGetTokenJson = JsonUtility.ToJson(mReqGetToken);

        mReqMainPacket.cmd = PacketStruct.EnumCmd.EGS_Router_GetToken.ToString();
        mReqMainPacket.payload = ReqGetTokenJson;
        mReqMainPacketJson = JsonUtility.ToJson(mReqMainPacket);

        Debug.Log("Start RESTFul: " + "\n" +
                  "url: " + "http://localhost:3000/egs-router/GetToken" + "\n" +
                  "json: " + mReqMainPacketJson);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/GetToken", mReqMainPacketJson);
        //-----------------------------------------------------------------------------------------------
        mMessageBoxButtonText.text = mAccountInputField.text + "/" + mPasswordInputField.text;
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

        ////Payload
        //PacketStruct.EGS_Router.ReqGetKey mReqGetKey = new PacketStruct.EGS_Router.ReqGetKey();
        //mReqGetKey.publicRSAKeyString = publicRSAKeyString;

        ////MainPacket
        //PacketStruct.ReqMainPacket mReqMainPacket = new PacketStruct.ReqMainPacket();
        //mReqMainPacket.enumCmd = PacketStruct.EnumCmd.EGS_Router_GetKey;
        //mReqMainPacket.payload = JsonUtility.ToJson(mReqGetKey);

        //Debug.Log("Start RESTFul");


        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/GetKey", "fuck");
    }

    public void OnMessageBoxButtonClick()
    {
        mMessageBoxButton.gameObject.SetActive(false);

        Debug.Log("OnMessageBoxButtonClick");
    }
}
