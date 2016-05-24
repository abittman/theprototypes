using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowPhoto : MonoBehaviour {

	public GameObject canvasToEnable;
	public RawImage imageObj;
	public Text nameText;
    public Text scoreText;
    //public Text distText;
	//public Text accText;
	//public Text boundsText;

	public int currentPhotoNumber;

	// Use this for initialization
	void Start () {
		canvasToEnable.SetActive(false);
		currentPhotoNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DisplayPhoto(Photograph photo)
	{
		imageObj.texture = photo.savedPhoto;
		nameText.text = photo.subjectMatter.creatureName;
        scoreText.text = photo.GetScore().ToString();
		/*distText.text = photo.distance.ToString();
		accText.text = photo.focus.ToString();
		boundsText.text = photo.boundsInShot.ToString();*/
		canvasToEnable.SetActive(true);
	}

	public void CloseDisplay()
	{
		canvasToEnable.SetActive(false);
	}
}
