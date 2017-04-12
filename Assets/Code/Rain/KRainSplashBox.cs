using UnityEngine;
using System.Collections;


public class KRainSplashBox : MonoBehaviour
{
	void Start()
	{
		transform.localRotation = Quaternion.identity;
		enabled = false;
	}

	void OnBecameVisible()
	{
		enabled = true;
	}

	void OnBecameInvisible()
	{
		enabled = false;
	}

	void OnDrawGizmos()
	{
		MeshFilter mf = GetComponent<MeshFilter>();
		Gizmos.color = new Color(1, 0.5f, 0.65f, 1);
		Gizmos.DrawWireCube(transform.position + mf.sharedMesh.bounds.center, 2 * mf.sharedMesh.bounds.extents);
	}
}

