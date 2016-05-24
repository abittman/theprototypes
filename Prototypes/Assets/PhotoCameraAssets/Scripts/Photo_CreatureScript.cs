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
    public int bonusPoints;
}

public class Photo_CreatureScript : MonoBehaviour {

    [Header("Creature Details + Scoring")]
    public Creature thisCreature;

    public List<PhotoCreaturePoses> creaturePoses = new List<PhotoCreaturePoses>();
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
                if (thisCreatureAnimator.GetBool("IsIdle"))
                {
                    thisCreatureAnimator.SetBool("IsIdle", false);
                    thisCreatureAnimator.SetBool("IsWalking", true);
                }
                else if (thisCreatureAnimator.GetBool("IsWalking"))
                {
                    thisCreatureAnimator.SetBool("IsWalking", false);
                    thisCreatureAnimator.SetBool("IsDancing", true);
                }
                else if (thisCreatureAnimator.GetBool("IsDancing"))
                {
                    thisCreatureAnimator.SetBool("IsDancing", false);
                    thisCreatureAnimator.SetBool("IsIdle", true);
                }
            }
            else
            {
                if (thisCreatureAnimator.GetBool("IsIdle"))
                {
                    thisCreatureAnimator.SetBool("IsIdle", false);
                    thisCreatureAnimator.SetBool("IsWalking", true);
                }
                else if (thisCreatureAnimator.GetBool("IsWalking"))
                {
                    thisCreatureAnimator.SetBool("IsWalking", false);
                    thisCreatureAnimator.SetBool("IsIdle", true);
                }
                else if (thisCreatureAnimator.GetBool("IsDancing"))
                {
                    thisCreatureAnimator.SetBool("IsDancing", false);
                    thisCreatureAnimator.SetBool("IsWalking", true);
                }
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
