using UnityEngine;
using System.Collections;


public class KRainBox : MonoBehaviour
{
	public float minYPos = 0;
	public float areaHeight = 15;
	public float fallingSpeed = 22;
	float _oriY = 0;
	void Start()
	{
		_oriY = transform.position.y;
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

	void Update()
	{
		float y = transform.position.y - Time.deltaTime * fallingSpeed;
		if (y + areaHeight < _oriY + minYPos)
		{
			y = _oriY + areaHeight * 2.0f;
		}
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}

	void OnDrawGizmos()
	{
		MeshFilter mf = GetComponent<MeshFilter>();
		Gizmos.color = new Color(0, 1, 1.0f, 0.35f);
		Gizmos.DrawWireCube(transform.position + mf.sharedMesh.bounds.center, 2 * mf.sharedMesh.bounds.extents);
	}
}


