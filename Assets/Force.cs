using UnityEngine;
using System.Collections;

public class Force : MonoBehaviour {

	public bool active=false;
	public float motorspeed=100.0f;
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (active) 
		{
			HingeJoint2D hinge = GetComponent<HingeJoint2D>();
			JointMotor2D motor = hinge.motor;
			motor.motorSpeed = motorspeed;
			hinge.motor = motor;
		}
	}
}
