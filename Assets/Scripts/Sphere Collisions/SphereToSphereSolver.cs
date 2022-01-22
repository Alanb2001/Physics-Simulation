using UnityEngine;

namespace Sphere_Collisions
{
    public class SphereToSphereSolver : MonoBehaviour
    {
        private GameObject[] spheres;
        private GameObject[] planes;
        private void Start()
        {
            spheres = GameObject.FindGameObjectsWithTag("Sphere");
            planes = GameObject.FindGameObjectsWithTag("Plane");
        }
        
        private void SphereToSphere()
        {
            for (int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < spheres.Length; j++)
                {
                    Vector3 p1 = spheres[i].transform.position;
                    Vector3 p2 = spheres[j].transform.position;
                    float r1 = spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude / 2;
                    float r2 = spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude / 2;
                    Vector3 v1 = spheres[i].GetComponent<StartingVelocity>().velocity;
                    Vector3 v2 = spheres[j].GetComponent<StartingVelocity>().velocity;
                    float a = ((v1 - v2) * Time.deltaTime).sqrMagnitude;
                    float b = Vector3.Dot(p1 - p2, (v1 - v2) * Time.deltaTime) * 2;
                    float c = (p1 - p2).sqrMagnitude - (r1 + r2) * (r1 + r2);
                    float t1 = (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
                    float t2 = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

                    if (t1 < 1 && t1 > 0 || t1 < 0 && t2 > 1)
                    {
                        print(t1);
                        print(t2);
                        print("Hit Sphere");
                        spheres[i].transform.position +=
                            spheres[i].GetComponent<StartingVelocity>().velocity * (Time.deltaTime * t1);
                        spheres[j].transform.position +=
                            spheres[j].GetComponent<StartingVelocity>().velocity * (Time.deltaTime * t1);
                        Vector3 g = Vector3.Normalize(spheres[j].transform.position - spheres[i].transform.position);
                        float q = Vector3.Dot(v1, g) * (v1.magnitude / spheres[j].GetComponent<StartingVelocity>().mass);
                        spheres[j].GetComponent<StartingVelocity>().velocity += g * q;
                        spheres[i].GetComponent<StartingVelocity>().velocity = new Vector3
                        {
                            x = (spheres[i].GetComponent<StartingVelocity>().velocity.x * spheres[i].GetComponent<StartingVelocity>().mass - spheres[j].GetComponent<StartingVelocity>().velocity.x * spheres[j].GetComponent<StartingVelocity>().mass) / spheres[i].GetComponent<StartingVelocity>().mass,
                            y = (spheres[i].GetComponent<StartingVelocity>().velocity.y * spheres[i].GetComponent<StartingVelocity>().mass - spheres[j].GetComponent<StartingVelocity>().velocity.y * spheres[j].GetComponent<StartingVelocity>().mass) / spheres[i].GetComponent<StartingVelocity>().mass,
                            z = (spheres[i].GetComponent<StartingVelocity>().velocity.z * spheres[i].GetComponent<StartingVelocity>().mass - spheres[j].GetComponent<StartingVelocity>().velocity.z * spheres[j].GetComponent<StartingVelocity>().mass) / spheres[i].GetComponent<StartingVelocity>().mass
                        };
                        
                        return;
                    }
                }
            }
        }

        private bool SphereToPlane()
        {
            for (int i = 0; i < planes.Length; i++)
            {
                for (int j = 0; j < spheres.Length; j++)
                {
                    Vector3 n = planes[i].transform.position.normalized;
                    Vector3 v = spheres[j].GetComponent<StartingVelocity>().velocity;
                    Vector3 k = planes[i].transform.TransformPoint(transform.position.x, transform.position.y, transform.position.z);
                    Vector3 p = k + spheres[j].transform.position;
                    float s1 = Vector3.Angle(n, -v);
                    float q1 = Vector3.Angle(n, p);
                    float q2 = Vector3.Angle(p, k);
                    float q3 = q1 + q2;
                    
                    float r = spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude / 2;

                    float d = Mathf.Sin(q2) - p.sqrMagnitude;
                    float s = Vector3.Angle(v, -n);
                    float vc = (d - r) / Mathf.Cos(s);
                    
                    if (q3 < 90)
                    {
                        print(vc);
                        print(v);
                        return true;
                    }
                    if (vc <= v.x)
                    {
                        print("Hit Plane");
                        return true;
                    }
                }
            }
            return false;
        }
        
        private void Update()
        {
            SphereToSphere(); 
            SphereToPlane();
        }
    }
}