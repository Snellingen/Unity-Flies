using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClusterInfo : MonoBehaviour
{

    private List<GameObject> _clusterObjects;
    private List<Vector3> _directions; 
    public Vector3 ClusterCenter;
    public Vector3 ClusterDirection;
    public int ControllerSpeed = 1;

	void Start ()
	{
	    _clusterObjects = GameObject.FindGameObjectsWithTag("boid").ToList();
        _directions = new List<Vector3>();

	    foreach (GameObject o in _clusterObjects)
	    {
	        _directions.Add(o.transform.InverseTransformDirection(o.gameObject.rigidbody.velocity));
	    }
	    foreach (var clusterObject in _clusterObjects)
	    {
	        ClusterCenter += clusterObject.transform.position;
	    }
	    ClusterCenter /= _clusterObjects.Count;
        Debug.Log(_clusterObjects.Count);
	}

	void Update () 
    {
        for (int i = 0; i < _clusterObjects.Count -1; i++)
        {
            ClusterCenter += _clusterObjects[i].transform.position;
            ClusterDirection += _directions[i];
        }
        ClusterCenter /= _clusterObjects.Count;
	    ClusterDirection /= _directions.Count;

        //if (Input.GetKey(KeyCode.W))
        //    ClusterDirection += new Vector3(0, 0, ControllerSpeed);
        //if (Input.GetKey(KeyCode.A))
        //    ClusterDirection += new Vector3(-ControllerSpeed, 0, 0);
        //if (Input.GetKey(KeyCode.S))
        //    ClusterDirection += new Vector3(0, 0, -ControllerSpeed);
        //if (Input.GetKey(KeyCode.D))
        //    ClusterDirection += new Vector3(ControllerSpeed, 0, 0);
        //if (Input.GetKey(KeyCode.LeftShift))
        //    ClusterDirection += new Vector3(0, ControllerSpeed, 0);
        //if (Input.GetKey(KeyCode.LeftControl))
        //    ClusterDirection += new Vector3(0, -ControllerSpeed, 0);


    }
}
