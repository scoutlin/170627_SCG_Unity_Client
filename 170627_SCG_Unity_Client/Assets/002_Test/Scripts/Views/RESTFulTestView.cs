using SCG_Unity_Client_API;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class RESTFulTestView : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        RegistTable.Instance.mView.mRESTFulTestView = this;
        Debug.Log("Yeah");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public NetModel.NetType mNetType;

    public void OnSetNetTypeButtonClick()
    {
        NetAPIModelModel.GetInstance().SetNetType(this.mNetType);
    }

    public void OnHelloWorldButtonClick()
    {



        StartCoroutine(Coroutine_OnHelloWorldButtonClick());
        


        //PacketStruct.Package_HelloWorld mPackage_HelloWorld = new PacketStruct.Package_HelloWorld();
        //mPackage_HelloWorld.count = 0;
        //mPackage_HelloWorld.text = string.Empty;


        //string mRESP_JSON = NetAPIModelModel.GetInstance().HelloWorldTest(mPackage_HelloWorld);

        //mPackage_HelloWorld = JsonUtility.FromJson<PacketStruct.Package_HelloWorld>(mRESP_JSON);

        //Debug.Log("count: " + mPackage_HelloWorld.count);
        //Debug.Log("text: " + mPackage_HelloWorld.text);
    }

    public IEnumerator Coroutine_OnHelloWorldButtonClick()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add( new MultipartFormDataSection("field1=foo&field2=bar") );
        //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        PacketStruct.Package_Version mPackage_Version = new PacketStruct.Package_Version();
        mPackage_Version.version = "1.0";
        mPackage_Version.bundle = "1";

        string json = JsonUtility.ToJson(mPackage_Version);

        formData.Add(new MultipartFormDataSection("HEADER", "ServerVersion"));
        formData.Add(new MultipartFormDataSection("JSON", json));

        UnityWebRequest mUWR = UnityWebRequest.Post("http://10.10.10.171:3000/ServerVersion", formData);
        yield return mUWR.Send();

        byte[] byte_RESP_param = mUWR.downloadHandler.data;
        string mRESP_JSON = Encoding.UTF8.GetString(byte_RESP_param);

        Debug.Log("mRESP_JSON: " + mRESP_JSON);
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
