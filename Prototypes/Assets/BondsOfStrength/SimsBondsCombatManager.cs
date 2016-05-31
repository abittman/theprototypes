using UnityEngine;
using System.Collections.Generic;



public class SimsBondsCombatManager : MonoBehaviour {

	[Header("Temp Stuff")]
	public GameObject combatScene;
	public GameObject homeScene;

	[Header("References")]
	public SimsBonds_LevelManager levelManager;
	public BondsInventory inventoryManager;

	public float timePerTick;
	float tickTimer = 0f;
	bool canMove = true;
	bool inCombat = false;

	public List<SimsBonds_Character> partyCharacters = new List<SimsBonds_Character>();
	public List<GameObject> associatedPartyObjects = new List<GameObject>();

	public int numberOfEnemies = 5;
	public List<GameObject> enemyPrefabs = new List<GameObject>();
	List<SimsBonds_Enemies> placedEnemies = new List<SimsBonds_Enemies>();

	//Combat storing
	SimsBonds_Enemies currentEnemy;
	SimsBonds_Character currentPartyMember;

	// Use this for initialization
	void Start () 
	{
		levelManager.combatManager = this;
		tickTimer = 0f;
		//SetupQuest();
	}
	
	// Update is called once per frame
	void Update () 
	{
		tickTimer += Time.deltaTime;
		if(tickTimer >= timePerTick)
		{
			tickTimer = 0f;
			if(canMove)
			{
				MoveParty();
			}
			else if(inCombat)
			{
				DoCombat();
			}
		}
	}

	public void SetupQuest()
	{
		//associatedPartyObjects[0].GetComponent<SimsBonds_Character>() = partyMember1;
		//partyCharacters[0] = partyMember1;
		associatedPartyObjects[0].GetComponent<SimsBonds_PartyCombat>().combatManager = this;
		associatedPartyObjects[0].GetComponent<SimsBonds_PartyCombat>().associatedCharacter = partyCharacters[0];

		levelManager.CreateLevel();
		PlaceEnemies();

		combatScene.SetActive(true);
		homeScene.SetActive(false);
	}

	public void PlaceEnemies()
	{
		for(int i = 0; i < numberOfEnemies; i++)
		{
			GameObject g = Instantiate(enemyPrefabs[0], enemyPrefabs[0].transform.position, enemyPrefabs[0].transform.rotation) as GameObject;

			//Place enemy randomly
			int random = Random.Range(0, levelManager.totalLevelSize);
			Vector3 blockPos = levelManager.GetEnemyBlockLocation(random);
			g.transform.position += blockPos;

			//Add to list
			placedEnemies.Add(g.GetComponent<SimsBonds_Enemies>());
		}
	}

	public void MoveParty()
	{
		foreach(GameObject g in associatedPartyObjects)
		{
			g.transform.position += Vector3.right;
		}
		levelManager.MoveForward();
	}

	public void BeginCombat(SimsBonds_Character partyMember, SimsBonds_Enemies enemy)
	{
		canMove = false;
		inCombat = true;
		currentPartyMember = partyMember;
		currentEnemy = enemy;
	}

	public void DoCombat()
	{
		currentPartyMember.LoseHealth(currentEnemy.damagePerHit);
		currentEnemy.TakeDamage(currentPartyMember.tempDamagePerHit);

		//Character gains exp for combat
		currentPartyMember.combatSkills[0].GainExp(currentPartyMember.tempDamagePerHit);

		if(currentEnemy.isDead)
		{
			inCombat = false;
			canMove = true;
		}
		if(currentPartyMember.isDead)
		{
			EndQuest();
		}
	}

	public void EndQuest()
	{
		canMove = false;
		inCombat = false;
		Debug.Log("Quest over!");

		//Give player character stuff
		inventoryManager.allItems.Find(x => x.name == "Gold").currentAmount += numberOfEnemies * 5;

		combatScene.SetActive(false);
		homeScene.SetActive(true);
	}
}
