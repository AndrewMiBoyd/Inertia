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
    public static int gridSize = 27;


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
			
		if (rotationalPosition >= 6) {
			rotationalPosition %= 6;
		}

		int localPosition = rotationalPosition;
		localPosition *= 60;

		transform.eulerAngles = new Vector3 (0.0f, 0.0f, localPosition);
	}

     public virtual void getNextCells(List<Vector3> list, Vector3 position, int possibleRange) {
        if (possibleRange <0)
            return;
        possibleRange--;
        list.Add(new Vector3(position.x + 1,position.y,position.z));
        list.Add(new Vector3(position.x + 2, position.y, position.z));
        list.Add(new Vector3(position.x + 1, position.y + 1, position.z));
        list.Add(new Vector3(position.x + 1, position.y, position.z - 1));
        getNextCells(list, new Vector3(position.x + 1, position.y + 1, position.z), possibleRange);
        getNextCells(list, new Vector3(position.x + 1, position.y, position.z - 1), possibleRange);
    }
    public virtual void getNextCells(List<Vector3> list, Vector3 position) {
        getNextCells(list, position, 0);

    }

    public override bool IsUnitAttackable(Unit other, Cell sourceCell)
    {
        Debug.Log("Cone is celcultated maybe");
        Vector3 sourcePosition = findPosition(cellgrid, gridSize-1);
        Vector3 otherPosition = other.findPosition(cellgrid, gridSize-1);
        //if (sourceCell.GetDistance(other.Cell) <= AttackRange)
        List<Vector3> vecList = new List<Vector3>();
        vecList.Add(new Vector3(sourcePosition.x + 1, sourcePosition.y, sourcePosition.z));
        getNextCells(vecList, new Vector3(1, 0, 0));
        Debug.Log(otherPosition.ToString());
        Debug.Log(vecList.Count());
        for (int x = 0; x < vecList.Count(); x++){
            Vector3 vec = vecList[x];
            switch (rotationalPosition){
                case 1:
                    vec.y = vec.x;
                    vec.z = vec.y;
                    vec.x = -vec.z;
                    break;
                case 2:
                    vec.y = -vec.z;
                    vec.z = vec.x;
                    vec.x = -vec.y;
                    break;
                case 3:
                    vec.y = -vec.z;
                    vec.z = -vec.y;
                    vec.x = -vec.x;
                    break;
                case 4:
                    vec.y = -vec.x;
                    vec.z = -vec.y;
                    vec.x = vec.z;
                    break;
                case 5:
                    vec.y = vec.z;
                    vec.z = -vec.x;
                    vec.x = vec.y;
                    break;
                default:
                    break;
            }
            SimplifyHexVector3(ref sourcePosition);
            vec = vec + sourcePosition;

            SimplifyHexVector3(ref vec);
            Debug.Log(vec.ToString() + "   " + otherPosition.ToString());
            if (vec.Equals(otherPosition))
            {
                return true;
            }
        }
        return false;
    }
	public void RotateByAmount(int amount) {
		rotationalPosition += amount;
		SetRotationalPosition (rotationalPosition);
	}

    public void parseInstructions(string instructions)
    {
        foreach(char c in instructions)
        {
            if (c == 'A')
            {
                ApplyAcceleration(1);
            }
            else if (c == 'T')
            {
                if (rotationalInertia < 0)
                {
                    RotateByAmount(-1);
                }
                else
                {
                    RotateByAmount(1);
                }
            }
        }

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

		SimplifyInertia ();
		Cell destination = cellList[cellList.IndexOf(Cell) + inertiaI * gridSize + jModifier()];
		List<Cell> destinationList = new List<Cell> ();
		destinationList.Add (destination);
        Move (destination, destinationList);

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
					positionModifer += gridSize;	
				}
				 
			}
		}
		else {
			for (int jTransform = i; jTransform < 0; jTransform++) {
				if ((cellPosition + positionModifer) % 2 == 0) {
					positionModifer -= gridSize;
				} else {
					positionModifer -= 1;	
				}
			}
		}
		return positionModifer;
	}
	/*
	public override void Move(Cell destinationCell, List<Cell> path) {
		Cell.IsTaken = false;
		Cell = destinationCell;
		destinationCell.IsTaken = true;


		if (MovementSpeed >= 0)
			StartCoroutine(MovementAnimation(path));
		else
			transform.position = Cell.transform.position;
   
	}
*/
	public override void OnTurnStart() {
		Debug.Log("Space SpaceMover OnTurnStart called");
		MovementPoints = TotalMovementPoints;
		ActionPoints = TotalActionPoints;

		SetState(new UnitStateMarkedAsFriendly(this));

		MoveShip ();


		//TODO 100
		//int number = cellgrid.GetComponents<RectangularHexGridGenerator>
	}

	/// <summary>
	/// Method is called at the end of each turn.
	/// </summary>
	public override void OnTurnEnd()
	{
		Debug.Log("Space SpaceMover OnTurnEnd called");
        parseInstructions(CreateOrderOfAction(rotationalInertia, 1));
		Buffs.FindAll(b => b.Duration == 0).ForEach(b => { b.Undo(this); });
		Buffs.RemoveAll(b => b.Duration == 0);
		Buffs.ForEach(b => { b.Duration--; });

		SetState(new UnitStateNormal(this));
	}



	public int movesPerTurn = 2;
	public int attacksPerTurn = 1;

	/*
	//Linear and Light/Hard Turn: need to rework unit/myunit or create our own unit script
	//Linear
	public int linearForward = 2;
	public int linearReverse = -1;

	//Light Turn
	public int lightTurnTurn = 1;
	public int lightTurnForward = 1;
	public int lightTurnReverse = -1;

	//Hard Turn
	public int hardTurnTurn = 2;
	public int hardTurnForward = 1;
	public int hardTurnReverse = -1;

	//Combat
	public int armor = 2;
	public int hull = 4;    // which one is regular health
	public int sheilds = 4;

	//General
	public int fighterPoints = 100;

	//Weapons
	public int fighterArc = 120;
	public int fighterNumber = 0;
	public string fighterPower = "2d4";
	public int fighterTorpedo = 2;
	//end of added variables
	*/
	//based off Unit.cs
	protected void Defend(Unit other, int damage) {
		/*
		var realDamage = damage;

		//statements needed for hull, armor, and sheilds
		if (other is Torpedo && sheilds <= 0)
			Destroy (this); // ship is destroyed
		else if (other is Torpedo && sheilds > 0) 
			// what happens when shields are up and torpedo hits
		base.Defend(other, realDamage);
		*/
	}

	protected void Attack() {
		//dice based 
	}

	protected void TorpedoHit() {
		//call defend with torpedo and damage
	}


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

    public string CreateOrderOfAction(int rotationalInertia, int acceleration)
    {
        List<string> actionOrder = new List<string>();

        int absoluteRotation = Math.Abs(rotationalInertia);

        actionOrder.Add("");
        actionOrder.Add("");
        actionOrder.Add("");
        actionOrder.Add("");

        int i = actionOrder.Count / 2;

        int turnsHalf = rotationalInertia / 2;
        int accelerationHalf = acceleration / 2;

        int leftOverRotation = rotationalInertia % 2;
        int leftOverAcceleration = acceleration % 2;

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

        Debug.Log(string.Join(string.Empty, actionOrder.ToArray()));

        return string.Join(string.Empty, actionOrder.ToArray());

    }
}
