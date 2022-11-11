using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlinNoise{

	public static float noise1D (float x){
		return 0.5f * (Mathf.Sin (2 * x) + Mathf.Sin (Mathf.PI * x));
	}

	public static float noise2D(float x, float y) {
		Vector2 pointVector = new Vector2 (x, y);
		//Pseudorandom gradient vectors
		float gx1 = Random.Range(-1f,1f);
		float gx2 = Random.Range(-1f,1f);
		float gx3 = Random.Range(-1f,1f);
		float gx4 = Random.Range(-1f,1f);

		float gy1 = Mathf.Sqrt (1 - Mathf.Pow (gx1,2));
		float gy2 = -Mathf.Sqrt (1 - Mathf.Pow (gx2,2));
		float gy3 = Mathf.Sqrt (1 - Mathf.Pow (gx3,2));
		float gy4 = -Mathf.Sqrt (1 - Mathf.Pow (gx4,2));

		//Take dot product of gradient vector and vector from associated grid point to x,y
		float s = Vector2.Dot (new Vector2 (gx1, gy1), pointVector - new Vector2 (0, 0));
		float t = Vector2.Dot (new Vector2 (gx2, gy2), pointVector - new Vector2 (1, 0));
		float u = Vector2.Dot (new Vector2 (gx3, gy3), pointVector - new Vector2 (0, 1));
		float v = Vector2.Dot (new Vector2 (gx4, gy4), pointVector - new Vector2 (1, 1));

		//Find factor for weighted average
		float Sx = 3 * Mathf.Pow (x, 2) - 2 * Mathf.Pow (x, 3);
		float Sy = 3 * Mathf.Pow (y, 2) - 2 * Mathf.Pow (y, 3);

		//calculate weighted averages
		float a = s + Sx * (t - s);
		float b = u + Sx * (v - u);

		//convert range from [-1,1] to [0,1]
		float z = a + Sy * (b - a);

		return (z+1f)/2f;

	}
}
