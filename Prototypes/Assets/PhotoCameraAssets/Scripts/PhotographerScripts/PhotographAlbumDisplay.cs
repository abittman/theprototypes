using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PhotographAlbumDisplay : MonoBehaviour {

    public GameObject panelToAddTo;
    public AlbumPhotoScript albumPhotoPrefab;

    public List<GameObject> photosInAlbum = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddPhotoToAlbum(Photograph photo)
    {
        //Instantiate the prefab
        GameObject g = Instantiate(albumPhotoPrefab.gameObject) as GameObject;

        //Set the values
        g.GetComponent<AlbumPhotoScript>().subjectNameTextObj.text = photo.subjectMatter.creatureName;
        g.GetComponent<AlbumPhotoScript>().scoreTextObj.text = photo.GetScore().ToString();
        g.GetComponent<AlbumPhotoScript>().photoImageObj.texture = photo.savedPhoto;

        //Align to panel
        g.transform.SetParent(panelToAddTo.transform);

        photosInAlbum.Add(g);
    }
}
