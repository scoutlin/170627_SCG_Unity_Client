using SCG_Unity_Client_API;
using System.Collections;
using System.Collections.Generic;
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


        Debug.Log("mainRespPacketJson:" + mainRespPacketJson);
        Debug.Log("mRespMainPacket.cmd:" + mRespMainPacket.cmd);
        Debug.Log("mRespMainPacket.isError:" + mRespMainPacket.isError);
        Debug.Log("mRespMainPacket.payload:" + mRespMainPacket.payload);
    }

    private IEnumerator InitialProcess()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Login", LoadSceneMode.Additive);
    }
}
