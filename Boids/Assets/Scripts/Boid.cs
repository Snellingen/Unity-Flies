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
    public int DirectionForce = 10;
    public int RepelForce = 40;
    public int RepelRadius = 2; 
    public int FlockForce = 10;
    public int Drag = 20;
    public float randomRange = 4f;
    private float _randomSpeed;
    public Vector3 Direction;
    private Vector3 _clusterPostion;
    private ClusterInfo _clusterInfo;
    private Collider[] _hitColliders;
    private Collider _closest; 


    // Use this for initialization
    private void Start()
    {
        Direction = Vector3.zero;
        _clusterInfo = transform.parent.GetComponent<ClusterInfo>();
        _randomSpeed = Random.Range(1f, randomRange + 1);
    }

    void Update()
    {

        _hitColliders = Physics.OverlapSphere(transform.position, RepelRadius);

        if (_hitColliders != null)
        {
            if (_hitColliders[0].transform.position != transform.position)
                _closest = _hitColliders[0];
            else if (_hitColliders.Length == 2)
                _closest = _hitColliders[1];
            else if (_hitColliders.Length > 2)
            {
            }
            foreach (var hitCollider in _hitColliders)
            {
                _closest = hitCollider;
                if(hitCollider.tag == "boid")
                    rigidbody.AddForce((transform.position - hitCollider.transform.position).normalized * (RepelForce/2) * _randomSpeed);
                else rigidbody.AddForce((transform.position - hitCollider.transform.position).normalized * RepelForce * 5 * _randomSpeed);
            }
            rigidbody.AddForce((transform.position - _closest.transform.position).normalized * RepelForce * _randomSpeed);
        }

        rigidbody.AddForce(_clusterInfo.ClusterDirection.normalized * DirectionForce * _randomSpeed);
        rigidbody.AddForce((_clusterInfo.ClusterCenter - transform.position).normalized * FlockForce * _randomSpeed);
        Direction = rigidbody.velocity;
        if (Direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(Direction);

        rigidbody.drag = Vector3.Magnitude(rigidbody.velocity) / Drag;


    }
}

