using UnityEngine;

namespace Ballistic_Trajectory
{
    public class Solver : MonoBehaviour
    {
        public Vector3 velocity;
    
        private void Update()
        {
            velocity.y += -9.8f * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
    }
}
