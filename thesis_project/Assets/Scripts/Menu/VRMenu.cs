using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VRMenu : MonoBehaviour {

	public GameObject block_prefab;
	public GameObject cam;
	private MenuManager mm;
//	public GameObject cv;
	private bool isRendered;
//	private GameObject block;
	private GameObject[] blocks;
	private float menu_items_distance = 5.0f;

	// Use this for initialization
	void Start () {
		mm = MenuManager.Instance;
		isRendered = false;
		blocks = new GameObject[1];
	}
	
	// Update is called once per frame
	void Update () {

		if(mm.IsToRenderMenu && !isRendered)
		{
			DrawMenu();

		}
		else if(!mm.IsToRenderMenu && isRendered)
		{
			foreach(GameObject block in blocks)
			{
				block.SetActive(false);
			}
			isRendered = false;
		}
	}

	private void DrawMenu()
	{
		int i = 0;
		foreach (GameObject block in blocks) {
			DrawBlock(block, i);
			i++;
		}
		//			cv.renderer.enabled = false;
	}

	private void DrawBlock(GameObject block, int i)
	{
		if (block == null) {
			block = Instantiate (block_prefab) as GameObject;
			block.name = "block";
			Transform child = block.transform.FindChild("Item_block");
			if(child != null)
				child.name = "mod_"+"Cylinder";
			//////
			/// 
			string title = "Cilindro";
			string imageName = "Cylinder_img";
			string price = "12,00";
			string currency = "€";

			SetMenuItemProperties(child.gameObject, title, imageName, price, currency);
			blocks [i] = block;
		}
		//		block.transform.LookAt (cam.transform.position);
//		RectTransform block_rectTrans = block.GetComponent<RectTransform> ();
		//			block_rectTrans.LookAt (cam.transform.forward);
		//		block_rectTrans.scale = new Vector3 (1.0f, 1.0f, 1.0f);
		//			block.transform.forward = cam.transform.forward;
		//			block.transform.parent = cv.transform;

		//ATIVAR DEPOIS SE NECESSARIO
//		block_rectTrans.SetParent (GameObject.Find ("OVRPlayerController").transform);
		
		Vector3 normalized_obj_vector = Vector3.Normalize (cam.transform.forward);
		
		float Nx = cam.transform.localPosition.x + (normalized_obj_vector.x * menu_items_distance);
		float Ny = cam.transform.localPosition.y + (normalized_obj_vector.y * menu_items_distance);
		float Nz = cam.transform.localPosition.z + (normalized_obj_vector.z * menu_items_distance);

		if(Ny <= 0)
			Ny = 0;
		
		block.transform.position = new Vector3 (Nx, Ny + 0.5f, Nz);
		//			Vector3 block_position = block.transform.position;
		//			block_rectTrans.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y+50.0f, 50.0f);
		//			cv.transform.position = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y+20.0f, cam.transform.localPosition.z+20.0f);
		block.transform.localScale = new Vector3 (0.05f, 0.05f, 0.05f);
		
		//set orientation
		block.transform.LookAt (cam.transform);
		block.transform.forward = -block.transform.forward;
		
		block.transform.SetParent (null, false);
		
		//identify end of first call
//		isFirstCall = false;
		isRendered = true;
		//			block_rectTrans.LookAt(cam.transform);
		block.SetActive (true);
	}

	private void SetMenuItemProperties(GameObject block, string title, string imageName, string price, string currency)
	{
		Transform panel_title = block.transform.Find ("Title");

		if(panel_title != null)
		{
			Text title_txt = panel_title.FindChild ("Text").GetComponent<Text> ();;
			title_txt.text = title;
		}

		Transform panel_body = block.transform.Find ("Body");
		if(panel_body != null)
		{
			Text price_txt = panel_body.FindChild ("Price").GetComponent<Text> ();;
			price_txt.text = price + currency;

			Image image_img = panel_body.FindChild ("Image").GetComponent<Image> ();;
			image_img.overrideSprite = Resources.Load<Sprite>(imageName);
			if(image_img.overrideSprite != null)
				Debug.Log(image_img.overrideSprite.name);
			else
				Debug.Log("Sprite not defined!!! "+imageName+".jpg");
		}

	}
}
