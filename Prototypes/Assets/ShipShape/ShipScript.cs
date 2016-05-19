using UnityEngine;
using System.Collections.Generic;

/* Class containing all details of ships 
Handles movement and details - probably break that down*/

public enum ShipType
{
	None,
	Cartography,
	Scientific,
	Cargo
}

public class ShipScript : MonoBehaviour {

	[Header("Ship Details")]
	public string thisShipName;
	public string shipTypeName;
	public ShipType thisShipType;
	public float shipSpeed = 1f;
	public int shipCargoHold = 100;
	public int minimumCrew = 1;
	public int maximumCrew = 5;

	[Header("Ship Status")]
	public ShipInventory myShipInventory;
	public SSLocationManager locationManager;
	public SSLocation currentLocation;
	public List<ShipCrew> currentCrew = new List<ShipCrew>();

	public bool isHome = true;

	// Use this for initialization
	void Start () 
	{
		ArriveHome();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ArriveHome()
	{
		isHome = true;
		myShipInventory.TransferItemsHome();
		currentLocation = locationManager.homeLocation;
	}

	public void GoExploring(SSLocation moveToLocation)
	{
		Debug.Log("aa");
		isHome = false;
		//Go to new place

		if(moveToLocation != null)
		{
			currentLocation = moveToLocation;
			Debug.Log("New location is " + moveToLocation.locationName);
		}
		else
		{
			Debug.Log("Location doesn't exist");
		}
	}

	public void ResolveEvent(ShipShapeItem receivedItem)
	{
		myShipInventory.AddItem(receivedItem.itemName, receivedItem.itemAmount);
	}
}
