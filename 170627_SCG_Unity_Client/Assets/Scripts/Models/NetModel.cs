using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEngine;

namespace NetModelNamespace
{
    public class NetModel
    {
        public enum NetType
        {
            Null,
            Socket,
            WebSocket,
            SocketIO,
            Http,
            Https,
            Http2
        }

        private static NetType mNetType = NetType.Null;
        private static NetModel mNetModel = null;

        public static void SetNetType(NetType netType)
        {
            mNetType = netType;
        }

        public static NetModel GetInstance()
        {
            if (mNetType == NetType.Null)
            {
                return null;
            }
            else
            {
                if (mNetModel == null)
                {
                    mNetModel = new NetModel();
                }

                return mNetModel;
            }
        }

        public string RESTFulGET(string url, string header, string json)
        {
            string mRESP_JSON = string.Empty;

            using (WebClient mWebClient = new WebClient())
            {
                ////No Form
                ////Start
                //string mRESP_JSON = mWebClient.UploadString(url, JSON);
                ////End

                //Form
                //Start
                NameValueCollection mREQ_param = new NameValueCollection();
                mREQ_param.Add("HEADER", header);
                mREQ_param.Add("JSON", json);
                byte[] byte_RESP_param = mWebClient.UploadValues(url, mREQ_param);
                mRESP_JSON = Encoding.UTF8.GetString(byte_RESP_param);
                //End
            }

            return mRESP_JSON;
        }

        public string RESTFulPOST(string url, string header, string json)
        {
            string mRESP_JSON = string.Empty;


            Debug.Log("Into RESTFul POST");
            Debug.Log("url: " + url);
            Debug.Log("header: " + header);
            Debug.Log("json: " + json);


            using (WebClient mWebClient = new WebClient())
            {
                ////No Form
                ////Start
                //string mRESP_JSON = mWebClient.UploadString(url, JSON);
                ////End

                //Form
                //Start
                NameValueCollection mREQ_param = new NameValueCollection();
                mREQ_param.Add("HEADER", header);
                mREQ_param.Add("JSON", json);
                byte[] byte_RESP_param = mWebClient.UploadValues(url, mREQ_param);
                
                mRESP_JSON = Encoding.UTF8.GetString(byte_RESP_param);
                //End
            }

            return mRESP_JSON;
        }
    }
}