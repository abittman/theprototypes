using UnityEngine;
using System;
using System.Collections.Generic;

public enum TileType
{
	None,
	BlueSea,
	Island
}

public class IslandProGen : MonoBehaviour {

	public GameObject islandTilePrefab;
	public GameObject blueSeaTilePrefab;

	public List<Vector2> neighbourCoordinates = new List<Vector2>();

	List<GameObject> tiles = new List<GameObject>();

	public int xDimension;
	public int yDimension;

	public int maxSpaceBetweenTiles;
	public int numberOfIslands;

	// Use this for initialization
	void Start () 
	{
		CreateWorld();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateWorld()
	{
		int totalTiles = xDimension * yDimension;
		TileType lastTileType = TileType.None;

		//Spiral version
		//Make water only first
		//Centre Tile
		CreateTile(0, 0);

		for(int currentLevel = 1; currentLevel <= xDimension / 2; currentLevel++)
		{
			for(int x = -currentLevel; x <= currentLevel; x++)
			{
				if(x == -currentLevel || x == currentLevel)
				{
					for(int y = -currentLevel; y <= currentLevel; y++)
					{
						CreateTile(x, y);
					}
				}
				else
				{
					//Positive y
					int y = currentLevel;
					CreateTile(x, y);
					//Negative y
					y = -currentLevel;
					CreateTile(x, y);
				}
			}
		}

		MakeTileVariations();
	}

	void CreateTile(int xLoc, int yLoc)
	{
		GameObject g = Instantiate(blueSeaTilePrefab) as GameObject;
		g.transform.position = new Vector3(xLoc, 0f, yLoc);
		g.transform.SetParent(gameObject.transform);
		g.GetComponent<TileClass>().xLoc = xLoc;
		g.GetComponent<TileClass>().yLoc = yLoc;
		tiles.Add(g);
	}

	void MakeTileVariations()
	{
		System.Random r = new System.Random();
		for(int i = 0; i < numberOfIslands; i++)
		{
			bool isChosen = false;
			do
			{
				int ranX = r.Next(-xDimension, xDimension + 1);
				int ranY = r.Next(-yDimension, yDimension + 1);

				GameObject chosenTile = tiles.Find(x => (x.GetComponent<TileClass>().yLoc == ranY) && (x.GetComponent<TileClass>().xLoc == ranX));

				if(chosenTile != null)
				{
					float ranChance = (float)r.NextDouble();
					int distance = ClosestIslandDistance(ranX, ranY);
					float result = Mathf.Pow(((float)distance / 6f), 2f);
					//Debug.Log(distance.ToString() + " " + result.ToString() + " " + ranChance.ToString());
					if(result >= ranChance)
					{
						Debug.Log("Chosen tile" + ranX.ToString() + ranY.ToString() + " at " + distance.ToString());
						int chosenTileIndex = tiles.IndexOf(chosenTile);

						GameObject g = Instantiate(islandTilePrefab) as GameObject;
						g.transform.position = new Vector3(chosenTile.GetComponent<TileClass>().xLoc, 0f, chosenTile.GetComponent<TileClass>().yLoc);
						g.transform.SetParent(gameObject.transform);
						g.GetComponent<TileClass>().xLoc = chosenTile.GetComponent<TileClass>().xLoc;
						g.GetComponent<TileClass>().yLoc = chosenTile.GetComponent<TileClass>().yLoc;

						tiles[chosenTileIndex] = g;
						Destroy(chosenTile);

						isChosen = true;
					}
				}
			}while(isChosen == false);
		}
	}

	//This is actually all wrong - probably need to use spiral search method like how they are created. This only gets immediate neighbours
	int ClosestIslandDistance(int x, int y)
	{
		for(int currentDistance = 1; currentDistance < maxSpaceBetweenTiles; currentDistance++)
		{
			//Check adjacent
			foreach(Vector2 coord in neighbourCoordinates)
			{
				Vector2 neighbourCoord = new Vector2(x, y) + coord;
				GameObject neighbourTile = tiles.Find(z => (z.GetComponent<TileClass>().yLoc == neighbourCoord.y) && (z.GetComponent<TileClass>().xLoc == neighbourCoord.x));	
				if(neighbourTile != null)
				{
					if(neighbourTile.GetComponent<TileClass>().thisType == TileType.Island)
					{
						return (int)(neighbourCoord.x + neighbourCoord.y);
					}
				}
			}
		}
		return maxSpaceBetweenTiles;
	}
}
