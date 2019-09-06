using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {
	public static string id = "";


	// Use this for initialization
	void Start () {
		//网络监听
		NetManager.AddEventListener(NetManager.NetEvent.Close, OnConnectClose);
		NetManager.AddMsgListener("MsgKick", OnMsgKick);
		//初始化
		PanelManager.Init();
		BattleManager.Init();
		//打开登陆面板
		PanelManager.Open<LoginPanel>();
		/*用于单机测试
		GameMain.id = "cat";
		TankInfo tankInfo = new TankInfo();
		tankInfo.camp = 1;
		tankInfo.id = GameMain.id;
		tankInfo.hp = 30;
		tankInfo.x = 262;
		tankInfo.y = -8;
		tankInfo.z = 342;
		BattleManager.GenerateTank(tankInfo);
		PanelManager.Open<BattlePanel>();
		PanelManager.Open<AimPanel>();

		TankInfo tankInfo2 = new TankInfo();
		tankInfo2.camp = 2;
		tankInfo2.id = "dog";
		tankInfo2.hp = 100;
		tankInfo2.z = 30;
		tankInfo2.y = 5;
		tankInfo2.ey = 130;
		BattleManager.GenerateTank(tankInfo2);
		*/
	}


	// Update is called once per frame
	void Update () {
		NetManager.Update();
	}

	//关闭连接
	void OnConnectClose(string err){
		Debug.Log("断开连接");
	} 

	//被踢下线
	void OnMsgKick(MsgBase msgBase){
		PanelManager.Open<TipPanel>("被踢下线");
	}
}
