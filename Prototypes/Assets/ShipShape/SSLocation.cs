using UnityEngine;
using System.Collections;

/* This class identifies a unique location.
Includes all information about that location */

public enum ShipLocationType
{
	None,
	Home,
	Blue_Sea,
	Dark_Sea
}

[System.Serializable]
public class SSLocation {

	public string locationName;
	public ShipLocationType locationType;
	public Vector2 locationCoordinate;
	public Sprite mapImage;
	public bool hasBeenFound = false;
}
