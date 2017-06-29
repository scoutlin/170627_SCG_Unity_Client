using NetAPIModelNamespace;
using NetModelNamespace;
using PacketStructNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RESTFulTestView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public NetModel.NetType mNetType;

    public void OnSetNetTypeButtonClick()
    {
        NetAPIModelModel.GetInstance().SetNetType(this.mNetType);
    }

    public void OnHelloWorldButtonClick()
    {
        PacketStruct.Package_HelloWorld mPackage_HelloWorld = new PacketStruct.Package_HelloWorld();
        mPackage_HelloWorld.count = 0;
        mPackage_HelloWorld.text = string.Empty;


        string mRESP_JSON = NetAPIModelModel.GetInstance().HelloWorldTest(mPackage_HelloWorld);

        mPackage_HelloWorld = JsonUtility.FromJson<PacketStruct.Package_HelloWorld>(mRESP_JSON);

        Debug.Log("count: " + mPackage_HelloWorld.count);
        Debug.Log("text: " + mPackage_HelloWorld.text);
    }

    public void OnGetServerVersionButtonClick()
    {
        PacketStruct.Package_Version mPackage_Version = new PacketStruct.Package_Version();
        mPackage_Version.version = "testVersion";
        mPackage_Version.bundle = "testBundle";


        string mRESP_JSON = NetAPIModelModel.GetInstance().GetServerVersion(mPackage_Version);

        mPackage_Version = JsonUtility.FromJson<PacketStruct.Package_Version>(mRESP_JSON);

        Debug.Log("version: " + mPackage_Version.version);
        Debug.Log("Bundle: " + mPackage_Version.bundle);
    }
}
