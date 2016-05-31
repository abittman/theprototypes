using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimsHouseInteraction : MonoBehaviour {

    public string reqLifeSkillName;
    public int reqSkillLevel;
    [Header("Gained")]
    public int expGainedForUse;
    [Space]
    public SimsBonds_Needs needGained;
    public float amountGained;
    [Space]
    public Canvas associatedCanvas;

    // Use this for initialization
    void Start () 
    {
    	if(associatedCanvas != null)
			associatedCanvas.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void UseInteraction(SimsBonds_Character characterRef)
    {
        bool canUse = false;

        if(reqLifeSkillName != "")		//If not null
        {
	        foreach(SimsLifeSkill lifeSkill in characterRef.lifeSkills)
	        {
	            if(lifeSkill.skillName == reqLifeSkillName)     //Skill found
	            {
	                if(lifeSkill.currentLevel >= reqSkillLevel)
	                {
	                    canUse = true;
	                    lifeSkill.GainExp(expGainedForUse);				//Add exp to this skill - probably should store it and do after loop
	                    break;          //Found with requirements met. So exit.
	                }
	                break;              //Found, but requirements not met. So exit.
	            }
	        }
        }
        else
        {
        	//Because there are no requirements, it can be used
        	canUse = true;
        }

        if (canUse)
        {
            switch (needGained)
            {
                case SimsBonds_Needs.Health:
                    {
                        characterRef.GainHealth(amountGained);
                        break;
                    }
                case SimsBonds_Needs.Energy:
	                {
	                	characterRef.GainEnergy(amountGained);
	                	break;
	                }
              	case SimsBonds_Needs.Stress:
	              	{
	              		characterRef.GainStress(amountGained);
	              		break;
	              	}
            }
        }

        if(associatedCanvas != null)
        {
        	associatedCanvas.gameObject.SetActive(true);
        }
    }
}
