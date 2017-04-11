using UnityEngine;
using System.Collections;
//using UnityEngine.Events;
//using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    //public CellGrid CellGrid;
//	public Button button;
/*
    void Start()
    {
        Debug.Log("Press 'n' to end turn");
    }

	void Update ()
    {
		if(false)
        {
            CellGrid.EndTurn();//User ends his turn by pressing "n" on keyboard.
			Debug.Log("A Turn Occured");
        }
	}
*/
	public void TaskOnClick(CellGrid CellGrid) {
		CellGrid.EndTurn();
		Debug.Log("A Turn Occured");
	}

    public void ForwardMove(SpaceMover player)
    {
        player.CreateOrderOfAction(player.rotationalInertia, 1);
    }

    public void BackwardMove(SpaceMover player)
    {
        player.CreateOrderOfAction(player.rotationalInertia, -1);
    }
    public void RightTurn(SpaceMover player)
    {
        player.CreateOrderOfAction(player.rotationalInertia+1, 0);
    }
    public void LeftTurn(SpaceMover player)
    {
        player.CreateOrderOfAction(player.rotationalInertia-1, 0);
    }
}
