using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum LaneID
{
	None,
	Left,
	Middle,
	Right
}

public class RunnerPlayerController : MonoBehaviour {

	public RunnerInventory inventory;

	public LaneID currentLane;
	public bool inTurnZone = false;

	public float forwardMoveSpeed = 1f;
	public float jumpForce = 500f;

	public RunningArea currentRunningArea;
	public BezierSpline currentPath;
	//public iTweenPath middlePath;
	//public LTSpline currentPath;
	public RunningArea startingRunningArea;

	float currentPercentage = 0f;

	bool canTurn = false;
	bool canMove = true;

	public float progress;

	// Use this for initialization
	void Start () 
	{
		currentLane = LaneID.Middle;
		currentRunningArea = startingRunningArea;
		currentRunningArea.InRunningArea(this);
		currentPath = startingRunningArea.middleSpline;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(canMove)
		{
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				GoLeft();
			}
			else if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				GoRight();
			}

			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				Jump();
			}

			MovePlayer();
		}
	}

	void Jump()
	{
		gameObject.GetComponent<Rigidbody>().AddForce(jumpForce * Vector3.up);
	}

	void MovePlayer()
	{
		/* Transform only move */
		//transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * forwardMoveSpeed;

		//Using Bezier Splines
		progress += Time.deltaTime * forwardMoveSpeed;
		if(progress >= 1f)
		{
            //If there is a next lane
            if (GetNextArea() != null)
            {
                MoveToNextArea();
            }
            else
            {
                //If there is no next lane
                transform.localPosition += Time.deltaTime * transform.TransformDirection(Vector3.forward);
            }
		}
		else
		{
			Vector3 splinePosition = currentPath.GetPoint(progress);
			Vector3 nextPos = new Vector3(splinePosition.x, 0f, splinePosition.z);
			transform.localPosition += new Vector3(nextPos.x - transform.position.x, 0f, nextPos.z - transform.position.z);
			transform.LookAt(transform.localPosition + currentPath.GetDirection(progress));
			//Debug.Log("When point is " + currentPath.GetPoint(progress) + ", the velocity is " + currentPath.GetVelocity(progress));
		}
	}

    public RunningArea GetNextArea()
    {
        RunningArea nextArea = null;

        if (!currentRunningArea.pathSplit)
        {
            nextArea = currentRunningArea.nextNonSplitArea;
        }
        else
        {
            switch (currentLane)
            {
                case LaneID.Left:
                    if (currentRunningArea.nextLeftArea != null)
                    {
                        nextArea = currentRunningArea.nextLeftArea;
                    }
                    else
                    {
                        //Do nothing - continue forward
                    }
                    break;
                case LaneID.Middle:
                    if (currentRunningArea.nextMiddleArea != null)
                    {
                        nextArea = currentRunningArea.nextMiddleArea;
                    }
                    else
                    {
                        //Do nothing - continue forward
                    }
                    break;
                case LaneID.Right:
                    if (currentRunningArea.nextRightArea != null)
                    {
                        nextArea = currentRunningArea.nextRightArea;
                    }
                    else
                    {
                        //Do nothing - continue forward
                    }
                    break;
            }
        }

        return nextArea;
    }

	public void MoveToNextArea()
	{
        RunningArea nextArea = GetNextArea();
		currentRunningArea.LeftRunningArea();
		currentRunningArea = nextArea;
		currentRunningArea.InRunningArea(this);
		switch(currentLane)
		{
		case LaneID.Left:
			currentPath = nextArea.leftSpline;
			break;
		case LaneID.Middle:
			currentPath = nextArea.middleSpline;
			break;
		case LaneID.Right:
			//transform.position += transform.TransformDirection(Vector3.left);
			currentPath = currentRunningArea.rightSpline;
			break;
		}
		progress = 0f;
	}

	public void GoLeft()
	{
		if(!canTurn)
		{
			switch(currentLane)
			{
			case LaneID.Left:
				break;
			case LaneID.Middle:
				//transform.position += transform.TransformDirection(Vector3.left); 
				currentPath = currentRunningArea.leftSpline;
				currentLane = LaneID.Left;
				break;
			case LaneID.Right:
				//transform.position += transform.TransformDirection(Vector3.left);
				currentPath = currentRunningArea.middleSpline;
				currentLane = LaneID.Middle;
				break;
			}
		}
	}

	public void GoRight()
	{
		if(!canTurn)
		{
			switch(currentLane)
			{
			case LaneID.Right:
				break;
			case LaneID.Middle:
				//transform.position += transform.TransformDirection(Vector3.right);
				currentPath = currentRunningArea.rightSpline;
				currentLane = LaneID.Right;
				break;
			case LaneID.Left:
				//transform.position += transform.TransformDirection(Vector3.right);
				currentPath = currentRunningArea.middleSpline;
				currentLane = LaneID.Middle;
				break;
			}
		}
	}

	public void KillPlayer()
	{
		Debug.Log("Player dead");
		canMove = false;
		RestartGame();
	}

	//Temporary - put in a game manager later
	public void RestartGame()
	{
		SceneManager.LoadScene("infiniterunnerRPG");
	}
}
