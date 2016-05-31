using UnityEngine;
using System.Collections.Generic;

public class SimsBonds_LevelManager : MonoBehaviour {

	public SimsBondsCombatManager combatManager;
	public List<GameObject> allBlocks = new List<GameObject>();
	public GameObject blockObj;
	public int visibleLevelSize = 10;
	public int totalLevelSize = 100;
	public Vector3 blockStartPos;

	public List<int> partyPositions = new List<int>();
	public List<int> enemyPositions = new List<int>();

	// Use this for initialization
	void Start () 
	{
		//CreateLevel();
		partyPositions[0] = 5;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void CreateLevel()
	{
		for(int i = 0; i < totalLevelSize; i++)
		{
			BuildNextBlock();
		}
	}

	public void BuildNextBlock()
	{
		GameObject g = Instantiate(blockObj);
		g.transform.position = blockStartPos + (Vector3.right * allBlocks.Count);
		g.transform.SetParent(gameObject.transform);

		allBlocks.Add(g);
	}

	public Vector3 GetEnemyBlockLocation(int blockNum)
	{
		enemyPositions.Add(blockNum);
		return allBlocks[blockNum].transform.position;
	}

	public void MoveForward()
	{
		partyPositions[0]++;
		if(partyPositions[0] >= totalLevelSize)
		{
			combatManager.EndQuest();
		}
	}

}
