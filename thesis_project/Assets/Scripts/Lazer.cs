using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour {
	
	public Light lightLazer;
	private Ray ray;
	
	void Start ()
	{
		lightLazer.enabled = false;
	}
	
	void Update ()
	{
		RaycastHit hit;
		ray.direction = transform.forward;
		ray.origin = transform.position;
		if (Physics.Raycast (ray, out hit))
		{
			Vector3 v3Pos = ray.GetPoint (hit.distance * 0.995f);
			lightLazer.enabled = true;
			lightLazer.gameObject.transform.position = v3Pos;
		}
		else
		{
			lightLazer.enabled = false;
		}
	}
}