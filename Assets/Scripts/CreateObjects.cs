using UnityEngine;
using System.Collections;

public class CreateObjects : MonoBehaviour {

	private bool creatingLeftWheel;
	private bool creatingRightWheel;
	private bool creatingNeutralWheel;

	private GameObject tempwheel;

	//publicos
	public GameObject wheelR;
	public GameObject wheelL;
	public GameObject wheelN;

	// Use this for initialization
	void Start () {
		creatingLeftWheel = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown (0))
		{
			if (creatingLeftWheel){

				creatingLeftWheel=!CreateW (wheelL);
			}

			else if (creatingRightWheel){

				creatingRightWheel=!CreateW (wheelR);
			}

			else if (creatingNeutralWheel){

				creatingNeutralWheel=!CreateW (wheelN);
			}
		}
	}

	public void CreateLeftWheelButton()
	{
		creatingLeftWheel=true;
	}
	public void CreateRightWheelButton()
	{
		creatingRightWheel=true;
	}
	public void CreateNeutralWheelButton()
	{
		creatingNeutralWheel=true;
	}

	private bool CreateW(GameObject wheel)
	{
		bool hinger = false;
		Vector3 mousePosition = Input.mousePosition;
		Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);
		
		RaycastHit2D hit = Physics2D.Raycast(v, Vector2.zero);
		
		if (hit != null && hit.collider != null && hit.collider.gameObject.tag=="Begin") {

			Collider2D[] c2d = Physics2D.OverlapCircleAll(v,(wheel.collider2D as CircleCollider2D).radius, 1 << LayerMask.NameToLayer("Kinetic"));

			if (c2d.Length==0){
				tempwheel = Instantiate(wheel, v, Quaternion.identity) as GameObject;
				return true;
			}
		}
		return false;
	}
}
