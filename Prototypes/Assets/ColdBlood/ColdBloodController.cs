using UnityEngine;
using System.Collections;

public class ColdBloodController : MonoBehaviour {

	public DayNightCycle dayNight;

	public float currentEnergy = 0f;
	public float maxEnergy = 10f;

	public float moveSpeed = 0.25f;
	public float rotateSpeed = 4.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(dayNight.isSunny)
		{
			GainEnergy();
		}
		else
		{
			LoseEnergy();
		}

		float xMove = Input.GetAxis("Horizontal");
		float zMove = Input.GetAxis("Vertical");
		MovePlayer(xMove, zMove);
	}

	void MovePlayer(float xMove, float zMove)
	{
		float xVal = xMove * Time.deltaTime * rotateSpeed * currentEnergy;
		float zVal = zMove * Time.deltaTime * moveSpeed * currentEnergy;

		transform.position += transform.TransformDirection(Vector3.forward) * zVal;
		transform.eulerAngles += transform.TransformDirection(Vector3.up) * xVal;
	}

	void GainEnergy()
	{
		currentEnergy += Time.deltaTime;
		if(currentEnergy >= maxEnergy)
			currentEnergy = maxEnergy;
	}

	void LoseEnergy()
	{
		currentEnergy -= Time.deltaTime;
		if(currentEnergy <= 0f)
			currentEnergy = 0f;
	}
}
