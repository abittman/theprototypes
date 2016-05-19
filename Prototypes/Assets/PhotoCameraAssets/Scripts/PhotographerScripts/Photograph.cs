using UnityEngine;
using System.Collections;

[System.Serializable]
public class Photograph {

	public Texture2D savedPhoto;
	public string subjectMatter;
	public float distance;
	public float focus;
	public int boundsInShot;

	public Photograph(Texture2D tex, string name, float dis, float acc, int boundsIn)
	{
		savedPhoto = tex;
		subjectMatter = name;
		distance = dis;
		focus = acc;
		boundsInShot = boundsIn;
	}
}