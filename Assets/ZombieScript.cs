using UnityEngine;
using System.Collections;
using System;

public class ZombieScript : MonoBehaviour {
	
	public GameObject player;
	
	// Use this for initialization
	void Start () {
		this.GetComponent<Renderer>().material.color = Color.magenta;
		
		InvokeRepeating ("moveToPlayer", 0, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void moveToPlayer() {
		Vector3 myPos = this.transform.position;
		Vector3 playerPos = player.transform.position;
		
		int dx = (int)(playerPos.x - myPos.x);
		int dz = (int)(playerPos.z - myPos.z);
		
		if (Math.Abs (dx) > 1) {
			if (dx > 0) {
				this.transform.Translate (Vector3.right); 
			} else if (dx < 0) { 
				this.transform.Translate (Vector3.left); 
			}
		}
		
		if (Math.Abs (dz) > 1) {
			if (dz > 0) {
				this.transform.Translate (Vector3.forward);
			} else if (dz < 0) {
				this.transform.Translate (Vector3.back);
			}
		}
	}
}
