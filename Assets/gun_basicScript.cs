﻿using UnityEngine;
using System.Collections;

public class gun_basicScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
