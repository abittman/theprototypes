using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ExplorationEvent {

	[Header("Event Requirements")]
	public List<ShipLocationType> possibleEventLocations = new List<ShipLocationType>();
	public List<SSLocation> eventLocations = new List<SSLocation>();

	[Header("Event Details")]
	public string eventText;

	[Header("Event Rewards")]
	public bool givesItem;
	public ShipShapeItem eventRewardItem;

	public void EventOccurs()
	{
		if(givesItem)
		{
			//playerShip.ResolveEvent(eventRewardItem);
		}
	}
}
