using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistTable
{
    public class CommonDate
    {
        public class Flags
        {
            public static bool isNeedEncrypt = false;

            public static bool reqRSAKeyComplete = false;
            public static bool reqAESKeyComplete = false;

            public static bool reqAdminRegistComplete = false;
            public static bool reqAdminEditComplete = false;
            public static bool reqAdminDeleteComplete = false;
            public static bool reqAdminLoginComplete = false;
            public static bool reqAdminLogoutComplete = false;

            public static bool reqMemberRegistComplete = false;
            public static bool reqMemberEditComplete = false;
            public static bool reqMemberDeleteComplete = false;
            public static bool reqMemberLoginComplete = false;
            public static bool reqMemberLogoutComplete = false;
        }

        public class Variables
        {
            public static string adminToken = string.Empty;
            public static string memberToken = string.Empty;
        }
    }

    public class View
    {
        public RESTFulTestView mRESTFulTestView = null;
        public LoginView mLoginView = null;
    }

    public static RegistTable mRegistTable = null;

    public View mView;

    public static RegistTable Instance
    {
        get
        {
            if(mRegistTable == null)
            {
                mRegistTable = new RegistTable();
                mRegistTable.mView = new View();
            }
            return mRegistTable;
        }
    }
}
