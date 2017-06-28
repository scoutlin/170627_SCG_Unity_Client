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
            None,
            Socket,
            WebSocket,
            SocketIO,
            Http,
            Https,
            Http2
        }

        private static NetType mNetType = NetType.None;
        private static NetModel mNetModel = null;

        public static NetModel GetInstance(NetType netType)
        {
            if (netType == NetType.None)
            {
                return null;
            }
            else
            {
                mNetType = netType;

                if (mNetModel == null)
                {
                    mNetModel = new NetModel();
                }

                return mNetModel;
            }
        }

        public bool RESTFulGET(string url, string JSON)
        {
            bool rt = false;

            using (WebClient mWebClient = new WebClient())
            {
                ////No Form
                ////Start
                //string mRESP_JSON = mWebClient.UploadString(url, JSON);
                ////End

                //Form
                //Start
                NameValueCollection mREQ_param = new NameValueCollection();
                mREQ_param.Add("JSON", JSON);
                byte[] byte_RESP_param = mWebClient.UploadValues(url, mREQ_param);
                string mRESP_JSON = Encoding.UTF8.GetString(byte_RESP_param);
                //End
            }

            return rt;
        }

        public bool RESTFulPOST(string url, string JSON)
        {
            bool rt = false;

            return rt;
        }
    }
}