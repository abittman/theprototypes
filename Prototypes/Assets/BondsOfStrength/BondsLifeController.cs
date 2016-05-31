using UnityEngine;
using System.Collections;

public class BondsLifeController : MonoBehaviour {

    public bool isSelected = true;
    public LayerMask myMask;
    RaycastHit hit;
    NavMeshAgent myAgent;

	// Use this for initialization
	void Start ()
    {
        myAgent = gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(isSelected)
        {
        	//Needs to have GUI take precedance.
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, 100f, myMask))
                {
                    myAgent.SetDestination(hit.point);
                }
            }
        }
	}
}
