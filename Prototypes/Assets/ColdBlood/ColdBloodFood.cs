using UnityEngine;
using System.Collections;

public class ColdBloodFood : MonoBehaviour {

	public float foodVal = 50f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<ColdBloodStats>().FillHunger(foodVal);
			Destroy(gameObject);
		}
	}
}
