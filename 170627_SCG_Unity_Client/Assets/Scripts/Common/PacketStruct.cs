using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketStructNamespace
{
    public class PacketStruct
    {
        public class Package_HelloWorld
        {
            public int count = 0;
            public string text = string.Empty;
        }

        public class Package_Version
        {
            public string version = string.Empty;
            public string bundle = string.Empty;
        }
    }
}