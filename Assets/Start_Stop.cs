using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Start_Stop : MonoBehaviour {
	
	private float x,y;
	private List<Vector2> initpositions = new List<Vector2>();
	private List<Quaternion > initrotation = new List<Quaternion >();
	private List<GameObject> golist = new List<GameObject>();


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void Saveitem(GameObject go)
	{
		initpositions.Add(new Vector2(go.transform.position.x,go.transform.position.y));
		initrotation.Add (new Quaternion(go.transform.rotation.x,go.transform.rotation.y,go.transform.rotation.z,go.transform.rotation.w));
		golist.Add(go);
	}
	private void Restoreitems(bool enable)
	{
		for (int i = 0; i < initpositions.Count; i++) {
			golist[i].transform.position=new Vector2(initpositions[i].x,initpositions[i].y);
			golist[i].transform.rotation = new Quaternion(initrotation[i].x,initrotation[i].y,initrotation[i].z,initrotation[i].w);
			golist[i].rigidbody2D.isKinematic=enable;
			golist[i].collider2D.enabled=false;
		}
	} 

	public void Startstop (bool enable) 
	{ 
		GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[]; 

		if (!enable) 
			{ 
				initpositions.Clear ();
				golist.Clear();
				for (int i = 0; i < goArray.Length; i++) { 
					if (goArray[i].layer == LayerMask.NameToLayer("Kinetic")) 
					{ 
						goArray[i].rigidbody2D.isKinematic=enable;
						goArray[i].collider2D.enabled=true;
						Saveitem(goArray[i]);
					} 
				}
			} 
		else 
			{
			Restoreitems(enable);
			}
	}
}
