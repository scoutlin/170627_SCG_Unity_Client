using SCG_Unity_Client_API;
using System.Collections;
using System.Collections.Generic;
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

    public void OnCreateButtonClick()
    {
        Debug.Log("OnCreateButtonClick");

        string mAccountInputFieldText = mAccountInputField.text;
        string mPasswordInputFieldText = mPasswordInputField.text;

        mAccountInputField.text = string.Empty;
        mPasswordInputField.text = string.Empty;

        ////RSA
        //Cryptography.Instance.CreateRSAKey();
        //var publicRSAKeyString = Cryptography.Instance.GetPublicKeyString();

        //Debug.Log("publicRSAKeyString: " + publicRSAKeyString);

        //Payload
        PacketStruct.EGS_Router.ReqGetKey mReqGetKey = new PacketStruct.EGS_Router.ReqGetKey();
        mReqGetKey.publicRSAKeyString = mAccountInputFieldText + "@" + mPasswordInputFieldText;
        var mReqGetKeyJson = JsonUtility.ToJson(mReqGetKey);

        //MainPacket
        PacketStruct.ReqMainPacket mReqMainPacket = new PacketStruct.ReqMainPacket();
        mReqMainPacket.cmd = PacketStruct.EnumCmd.EGS_Router_GetKey.ToString();
        mReqMainPacket.payload = mReqGetKeyJson;
        var mReqMainPacketJson = JsonUtility.ToJson(mReqMainPacket);


        Debug.Log("Start RESTFul: " + "/n" + 
                  "url: " + "http://localhost:3000/egs-router/GetKey" + "/n" + 
                  "json: " + mReqMainPacketJson);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/GetKey", mReqMainPacketJson);


        mMessageBoxButtonText.text = mAccountInputField.text + "/" + mPasswordInputField.text;
        mMessageBoxButton.gameObject.SetActive(true);
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
