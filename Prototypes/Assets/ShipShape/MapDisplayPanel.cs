using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapDisplayPanel : MonoBehaviour {

	public SSLocationManager locationManager;
	public ShipManager shipManager;
	SSLocation currentMapLocation;

	[Header("World")]
	public GameObject worldPanel;

	[Header("Home")]
	public GameObject homePanel;

	[Header("Blue Seas")]
	public GameObject blueSeasPanel;
	public Image blueSeasImage;
	public Text blueSeasLocationName;

	// Use this for initialization
	void Start () 
	{
		ShowWorldMap();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowWorldMap()
	{
		gameObject.SetActive(true);
		worldPanel.SetActive(true);
		homePanel.SetActive(false);
		blueSeasPanel.SetActive(false);
	}

	public void ShowLocationMap(string locationName)
	{
		SSLocation temp = locationManager.GetLocation(locationName);
		if(temp.locationType == ShipLocationType.Blue_Sea)
		{
			blueSeasImage.sprite = temp.mapImage;
			blueSeasLocationName.text = temp.locationName;
			worldPanel.SetActive(false);
			blueSeasPanel.SetActive(true);
			currentMapLocation = temp;
		}
		else if(temp.locationType == ShipLocationType.Home)
		{
			worldPanel.SetActive(false);
			homePanel.SetActive(true);
			currentMapLocation = temp;
		}
	}

	public void MoveToLocation()
	{
		locationManager.PlayerMovesToLocation(currentMapLocation);
		CloseMaps();
	}

	public void ReturnFromMap()
	{
		CloseMaps();
	}

	public void CloseMaps()
	{
		gameObject.SetActive(false);
		worldPanel.SetActive(false);
		homePanel.SetActive(false);
		blueSeasPanel.SetActive(false);
	}
}
