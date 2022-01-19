using UnityEngine;

namespace Sphere_Collisions
{
    public class SphereToPlaneSolver : MonoBehaviour
    {
        private GameObject[] planes;
        
        private void Start()
        {
            planes = GameObject.FindGameObjectsWithTag("Plane");
        }

        private void SphereToPlaneCollision()
        {
            
        }

        private void Update()
        {
        
        }
    }
}
