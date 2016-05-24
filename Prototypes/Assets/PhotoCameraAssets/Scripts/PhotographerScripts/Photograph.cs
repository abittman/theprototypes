using UnityEngine;
using System.Collections;

[System.Serializable]
public class Photograph {

	public Texture2D savedPhoto;
    public Creature subjectMatter = new Creature();
	public float distance;
	public float focus;
	public int boundsInShot;

	public Photograph(Texture2D tex, Creature subject, float dis, float acc, int boundsIn)
	{
		savedPhoto = tex;
		distance = dis;
		focus = acc;
		boundsInShot = boundsIn;

        //Copy creature details
        subjectMatter.creatureName = subject.creatureName;
        subjectMatter.currentPoseBonus = subject.currentPoseBonus;
        subjectMatter.distanceForScreenFill = subject.distanceForScreenFill;
	}

    public int GetScore()
    {
        int scoreTally = 0;

        //size
        if(subjectMatter.distanceForScreenFill <= distance)
        {
            scoreTally += 500;
        }
        else
        {
            scoreTally += 500 - (100 * (int)(subjectMatter.distanceForScreenFill - distance));
        }

        //accuracy
        if(focus == 0)
        {
            scoreTally += 500;
        }
        else
        {
            scoreTally += 500 - (int)(100f * focus);
        }

        //Poses
        scoreTally += subjectMatter.currentPoseBonus;

        //Multiple - to be done later

        //Facing - to be done later

        return scoreTally;
    }
}