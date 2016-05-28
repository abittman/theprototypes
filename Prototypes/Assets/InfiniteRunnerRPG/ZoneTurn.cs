using UnityEngine;
using System.Collections;

public class ZoneTurn : MonoBehaviour {

	public RunnerPlayerController playerRunner;

	//public iTweenPath transitionPath = new iTweenPath();

	public bool pathSplit = false;

	public RunningArea lastArea;
	public RunningArea nextNonSplitArea;
	public RunningArea nextLeftArea;
	public RunningArea nextMiddleArea;
	public RunningArea nextRightArea;
	
	// Use this for initialization
	void Start () 
	{
		//transitionPath.nodeCount = 2;
		//Idea here is to build the transition as a path. Might be more than needed.
		//transitionPath.nodes[0] = new Vector3(playerRunner.currentLane.
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
/*
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			//Debug.Log("Next area!");
			if(!pathSplit)
			{
				playerRunner.MoveToNextArea(nextNonSplitArea);
			}
			else
			{
				switch(playerRunner.currentLane)
				{
				case LaneID.Left:
					if(nextLeftArea != null)
					{
						playerRunner.MoveToNextArea(nextLeftArea);
					}
					else
					{
						//Do nothing - continue forward
					}
					break;
				case LaneID.Middle:
					if(nextMiddleArea != null)
					{
						playerRunner.MoveToNextArea(nextMiddleArea);
					}
					else
					{
						//Do nothing - continue forward
					}
					break;
				case LaneID.Right:
					if(nextRightArea != null)
					{
						playerRunner.MoveToNextArea(nextRightArea);
					}
					else
					{
						//Do nothing - continue forward
					}
					break;
				}
			}
		}
	}
    */
}
