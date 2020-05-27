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
        public static Color BackgroundColor = Color.LightSkyBlue;

        public static IReadOnlyList<Sphere> Objects = new List<Sphere>
        {
            new Sphere(new Vector3(-1, -8, 25), 0.3f, Color.CornflowerBlue, 1000, 0.5f),
            new Sphere(new Vector3(1, -8, 25), 0.3f, Color.CornflowerBlue, 1000, 0.5f),

            new Sphere(new Vector3(0, -7, 25), 0.3f, Color.DarkGoldenrod, 1000, 0.3f),

            new Sphere(new Vector3(-1, -6, 25), 0.3f, Color.DarkRed, 1000, 0.1f),
            new Sphere(new Vector3(0, -5.5f, 25), 0.3f, Color.DarkRed, 1000, 0.1f),
            new Sphere(new Vector3(1, -6, 25), 0.3f, Color.DarkRed, 1000, 0.1f),

            new Sphere(new Vector3(0, -10, 30), 3f, Color.Aquamarine, 10000, 0.1f),
            new Sphere(new Vector3(0, -8, 30), 4f, Color.White, 1000, 0.1f),
            new Sphere(new Vector3(0, 0, 30), 5f, Color.White, 100, 0.1f),
            new Sphere(new Vector3(0, 10, 30), 6f, Color.White, 10, 0.1f),

            new Sphere(new Vector3(0, 105, 30), 90f, Color.WhiteSmoke, 10, 0.1f),
        };

        public static Light AmbientLight = new Light(LightType.Ambient, 0.2f);

        public static IReadOnlyList<Light> Lights = new List<Light>
        {
            new Light(LightType.Point, 0.6f, new Vector3(2, 1, 0)),
            new Light(LightType.Directional, 0.2f, new Vector3(1, 4, 4))
        };
    }
}
