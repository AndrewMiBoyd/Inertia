using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public int turnState=0;
	public List<GameObject> ships;

	public void getShips() {
		ships = new List<GameObject>();
		GameObject[] allShips = GameObject.FindGameObjectsWithTag ("Ship");
		for (int i = 0; i < allShips.Length; i++) {
			GameObject tester = allShips [i];
			if (tester.GetComponent (typeof(SpaceMover)) != null) {
				ships.Add (tester);
			}
		}
	}

	public void moveShips() {
		foreach (GameObject currentShip in ships) {
			SpaceMover currentSpaceMover = currentShip.GetComponent<SpaceMover> ();
			currentSpaceMover.applyManuver (currentSpaceMover.manuver);
			currentSpaceMover.MoveShip ();
			currentSpaceMover.ActionPoints = 1;
		}
	}

	public void advanceTurnState() {
		if (turnState >= 3)
			turnState = 0;
		else turnState++;
		switch (turnState) {
		case 0:
			resetActionPoints (); // resolve attacks(override DealDamage, store damage as string, resolve damage function), reset action points
			//resolveAllDamage();
			// player 1 manuver turn
			break;
		case 1:
			// player 2 manuver turn
			break;
		case 2:
			moveShips (); // resolve movements, set action points
			//player 1 attack turn
			break;
		case 3:
			//player 2 attack turn
			break;
		}
	}

	public void resetActionPoints() {
		foreach (GameObject currentShip in ships) {
			currentShip.GetComponent<SpaceMover> ().ActionPoints = 0;
		}
	}
		



	// Use this for initialization
	void Start () {
		getShips ();
	}
	
	// Update is called once per frame
	void Update () {
		//advanceTurnState ();
	}
}