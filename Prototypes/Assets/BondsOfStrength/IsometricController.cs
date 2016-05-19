using UnityEngine;
using System.Collections;

public class IsometricController : MonoBehaviour {

	public float moveSpeed = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float xMove = Input.GetAxis("Horizontal");
		float zMove = Input.GetAxis("Vertical");

		MoveCharacter(xMove, zMove);
	}

	void MoveCharacter(float xVal, float zVal)
	{
		float xMove = xVal * moveSpeed * Time.deltaTime;
		float zMove = zVal * moveSpeed * Time.deltaTime;

		transform.position += new Vector3(xMove, 0f, zMove);
	}
}
