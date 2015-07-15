using UnityEngine;
using System.Collections;

public class AspectRatio : MonoBehaviour {
	public Camera Gamecamera;
	public Vector2 aspectRatio = new Vector2(9,16);
	// Use this for initialization
	void Start () {
		Gamecamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = aspectRatio.x / aspectRatio.y;
		
		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
		
		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		
		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{  
			Rect rect = Gamecamera.rect;
			
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
			
			Gamecamera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;
			
			Rect rect = Gamecamera.rect;
			
			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;
			
			Gamecamera.rect = rect;
		}
	}
}
