using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ShipShapeItem
{
	public string itemName;
	public int itemAmount;

	public ShipShapeItem(string s, int i)
	{
		itemName = s;
		itemAmount = i;
	}
}

public class HomeInventory : MonoBehaviour {

	public List<ShipShapeItem> homeItems = new List<ShipShapeItem>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetItem(string itemName, int itemAmount)
	{
		bool itemFound = false;
		foreach(ShipShapeItem item in homeItems)
		{
			if(item.itemName == itemName)
			{
				itemFound = true;
				item.itemAmount += itemAmount;
			}
		}

		if(!itemFound)
		{
			homeItems.Add(new ShipShapeItem(itemName, itemAmount));
		}
	}
}
