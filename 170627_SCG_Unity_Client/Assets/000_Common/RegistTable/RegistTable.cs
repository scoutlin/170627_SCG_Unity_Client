using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistTable
{
    public class CommonDate
    {
        public static bool reqRSAKeyComplete = false;

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
