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
		if (turnState == 0)
			resetActionPoints ();
		if (turnState == 2)
			moveShips ();
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