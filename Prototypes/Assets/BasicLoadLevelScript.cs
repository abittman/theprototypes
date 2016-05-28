using UnityEngine;
using System.Collections;

public class BasicLoadLevelScript : MonoBehaviour {

    public string levelName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLevel(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
