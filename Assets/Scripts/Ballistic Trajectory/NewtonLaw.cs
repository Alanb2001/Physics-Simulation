using UnityEngine;

namespace Ballistic_Trajectory
{
    public class NewtonLaw : MonoBehaviour
    {
        public Vector3 velocity;

        private readonly Vector3 _acceleration = new Vector3(0, -9.8f, 0);
        private Vector3 _startingPos;
        private float _time;

        void NewtonPhysics()
        {
            transform.position = _startingPos + (velocity * _time) + _acceleration * (_time * _time) / 2;
        }

        void Start()
        {
            _startingPos = transform.position;
        }

        void Update()
        {
            _time += Time.deltaTime;
            NewtonPhysics();
        }
    }
}
