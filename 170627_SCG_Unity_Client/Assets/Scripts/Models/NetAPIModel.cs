using NetModelNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketStructNamespace;

namespace NetAPIModelNamespace
{
    public class NetAPIModelModel
    {
        private static NetAPIModelModel mNetAPIModelModel = null;

        public static NetAPIModelModel GetInstance()
        {
            if(mNetAPIModelModel == null)
            {
                mNetAPIModelModel = new NetAPIModelModel();
            }

            return mNetAPIModelModel;
        }

        public void SetNetType(NetModel.NetType netType)
        {
            NetModel.SetNetType(netType);
        }


        public string HelloWorldTest(PacketStruct.Package_HelloWorld mPackage_HelloWorld)
        {
            string mRESP_JSON = string.Empty;

            //Call RESTFul
            string url = @"http://localhost:3000/HelloWorld";
            //url = "http://10.10.10.171:3000/HelloWorld";
            string header = "HelloWorld";
            string json = JsonUtility.ToJson(mPackage_HelloWorld);

            mRESP_JSON = NetModel.GetInstance().RESTFulPOST(url, header, json);

            return mRESP_JSON;
        }

        public string GetServerVersion(PacketStruct.Package_Version mPackage_Version)
        {
            string mREST_JSON = string.Empty;

            //Call RESTFul
            string url = @"http://localhost:3000/";
            url = "http://10.10.10.171:3000/ServerVersion";
            string header = "ServerVersion";
            string json = JsonUtility.ToJson(mPackage_Version);

            mREST_JSON = NetModel.GetInstance().RESTFulPOST(url, header, json);

            return mREST_JSON;
        }
    }
}
