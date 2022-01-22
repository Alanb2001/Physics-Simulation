using UnityEngine;

namespace Sphere_Collisions
{
    public class StartingVelocity : MonoBehaviour
    {
        public Vector3 velocity;
        public float mass;
        
        void Update()
        {
            transform.position += velocity * Time.deltaTime;
        }
    }
}
