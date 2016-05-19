using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PhotoCameraDetection : MonoBehaviour {

	public Camera photoCamera;
	public LevelPhotographs levelAlbum;

	List<GameObject> allTaggedObjects = new List<GameObject>();
	List<GameObject> currentObjectsOnScreen = new List<GameObject>();
	public string[] tagsToLookAt;
	//public GameObject anObject;
    //Collider anObjCollider;

	Plane[] planes;

	public LayerMask myMask;
	RaycastHit hit;
	Ray ray;
    public Image focusImage;
    bool isFocused = false;

	// Use this for initialization
	void Start () 
	{
		planes = GeometryUtility.CalculateFrustumPlanes(photoCamera);
		//anObjCollider = anObject.GetComponent<Collider>();


		foreach(string tag in tagsToLookAt)
		{
			GameObject[] go = GameObject.FindGameObjectsWithTag(tag);
			foreach(GameObject g in go)	
			{
				allTaggedObjects.Add(g);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		CheckFocus();

		planes = GeometryUtility.CalculateFrustumPlanes(GetComponent<Camera>());

		foreach(GameObject go in allTaggedObjects)
		{
			if (GeometryUtility.TestPlanesAABB(planes, go.GetComponent<Collider>().bounds))
    	    {
    	    	//Something detected - print new thing

    	    	//If it has not been detected previously...
    	    	if(!currentObjectsOnScreen.Contains(go))
    	    	{
    	    		currentObjectsOnScreen.Add(go);

    	    	}
    	    	else //but if it was detected previously
    	    	{
    	    		
    	    	}

    	        //Debug.Log(go.name + " has been detected!");
    	    }
			else if(currentObjectsOnScreen.Contains(go))
			{
				//Object was on screen, but now is not
				currentObjectsOnScreen.Remove(go);
			}
        	else
        	{
        		//Nothing detected - do nothing
            	//Debug.Log("Nothing has been detected");
            }
        }
	}

	void CheckFocus()
    {
		ray = photoCamera.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
		if(Physics.Raycast(ray, out hit, myMask))
		{
			Debug.DrawLine(ray.origin, hit.point, Color.blue);
			if(hit.collider.tag == "Creature")
			{
				focusImage.color = Color.red;
				isFocused = true;
			}
			else
			{
				focusImage.color = Color.black;
				isFocused = false;
			}
		}
		else
		{
			focusImage.color = Color.black;
			isFocused = false;
		}
    }

	public List<Photograph> GetScreenshotData()
	{
		List<Photograph> photoData = new List<Photograph>();
		Photograph bestPhoto = null;

		//Refresh ray before using it just in case
		CheckFocus();

		foreach(GameObject g in currentObjectsOnScreen)
		{
			//Creature name
			string name = g.name;
			//Distance from camera
			float distance = Vector3.Distance(g.transform.position, photoCamera.transform.position);
			//accuracy of shot
			Vector3 pointOfIntersection = ray.origin + ray.direction * Vector3.Dot(ray.direction, g.transform.position - ray.origin);
			Vector3 closestPoint = g.GetComponent<Collider>().ClosestPointOnBounds(pointOfIntersection);
			float accuracy = Vector3.Distance(pointOfIntersection, closestPoint);

			//cut off from shot
			//Check ray to each bounds. If the return of the bound is not, deduct (number of bounds visible being the marker?)
			//Points need to be: 1 - not blocked. 2 - within camera
			int boundsIn = 0;
			/* Old use of bounds
			if(CameraCanSeePoint(g.GetComponent<Collider>().bounds.min))
			{
				Debug.Log("Min in camera view");
				Vector3 minPoint = g.GetComponent<Collider>().bounds.center - g.GetComponent<Collider>().bounds.extents + new Vector3(0.1f, 0.1f, 0.1f);
				//Debug.DrawRay(photoCamera.transform.localPosition, minPoint, Color.red, 100f);
				if(Physics.Raycast(photoCamera.transform.localPosition, 
					minPoint, out hit, 100f, myMask))
				{
					Debug.DrawLine(photoCamera.transform.localPosition, hit.point, Color.red, 5f);
					Debug.Log(hit.collider.name);
					if(hit.collider.gameObject == g)
					{
						Debug.Log("Can see min");
						boundsIn++;
					}
				}
				else
				{
					Debug.Log(minPoint);
				}
				boundsIn++;
			}

			if(CameraCanSeePoint(g.GetComponent<Collider>().bounds.max))
			{
				Debug.Log("Max in camera view");
				Vector3 maxPoint = g.GetComponent<Collider>().bounds.center + g.GetComponent<Collider>().bounds.extents - new Vector3(0.1f, 0.1f, 0.1f);
				//Debug.DrawRay(photoCamera.transform.localPosition, maxPoint, Color.green, 100f);
				if(Physics.Raycast(photoCamera.transform.localPosition, 
									maxPoint, out hit, 100f, myMask))
				{
					//Debug.Log(hit.collider.name);
					//Debug.DrawLine(photoCamera.transform.localPosition, hit.point, Color.green, 5f);
					if(hit.collider.gameObject == g)
					{
						Debug.Log("Can see max");
						boundsIn++;
					}
				}
				boundsIn++;			
			}
			*/
			Photo_CreatureScript thisCreature = g.GetComponent<Photo_CreatureScript>();
			foreach(Transform t in thisCreature.focusPoints)
			{
				if(CameraCanSeePoint(t.position))
				{
					Debug.Log(t.gameObject.name + " is in shot.");
					if(Physics.Raycast(photoCamera.transform.position,
										t.position - photoCamera.transform.position, out hit, 100f, myMask))
					{
						if(hit.collider.transform.gameObject == g)
						{
							Debug.Log(t.gameObject.name + "is not blocked.");
							boundsIn++;
						}
					}
				}
			}
			Debug.Log("Creature: " + name + ". Distance: " + distance.ToString() + ". Accuracy: " + accuracy.ToString() + ". Bound: " + boundsIn.ToString());
			if(bestPhoto == null)
			{
				bestPhoto = new Photograph(null, name, distance, accuracy, boundsIn);
			}
			else
			{
				//Compare points
			}
		}
		if(bestPhoto != null)
			photoData.Add(bestPhoto);

		return photoData;
	}

	bool CameraCanSeePoint(Vector3 point)
	{
		Vector3 viewportPoint = photoCamera.WorldToViewportPoint(point);

		return (new Rect(0, 0, 1, 1).Contains(viewportPoint));
	}
}
