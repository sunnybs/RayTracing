using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace RayTracing
{
    public class Sphere
    {
        public Vector3 Center { get; }
        public float Radius { get; }
        public Color Color { get; }
        public float Specular { get; }
        public float Reflective { get; }

        public Sphere(Vector3 center, float radius, Color color, float specular, float reflective)
        {
            Center = center;
            Radius = radius;
            Color = color;
            Specular = specular;
            Reflective = reflective;
        }

        public bool TryGetIntersectionWithRay(Vector3 origin, Vector3 rayDirection, out float t)
        {
            t = float.NaN;

            var oc = origin - Center;
            var k1 = Vector3.Dot(rayDirection, rayDirection);
            var k2 = 2 * Vector3.Dot(oc, rayDirection);
            var k3 = Vector3.Dot(oc, oc) - Radius * Radius;

            var discriminant = k2 * k2 - 4 * k1 * k3;
            if (discriminant < 0)
            {
                return false;
            }

            var t1 = (-k2 + (float)Math.Sqrt(discriminant)) / (2 * k1);
            var t2 = (-k2 - (float)Math.Sqrt(discriminant)) / (2 * k1);

            t = Math.Min(t1, t2);
            return (t >= 1);
        }
    }
}
