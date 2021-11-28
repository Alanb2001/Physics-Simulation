using UnityEngine;

namespace Sphere_Collisions
{
    public class SphereToSphereSolver : MonoBehaviour
    {
        private GameObject[] spheres;
        
        private void Start()
        {
            spheres = GameObject.FindGameObjectsWithTag("Sphere");
        }

        private bool SphereDetection()
        {
            for (int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < spheres.Length; j++)
                {
                    Vector3 p1 = spheres[j].transform.position;
                    Vector3 p2 = spheres[i].transform.position;
                    float r1 = spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude;
                    float r2 = spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude;
                    Vector3 v1 = spheres[j].GetComponent<StartingVelocity>().velocity;
                    Vector3 v2 = spheres[i].GetComponent<StartingVelocity>().velocity;
                    float a = ((v1 - v2) * Time.deltaTime).sqrMagnitude;
                    float b = Vector3.Dot(p1 - p2, (v1 - v2) * Time.deltaTime) * 2;
                    float c = (p1 - p2).sqrMagnitude - (r1 + r2) * (r1 + r2);
                    float t = (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

                    if (t < 1 && t > 0)
                    {
                        print(t);
                        print("Hit");
                        return true;
                    }
                }
            }
            
            return false;
        }

        private void SphereCollision()
        {
            
        }
       
        private void Update()
        {
            SphereDetection();
            SphereCollision();
        }
    }
}