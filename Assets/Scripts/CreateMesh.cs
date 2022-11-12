using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CreateMesh : MonoBehaviour {

	Mesh mesh;
	[Range(1f,50f)]
	public int width, depth;
	[Range(1f,20f)]
	public float scale;
	[Range(1f,20f)]
	public float generalscale;
	[Range(1f,20f)]
	public float offsetX, offsetZ;
	int[] triangles; 
	int triangle_index;
	int min_index, max_index, second_index;
	int[] triangle_vertices;

	void OnValidate () {
		/*width = 4;
		depth = 4;
		scale = 1f;
		offsetX = 1f;
		offsetZ = 1f;*/
		triangles = new int[(width-1)*(depth-1)*2*3];
		triangle_index = 0;
		gameObject.GetComponent<MeshFilter> ().mesh = createTerrain();
	}

	int returnIndexOfVertex(int a, int b){
		return a * depth + b;
	}

	void computeTriangle(int x, int z, bool isUp){
		if(isUp) {
			triangle_vertices = new int[] {
				returnIndexOfVertex (x, z),
				returnIndexOfVertex (x+1, z),
				returnIndexOfVertex (x + 1, z+1)
			};
		} else {
			triangle_vertices = new int[] {
				returnIndexOfVertex (x, z),
				returnIndexOfVertex (x, z - 1),
				returnIndexOfVertex (x + 1, z)
			};
		}

		min_index = Mathf.Min (triangle_vertices [0], triangle_vertices [1], triangle_vertices [2]);
		second_index =0;
		max_index = Mathf.Max (triangle_vertices [0], triangle_vertices [1], triangle_vertices [2]);


		foreach(int mod_index in triangle_vertices){
			if(mod_index != min_index && mod_index != max_index) {
				second_index = mod_index;
			}						
		}

		if(isUp){
			triangles [triangle_index + 2] = min_index;
			triangles [triangle_index + 1] = second_index;
			triangles [triangle_index] = max_index;
		} else {
			triangles [triangle_index + 2] = max_index;
			triangles [triangle_index + 1] = second_index;
			triangles [triangle_index] = min_index;
		}

		triangle_index = triangle_index + 3;
	}

	Mesh createTerrain(){
		Mesh mesh = new Mesh ();
		int vertex_index = 0;
		Vector3[] vertices = new Vector3[width*depth];


		for (int x1 = 0; x1 < width; x1++) {
			for (int z1 = 0; z1 < depth; z1++) {
				vertices [vertex_index] = new Vector3 (x1, generalscale*PerlinNoise.noise2D(x1/width*scale+offsetX,z1/depth*scale+offsetZ), z1);
				vertex_index++;
			}
		}
		Debug.Log ("amount of vertices:" + vertices.Length);
		mesh.vertices = vertices;

		//Set triangles to form quads along all vertices
		//TODO:X is the wrong axis in my head
		for (int x = 0; x < width - 1; x++) {
			for (int z = 0; z < depth; z++) {
				if (z == 0) {
					computeTriangle (x, z, true);
				} else if (z == depth - 1) {
					computeTriangle (x, z, false);
				} else {
					computeTriangle (x, z, true);
					computeTriangle (x, z, false);
				}
			}
		}

		mesh.triangles = triangles;
		mesh.RecalculateNormals ();

		return mesh;
	}
}

