using UnityEngine;
using System.Collections;

public class KRainModelBuilder
{
	public float areaSize = 13;
	public float particleSize = 0.2f;
	public float flakeRandom = 0.1f;
	public int numberOfParticles = 200;
	public float areaHeight = 5;

	public Mesh CreateMesh()
	{
		Mesh mesh = new Mesh();
		Vector3 cameraRight = Camera.main.transform.right;
		Vector3 cameraUp = Camera.main.transform.up;

		Vector3[] verts = new Vector3[4 * numberOfParticles];
		Vector2[] uvs = new Vector2[4 * numberOfParticles];
		Vector2[] uvs2 = new Vector2[4 * numberOfParticles];
		Vector3[] normals = new Vector3[4 * numberOfParticles];
		int[] tris = new int[2 * 3 * numberOfParticles];

		Vector3 position = Vector3.zero;
		for (int i = 0; i < numberOfParticles; i++)
		{
			int i4 = i * 4;
			int i6 = i * 6;

			position.x = areaSize * (Random.value - 0.5f);
			position.y = areaHeight * Random.value;
			position.z = areaSize * (Random.value - 0.5f);

			float rand = Random.value;
			float widthWithRandom = particleSize * 0.215f;// + rand * flakeRandom;
			float heightWithRandom = particleSize + rand * flakeRandom;

			verts[i4 + 0] = position - cameraRight * widthWithRandom - cameraUp * heightWithRandom;
			verts[i4 + 1] = position + cameraRight * widthWithRandom - cameraUp * heightWithRandom;
			verts[i4 + 2] = position + cameraRight * widthWithRandom + cameraUp * heightWithRandom;
			verts[i4 + 3] = position - cameraRight * widthWithRandom + cameraUp * heightWithRandom;

			normals[i4 + 0] = -Camera.main.transform.forward;
			normals[i4 + 1] = -Camera.main.transform.forward;
			normals[i4 + 2] = -Camera.main.transform.forward;
			normals[i4 + 3] = -Camera.main.transform.forward;

			uvs[i4 + 0] = new Vector2(0.0f, 0.0f);
			uvs[i4 + 1] = new Vector2(1.0f, 0.0f);
			uvs[i4 + 2] = new Vector2(1.0f, 1.0f);
			uvs[i4 + 3] = new Vector2(0.0f, 1.0f);

			uvs2[i4 + 0] = new Vector2(Random.Range(-2, 2) * 4.0f, Random.Range(-1, 1) * 1.0f);
			uvs2[i4 + 1] = new Vector2(uvs2[i4 + 0].x, uvs2[i4 + 0].y);
			uvs2[i4 + 2] = new Vector2(uvs2[i4 + 0].x, uvs2[i4 + 0].y);
			uvs2[i4 + 3] = new Vector2(uvs2[i4 + 0].x, uvs2[i4 + 0].y);

			tris[i6 + 0] = i4 + 0;
			tris[i6 + 1] = i4 + 1;
			tris[i6 + 2] = i4 + 2;
			tris[i6 + 3] = i4 + 0;
			tris[i6 + 4] = i4 + 2;
			tris[i6 + 5] = i4 + 3;
		}

		mesh.vertices = verts;
		mesh.triangles = tris;
		mesh.normals = normals;
		mesh.uv = uvs;
		mesh.uv2 = uvs2;
		mesh.RecalculateBounds();

		return mesh;
	}
}


public class KRainSplashBuilder
{
	public float flakeWidth = 0.2f;
	public float flakeHeight = 0.2f;
	public float flakeRandom = 0.2f;
	public int numberOfParticles = 18;
	public float areaSize = 4;

	public Mesh CreateMesh()
	{
		Mesh mesh = new Mesh();
		// we use world space aligned and not camera aligned planes this time
		// 			Vector3 cameraRight = transform.right * Random.Range(0.1f, 2.0f) + transform.forward * Random.Range(0.1f, 2.0f);// Vector3.forward;//Camera.main.transform.right;
		// 			cameraRight = Vector3.Normalize(cameraRight);
		// 			Vector3 cameraUp = Vector3.Cross(cameraRight, Vector3.up);
		// 			cameraUp = Vector3.Normalize(cameraUp);
		Vector3 cameraRight = Vector3.right;
		Vector3 cameraUp = Vector3.forward;

		Vector3[] verts = new Vector3[4 * numberOfParticles];
		Vector2[] uvs = new Vector2[4 * numberOfParticles];
		Vector2[] uvs2 = new Vector2[4 * numberOfParticles];
		Vector3[] normals = new Vector3[4 * numberOfParticles];

		int[] tris = new int[2 * 3 * numberOfParticles];

		Vector3 position = Vector3.zero;
		for (int i = 0; i < numberOfParticles; i++)
		{
			int i4 = i * 4;
			int i6 = i * 6;

			position.x = areaSize * (Random.value - 0.5f);
			position.y = 0.0f;
			position.z = areaSize * (Random.value - 0.5f);

			float rand = Random.value;
			float widthWithRandom = flakeWidth + rand * flakeRandom;
			float heightWithRandom = widthWithRandom;

			verts[i4 + 0] = position - cameraRight * widthWithRandom;// - 0.0 * heightWithRandom;
			verts[i4 + 1] = position + cameraRight * widthWithRandom;// - 0.0 * heightWithRandom;
			verts[i4 + 2] = position + cameraRight * widthWithRandom + cameraUp * 2.0f * heightWithRandom;
			verts[i4 + 3] = position - cameraRight * widthWithRandom + cameraUp * 2.0f * heightWithRandom;

			normals[i4 + 0] = -Camera.main.transform.forward;
			normals[i4 + 1] = -Camera.main.transform.forward;
			normals[i4 + 2] = -Camera.main.transform.forward;
			normals[i4 + 3] = -Camera.main.transform.forward;

			uvs[i4 + 0] = new Vector2(0.0f, 0.0f);
			uvs[i4 + 1] = new Vector2(1.0f, 0.0f);
			uvs[i4 + 2] = new Vector2(1.0f, 1.0f);
			uvs[i4 + 3] = new Vector2(0.0f, 1.0f);

			Vector2 tc1 = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
			uvs2[i4 + 0] = new Vector2(tc1.x, tc1.y);
			uvs2[i4 + 1] = new Vector2(tc1.x, tc1.y); ;
			uvs2[i4 + 2] = new Vector2(tc1.x, tc1.y); ;
			uvs2[i4 + 3] = new Vector2(tc1.x, tc1.y); ;

			tris[i6 + 0] = i4 + 0;
			tris[i6 + 1] = i4 + 1;
			tris[i6 + 2] = i4 + 2;
			tris[i6 + 3] = i4 + 0;
			tris[i6 + 4] = i4 + 2;
			tris[i6 + 5] = i4 + 3;
		}

		mesh.vertices = verts;
		mesh.triangles = tris;
		mesh.normals = normals;
		mesh.uv = uvs;
		mesh.uv2 = uvs2;
		mesh.RecalculateBounds();

		return mesh;
	}
}

