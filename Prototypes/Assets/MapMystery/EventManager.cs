using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum AppCategory
{
	None,
	Map,
	Email,
	News
}

[System.Serializable]
public class MapMysteryEvent
{
	public string eventID;
	public List<GameObject> activatedObjects = new List<GameObject>();
	public string eventNotificationLog;
	public float eventTimeDelay;
}

public class EventManager : MonoBehaviour {

	public List<MapMysteryEvent> allEvents = new List<MapMysteryEvent>();

	public GameObject notificationCanvas;
	public Text notificationText;

	List<MapMysteryEvent> pendingEvents = new List<MapMysteryEvent>();

	public float notificationTimer = 5f;

	// Use this for initialization
	void Start () 
	{
		foreach(MapMysteryEvent e in allEvents)
		{
			foreach(GameObject g in e.activatedObjects)
			{
				g.SetActive(false);
			}
		}

		EventOccurs("NewEmail");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(pendingEvents.Count > 0)
		{
			foreach(MapMysteryEvent e in pendingEvents)
			{
				e.eventTimeDelay -= Time.deltaTime;
				if(e.eventTimeDelay <= 0f)
				{
					EventOccurs(e.eventID);
				}
			}
			pendingEvents.RemoveAll(x => x.eventTimeDelay <= 0f);
		}

		if(notificationCanvas.activeInHierarchy)
		{
			notificationTimer -= Time.deltaTime;
			if(notificationTimer <= 0f)
			{
				notificationCanvas.SetActive(false);
				notificationTimer = 5f;
			}
		}
	}

	public void EventOccurs(string eventID)
	{
		foreach(MapMysteryEvent e in allEvents)
		{
			if(e.eventID == eventID)
			{
				if(e.eventTimeDelay > 0f)
				{
					pendingEvents.Add(e);
				}
				else
				{
					foreach(GameObject g in e.activatedObjects)
					{
						g.SetActive(true);
					}
					notificationText.text = e.eventNotificationLog;
					notificationCanvas.SetActive(true);
				}
				break;
			}
		}
	}
}
