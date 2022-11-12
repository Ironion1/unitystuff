using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CreateMesh : MonoBehaviour {

	Mesh mesh;
	public int width = 2;
	public int depth = 2;


	void OnValidate () {
		gameObject.GetComponent<MeshFilter> ().mesh = createTerrain();
	}

	int returnIndexOfVertex(int x, int z){
		return x * depth + z;
	}

	Mesh createTerrain(){
		Mesh mesh = new Mesh ();
		int vertex_index = 0;
		Vector3[] vertices = new Vector3[width*depth];


		for (int x = 0; x < width; x++) {
			for (int z = 0; z < depth; z++) {
				vertices [vertex_index] = new Vector3 (x, 0, z);
				vertex_index++;
			}
		}
		Debug.Log ("amount of vertices:" + vertices.Length);
		mesh.vertices = vertices;

		int[] triangles = new int[(width - 1) * (depth) * 3];
		int triangle_index =0; 
		int min_index, max_index, second_index;
		int[] triangle_vertices;

		//Set triangles to form quads along all vertices
		//TODO:X is the wrong axis in my head
		for (int x = 0; x < width-1; x++) {
			for (int z = 0; z < depth; z++) {
				if(z%2 == 0) {
					Debug.Log ("zmod2");
					triangle_vertices = new int[] {
						returnIndexOfVertex (x, z),
						returnIndexOfVertex (x+1, z),
						returnIndexOfVertex (x + 1, z+1)
					};

					min_index = Mathf.Min (triangle_vertices [0], triangle_vertices [1], triangle_vertices [2]);
					second_index =0;
					max_index = Mathf.Max (triangle_vertices [0], triangle_vertices [1], triangle_vertices [2]);


					foreach(int i in triangle_vertices){
						if(i != min_index && i != max_index) {
							second_index = i;
						}						
					}

					triangles [triangle_index] = min_index;
					triangles [triangle_index + 1] = second_index;
					triangles [triangle_index + 2] = max_index;
					triangle_index = triangle_index + 3;
				} else { 
					Debug.Log("znotmod2");
					triangle_vertices = new int[] {
						returnIndexOfVertex (x, z),
						returnIndexOfVertex (x, z - 1),
						returnIndexOfVertex (x + 1, z)
					};

					min_index = Mathf.Min (triangle_vertices [0], triangle_vertices [1], triangle_vertices [2]);
					second_index =0;
					max_index = Mathf.Max (triangle_vertices [0], triangle_vertices [1], triangle_vertices [2]);


					foreach(int i in triangle_vertices){
						if(i != min_index && i != max_index) {
							second_index = i;
						}						
					}

					triangles [triangle_index] = max_index;
					triangles [triangle_index + 1] = second_index;
					triangles [triangle_index + 2] = min_index;
					triangle_index = triangle_index + 3;
				}
			}
		}

		mesh.triangles = triangles;
		foreach(int triangleint in triangles) {
			Debug.Log (triangleint);
		}
		mesh.RecalculateNormals ();

		return mesh;
	}
}
