using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public GameObject block;
	public GameObject tool;
	public Texture2D crosshairTexture;
	public float crosshairScale = 1;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		GameObject t = (GameObject)Instantiate (block, new Vector3(1.1f, 2.2f, 4.5f), Quaternion.identity);
		t.name = "test";	
		GameObject g = (GameObject)Instantiate (tool, new Vector3(1,1,1), Quaternion.identity);
		g.name = "tool";
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.visible = false;
	}

	void OnGUI(){
		//if not paused
		if(Time.timeScale != 0)
		{
			if(crosshairTexture!=null)
				GUI.DrawTexture(new Rect((Screen.width-crosshairTexture.width*crosshairScale)/2 ,(Screen.height-crosshairTexture.height*crosshairScale)/2, crosshairTexture.width*crosshairScale, crosshairTexture.height*crosshairScale),crosshairTexture);
			else
				Debug.Log("No crosshair texture set in the Inspector");
		}
	}
}
