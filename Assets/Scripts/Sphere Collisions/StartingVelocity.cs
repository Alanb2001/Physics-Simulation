using UnityEngine;

namespace Sphere_Collisions
{
    public class StartingVelocity : MonoBehaviour
    {
        public Vector3 velocity;
    
        void Update()
        {
            transform.position += velocity * Time.deltaTime;
        }
    }
}
