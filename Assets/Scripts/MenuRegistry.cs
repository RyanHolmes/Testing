using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//RYANTODO: Create all menu items (In Flash)
//RYANTODO: Do bounds checking - can't go left if no item exists
//RYANTODO: test add and delete functions for menu items
//RYANTODO: Toggle modes/blocks/guns and merge with Hannes

public class MenuRegistry : MonoBehaviour{

	public Camera cam; //player camera

	//list of all menu card items
	public List<GameObject> blocks;
	public List<GameObject> guns;
	/*public GameObject menu_default;
	public GameObject menu_delete;
	public GameObject menu_red_gun;
	public GameObject menu_green_gun;
	public GameObject menu_red_block;
	public GameObject menu_green_block;*/
	public string currentMode = "shoot"; //build, delete

	public int lastx = 0; //last postion defaults to default
	public int lasty = 0;
	public int curx = 0; //current centered poistion on the menu - default to begin with (0,0)
	public int cury = 0;

	//constructor
	void Start() {
		blocks = new List <GameObject>();
		guns = new List<GameObject>();
		//set positions of menu items to begin with
		GameObject.Find ("menu_default").SendMessage ("setx", 0);
		GameObject.Find ("menu_default").SendMessage ("sety", 0);

		GameObject.Find ("menu_delete").SendMessage ("setx", 2);
		GameObject.Find ("menu_delete").SendMessage ("sety", -1);
		blocks.Add (GameObject.Find ("menu_delete"));

		GameObject.Find ("menu_red_gun").SendMessage ("setx", 1);
		GameObject.Find ("menu_red_gun").SendMessage ("sety", 0);
		blocks.Add (GameObject.Find ("menu_red_gun"));

		GameObject.Find ("menu_green_gun").SendMessage ("setx", 1);
		GameObject.Find ("menu_green_gun").SendMessage ("sety", 1);
		blocks.Add (GameObject.Find ("menu_green_gun"));

		GameObject.Find ("menu_green_block").SendMessage ("setx", 2);
		GameObject.Find ("menu_green_block").SendMessage ("sety", 1);
		blocks.Add (GameObject.Find ("menu_green_block"));

		GameObject.Find ("menu_red_block").SendMessage ("setx", 2);
		GameObject.Find ("menu_red_block").SendMessage ("sety", 0);
		blocks.Add (GameObject.Find ("menu_red_block"));
	}

	void Update(){
		if (Input.GetKey (KeyCode.Q)) {
			GameObject.Find("Player").SendMessage("setFalse", "canShoot");
			GameObject.Find("Player").SendMessage("setFalse", "canPress");
			GameObject.Find("gun_basic").GetComponent<MeshRenderer>().enabled = false;
			drawMenu();

			if (Input.GetKeyDown (KeyCode.W)) {
				shiftUp();

			} else if (Input.GetKeyDown (KeyCode.S)) {
				shiftDown();

			} else if (Input.GetKeyDown (KeyCode.A)) {
				shiftLeft();

			} else if (Input.GetKeyDown (KeyCode.D)) {
				shiftRight();
			}

		} else {
			hideMenu ();
			GameObject.Find("Player").SendMessage("setTrue", "canShoot");
			GameObject.Find("Player").SendMessage("setTrue", "canPress");
			GameObject.Find("gun_basic").GetComponent<MeshRenderer>().enabled = true; //later to be replaces with "currentgun"
			curx = 0;
			cury = 0;
		}
	}

	//add an item to menu
	public void addMenuItem (string type, string tag){
		switch (type) {

			case "block":
				blocks.Add(GameObject.Find (tag));
				//instantiate menu items far below map, then just move them for convenience - **may be unnecessary**
				//Instantiate(GameObject.Find (tag), new Vector3 (-100, -100, -100), Quaternion.identity);
				break;

			case "gun":
				guns.Add(GameObject.Find (tag));
				//Instantiate(GameObject.Find (tag), new Vector3 (-100, -100, -100), Quaternion.identity);
				break;
		}

	}

	//remove item
	public void removeMenuItem(string type, string tag){
		switch (type) {
			case "block":
				//remove from array list and place under map
				blocks.Remove(GameObject.Find (tag)); 
				GameObject.Find (tag).transform.Translate(new Vector3(-100,-100,-100));
				break;
			
			case "gun":
				guns.Remove(GameObject.Find (tag));
				GameObject.Find (tag).transform.Translate(new Vector3(-100,-100,-100));
				break;
		}
	}

	//draw screen
	public void drawMenu(){
		//NOTE: THIS IS A FUCKSHOW - ASK ME ABOUT IT
		//draw menu items at current position
		//draw menu items at camera position + cam's forward vector + up and right vector * item posiition, * 0.25
		GameObject.Find ("menu_default").transform.position = cam.transform.position + cam.transform.forward + (cam.transform.right * (0.25f * curx)) + (cam.transform.up * (0.25f * cury));// +

		GameObject.Find ("menu_default").transform.rotation = cam.transform.rotation;

		for(int i = 0; i < guns.Count; i++){
			guns[i].transform.position = cam.transform.position + cam.transform.forward + 
			cam.transform.right * (0.25f * (guns[i].GetComponent<MenuItem>().posx + curx)) + cam.transform.up * (0.25f * (guns[i].GetComponent<MenuItem>().posy + cury));

			guns[i].transform.rotation = cam.transform.rotation;
		}

		for(int j = 0; j < blocks.Count; j++){
			blocks[j].transform.position = cam.transform.position + cam.transform.forward + 
			cam.transform.right * (0.25f * (blocks[j].GetComponent<MenuItem>().posx + curx)) + cam.transform.up * (0.25f * (blocks[j].GetComponent<MenuItem>().posy + cury));
			blocks[j].transform.rotation = cam.transform.rotation;
		}
	}

	public void hideMenu(){
		//move items to under screen
		GameObject.Find ("menu_default").transform.position = new Vector3 (-100,-100,-100);

		for(int i = 0; i < guns.Count; i++){
			guns[i].transform.position =  new Vector3 (-100,-100,-100);
		}

		for(int j = 0; j < blocks.Count; j++){
			blocks[j].transform.position = new Vector3 (-100,-100,-100);

		}
		
	}

	//resetMenu - used for respecing probably
	public void resetMenu(){
		blocks.Clear ();
		guns.Clear ();

		//re initialize
		blocks.Add(GameObject.Find ("delete_mode"));
		guns.Add (GameObject.Find ("red_gun"));
		guns.Add(GameObject.Find ("green_gun"));
		blocks.Add(GameObject.Find ("green_block"));
		blocks.Add(GameObject.Find ("red_block"));
	}

	//for movement leftward
	public void shiftLeft() {
		//shift left then draw menu again and update current centered position 
		curx += 1;
		this.drawMenu ();
	}

	//for movement rightward
	public void shiftRight() {
		curx -= 1;
		this.drawMenu ();
	}

	//for movement upward
	public void shiftUp() {
		cury -= 1;
		this.drawMenu ();
	}

	//for movement downward
	public void shiftDown() {
		cury += 1;
		this.drawMenu ();
	}
}
