using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (this.transform.position.x - this.transform.position.x%1, this.transform.position.y - this.transform.position.y%1, this.transform.position.z -  this.transform.position.z%1);
	}
}
