using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour {

	private int maxLines = 20;
	public Log eventLog;

	public void AddEvent(string eventString) {
		List<GameObject> logUpdates = new List<GameObject> ();
		GameObject[] allLogUpdates = GameObject.FindGameObjectsWithTag ("logText");
		foreach (GameObject obj in allLogUpdates)
			logUpdates.Add (obj);
		

		//delete top log inputs after the maxLines is reached
		while (logUpdates.Count > maxLines) {
			Destroy (logUpdates [0]);
			logUpdates.RemoveAt(0);
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
}
