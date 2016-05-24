using UnityEngine;
using System.Collections;

public class SimsHouseInteraction : MonoBehaviour {

    public string reqLifeSkillName;
    public int reqSkillLevel;
    [Header("Gained")]
    public int expGainedForUse;
    [Space]
    public SimsBonds_Needs needGained;
    public float amountGained;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UseInteraction(SimsBonds_Character characterRef)
    {
        bool canUse = false;
        foreach(SimsLifeSkill lifeSkill in characterRef.lifeSkills)
        {
            if(lifeSkill.skillName == reqLifeSkillName)     //Skill found
            {
                if(lifeSkill.currentLevel >= reqSkillLevel)
                {
                    canUse = true;
                    break;          //Found with requirements met. So exit.
                }
                break;              //Found, but requirements not met. So exit.
            }
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
            }
        }
    }
}
