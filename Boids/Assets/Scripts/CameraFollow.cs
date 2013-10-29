using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject cluster; 
        private ClusterInfo _info; 

        void Start ()
        {
            _info = cluster.GetComponent<ClusterInfo>();
        }
	
        void Update () {
            camera.transform.LookAt(_info.ClusterCenter);
        }
    }
}
