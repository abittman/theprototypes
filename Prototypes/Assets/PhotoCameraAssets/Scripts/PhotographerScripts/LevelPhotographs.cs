using UnityEngine;
using System.Collections.Generic;

public class LevelPhotographs : MonoBehaviour {

	public List<Photograph> allTakenPhotographs = new List<Photograph>();
	public PhotoCameraControl photoControl;


    public BasicLoadLevelScript levelLoadScript;
    PhotographAlbumDisplay album;

    bool newScene = false;

	//Temporary
	//public ShowPhoto showPhoto;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))     //temp scene end
		{
            /*
			showPhoto.DisplayPhoto(allTakenPhotographs[0]);
			photoControl.canTakePhotos = false;
            */
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
            
            levelLoadScript.LoadLevel("LookAtPhotosScene");
            newScene = true;
		}

        if (newScene)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene() == UnityEngine.SceneManagement.SceneManager.GetSceneByName("LookAtPhotosScene"))
            {
                album = GameObject.Find("AlbumManager").GetComponent<PhotographAlbumDisplay>();
                AddPhotosToAlbum();
                newScene = false;
            }
        }
	}

	public void AddPhotograph(Texture2D photo, Creature creature, float distance, float accuracy, int boundsIn)
	{
		Photograph newPhoto = new Photograph(photo, creature, distance, accuracy, boundsIn);
		allTakenPhotographs.Add(newPhoto);
	}

    public void AddPhotosToAlbum()
    {
        foreach(Photograph p in allTakenPhotographs)
        {
            album.AddPhotoToAlbum(p);
        }
    }
/*
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
    */
}
