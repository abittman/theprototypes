using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CharacterConnection
{
	public BondCharacter character;
	public float connectionStrength;
	float defaultStartingStrength = 5f;

	public CharacterConnection()
	{
		connectionStrength = defaultStartingStrength;
	}
}

public class BondCharacter : MonoBehaviour {

	public List<CharacterConnection> connections = new List<CharacterConnection>();

	bool touchingCharacter;
	BondCharacter currentInteraction;

	public BondShield shieldObj;

	public float health = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(touchingCharacter)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				IncreaseBond();
			}
			else if(Input.GetButtonDown("Fire2"))
			{
				RaiseShield();
			}
			if(Input.GetButtonUp("Fire2"))
			{
				LowerShield();
			}
		}

		if(Input.GetKeyDown(KeyCode.Q))
		{
			DamageCharacter(10f);
		}
	}

	void IncreaseBond()
	{
		//Increase side 1's bond for side 2
		foreach(CharacterConnection con in connections)
		{
			if(con.character == currentInteraction)
			{
				con.connectionStrength++;
				break;
			}
		}
	}

	void RaiseShield()
	{
		float connectionStrength = 0;
		//Increase side 1's bond for side 2
		foreach(CharacterConnection con in connections)
		{
			if(con.character == currentInteraction)
			{
				connectionStrength = con.connectionStrength;
				break;
			}
		}
		Vector3 shieldSpawnPoint = ((currentInteraction.gameObject.transform.position - transform.position) * 0.5f) + transform.position;
		shieldObj.ActivateShield(shieldSpawnPoint, connectionStrength);
	}

	void LowerShield()
	{
		shieldObj.DisableShield();
	}

	void DamageCharacter(float damage)
	{
		float finalDamage = damage;
		if(shieldObj.isActive)
		{
			finalDamage -= shieldObj.shieldStrength;
		}
		health -= finalDamage;
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Character")
		{
			touchingCharacter = true;
			currentInteraction = col.gameObject.GetComponent<BondCharacter>();
		}
	}


	void OnCollisionExit(Collision col)
	{
		if(col.gameObject.tag == "Character")
		{
			touchingCharacter = false;
			currentInteraction = null;
		}
	}
}
