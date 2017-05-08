using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public List<SpaceMover> shootingShips = new List<SpaceMover>();
	public Log eventLog;
	public int turnState=0;
	public List<GameObject> ships;
    public SpaceMover inUse;
	public LineRenderer inertiaLine;

    public GameObject[] maneuverButtons;



	public void getShips() {
		Debug.Log ("Get ships is called");
		ships = new List<GameObject>();
		GameObject[] allShips = GameObject.FindGameObjectsWithTag ("Ship");
		for (int i = 0; i < allShips.Length; i++) {
			GameObject tester = allShips [i];
			if (tester.GetComponent (typeof(SpaceMover)) != null && tester != null) {
				ships.Add (tester);
			}
		}

	}

	public void moveShips() {
		getShips ();
		foreach (GameObject currentShip in ships) {
			SpaceMover currentSpaceMover = currentShip.GetComponent<SpaceMover> ();
			currentSpaceMover.applyManuver (currentSpaceMover.manuver);
			currentSpaceMover.MoveShip ();
			currentSpaceMover.ActionPoints = 1;
			currentSpaceMover.manuver = currentSpaceMover.CreateOrderOfAction (0, 0);
			Debug.Log (currentSpaceMover.manuver);
		}
		for (int i = 0; i < ships.Count-1; i++) {
			for (int j = i + 1; j < ships.Count; j++) {
				if (ships[i] != null && ships[j] != null){
					if (ships [i].GetComponent<SpaceMover> ().getCell () == ships [j].GetComponent<SpaceMover> ().getCell ()) {
						ships [i].GetComponent<SpaceMover>().TakeDamage(ships [i].GetComponent<SpaceMover>(), 1);
						ships [j].GetComponent<SpaceMover>().TakeDamage(ships [j].GetComponent<SpaceMover>(), 1);
					}
				}
			}
		}
		getShips ();
	}

	public void advanceTurnState() {
        inUse = null;
		inertiaLine.SetPosition (1, inertiaLine.GetPosition(0));
        if (turnState >= 3)
			turnState = 0;
		else turnState++;
		switch (turnState) {
		case 0:
			getShips ();
			eventLog.AddEvent ("Player 1 Maneuver Turn");
			resetActionPoints (); // resolve attacks(override DealDamage, store damage as string, resolve damage function), reset action points
			//resolveAllDamage();
			Debug.Log ("ApplyDamageToShips Called");
			ApplyDamageToShips ();
            GameObject.Find("Button").GetComponentInChildren<UnityEngine.UI.Text>().text = "End Player 1 Movement Turn";
            break;
		case 1:
			eventLog.AddEvent ("Player 2 Maneuver Turn");
            GameObject.Find("Button").GetComponentInChildren<UnityEngine.UI.Text>().text = "End Player 2 Movement Turn";
            break;
		case 2:
			eventLog.AddEvent ("Player 1 Attack Turn");
			moveShips (); // resolve movements, set action points
            GameObject.Find("Button").GetComponentInChildren<UnityEngine.UI.Text>().text = "End Player 1 Attack Turn";
            break;
		case 3:
			eventLog.AddEvent ("Player 2 Attack Turn");
            GameObject.Find("Button").GetComponentInChildren<UnityEngine.UI.Text>().text = "End Player 2 Attack Turn";
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
		Debug.Log ("ApplyDamageToShips Called");
		foreach (SpaceMover currentShip in shootingShips)
        {
			currentShip.TakeDamage (currentShip, 1);
			while(shootingShips.Remove(null)) {
				;
			}
        }
		shootingShips.Clear ();
		while(ships.Remove(null)) {
			;
		}

    }
    // Use this for initialization
    void Start () {
        getShips ();
		eventLog.AddEvent ("Player 1 Maneuver Turn");
		resetActionPoints ();

        maneuverButtons = GameObject.FindGameObjectsWithTag("ManeuverButtons");
    }
	void Update()
    {

        if (inUse != null && (turnState == 1 || turnState == 0))
        {
            Vector3 position = inUse.transform.position;
            inertiaLine.SetPosition(0, position);
            inertiaLine.SetPosition(1, inUse.getFuturePosition());

            foreach (GameObject i in maneuverButtons)
            {
                i.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject i in maneuverButtons)
            {
                i.SetActive(false);
            }
        }
        
	}
}