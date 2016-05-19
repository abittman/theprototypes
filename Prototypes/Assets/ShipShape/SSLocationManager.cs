using UnityEngine;
using System.Collections.Generic;

public class SSLocationManager : MonoBehaviour {

	public ShipManager shipManager;

	public SSLocation homeLocation;
	public List<SSLocation> seaLocations = new List<SSLocation>();
	SSLocation playerCurrentLocation;

	//Probably break this away later
	[Header("Location Objects")]
	public GameObject homeObj;
	public GameObject blueSeasObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public SSLocation GetLocation(string name)
	{
		SSLocation tempLoc;
		if(name == homeLocation.locationName)
		{
			tempLoc = homeLocation;
		}
		else
		{
			tempLoc = seaLocations.Find(x => x.locationName == name);
		}
		return tempLoc;
	}

	public void PlayerMovesToLocation(SSLocation moveToLocation)
	{
		playerCurrentLocation = moveToLocation;
		switch(playerCurrentLocation.locationType)
		{
		case ShipLocationType.Blue_Sea:
		{
			blueSeasObj.SetActive(true);
			shipManager.MoveSelectedShip(moveToLocation);
			break;
		}
		case ShipLocationType.Home:
		{
			homeObj.SetActive(true);
			shipManager.MoveSelectedShip(moveToLocation);
			break;
		}
		}
	}

	public void LookAtMap()
	{
		homeObj.SetActive(false);
		blueSeasObj.SetActive(false);
	}
}
