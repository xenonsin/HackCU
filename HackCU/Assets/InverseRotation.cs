using UnityEngine;
using System.Collections;

public class InverseRotation : MonoBehaviour
{

    private Quaternion rotation;
	// Use this for initialization
	void Start () {
	    
	}

    void Awake()
    {
        rotation = transform.rotation;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    transform.rotation = rotation;
	}
}
