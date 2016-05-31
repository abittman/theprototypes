using UnityEngine;
using System.Collections.Generic;

public class RunningArea : MonoBehaviour {

	public RunnerPlayerController pController;
	bool isInArea = false;

	public BezierSpline leftSpline;
	public BezierSpline rightSpline;
	public BezierSpline middleSpline;

	public List<InteractiveLane> interactiveLanes = new List<InteractiveLane>();

    public bool pathSplit = false;

    public RunningArea lastArea;
    public RunningArea nextNonSplitArea;
    public RunningArea nextLeftArea;
    public RunningArea nextMiddleArea;
    public RunningArea nextRightArea;

    // Use this for initialization
    void Start () {
		InteractiveLane[] iLanes = gameObject.GetComponentsInChildren<InteractiveLane>();
		interactiveLanes.AddRange(iLanes);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isInArea)
		{
			foreach(InteractiveLane iLane in interactiveLanes)
			{
				if(iLane.associatedSpline == pController.currentPath)
				{
					iLane.CheckForInteraction(pController.progress);
				}
				else if(iLane.isActive)
				{
					iLane.DisableInteraction();
				}
			}
		}
	}

	public void InRunningArea(RunnerPlayerController playerRef)
	{
		pController = playerRef;
		isInArea = true;
	}

	public void LeftRunningArea()
	{
		isInArea = false;
		foreach(InteractiveLane iLane in interactiveLanes)
		{
			iLane.DisableInteraction();
		}
	}
}
