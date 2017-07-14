﻿using SCG_Unity_Client_API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketPaserModule
{
    private static PacketPaserModule mPacketPaserModule;

    public static PacketPaserModule Instance
    {
        get
        {
            if(mPacketPaserModule == null)
            {
                mPacketPaserModule = new PacketPaserModule();
            }

            return mPacketPaserModule;
        }
    }

    public void ProcessParser(PacketStruct.EnumCmd enumCmd, string json)
    {
        switch(enumCmd)
        {
            default:
                {

                }
                break;

            case PacketStruct.EnumCmd.EGS_Router_GetKey:
                {
                    Debug.Log("PacketParserModule - EGS_Router_GetKey");
                }
                break;

            case PacketStruct.EnumCmd.EGS_Router_GetToken:
                {
                    Debug.Log("PacketParserModule - EGS_Router_GetToken");
                }
                break;
        }
    }


}
