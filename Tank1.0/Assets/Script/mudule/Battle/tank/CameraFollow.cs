using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	//距离矢量
	public Vector3 distance = new Vector3(0, 8, -18);
	//相机
	public Camera camera;
	//相机移动速度
	public float speed = 3f;

	// Use this for initialization
	void Start () {
		//默认为主相机
		camera = Camera.main;
	}

	//所有组件update之后发生
	void LateUpdate () {
		//坦克位置
		Vector3 pos = transform.position;
		//坦克方向
		Vector3 forward = transform.forward;
		//相机相对与目标位置
		Vector3 targetPos = new Vector3(0,15,-15);
		targetPos.y += distance.y;
		//相机位置
		//Vector3 cameraPos = camera.transform.position;
		//cameraPos = targetPos + pos;
		camera.transform.position = targetPos + pos;
		camera.transform.Rotate (forward + new Vector3(30,0,0));
		//对准坦克
		Camera.main.transform.LookAt(pos);
	}
}



