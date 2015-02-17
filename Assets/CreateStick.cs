using UnityEngine;
using System.Collections;

public class CreateStick : MonoBehaviour {

	private Vector2 _start;
	private Vector2 _end;
	private bool creating;
	private int counterclick;
	private GameObject tempstick;

	public GameObject stick;

	// Use this for initialization
	void Start () {
		creating = false;
		counterclick = 0;
	}
	
	// Update is called once per frame
	void Update() {

		if(Input.GetMouseButtonDown (0) && creating)
		{
			if (counterclick==0) {
					CreateS();
					counterclick++;
			}
			else if (counterclick==1) 
			{
				counterclick=0;
				EndStick();
				creating=false;
			}
		}
	}

	void FixedUpdate()
	{
		Vector3 mousePosition = Input.mousePosition;
		_end = Camera.main.ScreenToWorldPoint(mousePosition);
		if (creating && counterclick>0) {ResizeStick(_end);}
	}

	public void CreateStickButton()
	{
		creating=true;
		counterclick=0;
	}


	void CreateS()
	{
		bool hinger = false;
		Vector3 mousePosition = Input.mousePosition;
		_start = Camera.main.ScreenToWorldPoint(mousePosition);

		RaycastHit2D hit = Physics2D.Raycast(_start, Vector2.zero);

		if (hit != null && hit.collider != null && (hit.collider.gameObject.tag == "lock")) {
			_start=hit.transform.position;
			hinger = true;
		}

		tempstick = Instantiate(stick, _start, Quaternion.identity) as GameObject;

		//Rigidbody2D rb2d= tempstick.GetComponent<Rigidbody2D>();
		//rb2d.isKinematic = true;

		if (hinger){addjoints(hit.collider.gameObject,0);}
	}
	void ResizeStick(Vector2 v)
	{

		
		Bounds bounds = tempstick.GetComponent<SpriteRenderer>().sprite.bounds;
	
		float length = bounds.size.x;
		float distance=Vector2.Distance(_start, v);
		
		Vector2 temp2 = tempstick.transform.localScale;

		if ( v.x>_start.x) temp2.x = (distance / length); 
		else temp2.x = -(distance / length);

		tempstick.transform.localScale = temp2;

		Vector2 vect1 = new Vector2 (_start.x+1,_start.y);
		Vector2 primervector = (vect1 - _start);
		Vector2 segundovector = (v - _start);

		float angle = Vector2.Angle (primervector,segundovector);

		if (v.x > _start.x) 
		{
			if ( v.y < _start.y) {angle=360-angle;}
		} 
		if (v.x < _start.x)
		{
			if ( v.y < _start.y) {angle=(180-angle);}
			if ( v.y > _start.y) {angle=angle-180;} 
		}
		
		tempstick.transform.localRotation =  Quaternion.Euler(0.0f,0.0f,angle);
		
	}
	void EndStick()
	{
		Vector3 mousePosition = Input.mousePosition;

		Debug.Log ("end stick");
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

		if (hit != null && hit.collider != null && (hit.collider.gameObject.tag == "lock")) {
			Debug.Log ("I'm hitting "+hit.collider.name);
			Vector2 v=hit.transform.position;
			ResizeStick (v);
			_end=v;
			addjoints(hit.collider.gameObject,1);
		}
	
		//Rigidbody2D rb2d= tempstick.GetComponent<Rigidbody2D>();
		//rb2d.isKinematic = false;
	}
	void addjoints(GameObject lockobject,int position)
	{
		HingeJoint2D myJoint = (HingeJoint2D)tempstick.AddComponent("HingeJoint2D");
		myJoint.connectedBody = lockobject.transform.parent.rigidbody2D;
		myJoint.collideConnected = true;
		
		if (lockobject.name == "Leftlock") {
			myJoint.connectedAnchor = new Vector2 (-1.3f, 0f);
		}
		if (lockobject.name == "Rightlock") {
			myJoint.connectedAnchor = new Vector2 (1.3f, 0f);
		}
		if (lockobject.name == "Toplock") {
			myJoint.connectedAnchor = new Vector2 (0, 1.3f);
		}
		if (lockobject.name == "Bottomlock") {
			myJoint.connectedAnchor = new Vector2 (0f, -1.3f);
		}
		if (lockobject.name == "Centerlock") {
			
			myJoint.connectedAnchor = new Vector2 (0.0f, 0.0f);
			myJoint.collideConnected = false;
		}

		if (position == 0) ///leftlock del stick
		{ 
			//de momento nada relevante.
		}
		if (position == 1) /// rightlock
		{ 
			Vector2 temp=new Vector2(tempstick.GetComponent<BoxCollider2D>().size.x,0f);
			myJoint.anchor=temp;
		}
	}

}
