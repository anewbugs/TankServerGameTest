using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTank : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//坦克
		GameObject tankObj = new GameObject("myTank");
		CtrlTank ctrlTank = tankObj.AddComponent<CtrlTank>();
		ctrlTank.Init("Tank");
		//相机
		tankObj.AddComponent<CameraFollow>();
		//被打的坦克
		GameObject tankObj2 = new GameObject("enemyTank");
		BaseTank baseTank = tankObj2.AddComponent<BaseTank>();
		baseTank.Init("Tank");
		baseTank.transform.position = new Vector3(5, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
