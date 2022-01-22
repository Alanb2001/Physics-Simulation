using UnityEngine;

namespace Sphere_Collisions
{
    public class Velocity : MonoBehaviour
    {
        public Vector3 velocity;
        public float mass;
        
        void Update()
        {
            transform.position += velocity * Time.deltaTime;
        }
    }
}
