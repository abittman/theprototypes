using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour {

	public bool isPickUp;					//Pickup gives item to player
	public bool isCollider;					//Collider kills player (if they don't have required items)
	public bool requiresItem;				//Object requires item, but does not spend it (axes, picks, swords)
	public bool takesItem;					//Object takes item from players inventory to do something.
	public bool changingObject;				//Object changes if properly used

	public string itemName;
	public int itemAmount;

	public string requiredItem;

	public string takenItem;
	public int takenItemAmount;

	public GameObject originalObject;
	public bool objectTransforming;
	public GameObject transformedObject;
	public float timeToTransform = 1f;
	float transformTimer = 0f;

	// Use this for initialization
	void Start () 
	{
		originalObject.SetActive(true);
		if(transformedObject != null)
			transformedObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(objectTransforming)
		{
			transformTimer += Time.deltaTime;
			if(transformTimer > timeToTransform)
			{
				originalObject.SetActive(false);
				transformTimer = 0f;
				if(transformedObject != null)
					transformedObject.SetActive(true);		
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			if(takesItem || requiresItem)
			{
				if(col.gameObject.GetComponent<RunnerPlayerController>().inventory.HasItem(takenItem, takenItemAmount)
				|| col.gameObject.GetComponent<RunnerPlayerController>().inventory.HasItem(requiredItem, 1))
				{
					if(takesItem)
					{
						col.gameObject.GetComponent<RunnerPlayerController>().inventory.RemoveItem(takenItem, takenItemAmount);
					}
					if(changingObject)
					{
						objectTransforming = true;
					}
					if(isPickUp)
					{
						col.gameObject.GetComponent<RunnerPlayerController>().inventory.AddItem(itemName, itemAmount);
					}
				}
				else if(isCollider)
				{
					col.gameObject.GetComponent<RunnerPlayerController>().KillPlayer();
				}
			}
			else if(isCollider)		//If it is just a collider (for a collider to pick up it must require or take an item)
			{
				col.gameObject.GetComponent<RunnerPlayerController>().KillPlayer();
			}
			else if(isPickUp)		//If it is just a pickup
			{
				col.gameObject.GetComponent<RunnerPlayerController>().inventory.AddItem(itemName, itemAmount);
				originalObject.SetActive(false);
			}
		}
	}
}
