﻿using System;
using UnityEngine;
using System.Linq;

public class MsgBase{
	public string protoName = "null";
    public static Int16 msgLength= 1;
	//编码
	public static byte[] Encode(MsgBase msgBase){
		string s = JsonUtility.ToJson(msgBase); 
		return System.Text.Encoding.UTF8.GetBytes(s);
	}

	//解码
	public static MsgBase Decode(string protoName, byte[] bytes, int offset, int count){
		string s = System.Text.Encoding.UTF8.GetString(bytes, offset, count);
		Debug.Log("Debug decode:" + s);
		MsgBase msgBase = (MsgBase)JsonUtility.FromJson(s, Type.GetType(protoName));
		return msgBase;
	}

    //编码协议名（2字节长度+字符串）
    /*改成msgLength个字节长度
     *time 20msgLength9.9.6 5:00
     **/
    public static byte[] EncodeName(MsgBase msgBase){
		//名字bytes和长度
		byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(msgBase.protoName);
		Int16 len = (Int16)nameBytes.Length;
        //申请bytes数值
        //byte[] bytes = new byte[2+len];
        byte[] bytes = new byte[msgLength + len];
        //组装2字节的长度信息
        //bytes[0] = (byte)(len%256);
        //bytes[msgLength] = (byte)(len/256);
        bytes[0] = (byte)len;
        //组装名字bytes
        //Array.Copy(nameBytes, 0, bytes, 2, len);
        Array.Copy(nameBytes, 0, bytes, msgLength, len);
        return bytes;
	}

	//解码协议名（2字节长度+字符串）
    /*
     * msgLength字节表示长度
     */
	public static string DecodeName(byte[] bytes, int offset, out int count){
		count = 0;
        /*//必须大于2字节
		if(offset + 2 > bytes.Length)*/
        if(offset + msgLength > bytes.Length)
        {
			return "";
		}
		//读取长度
		Int16 len = (Int16)(bytes[offset] );
        //长度必须足够
        /*if(offset + 2 + len > bytes.Length)*/
        if (offset + msgLength + len > bytes.Length)
        {
			return "";
		}
        //解析
        //count = 2+len;
        count = msgLength + len;
        //string name = System.Text.Encoding.UTF8.GetString(bytes, offset+2, len);
        string name = System.Text.Encoding.UTF8.GetString(bytes, offset + msgLength, len);
        return name;
	}
}


