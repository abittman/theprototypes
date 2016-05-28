using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Creature
{
    public string creatureName;
    public float distanceForScreenFill = 2f;
    public int currentPoseBonus = 0;
}

[System.Serializable]
public class PhotoCreaturePoses
{
    //public Animation associatedAnimation;
    public string animationName;
    public CreatureAIStates associatedAIState;
    public int bonusPoints;
}

public enum CreatureAIStates
{
    None,
    Idle,
    Walk_Path,
    Walk_To_Object,
    Pose
}
public class Photo_CreatureScript : MonoBehaviour {

    [Header("Creature Details + Scoring")]
    public Creature thisCreature;

    public List<PhotoCreaturePoses> creaturePoses = new List<PhotoCreaturePoses>();
    public CreatureAIStates currentState;
    public Animator thisCreatureAnimator;
    public float transitionTime = 2f;
    float transitionTimer;

    [Header("Creature Photo Requirements")]
	public Transform faceFocus;		//seperate to below. Face should be in both.
	public List<Transform> focusPoints = new List<Transform>();

	// Use this for initialization
	void Start ()
    {
        transitionTimer = transitionTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transitionTimer -= Time.deltaTime;
        bool transitionForward = false;
        if (transitionTimer < 0f)
        {
            transitionTimer = transitionTime;
            if (Random.Range(0, 2) == 0)
            {
                transitionForward = true;
            }

            if (transitionForward)
            {
                switch (currentState)
                {
                    case CreatureAIStates.Idle:
                        currentState = CreatureAIStates.Walk_Path;
                        break;
                    case CreatureAIStates.Walk_Path:
                        currentState = CreatureAIStates.Pose;
                        break;
                    case CreatureAIStates.Walk_To_Object:
                        currentState = CreatureAIStates.Pose;
                        break;
                    case CreatureAIStates.Pose:
                        currentState = CreatureAIStates.Idle;
                        break;
                }
            }
            else
            {
                switch (currentState)
                {
                    case CreatureAIStates.Idle:
                        currentState = CreatureAIStates.Walk_Path;
                        break;
                    case CreatureAIStates.Walk_Path:
                        currentState = CreatureAIStates.Pose;
                        break;
                    case CreatureAIStates.Walk_To_Object:
                        currentState = CreatureAIStates.Pose;
                        break;
                    case CreatureAIStates.Pose:
                        currentState = CreatureAIStates.Idle;
                        break;
                }
            }

            switch (currentState)
            {
                case CreatureAIStates.Idle:
                    thisCreatureAnimator.SetTrigger("IsIdle");
                    break;
                case CreatureAIStates.Walk_Path:
                    thisCreatureAnimator.SetTrigger("IsWalking");
                    break;
                case CreatureAIStates.Walk_To_Object:
                    thisCreatureAnimator.SetTrigger("IsWalking");
                    break;
                case CreatureAIStates.Pose:
                    thisCreatureAnimator.SetTrigger("IsDancing");
                    break;
            }
        }
	}

    public PhotoCreaturePoses GetCurrentPose()
    {
        AnimatorStateInfo currentState = thisCreatureAnimator.GetCurrentAnimatorStateInfo(0);

        foreach (PhotoCreaturePoses pcp in creaturePoses)
        {
            if (currentState.IsName(pcp.animationName))
            {
                thisCreature.currentPoseBonus = pcp.bonusPoints;
                return pcp;
            }
        }

        return null;
    }
}
