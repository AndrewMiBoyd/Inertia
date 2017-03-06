using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {

	//Combat
	public int armor = 2;
	public int hull = 4;
	public int sheilds = 4;

	//Linear
	public int linear_forward = 2;
	public int linear_reverse = -1;

	//Light Turn
	public int light_turn_turn = 1;
	public int light_turn_forward = 1;
	public int light_turn_reverse = -1;

	//Hard Turn
	public int hard_turn_turn = 2;
	public int hard_turn_forward = 1;
	public int hard_turn_reverse = -1;

	//General
	public int fighter_points = 100;
	//public Fighter fighter;

	//Weapons
	public int fighter_arc = 120;
	public int fighter_number = 0;
	public string fighter_power = "2d4";
	public int fighter_torpedo = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void takeDamage() {
		
	}

	void fireTorpedo(string location) {

	}

	void linearMove(int movement) {

	}

	void lightTurn(int turn) {

	}

	void hardTurn(int turn) {

	}
}