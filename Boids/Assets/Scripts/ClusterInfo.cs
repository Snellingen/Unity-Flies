using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using System.Collections;

public class ClusterInfo : MonoBehaviour
{

    private List<GameObject> clusterObjects;
    private List<Vector3> directions; 
    public Vector3 clusterCenter;
    public Vector3 clusterDirection;
    public int controllerSpeed = 5;

	void Start ()
	{
	    clusterObjects = GameObject.FindGameObjectsWithTag("boid").ToList();
        directions = new List<Vector3>();

	    foreach (GameObject o in clusterObjects)
	    {
	        directions.Add(o.transform.InverseTransformDirection(o.gameObject.rigidbody.velocity));
	    }
	    foreach (var clusterObject in clusterObjects)
	    {
	        clusterCenter += clusterObject.transform.position;
	    }
	    clusterCenter /= clusterObjects.Count;
        Debug.Log(clusterObjects.Count);
	}

	void Update () 
    {
        for (int i = 0; i < clusterObjects.Count -1; i++)
        {
            clusterCenter += clusterObjects[i].transform.position;
            clusterDirection += directions[i];
        }
        clusterCenter /= clusterObjects.Count;
	    clusterDirection /= directions.Count;

	    if (Input.GetKey(KeyCode.W))
            clusterDirection += new Vector3(0, controllerSpeed, 0);
        if (Input.GetKey(KeyCode.A))
            clusterDirection += new Vector3(-controllerSpeed, 0, 0);
        if (Input.GetKey(KeyCode.S))
            clusterDirection += new Vector3(0, -controllerSpeed, 0);
        if (Input.GetKey(KeyCode.D))
            clusterDirection += new Vector3(controllerSpeed, 0, 0);
        if (Input.GetKey(KeyCode.LeftShift))
            clusterDirection += new Vector3(0, 0, -controllerSpeed);
        if (Input.GetKey(KeyCode.LeftControl))
            clusterDirection += new Vector3(0, 0, controllerSpeed);


    }
}
