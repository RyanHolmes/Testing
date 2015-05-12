using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public Camera cam;
	public GameObject g;
	public GameObject cube;
	public Vector3 focusBlock;
	public float speed = 0.1f;

	public bool canJump;
	public int blockCount = 0;

	public Dictionary<point3D,GameObject>  map = new Dictionary<point3D,GameObject>();

	private Vector3 lastFocusBlock;

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 10F;
	public float sensitivityY = 10F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationY = 0F;

	
	
	// Use this for initialization
	void Start () {
		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		canJump = true;
	}
	
	// Update is called once per frame
	void Update () {
		//mouse stuff
		if (axes == RotationAxes.MouseX){
			cam.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else if (axes == RotationAxes.MouseXAndY){
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
		else{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			cam.transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
		
		Vector3 forward = cam.transform.forward;
		forward.y = 0; // this should be changed to being relative to the ground. later.
		if (Input.GetKey (KeyCode.W)) {
			this.transform.position = this.transform.position + forward * speed;
		}
		if (Input.GetKey (KeyCode.S)) {
			this.transform.position = this.transform.position - forward *speed;
		}
		if (Input.GetKey (KeyCode.D)) {
			//transform.Rotate (Vector3.up * Time.deltaTime * 100);
			this.transform.Translate(Vector3.right * 0.1f);
		}
		if (Input.GetKey (KeyCode.A)) {
			//transform.Rotate (Vector3.down * Time.deltaTime * 100);
			this.transform.Translate(Vector3.left * 0.1f);
		}
		if (Input.GetKey (KeyCode.Mouse0) && focusBlock != lastFocusBlock) {
			//ideally check if other cubes exist/check map, but for now. make sure to only make one cube/ click
			lastFocusBlock = focusBlock;
			cube.layer = LayerMask.NameToLayer("default");
			cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.name = "block" + blockCount; //doesn't work - should be named based on placement
			cube.layer = LayerMask.NameToLayer("Ignore Raycast");
			//count the number of blocks on screen for naming
			blockCount++;
			//keep track of block coordinates
			point3D point = new point3D((int)focusBlock.x,(int)focusBlock.y, (int)focusBlock.z);
			map.Add(point, cube); //TODO: pushing the wrong values
			//Debug.Log(map[blockCount-1].ToString());

		}
		if (Input.GetKey (KeyCode.Mouse1)) {
			GameObject.Destroy(map[new point3D((int)focusBlock.x,(int)focusBlock.y,(int)focusBlock.z)]);
		}
		if (Input.GetKey (KeyCode.Space) && canJump) {
			GetComponent <Rigidbody>().AddForce(Vector3.up * 250f);
			canJump = false;
		}
		Ray ray = this.cam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
		RaycastHit[] hits = Physics.RaycastAll (ray, 5);
		if (hits.Length > 0) {

			//normalize point
			focusBlock = hits [hits.Length - 1].point;
			focusBlock.x = Mathf.Round (focusBlock.x);
			focusBlock.y = Mathf.Floor ((focusBlock.y + 1));
			focusBlock.z = Mathf.Round (focusBlock.z);
			cube.transform.position = focusBlock;
		} else {
			ray = this.cam.ViewportPointToRay (new Vector3 (0.7f, 0.5f, 0));
			hits = Physics.RaycastAll (ray, 5);
			if (hits.Length > 0) {
				
				//normalize point
				focusBlock = hits [hits.Length - 1].point;
				focusBlock.x = Mathf.Round (focusBlock.x);
				focusBlock.y = Mathf.Floor ((focusBlock.y + 1));
				focusBlock.z = Mathf.Round (focusBlock.z);
				
				cube.transform.position = focusBlock;
			}
		}
	}

	void OnCollisionEnter(Collision c){
		if(c.gameObject.tag == "floor"){
			canJump = true;
		}
	}
}
