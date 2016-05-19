using UnityEngine;
using System.Collections;

public class MapControls : MonoBehaviour {

	public GameObject cameraObj;

	public GameObject streetView;

	public float zoomScale = 10f;

	Vector3 screenPoint;
	Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		streetView.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void BeginDrag(GameObject draggedObj)
	{
		screenPoint = cameraObj.GetComponent<Camera>().ScreenToWorldPoint(draggedObj.transform.position);

		offset = draggedObj.transform.position - cameraObj.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	public void DragMapAround(GameObject draggedObj)
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		Vector3 curPosition = cameraObj.GetComponent<Camera>().ScreenToWorldPoint(curScreenPoint) + offset;

		draggedObj.transform.position = curPosition;
	}

	public void ZoomIn()
	{
		cameraObj.transform.position += Vector3.forward * zoomScale;
	}

	public void ZoomOut()
	{
		cameraObj.transform.position += Vector3.back * zoomScale;
	}

	public void StreetView()
	{
		streetView.SetActive(true);
	}
}
