using UnityEngine;
using System.Collections.Generic;

/* Manages and holds detail of all events */

public class ShipEventManager : MonoBehaviour {

	public ShipManager shipManager;
	public SSLocationManager locationManager;

	public List<ExplorationEvent> allExploreEvents = new List<ExplorationEvent>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			EventOccurs();
		}
	}

	public void EventOccurs()
	{
		
	}
}
