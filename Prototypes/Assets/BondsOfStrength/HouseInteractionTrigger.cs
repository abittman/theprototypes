using UnityEngine;
using System.Collections;

public class HouseInteractionTrigger : MonoBehaviour {

	public SimsHouseInteraction houseInteraction;
	public SimsBonds_Character currentUsingCharacter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Character")
		{
			if(currentUsingCharacter == null)
			{
				currentUsingCharacter = col.gameObject.GetComponent<SimsBonds_Character>();
				houseInteraction.UseInteraction(currentUsingCharacter);
			}
			else //already in use
			{
				//If used by different character
				if(col.gameObject.GetComponent<SimsBonds_Character>() != currentUsingCharacter)
				{
					Debug.Log("Character already using this");
				}
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag == "Character")
		{
			//Debug.Log("Character leaves");
			currentUsingCharacter = null;
		}
	}

}
