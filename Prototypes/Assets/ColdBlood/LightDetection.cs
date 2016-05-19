using UnityEngine;
using System.Collections.Generic;

public class LightDetection : MonoBehaviour {

	public List<Light> allSunlights = new List<Light>();
	public GameObject playerObj;

	public LayerMask lightingMask;

	RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(Light l in allSunlights)
		{
			if(Physics.Raycast(l.transform.position, playerObj.transform.position - l.transform.position, out hit, l.range, lightingMask))
			{
				if(hit.collider.gameObject == playerObj)
				{
					Debug.Log("Player lit");
				}
				else
				{
					Debug.Log("Player Shade");
				}
			}
			Debug.DrawRay(allSunlights[0].transform.position, playerObj.transform.position - l.transform.position, Color.red, allSunlights[0].range);
		}
	}
}
