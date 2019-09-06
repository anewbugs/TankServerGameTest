using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class BaseTank : MonoBehaviour {
    //属于哪一名玩家
    public string id = "";
    //阵营
    public int camp = 0;
    //坦克模型
    private GameObject skin;
	//刚体属性
	public Rigidbody rb;
	//转向速度
	public float steer = 180;
	//移动速度
	public float speed = 15f;
	//炮塔旋转速度
	//public float turretSpeed = 30f;
	//炮塔
	//public Transform turret;
	//炮管
	//public Transform gun;
	//发射点
	public Transform firePoint;
	//炮弹Cd时间
	public float fireCd = 0.5f;
	//上一次发射炮弹的时间
	public float lastFireTime = 0;
	//生命值
	public float hp = 100;
	//Canvas幕布
	public Transform canvas;
	//血槽
	public Slider slider;

	// Use this for initialization
	public void Start () {

	}

	//初始化
	public virtual void Init(string skinPath){
		//皮肤
		GameObject skinRes = ResManager.LoadPrefab(skinPath);
		skin = (GameObject)Instantiate(skinRes);
		skin.transform.parent = this.transform;
		skin.transform.localPosition = Vector3.zero;
		rb = skin.GetComponent<Rigidbody> ();
		//物理
		rb = gameObject.AddComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationZ;
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.center = new Vector3(0, 0.85f, 0);
		boxCollider.size = new Vector3(1.5f, 1.7f, 1.6f);
		//血槽组件获取
		canvas = skin.transform.Find("Canvas");
		slider = canvas.transform.Find("HealthSlider").GetComponent<Slider>();
		//日志
		Debug.Log (DateTime.Now.ToString()+": Tank Init, Data:{name = "+this.name+",health = " +slider.value.ToString() +"}");
		//炮塔炮管
		//turret = skin.transform.Find("Turret");
		//gun = turret.transform.Find("Gun");
		firePoint =skin.transform.Find("FirePosition"); //gun.transform.Find("FirePoint");
	}

	//发射炮弹
	public Bullet Fire(){
		//已经死亡
		if(IsDie()){
			
			return null;
		}
		//产生炮弹
		GameObject bulletObj = new GameObject("bullet");
		Bullet bullet = bulletObj.AddComponent<Bullet>();
		bullet.Init();
		bullet.tank = this;
		//位置
		bullet.transform.position = firePoint.position;
		bullet.transform.rotation = firePoint.rotation;
		//更新时间
		lastFireTime = Time.time;
        return bullet;
	}

	//是否死亡
	public bool IsDie(){
		return hp <= 0;
	}

	//被攻击
	public void Attacked(float att){
		//已经死亡
		if(IsDie()){
			skin.SetActive (false);
			return;
		}
		//扣血
		hp -= att;
		slider.value -= att;
		//日志
		Debug.Log (DateTime.Now.ToString()+": Tank Attacked, Data:{name = "+this.name+",health = " +slider.value.ToString() +"}");
		//死亡
		if(IsDie()){
			//显示焚烧效果
			GameObject explode = ResManager.LoadPrefab("explosion");
			Instantiate(explode, transform.position, transform.rotation);
			//日志
			Debug.Log (DateTime.Now.ToString()+": Tank Died, Data:{name = "+this.name+",health = " +slider.value.ToString() +"}");
		}
	}


	// Update is called once per frame
	public void Update () {

	}
}
