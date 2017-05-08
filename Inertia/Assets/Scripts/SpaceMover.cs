using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System; // for MATH
using UnityEngine.UI;

public class SpaceMover : Unit
{

    public CellGrid cellgrid; //for the love of all that is holy, generate the cell grid with the SAME WIDTH AND HEIGHT
    public int inertiaI = 0;
    public int inertiaJ = 0;
    public int inertiaK = 0;
    public int rotationalPosition = 0;
    public int rotationalInertia = 0;
    public int gridSize;    
	public Log log;
    public TurnManager turnManager;
	public string manuver = "";
    public int rotationalDir = 0;
   

    public void setGridSize()
    {
        gridSize = (int)(Math.Sqrt(cellgrid.Cells.Count));
    }


    public void SimplifyInertia() {
        while (inertiaK < 0)
        {
            inertiaI++;
            inertiaJ--;
            inertiaK++;
        }
        while (inertiaK > 0)
        {
            inertiaI--;
            inertiaJ++;
            inertiaK--;
        }
    }

	// rotates unit
	public void SetRotationalPosition(int positionChange) {

		rotationalPosition = positionChange;
	
		while (rotationalPosition < 0) {
			rotationalPosition += 6;
		}
			

		rotationalPosition %= 6;
		

		int localPosition = rotationalPosition;
		localPosition *= 60;

		transform.eulerAngles = new Vector3 (0.0f, 0.0f, -localPosition);
	}

     public virtual void getNextCells(List<Vector3> list, Vector3 position, int possibleRange) {
		if (position.x+position.y-position.z > possibleRange)
            return;
		if (position.x + position.y - position.z + 1 <= possibleRange) { 
			list.Add(new Vector3(position.x + 1,position.y,position.z));
			list.Add (new Vector3 (position.x + 1, position.y + 1, position.z));
			list.Add (new Vector3 (position.x + 1, position.y, position.z - 1));
		}
		if (position.x + position.y - position.z + 2  <= possibleRange) { 
			list.Add (new Vector3 (position.x + 2, position.y, position.z));
		}

		if (position.x + position.y - position.z <= 1) { 
			list.Add (new Vector3 (position.x + (possibleRange), position.y, position.z));
		}
        getNextCells(list, new Vector3(position.x + 1, position.y + 1, position.z), possibleRange);
        getNextCells(list, new Vector3(position.x + 1, position.y, position.z - 1), possibleRange);
		getNextCells(list, new Vector3(position.x + 1, position.y, position.z), possibleRange);
    }
    public virtual void getNextCells(List<Vector3> list, Vector3 position) {
        getNextCells(list, position, 1);

    }

    public override bool IsUnitAttackable(Unit other, Cell sourceCell)
    {
        Debug.Log("Cone is celcultated maybe");
		Boolean attackable = false;
		List<Cell> cellList = cellgrid.Cells;
        Vector3 sourcePosition = findPosition(cellgrid, gridSize);
        Vector3 otherPosition = other.findPosition(cellgrid, gridSize);
        //if (sourceCell.GetDistance(other.Cell) <= AttackRange)
        List<Vector3> vecList = new List<Vector3>();
        //vecList.Add(new Vector3(1, 0, 0));
        getNextCells(vecList, new Vector3(0, 0, 0), 9);
        //Debug.Log(otherPosition.ToString());
        //Debug.Log(vecList.Count());
		String result = "";
		foreach (Vector3 vec in vecList)
		{
			result = result + vec.ToString();
		}
		Debug.Log(result);
		result = "";

		for (int x = 0; x < vecList.Count (); x++) {
			Vector3 vec = vecList [x];
			Vector3 temp = new Vector3(vec.x,vec.y,vec.z);
			switch (rotationalPosition) {
			case 1:
				vec.y = temp.x;
				vec.z = temp.y;
				vec.x = -temp.z;
				break;
			case 2:
				vec.y = -temp.z;
				vec.z = temp.x;
				vec.x = -temp.y;
				break;
			case 3:
				vec.y = -temp.y;
				vec.z = -temp.z;
				vec.x = -temp.x;
				break;
			case 4:
				vec.y = -temp.x;
				vec.z = -temp.y;
				vec.x = temp.z;
				break;
			case 5:
				vec.y = temp.z;
				vec.z = -temp.x;
				vec.x = temp.y;
				break;
			default:
				break;
			}
			result = result + vec.ToString();
			/*
			List<Cell> cellList = cellgrid.Cells;
			int cellPosition = cellList.IndexOf(Cell);
			Cell currentCell = Cell;
			int i = inertiaJ;
			if (i > 0) {
				for (int jTransform = i; jTransform > 0; jTransform--) {
					if ((cellPosition + positionModifer) % 2 == 0) {
						positionModifer += 1;
					} else {
						positionModifer += gridSize;	
					}

				}
			}
			*/

			int currentPosition = cellList.IndexOf (Cell);


			SimplifyHexVector3 (ref vec);
		//	Debug.Log (vec.ToString ());
			currentPosition += (int)(vec.x * (gridSize));
			for (int j = (int)vec.y; j > 0; j--) {
				if (currentPosition % 2 == 0) {
					currentPosition += 1;
				} else {
					currentPosition += gridSize+1;	
				}
			}
			for (int j = (int)vec.y; j < 0; j++) {
				if (currentPosition % 2 == 0) {
					currentPosition -= gridSize+1;
				} else {
					currentPosition -= 1;	
				}
			}

            //	Debug.Log(currentPosition);
            if (currentPosition >=0 && currentPosition < gridSize*gridSize && Cell.GetDistance(cellList[currentPosition]) < 10)
			    cellList [currentPosition].MarkAsReachable ();

			SimplifyHexVector3 (ref sourcePosition);

			vec = vec + sourcePosition;

				//Debug.Log (vec.ToString () + "   " + otherPosition.ToString ());

				if (vec.Equals(otherPosition))
				{
					attackable = true;
				}
			//foreach (Vector3 vec in vecList)




			}

		Debug.Log(result);
		return attackable;
    }

	public void RotateByAmount(int amount) {
		rotationalPosition += amount;
		SetRotationalPosition (rotationalPosition);
	}

    public void applyManuver(string instructions)
    {
        int newRotationalInertia = 0;
        foreach (char c in instructions)
        {
            if (c == 'A')
            {
                if (instructions[0] == '-')
                    ApplyAcceleration(-1);
                else
                {
                    ApplyAcceleration(1);
                }

            }
            else if (c == 'T')
            {
                if (rotationalDir < 0)
                {
                    newRotationalInertia--;
                    RotateByAmount(-1);
                }
                else if (rotationalDir > 0)
                {
                    newRotationalInertia++;
                    RotateByAmount(1);
                }
            }
        }
        rotationalInertia = newRotationalInertia;
        manuver = "";
        int i = 0;
        while (i < rotationalInertia)
        { manuver += "T"; i++; }

    }


    //adds new inertia each turn
    public void ApplyAcceleration(int acceleration) { 

		switch(rotationalPosition) {
		case 0:
			inertiaI += acceleration;
			break;
		case 1:
			inertiaJ += acceleration;
			break;
		case 2:
			inertiaK += acceleration;
			break;
		case 3:
			inertiaI -= acceleration;
			break;
		case 4:
			inertiaJ -= acceleration;
			break;
		case 5:
			inertiaK -= acceleration;
			break;
		}

	}

	//moves the ship each turn
	public void MoveShip() { 

		List<Cell> cellList = cellgrid.Cells;

        int destinationIndex = cellList.IndexOf(Cell) + inertiaI * gridSize + jModifier();

        if (destinationIndex >= 0 && destinationIndex < gridSize * gridSize)
        {
            SimplifyInertia();
            Cell destination = cellList[destinationIndex];
            List<Cell> destinationList = new List<Cell>();
            destinationList.Add(destination);
            if (Cell.GetDistance(destination) > (Math.Abs(inertiaI) + Math.Abs(inertiaJ)))
            {
                this.Defend(this, 1);
            }
            Move(destination, destinationList);
        }
        else
        {
            this.Defend(this, 1);
        }
	}
		
	public int jModifier(){
		int positionModifer = 0;

		List<Cell> cellList = cellgrid.Cells;
		int cellPosition = cellList.IndexOf(Cell);
		Cell currentCell = Cell;
		int i = inertiaJ;
		if (i > 0) {
			for (int jTransform = i; jTransform > 0; jTransform--) {
				if ((cellPosition + positionModifer) % 2 == 0) {
					positionModifer += 1;
				} else {
					positionModifer += (gridSize +1);	
				}
				 
			}
		}
		else {
			for (int jTransform = i; jTransform < 0; jTransform++) {
				if ((cellPosition + positionModifer) % 2 == 0) {
					positionModifer -= (gridSize+1);
				} else {
					positionModifer -= 1;	
				}
			}
		}
		return positionModifer;
	}

	private int testCount = 0;
    public override void OnTurnStart()
    {
        setGridSize();
        MovementPoints = TotalMovementPoints;
        //ActionPoints = TotalActionPoints;

        SetState(new UnitStateMarkedAsFriendly(this));
    }
		//MoveShip ();


		//TODO 100
		//int number = cellgrid.GetComponents<RectangularHexGridGenerator>
    /// <summary>
    /// Method is called at the end of each turn.
    /// </summary>
    public override void OnTurnEnd()
	{
		Debug.Log("Space SpaceMover OnTurnEnd called");
		Buffs.FindAll(b => b.Duration == 0).ForEach(b => { b.Undo(this); });
		Buffs.RemoveAll(b => b.Duration == 0);
		Buffs.ForEach(b => { b.Duration--; });

		SetState(new UnitStateNormal(this));
	}

	public Cell getCell(){
		return Cell;
	}

	public int movesPerTurn = 2;
	public int attacksPerTurn = 1;






	public Color PlayerColor;

	public string UnitName;

	private Transform Highlighter;

	public override void Initialize()
	{
		base.Initialize();
		SetColor(PlayerColor);

		Highlighter = transform.Find("Highlighter");
		if (Highlighter != null)
		{
			Highlighter.position = transform.position + new Vector3(0, 0, 1.5f);
			foreach (Transform cubeTransform in Highlighter)
				Destroy(cubeTransform.GetComponent<BoxCollider>());
		}     
		gameObject.transform.position = Cell.transform.position + new Vector3(0, 0, -1.5f);
	}
		
	public override void MarkAsAttacking(Unit other)
	{
		StartCoroutine(Jerk(other));
	}
	public override void MarkAsDefending(Unit other)
	{
		StartCoroutine(Glow(new Color(1, 0.5f, 0.5f), 1));
	}
	public override void MarkAsDestroyed()
	{
	}
    public override void DealDamage(Unit other)
    {
		if (isMoving)
			return;
		if (ActionPoints == 0)
			return;
		if (!IsUnitAttackable(other, Cell))
			return;
		int dieRoll = UnityEngine.Random.Range(0, 99);
		if (dieRoll < (101 - (Cell.GetDistance(other.Cell) * Cell.GetDistance(other.Cell)))){
			Debug.Log("Hit!");
			MarkAsAttacking(other);
			//SpaceMover[] shipPair = new SpaceMover[2];
			//shipPair [0] = this.GetComponent<SpaceMover>();
			//shipPair [1] = other.GetComponent<SpaceMover>();
			turnManager.shootingShips.Add (other.GetComponent<SpaceMover>());
		}
		ActionPoints--;
		if (ActionPoints == 0)
		{
			SetState(new UnitStateMarkedAsFinished(this));
			MovementPoints = 0;
		} 


    }
    private IEnumerator Jerk(Unit other)
	{
		var heading = other.transform.position - transform.position;
		var direction = heading / heading.magnitude;
		float startTime = Time.time;

		while (startTime + 0.25f > Time.time)
		{
			transform.position = Vector3.Lerp(transform.position, transform.position + (direction / 2.5f), ((startTime + 0.25f) - Time.time));
			yield return 0;
		}
		startTime = Time.time;
		while (startTime + 0.25f > Time.time)
		{
			transform.position = Vector3.Lerp(transform.position, transform.position - (direction / 2.5f), ((startTime + 0.25f) - Time.time));
			yield return 0;
		}
		transform.position = Cell.transform.position + new Vector3(0, 0, -1.5f); ;
	}
	private IEnumerator Glow(Color color, float cooloutTime)
	{
		float startTime = Time.time;

		while (startTime + cooloutTime > Time.time)
		{
			SetColor(Color.Lerp(PlayerColor, color, (startTime + cooloutTime) - Time.time));
			yield return 0;
		}

		SetColor(PlayerColor);
	}
    public void setFutureRotationIncrement(int change)
    {
        
    }
    public void respondToManeuverButton()
    {

    }
	public override void MarkAsFriendly()
	{
		SetHighlighterColor(new Color(0.8f,1,0.8f));
	}
	public override void MarkAsReachableEnemy()
	{
		SetHighlighterColor(Color.red);
	}
	public override void MarkAsSelected()
	{
		SetHighlighterColor(new Color(0,1,0));
        turnManager.inUse = this;
	}


public override void MarkAsFinished()
	{
		SetColor(PlayerColor - Color.gray);
		SetHighlighterColor(new Color(0.8f, 1, 0.8f));
	}
	public override void UnMark()
	{
		SetColor(PlayerColor);
		SetHighlighterColor(Color.white);
		if (Highlighter == null) return;
		    Highlighter.position = transform.position + new Vector3(0, 0, 1.52f);
	}

	private void UpdateHpBar()
	{
		if (GetComponentInChildren<Image>() != null)
		{
			GetComponentInChildren<Image>().transform.localScale = new Vector3((float)((float)HitPoints / (float)TotalHitPoints), 1, 1);
			GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green,
				(float)((float)HitPoints / (float)TotalHitPoints));
		}
	}
	private void SetColor(Color color)
	{
		GetComponent<Renderer>().material.color = color;
	}
	private void SetHighlighterColor(Color color)
	{

		if (Highlighter == null) return;

		Highlighter.position = transform.position + new Vector3(0, 0, 1.48f);
		for (int i = 0; i < Highlighter.childCount; i++)
		{
			var rendererComponent = Highlighter.transform.GetChild(i).GetComponent<Renderer>();
			if (rendererComponent != null)
				rendererComponent.material.color = color;
		}
	}
	public Vector3 getFuturePosition() {
		Vector3 vector = transform.position;
		vector.x += inertiaJ*1.65497f;
		vector.y += inertiaJ*0.95535f;
		vector.y += inertiaI*1.91071f;
		return vector;
	}
    public string CreateOrderOfAction(int rotAccellerationChange, int accelleration)
    {
        List<string> actionOrder = new List<string>();
        int nextRotation = rotationalInertia + rotAccellerationChange;
        if (nextRotation > 0)
            rotationalDir = 1;
        else if (nextRotation < 0)
            rotationalDir = -1;

   
        int absAccelleration = Math.Abs(accelleration);
        int absoluteRotation = Math.Abs(nextRotation);

        actionOrder.Add("");
        actionOrder.Add("");
        actionOrder.Add("");
        actionOrder.Add("");

        int i = actionOrder.Count / 2;

        int turnsHalf = absoluteRotation / 2;
        int accelerationHalf = absAccelleration / 2;

        int leftOverRotation = absoluteRotation % 2;
        int leftOverAcceleration = absAccelleration % 2;

        while (turnsHalf > 1 || accelerationHalf > 1)
        {
            if (turnsHalf > accelerationHalf)
            {
                actionOrder.Insert(i + 1, "T");
                actionOrder.Insert(i + 1, "T");
                turnsHalf -= 2;
                i = actionOrder.Count / 2;
            }
            else if (turnsHalf < accelerationHalf)
            {
                actionOrder.Insert(i + 1, "A");
                actionOrder.Insert(i + 1, "A");
                accelerationHalf -= 2;
                i = actionOrder.Count / 2;
            }
            else if (turnsHalf == accelerationHalf)
            {
                actionOrder.Insert(i + 1, "A");
                actionOrder.Insert(i + 1, "T");
                turnsHalf -= 1;
                accelerationHalf--;
                i = actionOrder.Count / 2;
            }
        }

        while (accelerationHalf + turnsHalf > 0)
        {
            if (accelerationHalf > 0)
            {
                actionOrder.Insert(i + 1, "A");
                accelerationHalf--;
                i = actionOrder.Count / 2;
            }
            if (turnsHalf > 0)
            {
                actionOrder.Insert(i + 1, "T");
                turnsHalf--;
                i = actionOrder.Count / 2;
            }
        }
        List<string> temp = new List<string>(actionOrder);
        temp.Reverse();
        temp.InsertRange(temp.Count, actionOrder);
        actionOrder = temp;

        i = actionOrder.Count / 2;
        while (leftOverAcceleration > 0)
        {
            actionOrder.Insert(i + 1, "A");
            leftOverAcceleration--;
        }
        while (leftOverRotation > 0)
        {
            actionOrder.Insert(i + 1, "T");
            leftOverRotation--;
        }

        if (accelleration < 1)
            actionOrder.Insert(0, "-");

        Debug.Log(string.Join(string.Empty, actionOrder.ToArray()));

        return string.Join(string.Empty, actionOrder.ToArray());

    }
}
