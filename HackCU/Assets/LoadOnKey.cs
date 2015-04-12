using UnityEngine;
using System.Collections;

public class LoadOnKey : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.R))
	        Application.LoadLevel(1);
	}
}
