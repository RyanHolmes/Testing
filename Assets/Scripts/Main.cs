using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public GameObject block;
	public GameObject tool;
	public MenuRegistry mr; //global menu registry
	//public Texture2D crosshairTexture;
	//public float crosshairScale = 1;

	// Use this for initialization
	void Start () {
		//Cursor.visible = false;
		GameObject t = (GameObject)Instantiate (block, new Vector3(1.1f, 2.2f, 4.5f), Quaternion.identity);
		t.name = "test";	
		GameObject g = (GameObject)Instantiate (tool, new Vector3(1,1,1), Quaternion.identity);
		g.name = "tool";

		mr = new MenuRegistry ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
