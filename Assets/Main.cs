using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public GameObject block;
	public GameObject tool;

	// Use this for initialization
	void Start () {
		GameObject t = (GameObject)Instantiate (block, new Vector3(1.1f, 2.2f, 4.5f), Quaternion.identity);
		t.name = "test";	
		GameObject g = (GameObject)Instantiate (tool, new Vector3(1,1,1), Quaternion.identity);
		g.name = "tool";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
