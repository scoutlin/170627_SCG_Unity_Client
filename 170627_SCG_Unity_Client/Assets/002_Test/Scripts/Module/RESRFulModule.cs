using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEngine;

public class RESRFulModule : MonoBehaviour
{
    public static void DotNetVersionRESTFul(int count)
    {
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["HEADER"] = "ServerVersion";
            values["JSON"] = count.ToString();

            var response = client.UploadValues("http://localhost:3000/ServerVersion", values);

            var responseString = Encoding.Default.GetString(response);
        }
    }
}
