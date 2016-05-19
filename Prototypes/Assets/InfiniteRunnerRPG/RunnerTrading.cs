using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class TradeClass
{
	public RunnerItem itemGained;
	public RunnerItem itemCost;
}

public class RunnerTrading : MonoBehaviour {

	public RunnerInventory playerInventory;
	public List<TradeClass> trades = new List<TradeClass>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BuyItem(string itemToBuy)
	{
		TradeClass currentTrade = trades.Find(x => x.itemGained.itemName == itemToBuy);
		if(playerInventory.HasItem(currentTrade.itemCost.itemName, currentTrade.itemCost.itemAmount))
		{
			playerInventory.AddItem(currentTrade.itemGained.itemName, currentTrade.itemGained.itemAmount);
			playerInventory.RemoveItem(currentTrade.itemCost.itemName, currentTrade.itemCost.itemAmount);
		}
	}
}
