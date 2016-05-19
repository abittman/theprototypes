using UnityEngine;
using System.Collections;

public class BondShield : MonoBehaviour {

	public bool isActive;
	public float shieldStrength;
	public float scalePerPoint = 1f;
	public Vector3 minimumScale;
	public Vector3 maximumScale;


	// Use this for initialization
	void Start () {
		isActive = false;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateShield(Vector3 shieldSpawnPoint, float connectionStrength)
	{
		transform.position = shieldSpawnPoint;

		Vector3 shieldScale = minimumScale + new Vector3((connectionStrength * scalePerPoint), (connectionStrength * scalePerPoint), (connectionStrength * scalePerPoint));
		if(shieldScale.x > maximumScale.x)
			shieldScale = maximumScale;
		transform.localScale = shieldScale;

		shieldStrength = connectionStrength;
		isActive = true;

		gameObject.SetActive(true);
	}

	public void DisableShield()
	{
		isActive = false;
		gameObject.SetActive(false);
	}
}
