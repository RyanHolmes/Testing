using UnityEngine;
using System.Collections;

public class bullet_basic_script : MonoBehaviour {

	public Vector3 aim;
	public int id;
	public float TTL;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.color = Color.white;
		if (aim != null) {
			GetComponent<Rigidbody> ().AddForce (aim * 1000f);
		}

	}
	
	// Update is called once per frame
	void Update () {
		//if(Time.time - TTL > 5.0f){
		//	Destroy(GameObject.Find("bullet" + id));
		//}
	}

	void setAim(Vector3 a){
		aim = a;
	}

	void setId(int i){
		id = i;
	}

	void Awake(){
		Destroy(this, 1);
	}
}
