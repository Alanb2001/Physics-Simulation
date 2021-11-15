using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

namespace Sphere_Collisions
{
    public class SphereToSphereSolver : MonoBehaviour
    {
        public GameObject sphere;
        public GameObject sphere2;

        private bool SphereDetection()
        {
            Vector3 p1 = sphere.transform.position;
            Vector3 p2 = sphere2.transform.position;
            float r1 = sphere.GetComponent<MeshRenderer>().bounds.extents.magnitude;
            float r2 = sphere2.GetComponent<MeshRenderer>().bounds.extents.magnitude;
            Vector3 v1 = sphere.GetComponent<StartingVelocity>().velocity;
            Vector3 v2 = sphere2.GetComponent<StartingVelocity>().velocity;
            float a = ((v1 - v2) * Time.deltaTime).sqrMagnitude;
            float b = Vector3.Dot(p1 - p2, (v1 - v2) * Time.deltaTime) * 2;
            float c = (p1 - p2).sqrMagnitude - (r1 + r2) * (r1 + r2);
            float t = (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

            //print(t);
            
            if (t < 1 && t > 0)
            {
                print("Hit");
                return true;
            }
            
            //float q = Vector3.Angle(v, a);
            //float d = Mathf.Sin(q) + a.magnitude;

            //float e = Mathf.Sqrt(r1 + r2 * 2 - d * 2);
            //float vc = Mathf.Cos(q) + a.magnitude - e;
            //vc = vc + e + Mathf.Cos(q) + a.magnitude;

            //if (d < r1 + r2)
            //{
            //    print("Hit");
            //    return true;
            //}

            //Vector3 distance = sphere.transform.position - sphere2.transform.position;
            //float length = distance.magnitude;
            //
            //float radius = sphere.GetComponent<MeshRenderer>().bounds.extents.magnitude +
            //         sphere2.GetComponent<MeshRenderer>().bounds.extents.magnitude;
            //
            //if (length <= radius)
            //{
            //    print("Hit");
            //    return true;
            //}

            return false;
        }

        private void SphereCollision()
        {



            //float m1, m2, x1, x2;
            //var position = sphere.transform.position;
            //var position1 = sphere2.transform.position;
            //Vector3 v1, v2, v1x, v2x, v1y, v2y, x = position - position1;

            //var xNormalized = x.normalized;
            //v1 = position;
            //x1 = Vector3.Dot(xNormalized, v1);
            //v1x = xNormalized * x1;
            //v1y = v1 - v1x;
            //m1 = position.magnitude;

            //x = xNormalized * -1;
            //v2 = position1;
            //x2 = Vector3.Dot(x, v2);
            //v2x = xNormalized * x2;
            //v2y = v2 - v2x;
            //m2 = position1.magnitude;
            //
            //sphere.transform.position = (v1x * (m1 - m2) / (m1 + m2) + v2x * (2 * m2) / (m1 + m2) + v1y); 
            //sphere2.transform.position = (v1x * (2 * m1) /( m1 + m2) + v2x * (m2 - m1) / (m1 + m2) + v2y);
        }
       
        private void Update()
        {
            SphereDetection();
            //SphereCollision();
        }
    }
}