using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour {

	private int maxLines = 10;
	public List<GameObject> logUpdates;
	public Log eventLog;

	public void AddEvent(string eventString) {

		GameObject[] allLogUpdates = GameObject.FindGameObjectsWithTag ("logText");

		//delete top log inputs after the maxLines is reached
		if (allLogUpdates.Length > maxLines) {
			Destroy (allLogUpdates [0]);
		}

		//create new GameObject to add
		GameObject newText = new GameObject ("newText");
		newText.transform.SetParent (eventLog.transform);

		//add text to new GameObject
		Text myText = newText.AddComponent<Text>();
		Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		myText.font = ArialFont;
		myText.text = eventString;
		//myText.material = ArialFont.material; //this causes the text to scroll outside of the scrollview
		myText.color = Color.black;
		myText.tag = "logText";
	}

	// Use this for initialization
	void Start () {
		eventLog = GetComponent<Log> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
