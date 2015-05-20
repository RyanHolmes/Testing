using UnityEngine;
using System.Collections;

public class gun_basicScript : MonoBehaviour {

	public static Color col;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.color = Color.red;
		col = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject.GetComponent<Renderer> ().material.color = col;
	}

	void colorChange(Color c){
		col = c;
	}
}
