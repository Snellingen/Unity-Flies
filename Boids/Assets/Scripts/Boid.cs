using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class Boid : MonoBehaviour
{
    public int directionForce = 10;
    public int repelForce = 100;
    public int repelRadius = 4; 
    public int flockForce = 10;
    public int drag = 5;
    public float speedDif = 2f; 
    public Vector3 direction;
    private Vector3 clusterPostion;
    private ClusterInfo clusterInfo;
    private Collider[] hitColliders;
    private Collider closest; 


    // Use this for initialization
    private void Start()
    {
        direction = Vector3.zero;
        clusterInfo = transform.parent.GetComponent<ClusterInfo>();
        speedDif = Random.Range(1f, speedDif+1f);
    }

    void Update()
    {

        hitColliders = Physics.OverlapSphere(transform.position, repelRadius);

        if (hitColliders != null)
        {
            if (hitColliders[0].transform.position != transform.position)
                closest = hitColliders[0];
            else if (hitColliders.Length == 2)
                closest = hitColliders[1];
            else if (hitColliders.Length > 2)
            {
            }
            foreach (Collider hitCollider in hitColliders)
            {
                closest = hitCollider;
                if(hitCollider.tag == "boid")
                    rigidbody.AddForce((transform.position - hitCollider.transform.position).normalized * (repelForce/2) * speedDif);
                else rigidbody.AddForce((transform.position - hitCollider.transform.position).normalized * repelForce * 5 * speedDif);
            }
            rigidbody.AddForce((transform.position - closest.transform.position).normalized * repelForce * speedDif);
        }

        rigidbody.AddForce(clusterInfo.clusterDirection.normalized * directionForce * speedDif);
        rigidbody.AddForce((clusterInfo.clusterCenter - transform.position).normalized * flockForce * speedDif);
        direction = rigidbody.velocity;
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        rigidbody.drag = Vector3.Magnitude(rigidbody.velocity) / drag;


    }
}

