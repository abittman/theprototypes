using UnityEngine;
using System.Collections.Generic;

public class ShipInventory : MonoBehaviour {

	public List<ShipShapeItem> shipItems = new List<ShipShapeItem>();
	public HomeInventory homeInventory;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddItem(string itemName, int itemAmount)
	{
		bool itemFound = false;
		foreach(ShipShapeItem item in shipItems)
		{
			if(item.itemName == itemName)
			{
				itemFound = true;
				item.itemAmount += itemAmount;
			}
		}

		if(!itemFound)
		{
			shipItems.Add(new ShipShapeItem(itemName, itemAmount));
		}
	}

	public void TransferItemsHome()
	{
		foreach(ShipShapeItem item in shipItems)
		{
			homeInventory.GetItem(item.itemName, item.itemAmount);
		}

		shipItems.Clear();
	}
}
