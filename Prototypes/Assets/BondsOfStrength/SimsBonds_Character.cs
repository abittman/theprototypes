using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SimsLifeSkill
{
    public string skillName;
    public int currentLevel;
    public int currentExp;
    public int maxLevel;
    public List<int> expPerLevel = new List<int>();

    //Some sort of constructor?
}

[System.Serializable]
public class SimsCombatSkill
{
    public string skillName;
    public int currentLevel;
    public int currentExp;
    public int maxLevel;
    public List<int> expPerLevel = new List<int>();

    //Some sort of constructor?
}

public enum SimsBonds_Needs
{
    None,
    Energy,
    Health,
    Stress
}

public class SimsBonds_Character : MonoBehaviour {

    [Header("Character Needs")]
    public float currentEnergy;
    public float maxEnergy;
    [Space]
    public float currentHealth;
    public float maxHealth;
    [Space]
    public float currentStress;
    public float maxStress;

    [Header("Life Skills")]
    public List<SimsLifeSkill> lifeSkills = new List<SimsLifeSkill>();

    [Header("Combat Skills")]
    public List<SimsCombatSkill> combatSkills = new List<SimsCombatSkill>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GainHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void LoseHealth(float amount)
    {
        currentHealth -= amount;
        //If less than 0 do something. Does not go below 0.
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            //dead
        }
    }

    public void GainEnergy(float amount)
    {
        currentEnergy += amount;
        if(currentEnergy >= maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }

    public void LoseEnergy(float amount)
    {
        currentEnergy -= amount;
        //If less than 0 do something. Does not go below 0.
        if (currentEnergy <= 0f)
        {
            currentEnergy = 0f;
            //fatigued
        }
    }
}
