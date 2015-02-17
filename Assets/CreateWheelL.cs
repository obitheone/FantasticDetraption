using UnityEngine;
using System.Collections;

public class CreateWheelL : MonoBehaviour {

	private bool creating;
	public GameObject wheel;
	
	private GameObject tempwheel;
	
	// Use this for initialization
	void Start () {
		creating = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown (0) && creating)
		{
			CreateW ();
			creating=false;
		}
	}
	public void CreateWheelButton()
	{
		creating=true;
	}
	
	
	private void CreateW()
	{
		bool hinger = false;
		Vector3 mousePosition = Input.mousePosition;
		Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);
		
		RaycastHit2D hit = Physics2D.Raycast(v, Vector2.zero);
		
		if (hit != null && hit.collider != null) {
			//no se crea porque esta encima de otro collider
		}
		else {
			tempwheel = Instantiate(wheel, v, Quaternion.identity) as GameObject;
		}
	}
}
