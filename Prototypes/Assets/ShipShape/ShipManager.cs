using UnityEngine;
using System.Collections.Generic;

/* Manages and holds reference to all ships */

public class ShipManager : MonoBehaviour {

	public List<ShipScript> allShips = new List<ShipScript>();
	ShipScript selectedShip;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	//Return ship by name
	public ShipScript GetShip(string shipName)
	{
		ShipScript returnShip = allShips.Find(x => x.thisShipName == shipName);
		return returnShip;
	}

	public void SelectShip(string shipName)
	{
		selectedShip = allShips.Find(x => x.thisShipName == shipName);
	}

	public void MoveSelectedShip(SSLocation moveToLocation)
	{
		if(selectedShip != null)
			selectedShip.GoExploring(moveToLocation);
	}
}
