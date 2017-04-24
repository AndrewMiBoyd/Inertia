using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public Log eventLog;
	public int turnState=0;
	public List<GameObject> ships;
    public SpaceMover inUse;
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
        inUse = null;
        if (turnState >= 3)
			turnState = 0;
		else turnState++;
		switch (turnState) {
		case 0:
			eventLog.AddEvent ("Player 1 Maneuver Turn");
			resetActionPoints (); // resolve attacks(override DealDamage, store damage as string, resolve damage function), reset action points
			//resolveAllDamage();
			break;
		case 1:
			eventLog.AddEvent ("Player 2 Maneuver Turn");
			break;
		case 2:
			eventLog.AddEvent ("Player 1 Attack Turn");
			moveShips (); // resolve movements, set action points
			break;
		case 3:
			eventLog.AddEvent ("Player 2 Attack Turn");
			break;
		}
	}

	public void resetActionPoints() {
		foreach (GameObject currentShip in ships) {
			currentShip.GetComponent<SpaceMover> ().ActionPoints = 0;
		}
	}
		
    public void giveShipRotationalCommand(int change)
    {
        inUse.manuver = inUse.CreateOrderOfAction(change, 0);
    }

    public void giveShipLinearCommand(int change)
    {
        inUse.manuver = inUse.CreateOrderOfAction(0, change);
    }

    public void accellerateAndTurn(int turn)
    {
        inUse.manuver = inUse.CreateOrderOfAction(turn, 1);
    }



    public void ApplyDamageToShips()
    {
        foreach (GameObject currentShip in ships)
        {
     //       currentShip.GetComponent<SpaceMover>().DealDamage(currentShip.GetComponent<SpaceMover>.other);
        }
    }
    // Use this for initialization
    void Start () {
		getShips ();
		eventLog.AddEvent ("Player 1 Maneuver Turn");
	}
}