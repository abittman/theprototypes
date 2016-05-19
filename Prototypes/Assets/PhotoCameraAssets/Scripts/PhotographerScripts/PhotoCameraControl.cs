using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class PhotoCameraControl : MonoBehaviour {

	public bool photoCameraCanMove = true;
	public bool canTakePhotos = true;

	public GameObject attachedCamera;
	public Camera screenshotCamera;
	public PhotoCameraDetection cameraDetection;
	public LevelPhotographs levelAlbum;

	public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;

	private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;

    public RenderTexture renderTexture;
    string filename = "";
    public string path = "";

	// Use this for initialization
	void Start () {
		Init();
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(photoCameraCanMove && canTakePhotos)
		{
			float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        	float xRot = Input.GetAxis("Mouse Y") * YSensitivity;
			LookRotation(xRot, yRot);
		}
	}

	void LateUpdate()
	{
		if(Input.GetButtonDown("Fire1") && canTakePhotos)
		{
			TakePhoto();
		}	
	}

	public void TakePhoto()
	{
		StartCoroutine(TakeScreenshot());
	}

    public void Init()
    {
        m_CharacterTargetRot = transform.localRotation;
        m_CameraTargetRot = attachedCamera.transform.localRotation;
    }

    public void LookRotation(float xRot, float yRot)
    {
        m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

        if(clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

        if(smooth)
        {
			transform.localRotation = Quaternion.Slerp (transform.localRotation, m_CharacterTargetRot,
                smoothTime * Time.deltaTime);
			attachedCamera.transform.localRotation = Quaternion.Slerp (attachedCamera.transform.localRotation, m_CameraTargetRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            transform.localRotation = m_CharacterTargetRot;
			attachedCamera.transform.localRotation = m_CameraTargetRot;
        }
    }


    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

        angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    string fileName(int width, int height)
    {
    	return string.Format("screen_{0}x{1}_{2}.png",
    							width,
    							height,
    							System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public IEnumerator TakeScreenshot()
    {
    	yield return new WaitForEndOfFrame();

    	RenderTexture currentRT = RenderTexture.active;

		RenderTexture.active = screenshotCamera.targetTexture;

		screenshotCamera.Render();

		Texture2D imageOverview = new Texture2D(screenshotCamera.targetTexture.width, 
												screenshotCamera.targetTexture.height,
												TextureFormat.RGB24,
												false);
		imageOverview.ReadPixels(new Rect(0, 0, screenshotCamera.targetTexture.width, screenshotCamera.targetTexture.height), 0, 0);
		imageOverview.Apply();

		RenderTexture.active = currentRT;

		List<Photograph> photoData = cameraDetection.GetScreenshotData();
		if(photoData.Count > 0)
		{
			foreach(Photograph p in photoData)
			{
				levelAlbum.AddPhotograph(imageOverview, p.subjectMatter, p.distance, p.focus, p.boundsInShot);
			}
		}
		/* Saving to file - probably unnecessary? Need to save somewhere though.
		//Encode to PNG
		byte[] bytes = imageOverview.EncodeToPNG();

		//Save in memory
		//NEED to relook at how it is saved and loaded. Resoures doesn't work well. Look at asset bundles or straight up file savings.
		filename = fileName(Convert.ToInt32(imageOverview.width), Convert.ToInt32(imageOverview.height));
		path = Application.dataPath + "/Resources/Snapshots/" + filename;

		System.IO.File.WriteAllBytes(path, bytes);
		*/
    }
}
