using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RunnerItem
{
	public string itemName;
	public int itemAmount;
}

public class RunnerInventory : MonoBehaviour {

	public List<RunnerItem> items = new List<RunnerItem>();
	public bool hasAxe = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool HasItem(string name, int amount)
	{
		foreach(RunnerItem i in items)
		{
			if(i.itemName == name)
			{
				if(i.itemAmount >= amount)
				{
					return true;
				}
				else
				{
					break;
				}
			}
		}
		return false;
	}

	public void AddItem(string name, int amount)
	{
		foreach(RunnerItem i in items)
		{
			if(i.itemName == name)
			{
				i.itemAmount += amount;
			}
		}
	}

	public void RemoveItem(string name, int amount)
	{
		foreach(RunnerItem i in items)
		{
			if(i.itemName == name)
			{
				i.itemAmount -= amount;
			}
		}
	}
}
