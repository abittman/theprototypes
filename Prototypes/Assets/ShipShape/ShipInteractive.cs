using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipInteractive : MonoBehaviour {

	public ShipManager shipManager;
	ShipScript associatedShip;
	public string thisShipName;

	// Use this for initialization
	void Start () 
	{
		//associatedShip = shipManager.GetShip
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		shipManager.SelectShip(thisShipName);
	}
}
