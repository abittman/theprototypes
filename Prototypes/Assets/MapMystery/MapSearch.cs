using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class MapLocations
{
	public GameObject mapLocationPanel;
	public List<string> locationKeywords = new List<string>();
}

public class MapSearch : MonoBehaviour {

	public List<MapLocations> locations = new List<MapLocations>();
	public InputField searchField;

	// Use this for initialization
	void Start () 
	{
		foreach(MapLocations m in locations)
		{
			m.mapLocationPanel.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SearchLocations()
	{
		string search = searchField.text;
		foreach(MapLocations m in locations)
		{
			bool found = false;
			foreach(string s in m.locationKeywords)
			{
				if(s == search)
				{
					m.mapLocationPanel.SetActive(true);
					found = true;
					break;
				}
			}

			if(!found)
			{
				m.mapLocationPanel.SetActive(false);
			}
		}
	}
}
