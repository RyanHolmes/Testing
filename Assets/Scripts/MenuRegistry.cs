using UnityEngine;
using System.Collections;

//RYANTODO: Create all menu items (In Flash), use positions variables to draw menu

public class MenuRegistry{

	public Camera cam; //player camera

	//list of all menu card items
	public ArrayList blocks;
	public ArrayList guns;
	public GameObject menu_default;
	public GameObject menu_delete;
	public GameObject menu_red_gun;
	public GameObject menu_green_gun;
	public string currentMode = "default";

	public int lastx = 0; //last postion defaults to default
	public int lasty = 0;
	public int curx = 0; //current centered poistion on the menu - default to begin with (0,0)
	public int cury = 0;

	//constructor
	public MenuRegistry (){
		//Instantiate every menu item to begin with
		//automatically add default, delete button, basic brick and basic gun
		GameObject t = (GameObject) GameObject.Instantiate(menu_default, new Vector3 (-100, -100, -100), Quaternion.identity);
		t.name = "menu_default";

		GameObject s = (GameObject) GameObject.Instantiate(menu_delete, new Vector3 (-100, -100, -100), Quaternion.identity);
		s.name = "menu_delete";
		blocks.Add(s);

		GameObject u = (GameObject) GameObject.Instantiate(menu_red_gun, new Vector3 (-100, -100, -100), Quaternion.identity);
		u.name = "menu_red_gun";
		guns.Add(u);

		GameObject v = (GameObject) GameObject.Instantiate(menu_red_gun, new Vector3 (-100, -100, -100), Quaternion.identity);
		v.name = "menu_green_gun";
		guns.Add(v);

		GameObject w = (GameObject) GameObject.Instantiate(menu_red_gun, new Vector3 (-100, -100, -100), Quaternion.identity);
		w.name = "menu_green_block";
		blocks.Add(w);

		GameObject x = (GameObject) GameObject.Instantiate(menu_red_gun, new Vector3 (-100, -100, -100), Quaternion.identity);
		x.name = "menu_red_block";
		guns.Add(x);

		//here instantiate all other menu items way below map
	}

	//add an item to menu
	public void addMenuItem (string type, string tag){
		switch (type) {

			case "block":
				blocks.Add(GameObject.Find (tag));
				//instantiate menu items far below map, then just move them for convenience - may be unnecessary
				GameObject.Instantiate(GameObject.Find (tag), new Vector3 (-100, -100, -100), Quaternion.identity);
				break;

			case "gun":
				guns.Add(GameObject.Find (tag));
				GameObject.Instantiate(GameObject.Find (tag), new Vector3 (-100, -100, -100), Quaternion.identity);
				break;
		}

	}

	//remove item
	public void removeMenuItem(string type, string tag){
		switch (type) {
			case "block":
				blocks.Remove(GameObject.Find (tag));
				//destroy it as well as remve from array list
				GameObject.Destroy(GameObject.Find (tag));
				break;
			
			case "gun":
				guns.Remove(GameObject.Find (tag));
				GameObject.Destroy(GameObject.Find (tag));
				break;
		}
	}

	//draw screen
	public void drawMenu(){
		//draw menu items at current position
		//check if current position is possible then move there. If not, stay at last position
		
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
		curx -= 1;
		this.drawMenu ();
	}

	//for movement rightward
	public void shiftRight() {
		curx += 1;
		this.drawMenu ();
	}

	//for movement upward
	public void shiftUp() {
		cury += 1;
		this.drawMenu ();
	}

	//for movement downward
	public void shiftDown() {
		cury -= 1;
		this.drawMenu ();
	}
}
