using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTexture : MonoBehaviour {

	[Range(0,500)]
	[SerializeField] int height, width;
	[Range(0,100)]
	[SerializeField] float scale = 20f;
	Renderer rend;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<Renderer> ();

	}

	Texture2D generateTexture(int width, int height){
		Texture2D texture = new Texture2D (width, height);

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				float noise = PerlinNoise.noise2D ((float)x/width*scale, (float)y/height*scale);
				texture.SetPixel (x, y, new Color (noise, noise, noise));
			}
		}

		texture.Apply ();
		return texture;
	}

	// Update is called once per frame
	void OnValidate () {
		if(height != null && width != null && rend != null) {
			rend.material.mainTexture = generateTexture (height,width);
		}
	}
}
