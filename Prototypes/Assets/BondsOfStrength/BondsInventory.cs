using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BondsItems
{
	public string name;
	public int currentAmount;

	public string spentItemName;
	public int spentItemAmount;
}

public class BondsInventory : MonoBehaviour {

	public List<BondsItems> allItems = new List<BondsItems>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool CanBuyItem(string itemName)
	{
		BondsItems purchasingItem = allItems.Find(x => x.name == itemName);
		if(purchasingItem.spentItemAmount <= allItems.Find(y => y.name == purchasingItem.spentItemName).currentAmount)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	//for single purchase
	public void BuyItem(string itemName)
	{
		BondsItems item = allItems.Find(x => x.name == itemName);
		if(CanBuyItem(item.name))
		{
			item.currentAmount++;
			SpendItem(item.spentItemName, item.spentItemAmount);
		}
	}

	public void SpendItem(string itemName, int deductedAmount)
	{
		allItems.Find(x => x.name == itemName).currentAmount -= deductedAmount;
	}
}
