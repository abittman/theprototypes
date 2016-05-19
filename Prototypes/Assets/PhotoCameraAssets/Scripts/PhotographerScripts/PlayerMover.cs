using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

	public BezierSpline levelPath;
	public float secondsToCompleteLevel = 10f;
	float levelTimer = 0f;
	bool isMoving = false;
	float progress;

	// Use this for initialization
	void Start () {
		progress = 0f;
		levelTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			StartMovement();
		}

		if(isMoving)
		{
			MovePlayer();
		}
	}

	public void StartMovement()
	{
		isMoving = true;
	}

	public void MovePlayer()
	{
		levelTimer += Time.deltaTime;
		progress = levelTimer / secondsToCompleteLevel;
		if(progress > 1f)
		{
			//Stop
			progress = 1f;
			//transform.localPosition += Time.deltaTime * transform.TransformDirection(Vector3.forward);
		}
		else
		{
			Vector3 splinePosition = levelPath.GetPoint(progress);
			//Vector3 nextPos = new Vector3(splinePosition.x, 0f, splinePosition.z);
			//transform.localPosition += new Vector3(nextPos.x - transform.position.x, 0f, nextPos.z - transform.position.z);
			transform.localPosition = splinePosition;
			transform.LookAt(transform.localPosition + levelPath.GetDirection(progress));
			//Debug.Log("When point is " + currentPath.GetPoint(progress) + ", the velocity is " + currentPath.GetVelocity(progress));
		}
	}
}
