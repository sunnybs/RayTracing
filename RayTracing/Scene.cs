using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public static class Scene
    {
        public static Vector3 CameraPosition = new Vector3(0,0,0);
        public static float ViewportSize = 1.0f;
        public static float ZProjection = 1.0f;
        public static Color BackgroundColor = Color.Black;

        public static IReadOnlyList<Sphere> Objects = new List<Sphere>
        {
            new Sphere(new Vector3(0, -1, 30), 5f, Color.Aqua, 100, 0.1f),
            new Sphere(new Vector3(2, 0, 4), 1f, Color.Aquamarine, 10, 0.2f),
            new Sphere(new Vector3(-2, 0, 4), 1f, Color.BlueViolet, 1000, 0.3f)
        };

        public static Light AmbientLight = new Light(LightType.Ambient, 0.2f);

        public static IReadOnlyList<Light> Lights = new List<Light>
        {
            new Light(LightType.Point, 0.6f, new Vector3(2, 1, 0)),
            new Light(LightType.Directional, 0.2f, new Vector3(1, 4, 4))
        };
    }
}
