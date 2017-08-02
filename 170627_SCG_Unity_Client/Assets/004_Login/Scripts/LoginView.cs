using EgamingPacketStructModel;
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

    public Text TestText;

    // Use this for initialization
    void Start()
    {
        RegistTable.Instance.mView.mLoginView = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator Enumerator_CreateButtonClick()
    {
        Debug.Log("OnCreateButtonClick");

        ////Encrypt versino
        //string mAccountInputFieldText = mAccountInputField.text;
        //string mPasswordInputFieldText = mPasswordInputField.text;
        //Cryptography.AESKeyPaire mAESKeyPaire;
        //string jsonReqRegistNewNumber = string.Empty;
        //PacketStruct.EGS_Router.ReqRegistMember mReqRegistMember = null;
        //PacketStruct.ReqMainPacket mReqMainPacket = null;
        //string jsonMainReqPacket = string.Empty;
        //RSAParameters mRSAParameters;
        //string plaintextToken = string.Empty;
        //string cryptotextToken = string.Empty;
        //string cryptoPayload = string.Empty;

        //mReqRegistMember = new PacketStruct.EGS_Router.ReqRegistMember();
        //mReqRegistMember.account = mAccountInputFieldText;
        //mReqRegistMember.password = mPasswordInputFieldText;
        //jsonReqRegistNewNumber = JsonUtility.ToJson(mReqRegistMember);

        ////----------------------Encrypt Token-------------------------------------------------
        ////Get Token
        //plaintextToken = Cryptography.Instance.GetToken();
        ////Get server RSAPublicKey
        //mRSAParameters = Cryptography.Instance.GetRSAPublicKey("server");
        ////Encrypt Token
        //Debug.Log("plaintextToken: " + plaintextToken);
        //cryptotextToken = Cryptography.Instance.RSAEncrypt(mRSAParameters, plaintextToken);

        ////----------------------Encrypt payload-----------------------------------------------
        ////Get AESKeyPair
        //mAESKeyPaire = Cryptography.Instance.GetAESKeyPair("local");
        ////Encrypt jsonMainReqPacket
        //Debug.Log("jsonReqRegistNewNumber: " + jsonReqRegistNewNumber);
        //Debug.Log("length: " + mAESKeyPaire.mAesKey.Length.ToString());
        //Debug.Log("length: " + mAESKeyPaire.mAesIV.Length.ToString());
        //cryptoPayload = Cryptography.Instance.AESEncrypt(jsonReqRegistNewNumber, mAESKeyPaire.mAesKey, mAESKeyPaire.mAesIV);
        //Debug.Log("cryptoPayload: " + cryptoPayload);

        ////Set ReqMainPacket
        //mReqMainPacket = new PacketStruct.ReqMainPacket();
        //mReqMainPacket.cmd = PacketStruct.EnumCmd.EGS_Router_RegistMember.ToString();
        //mReqMainPacket.token = cryptotextToken;
        //mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        //mReqMainPacket.payload = cryptoPayload;
        //jsonMainReqPacket = JsonUtility.ToJson(mReqMainPacket);

        //Debug.Log("Start RESTFul: " + "\n" +
        //          "url: " + "http://localhost:3000/egs-router/" + "\n" +
        //          "json: " + jsonMainReqPacket);

        //NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonMainReqPacket);
        ////-----------------------------------------------------------------------------------------------

        //while (RegistTable.CommonDate.Flags.reqRegistMemberComplete == false)
        //{
        //    yield return null;
        //}

        ////Reset Flag
        ////This version can multy regist for one token 
        //RegistTable.CommonDate.Flags.reqRegistMemberComplete = false;

        string ID = string.Empty;
        string pwd = string.Empty;
        PacketStructModel.EGS_Router.ReqMemberRegist mReqMemberRegist = new PacketStructModel.EGS_Router.ReqMemberRegist();
        PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
        string jsonReqRegistMember = string.Empty;
        string jsonReqMainPacket = string.Empty;

        ID = mAccountInputField.text;
        pwd = mPasswordInputField.text;

        mReqMemberRegist.account = ID;
        mReqMemberRegist.password = pwd;
        jsonReqRegistMember = JsonUtility.ToJson(mReqMemberRegist);

        mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_MemberRegist.ToString();
        mReqMainPacket.token = "";
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = jsonReqRegistMember;
        jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);

        yield return null;


        //Simulate Message Box

        mMessageBoxButtonText.text = "Regist Member Success!!";
        mMessageBoxButton.gameObject.SetActive(true);

        mAccountInputField.text = string.Empty;
        mPasswordInputField.text = string.Empty;
    }

    public void OnCreateButtonClick()
    {
        StartCoroutine(Enumerator_CreateButtonClick());
    }



    public void OnLoginButtonClick()
    {
        Debug.Log("OnLoginButtonClick");
    }

    public void OnFacebookLoginButtonClick()
    {
        Debug.Log("OnFacebookLoginButtonClick");

        ////RSA and AES Test
        //Debug.Log("----------------RSA-------------------");

        //Cryptography.Instance.CreateRSAKey();


        //var mRSAString = "TestString";
        //Debug.Log("mRSAString: " + mRSAString);
        //var byteTest = Encoding.Unicode.GetBytes(mRSAString);
        //var byteTestString = "byteTest ";
        //for (int i = 0; i < byteTest.Length; i++)
        //{
        //    byteTestString += byteTest[i].ToString() + ",";
        //}
        //Debug.Log("byteTestString: " + byteTestString);
        //var testStringEncrypt = "testStringEncrypt"; // Cryptography.Instance.RSAEncrypt(byteTest);
        //Debug.Log("testStringEncrypt: " + testStringEncrypt);
        //var testStringDecrypt = "testStringDecrypt"; // Cryptography.Instance.RSADecrypt(testStringEncrypt);
        ////Debug.Log("testStringDecrypt: " + testStringDecrypt);


        //Debug.Log("---------------AES------------------");

        //Cryptography.Instance.CreateAESKey();

        //var mAESPair = Cryptography.Instance.GetAESKeyPair("Local");

        //var mAESString = "TestString";
        //Debug.Log("mAESString: " + mAESString);
        //var mAESEncryptString = Cryptography.Instance.AESEncrypt(mAESString, mAESPair.mAesKey, mAESPair.mAesIV);
        //Debug.Log("mAESEncryptString: " + mAESEncryptString);
        //var mAESDecryptString = Cryptography.Instance.AESDecrypte(mAESEncryptString, mAESPair.mAesKey, mAESPair.mAesIV);
        //Debug.Log("mAESDecryptString: " + mAESDecryptString);

        StartCoroutine(TestIfNodejsEventUseSameResAndReq());
    }

    public void OnMessageBoxButtonClick()
    {
        mMessageBoxButton.gameObject.SetActive(false);

        Debug.Log("OnMessageBoxButtonClick");
    }

    public IEnumerator TestIfNodejsEventUseSameResAndReq()
    {

        for (int i = 0; i < 1000; i++)
        {
            PacketStructModel.EGS_Router.ReqMemberRegist mReqMemberRegist = new PacketStructModel.EGS_Router.ReqMemberRegist();
            mReqMemberRegist.account = "3";
            var jsonReqRegistMember = JsonUtility.ToJson(mReqMemberRegist);

            PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
            mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_MemberRegist.ToString();
            mReqMainPacket.payload = jsonReqRegistMember;
            var jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

            NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);
        }

        yield return null;
    }




    #region Test
    public void OnAdminRegistButtonClicked()
    {
        PacketStructModel.EGS_Router.ReqAdminRegist mReqAdminRegist = new PacketStructModel.EGS_Router.ReqAdminRegist();
        mReqAdminRegist.account = mAccountInputField.text;
        mReqAdminRegist.password = mPasswordInputField.text;
        string jsonReqAdminRegist = JsonUtility.ToJson(mReqAdminRegist);

        PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
        mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_AdminRegist.ToString();
        mReqMainPacket.token = string.Empty;
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = jsonReqAdminRegist;
        string jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);
    }
    public void OnAdminEditButtonClicked()
    {
        PacketStructModel.EGS_Router.ReqAdminEdit mReqAdminEdit = new PacketStructModel.EGS_Router.ReqAdminEdit();
        mReqAdminEdit.account = mAccountInputField.text;
        mReqAdminEdit.password = mPasswordInputField.text;
        string jsonReqAdminEdit = JsonUtility.ToJson(mReqAdminEdit);

        PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
        mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_AdminEdit.ToString();
        mReqMainPacket.token = RegistTable.CommonDate.Variables.adminToken;
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = jsonReqAdminEdit;
        string jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);
    }
    public void OnAdminDeleteButtonClicked()
    {
        PacketStructModel.EGS_Router.ReqAdminDelete mReqAdminDelete = new PacketStructModel.EGS_Router.ReqAdminDelete();
        mReqAdminDelete.account = mAccountInputField.text;
        mReqAdminDelete.password = mPasswordInputField.text;
        string jsonReqAdminDelete = JsonUtility.ToJson(mReqAdminDelete);

        PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
        mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_AdminEdit.ToString();
        mReqMainPacket.token = RegistTable.CommonDate.Variables.adminToken;
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = jsonReqAdminDelete;
        string jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);
    }
    public void OnAdminLoginButtonClicked()
    {
        PacketStructModel.EGS_Router.ReqAdminLogin mReqAdminLogin = new PacketStructModel.EGS_Router.ReqAdminLogin();
        mReqAdminLogin.account = mAccountInputField.text;
        mReqAdminLogin.password = mPasswordInputField.text;
        string jsonReqAdminLogin = JsonUtility.ToJson(mReqAdminLogin);

        PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
        mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_AdminLogin.ToString();
        mReqMainPacket.token = string.Empty;
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = jsonReqAdminLogin;
        string jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);
    }
    public void OnAdminLogoutButtonClicked()
    {
        PacketStructModel.EGS_Router.ReqAdminLogout mReqAdminLogout = new PacketStructModel.EGS_Router.ReqAdminLogout();
        string jsonReqAdminLogout = JsonUtility.ToJson(mReqAdminLogout);

        PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
        mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_AdminLogout.ToString();
        mReqMainPacket.token = RegistTable.CommonDate.Variables.adminToken;
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = jsonReqAdminLogout;
        string jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);
    }

    public void OnMemberRegistButtonClicked()
    {
        PacketStructModel.EGS_Router.ReqMemberRegist mReqMemberRegist = new PacketStructModel.EGS_Router.ReqMemberRegist();
        mReqMemberRegist.account = mAccountInputField.text;
        mReqMemberRegist.password = mPasswordInputField.text;
        string jsonReqMemberRegist = JsonUtility.ToJson(mReqMemberRegist);

        PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
        mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_MemberRegist.ToString();
        mReqMainPacket.token = RegistTable.CommonDate.Variables.adminToken;
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = jsonReqMemberRegist;
        string jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);
    }
    public void OnMemberEditButtonClicked()
    {
        PacketStructModel.EGS_Router.ReqMemberEdit mReqMemberEdit = new PacketStructModel.EGS_Router.ReqMemberEdit();
        mReqMemberEdit.account = mAccountInputField.text;
        mReqMemberEdit.password = mPasswordInputField.text;
        string jsonReqMemberEdit = JsonUtility.ToJson(mReqMemberEdit);

        PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
        mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_MemberEdit.ToString();
        mReqMainPacket.token = RegistTable.CommonDate.Variables.adminToken;
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = jsonReqMemberEdit;
        string jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);
    }
    public void OnMemberDeleteButtonClicked()
    {

    }
    public void OnMemberLoginButtonClicked()
    {
        PacketStructModel.EGS_Router.ReqMemberLogIn mReqMemberLogIn = new PacketStructModel.EGS_Router.ReqMemberLogIn();
        mReqMemberLogIn.account = mAccountInputField.text;
        mReqMemberLogIn.password = mPasswordInputField.text;
        mReqMemberLogIn.cash = 9478;
        string jsonReqMemberLogIn = JsonUtility.ToJson(mReqMemberLogIn);

        PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
        mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_MemberLogin.ToString();
        mReqMainPacket.token = RegistTable.CommonDate.Variables.adminToken;
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = jsonReqMemberLogIn;
        string jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);
    }
    public void OnMemberLogoutButtonClicked()
    {
        PacketStructModel.EGS_Router.ReqMemberLogOut mReqMemberLogOut = new PacketStructModel.EGS_Router.ReqMemberLogOut();
        mReqMemberLogOut.token = RegistTable.CommonDate.Variables.memberToken;
        string jsonReqMemberLogOut = JsonUtility.ToJson(mReqMemberLogOut);

        PacketStructModel.ReqMainPacket mReqMainPacket = new PacketStructModel.ReqMainPacket();
        mReqMainPacket.cmd = PacketStructModel.EnumCmd.EGS_Router_MemberLogout.ToString();
        mReqMainPacket.token = RegistTable.CommonDate.Variables.adminToken;
        mReqMainPacket.timeStamp = DateTime.Now.Ticks.ToString();
        mReqMainPacket.payload = jsonReqMemberLogOut;
        string jsonReqMainPacket = JsonUtility.ToJson(mReqMainPacket);

        NetAPIModel.Instance.Send("http://localhost:3000/egs-router/", jsonReqMainPacket);
    }
    #endregion
}
