using UnityEngine;
using System.Collections;

public class CameraVertical : MonoBehaviour
{
    private Quaternion rotation;

    void Awake()
    {
        rotation = transform.rotation;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.rotation = rotation;
	}
}
