using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColdBloodStats : MonoBehaviour {

	public Image hungerBar;

	public float startingHunger = 100f;
	public float currentHunger = 100f;
	public float hungerFallSpeed = 1f;

	bool hungerFalls = true;

	// Use this for initialization
	void Start () 
	{
		currentHunger = startingHunger;
		hungerFalls = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(hungerFalls)
		{
			currentHunger -= Time.deltaTime;
			if(currentHunger <= 0f)
			{
				Debug.Log("You died");
			}
		}
		hungerBar.fillAmount = currentHunger / startingHunger;
	}

	public void FillHunger(float amount)
	{
		currentHunger += amount;
	}
}
