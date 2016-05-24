using UnityEngine;
using System.Collections.Generic;

public class LevelPhotographs : MonoBehaviour {

	public List<Photograph> allTakenPhotographs = new List<Photograph>();
	public PhotoCameraControl photoControl;

	//Temporary
	public ShowPhoto showPhoto;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
		{
			showPhoto.DisplayPhoto(allTakenPhotographs[0]);
			photoControl.canTakePhotos = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	public void AddPhotograph(Texture2D photo, Creature creature, float distance, float accuracy, int boundsIn)
	{
		Photograph newPhoto = new Photograph(photo, creature, distance, accuracy, boundsIn);
		allTakenPhotographs.Add(newPhoto);
	}

	public void NextPhoto()
	{
		if(allTakenPhotographs.Count - 1 > showPhoto.currentPhotoNumber)
		{
			showPhoto.currentPhotoNumber++;
		}
		else
		{
			showPhoto.currentPhotoNumber = 0;
		}
		showPhoto.DisplayPhoto(allTakenPhotographs[showPhoto.currentPhotoNumber]);
	}

	public void PreviousPhoto()
	{
		if(0 < showPhoto.currentPhotoNumber)
		{
			showPhoto.currentPhotoNumber--;
		}
		else
		{
			showPhoto.currentPhotoNumber = allTakenPhotographs.Count - 1;
		}
		showPhoto.DisplayPhoto(allTakenPhotographs[showPhoto.currentPhotoNumber]);
	}
}
