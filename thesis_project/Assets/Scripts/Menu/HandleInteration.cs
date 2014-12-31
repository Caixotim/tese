﻿using UnityEngine;
using System.Collections;

public class HandleInteration : MonoBehaviour {

	private MenuManager mm;
	public GameObject cam;
	private float object_distance;
	private EditorGameManager egm;

	// Use this for initialization
	void Start () {
		egm = EditorGameManager.Instance;
		mm = MenuManager.Instance;
		object_distance = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (egm.CurrentWiimote.NUNCHUK_C == 1.0f) 
		{
			mm.IsToRenderMenu = !mm.IsToRenderMenu;
		}
		if (egm.CurrentWiimote.NUNCHUK_C == 1.0f) {
			RaycastHit hit;
			if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity))
			{
				if(hit.transform.name.StartsWith("mod_"))
				{
					string name_splited = hit.transform.name.Replace("mod_", "");
					GameObject cylinder = Instantiate(Resources.Load(name_splited)) as GameObject;
					cylinder.transform.name = "NEW_CYLINDER";
					cylinder.transform.SetParent(GameObject.Find("OVRPlayerController").transform);

					Vector3 normalized_obj_vector = Vector3.Normalize (cam.transform.forward);
					
					float Nx = cam.transform.localPosition.x + (normalized_obj_vector.x * object_distance);
					float Ny = cam.transform.localPosition.y + (normalized_obj_vector.y * object_distance);
					float Nz = cam.transform.localPosition.z + (normalized_obj_vector.z * object_distance);

					cylinder.transform.localPosition = new Vector3(Nx, Ny, Nz);

					cylinder.transform.SetParent(null, false);

					//indicar posição a uma distancia de onde a "arma" está a apontar
					mm.IsToRenderMenu = false;
//					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//					cube.name = "CUBO";
//					cube.transform.position = new Vector3(0,0,0);
				}
			}
		}
	}
}
