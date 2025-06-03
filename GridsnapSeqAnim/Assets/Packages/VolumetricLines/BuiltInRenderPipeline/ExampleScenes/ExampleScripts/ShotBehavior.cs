using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	Rigidbody rb;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.position += transform.forward * Time.deltaTime * 1000f;
		rb.AddForce(transform.forward * 250, ForceMode.Impulse);
	
	}
}
