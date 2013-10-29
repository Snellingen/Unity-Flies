using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject cluster; 
    private ClusterInfo info; 

	void Start ()
	{
	    info = cluster.GetComponent<ClusterInfo>();
	}
	
	void Update () {
        camera.transform.LookAt(info.clusterCenter);
	}
}
