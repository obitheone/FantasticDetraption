using UnityEngine;
using System.Collections;

public class DeleteObject : MonoBehaviour {
	
	private bool deleting;
	private GameObject target;

	//private GameObject tempwheel;
	
	// Use this for initialization
	void Start () {
		deleting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown (0) && deleting)
		{
			DeleteObj ();
			deleting=false;
		}
	}
	public void DeleteObjectButton()
	{
		deleting=true;
	}

	public void DeleteObj()
	{
		DestroyTarget ();
	}

	private void DestroyTarget()
	{
		Vector3 mousePosition = Input.mousePosition;
		Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);
		
		RaycastHit2D hit = Physics2D.Raycast(v, Vector2.zero);
		
		if (hit != null && hit.collider != null) {
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Kinetic")) 
			{ 
				Destroy(hit.collider.gameObject);
			}
		}
	}
}
