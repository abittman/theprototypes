using UnityEngine;
using System.Collections;

public class SimsBonds_Enemies : MonoBehaviour {

	SimsBondsCombatManager combatManager;
	public string name;
	public bool isDead;
	public int health;
	public int damagePerHit;

	public void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			
		}
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
		if(health <= 0)
		{
			EnemyDead();
		}
	}

	public void EnemyDead()
	{
		isDead = true;
		gameObject.SetActive(false);
	}
}
