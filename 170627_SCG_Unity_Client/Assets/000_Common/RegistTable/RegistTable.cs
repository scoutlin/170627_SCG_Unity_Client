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
            public static bool reqRegistMemberComplete = false;
        }

        public class Variables
        {
            
        }
    }

    public class View
    {
        public RESTFulTestView mRESTFulTestView = null;
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
