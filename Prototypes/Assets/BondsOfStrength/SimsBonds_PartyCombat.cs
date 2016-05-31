using UnityEngine;
using System.Collections;

public class SimsBonds_PartyCombat : MonoBehaviour {

	public SimsBondsCombatManager combatManager;
	public SimsBonds_Character associatedCharacter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			combatManager.BeginCombat(associatedCharacter, col.gameObject.GetComponent<SimsBonds_Enemies>());
		}
	}
}
