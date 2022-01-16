using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sphere_Collisions
{
    public class SphereToSphereSolver : MonoBehaviour
    {
        private GameObject[] spheres;
        
        private void Start()
        {
            spheres = GameObject.FindGameObjectsWithTag("Sphere");
        }

        //private void SphereTest()
        //{
        //    for (int i = 0; i < spheres.Length; i++)
        //    {
        //        for (int j = 0; j < spheres.Length; j++)
        //        {
        //            Vector3 dp = spheres[i].transform.position - spheres[j].transform.position;
        //            Vector3 dv = spheres[i].GetComponent<StartingVelocity>().velocity -
        //                         spheres[j].GetComponent<StartingVelocity>().velocity * Time.deltaTime;
        //            float r1r2 = spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude +
        //                         spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude;
//
        //            float a2 = dv.sqrMagnitude * 2.0f;
        //            float b = 2.0f * Vector3.Dot(dp, dv);
        //            float c = dp.sqrMagnitude - Mathf.Pow(r1r2, 2.0f);
        //            
        //            float disc = Mathf.Sqrt(Mathf.Pow(b, 2.0f) - 2 * a2 * c);
//
        //            float t1 = (-b - disc) / a2;
        //            float t2 = (-b + disc) / a2;
//
        //            bool areColliding = t1 < 1 && t1 > 0;
        //            bool areInside = t1 < 0 && t2 > 1;
        //            if (!areColliding && !areInside)
        //            {
        //                return;
        //            }
        //            
        //            spheres[j].transform.position =
        //                spheres[j].transform.position + spheres[j].GetComponent<StartingVelocity>().velocity * Time.deltaTime * t1;
        //            spheres[i].transform.position =
        //                spheres[i].transform.position + spheres[i].GetComponent<StartingVelocity>().velocity * Time.deltaTime * t1;
        //            Vector3 v1 = spheres[j].GetComponent<StartingVelocity>().velocity;
        //            Vector3 g = Vector3.Normalize(spheres[i].transform.position - spheres[j].transform.position);
        //            float q = Vector3.Dot(v1, g) * (Vector3.SqrMagnitude(v1) / spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude);
        //            spheres[i].GetComponent<StartingVelocity>().velocity = spheres[i].GetComponent<StartingVelocity>().velocity + g * q;
        //            spheres[j].GetComponent<StartingVelocity>().velocity = spheres[j].GetComponent<StartingVelocity>().velocity + g * q;
        //        }
        //    }
        //}

        private bool SphereDetection()
        {
            for (int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < spheres.Length; j++)
                {
                    Vector3 p1 = spheres[j].transform.position;
                    Vector3 p2 = spheres[i].transform.position;
                    float r1 = spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude / 2;
                    float r2 = spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude / 2;
                    Vector3 v1 = spheres[j].GetComponent<StartingVelocity>().velocity;
                    Vector3 v2 = spheres[i].GetComponent<StartingVelocity>().velocity;
                    float a = ((v1 - v2) * Time.deltaTime).sqrMagnitude;
                    float b = Vector3.Dot(p1 - p2, (v1 - v2) * Time.deltaTime) * 2;
                    float c = (p1 - p2).sqrMagnitude - (r1 + r2) * (r1 + r2);
                    float t1 = (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
                    //float t2 = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

                    if (t1 < 1 && t1 > 0)
                    {
                        print(t1);
                        print("Hit");
                        //SphereCollision();
                        spheres[j].transform.position =
                            spheres[j].transform.position + spheres[j].GetComponent<StartingVelocity>().velocity * Time.deltaTime * t1;
                        spheres[i].transform.position =
                            spheres[i].transform.position + spheres[i].GetComponent<StartingVelocity>().velocity * Time.deltaTime * t1;
                        //Vector3 v1 = spheres[j].GetComponent<StartingVelocity>().velocity;
                        Vector3 g = Vector3.Normalize(spheres[i].transform.position - spheres[j].transform.position);
                        float q = Vector3.Dot(v1, g) * (Vector3.SqrMagnitude(v1) / spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude);
                        spheres[i].GetComponent<StartingVelocity>().velocity = spheres[i].GetComponent<StartingVelocity>().velocity + g * q;
                        spheres[j].GetComponent<StartingVelocity>().velocity.x = spheres[i].GetComponent<StartingVelocity>().velocity.x * spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude - spheres[j].GetComponent<StartingVelocity>().velocity.x * spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude / spheres
                            [i].GetComponent<MeshRenderer>().bounds.extents.magnitude;
                        spheres[j].GetComponent<StartingVelocity>().velocity.y = spheres[i].GetComponent<StartingVelocity>().velocity.y * spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude - spheres[j].GetComponent<StartingVelocity>().velocity.y * spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude / spheres
                            [i].GetComponent<MeshRenderer>().bounds.extents.magnitude;
                        spheres[j].GetComponent<StartingVelocity>().velocity.z = spheres[i].GetComponent<StartingVelocity>().velocity.z * spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude - spheres[j].GetComponent<StartingVelocity>().velocity.z * spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude / spheres
                            [i].GetComponent<MeshRenderer>().bounds.extents.magnitude;
                        return true;
                    }
                }
            }
            
            return false;
        }

        private void SphereCollision()
        {
            for (int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < spheres.Length; j++)
                {
                    spheres[j].transform.position =
                        spheres[j].transform.position + spheres[j].GetComponent<StartingVelocity>().velocity * Time.deltaTime;
                    spheres[i].transform.position =
                        spheres[i].transform.position + spheres[i].GetComponent<StartingVelocity>().velocity * Time.deltaTime;
                    Vector3 v1 = spheres[j].GetComponent<StartingVelocity>().velocity;
                    Vector3 g = Vector3.Normalize(spheres[i].transform.position - spheres[j].transform.position);
                    float q = Vector3.Dot(v1, g) * (Vector3.SqrMagnitude(v1) / spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude);
                    spheres[i].GetComponent<StartingVelocity>().velocity = spheres[i].GetComponent<StartingVelocity>().velocity + g * q;
                    spheres[j].GetComponent<StartingVelocity>().velocity = spheres[j].GetComponent<StartingVelocity>().velocity + g * q;
                    //{ 
                    //    (spheres[i].GetComponent<StartingVelocity>().velocity.x * spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude - spheres[j].GetComponent<StartingVelocity>().velocity.x * spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude) / spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude,
                    //    (spheres[i].GetComponent<StartingVelocity>().velocity.y * spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude - spheres[j].GetComponent<StartingVelocity>().velocity.y * spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude) / spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude,
                    //    (spheres[i].GetComponent<StartingVelocity>().velocity.z * spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude - spheres[j].GetComponent<StartingVelocity>().velocity.z * spheres[j].GetComponent<MeshRenderer>().bounds.extents.magnitude) / spheres[i].GetComponent<MeshRenderer>().bounds.extents.magnitude
                    //};
                }
            }
            print("Response");   
        }
       
        private void Update()
        {
            SphereDetection();
            //SphereTest();
        }
    }
}