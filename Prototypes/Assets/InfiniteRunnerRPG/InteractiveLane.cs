using UnityEngine;
using System.Collections;

public class InteractiveLane : MonoBehaviour {

	public GameObject canvasForInteractiveLane;
	public BezierSpline associatedSpline;
	[Range(0f, 1f)]
	public float minPercentageOfLaneForInteraction;
	[Range(0f, 1f)]
	public float maxPercentageOfLaneForInteraction;

	public bool isActive = false;

	// Use this for initialization
	void Start () {
		canvasForInteractiveLane.SetActive(false);
		isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CheckForInteraction(float currentProgress)
	{
		if(currentProgress > minPercentageOfLaneForInteraction
		&& currentProgress < maxPercentageOfLaneForInteraction)
		{
			canvasForInteractiveLane.SetActive(true);
			isActive = true;
		}
		else
		{
			canvasForInteractiveLane.SetActive(false);
			isActive = false;
		}
	}

	public void DisableInteraction()
	{
		canvasForInteractiveLane.SetActive(false);
		isActive = false;
	}
}
