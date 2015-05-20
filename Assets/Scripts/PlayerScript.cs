using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	//Game Objects
	public Camera cam;
	public GameObject g;
	public GameObject cube;
	public GameObject bullet_circle;
	public GameObject bullet_square;

	//menu Game Objects
	/*public GameObject menu_default;
	public GameObject menu_guns;
	public GameObject menu_blocks;*/

	//Vectors
	public Vector3 focusBlock;
	private Vector3 lastFocusBlock;
	public float speed = 0.1f;

	//counters
	public int bulletCount;
	public int blockCount = 0;

	//Block storage?
	public Dictionary<point3D,GameObject>  map = new Dictionary<point3D,GameObject>();

	//Mouse Crap
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 10F;
	public float sensitivityY = 10F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0F;
	
	//pngs
	public Texture2D crosshairTexture;

	//booleans
	public bool canPress = true; //disables walking ability
	public bool canDrawCH = true; //enables crosshair drawing
	public bool canDrawMenu = true; //Enables menu drawing
	public bool canJump = true; //Enables player jump movement when they rreturn to ground
	public bool canShoot = true; //disable shooting when in menu mode
	public bool leftShift = false;


	public string currentState = "red_gun";

	
	// Use this for initialization
	void Start () {
		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		canJump = true;

		bulletCount = 0;
		Cursor.visible = false; //no curser bitch!
		canDrawMenu = true;
		canShoot = true;
		/*GameObject d = (GameObject) Instantiate(menu_default, new Vector3(-10,-10,-10), cam.transform.rotation);
		GameObject g = (GameObject) Instantiate(menu_guns, new Vector3(-10,-10,-10), cam.transform.rotation);
		GameObject b = (GameObject) Instantiate(menu_blocks, new Vector3(-10,-10,-10), cam.transform.rotation);
		d.name = "menu_default";
		g.name = "menu_guns";
		b.name = "menu_blocks";*/
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.visible = false;
		//mouse stuff
		if (axes == RotationAxes.MouseX && canPress) {
			cam.transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);
		} else if (axes == RotationAxes.MouseXAndY && canPress) {
			float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;

			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
		} else if (canPress) {
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			cam.transform.localEulerAngles = new Vector3 (-rotationY, transform.localEulerAngles.y, 0);
		}
		
		Vector3 forward = cam.transform.forward;
		forward.y = 0; // this should be changed to being relative to the ground. later.
		if (Input.GetKey (KeyCode.W) && canPress) {
			this.transform.position = this.transform.position + forward * speed;
		}
		if (Input.GetKey (KeyCode.S) && canPress) {
			this.transform.position = this.transform.position - forward * speed;
		}
		if (Input.GetKey (KeyCode.D) && canPress) {
			//transform.Rotate (Vector3.up * Time.deltaTime * 100);
			this.transform.Translate (Vector3.right * 0.1f);
		}
		if (Input.GetKey (KeyCode.A) && canPress) {
			//transform.Rotate (Vector3.down * Time.deltaTime * 100);
			this.transform.Translate (Vector3.left * 0.1f);
		}
//		if (Input.GetKey (KeyCode.Mouse0) && focusBlock != lastFocusBlock) {
//			//ideally check if other cubes exist/check map, but for now. make sure to only make one cube/ click
//			lastFocusBlock = focusBlock;
//			cube.layer = LayerMask.NameToLayer("default");
//			cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//			cube.name = "block" + blockCount; //doesn't work - should be named based on placement
//			cube.layer = LayerMask.NameToLayer("Ignore Raycast");
//			//count the number of blocks on screen for naming
//			blockCount++;
//			//keep track of block coordinates
//			point3D point = new point3D((int)focusBlock.x,(int)focusBlock.y, (int)focusBlock.z);
//			map.Add(point, cube); //TODO: pushing the wrong values
//			//Debug.Log(map[blockCount-1].ToString());
//
//		}
//		if (Input.GetKey (KeyCode.Mouse1)) {
//			GameObject.Destroy(map[new point3D((int)focusBlock.x,(int)focusBlock.y,(int)focusBlock.z)]);
//		}
		if (Input.GetKey (KeyCode.Space) && canJump) {
			GetComponent <Rigidbody> ().AddForce (Vector3.up * 250f);
			canJump = false;
		}
//		Ray ray = this.cam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
//		RaycastHit[] hits = Physics.RaycastAll (ray, 5);
//		if (hits.Length > 0) {
//
//			//normalize point
//			focusBlock = hits [hits.Length - 1].point;
//			focusBlock.x = Mathf.Round (focusBlock.x);
//			focusBlock.y = Mathf.Floor ((focusBlock.y + 1));
//			focusBlock.z = Mathf.Round (focusBlock.z);
//			cube.transform.position = focusBlock;
//		} else {
//			ray = this.cam.ViewportPointToRay (new Vector3 (0.7f, 0.5f, 0));
//			hits = Physics.RaycastAll (ray, 5);
//			if (hits.Length > 0) {
//				
//				//normalize point
//				focusBlock = hits [hits.Length - 1].point;
//				focusBlock.x = Mathf.Round (focusBlock.x);
//				focusBlock.y = Mathf.Floor ((focusBlock.y + 1));
//				focusBlock.z = Mathf.Round (focusBlock.z);
//				
//				cube.transform.position = focusBlock;
//			}
//		}
		//FIRES GUN 
		if (Input.GetKey (KeyCode.Mouse0) && canShoot) {
			Vector3 aim = new Vector3 (GameObject.Find ("marker_front").transform.position.x - GameObject.Find ("marker_back").transform.position.x, 
			                          GameObject.Find ("marker_front").transform.position.y - GameObject.Find ("marker_back").transform.position.y,
			                          GameObject.Find ("marker_front").transform.position.z - GameObject.Find ("marker_back").transform.position.z);
			GameObject b = (GameObject)Instantiate (bullet_circle, new Vector3 (GameObject.Find ("marker_front").transform.position.x, GameObject.Find ("marker_front").transform.position.y, GameObject.Find ("marker_front").transform.position.z), Quaternion.identity);
			b.name = "bullet" + bulletCount;
			b.SendMessage ("setAim", aim);
			b.SendMessage ("setId", bulletCount);
			bulletCount++;
		}
		//RYANTODO: aim down the sights
		if (Input.GetKey (KeyCode.E)) {
			Vector3 a = new Vector3 (GameObject.Find ("marker_front").transform.position.x - GameObject.Find ("marker_back").transform.position.x, 
			                          GameObject.Find ("marker_front").transform.position.y - GameObject.Find ("marker_back").transform.position.y,
			                          GameObject.Find ("marker_front").transform.position.z - GameObject.Find ("marker_back").transform.position.z);
			//change cams rotation and position 
			cam.transform.rotation = Quaternion.LookRotation (a);
			cam.transform.position = new Vector3(GameObject.Find("marker_back").transform.position.x,GameObject.Find("marker_back").transform.position.y + 0.5f,GameObject.Find("marker_back").transform.position.z);
			canDrawCH = false;
			//cam.transform.position.y = GameObject.Find("marker_back").transform.position.y + 0.25f;
		} else {
			//return to old view
			cam.transform.rotation = Quaternion.LookRotation (this.transform.forward);
			cam.transform.position = this.transform.position;
			canDrawCH = true;
		}
	


		//RYANTODO: weapon toggle gui - handled by menu registry hopefuly
		/*if (Input.GetKey (KeyCode.Q)) {
			canPress = false;
			canShoot = false;
			Vector3 distCheck = this.transform.position - GameObject.Find ("menu_default").transform.position;

			if(canDrawMenu){
				//instantiate weapons menu
				GameObject.Find ("menu_default").transform.position = cam.transform.position + cam.transform.forward;
				GameObject.Find ("menu_default").transform.rotation = cam.transform.rotation;
				GameObject.Find ("menu_guns").transform.position = cam.transform.position + cam.transform.forward + cam.transform.right * 0.25f + cam.transform.up * 0.125f;
				GameObject.Find ("menu_guns").transform.rotation = cam.transform.rotation;
				GameObject.Find ("menu_blocks").transform.position = cam.transform.position + cam.transform.forward + cam.transform.right * 0.5f + cam.transform.up * 0.125f;
				GameObject.Find ("menu_blocks").transform.rotation = cam.transform.rotation;
				GameObject.Find("gun_basic").GetComponent<MeshRenderer>().enabled = false;
			}

			if(distCheck.magnitude < 5){
				canDrawMenu = false;
			}
			//RYANTODO: make only columns go up down when selected, make states, change guns
			if (Input.GetKeyDown (KeyCode.W)) {
				//select up menu item
				//GameObject.Find ("menu_default").transform.position = GameObject.Find ("menu_default").transform.position  - cam.transform.up * 0.25f;
				GameObject.Find ("menu_guns").transform.position =  GameObject.Find ("menu_guns").transform.position - cam.transform.up * 0.25f;
				GameObject.Find ("menu_blocks").transform.position = GameObject.Find ("menu_blocks").transform.position - cam.transform.up * 0.25f;
			} else if (Input.GetKeyDown (KeyCode.S)) {
				//
				//GameObject.Find ("menu_default").transform.position = GameObject.Find ("menu_default").transform.position  + cam.transform.up * 0.25f;
				GameObject.Find ("menu_guns").transform.position =  GameObject.Find ("menu_guns").transform.position + cam.transform.up * 0.25f;
				GameObject.Find ("menu_blocks").transform.position = GameObject.Find ("menu_blocks").transform.position + cam.transform.up * 0.25f;
			} else if (Input.GetKeyDown (KeyCode.A)) {
				//
				GameObject.Find ("menu_default").transform.position = GameObject.Find ("menu_default").transform.position  + cam.transform.right * 0.25f;
				GameObject.Find ("menu_guns").transform.position =  GameObject.Find ("menu_guns").transform.position + cam.transform.right * 0.25f;
				GameObject.Find ("menu_blocks").transform.position = GameObject.Find ("menu_blocks").transform.position + cam.transform.right * 0.25f;
			} else if (Input.GetKeyDown (KeyCode.D)) {
				//leftShift = true;
				GameObject.Find ("menu_default").transform.position = GameObject.Find ("menu_default").transform.position  - cam.transform.right * 0.25f;
				GameObject.Find ("menu_guns").transform.position =  GameObject.Find ("menu_guns").transform.position - cam.transform.right * 0.25f;
				GameObject.Find ("menu_blocks").transform.position = GameObject.Find ("menu_blocks").transform.position - cam.transform.right * 0.25f;
			}
		} else {
			GameObject.Find ("menu_default").transform.position = new Vector3(-10,-10,-10);
			GameObject.Find ("menu_guns").transform.position = new Vector3(-10,-10,-10);
			GameObject.Find ("menu_blocks").transform.position = new Vector3(-10,-10,-10);
			GameObject.Find("gun_basic").GetComponent<MeshRenderer>().enabled = true;
			canPress = true;
			canShoot = true;
			canDrawMenu = true;
			//leftShift = false;
		}*/
	}

	void OnCollisionEnter(Collision c){
		if(c.gameObject.tag == "floor"){
			canJump = true;

		}
	}

	void OnGUI(){
		//if not paused
		if (Time.timeScale != 0) {
			if (crosshairTexture != null && canDrawCH)
				GUI.DrawTexture (new Rect ((Screen.width - crosshairTexture.width * 1) / 2, (Screen.height - crosshairTexture.height * 1) / 2, crosshairTexture.width * 1, crosshairTexture.height * 1), crosshairTexture);
		}
	}

	void setTrue (string b){
		switch (b){
		case "canPress" :
			if(!canPress) canPress = true;
			break;

		case "canShoot" :
			if(!canShoot) canShoot = true;
			break;
		}
	}

	void setFalse (string b){
		switch (b){
		case "canPress" :
			if(canPress) canPress = false;
			break;
			
		case "canShoot" :
			if(canShoot) canShoot = false;
			break;
		}
	}

}
