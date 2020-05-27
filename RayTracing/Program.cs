using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Program
    {
        public const int Height = 600;
        public const int Width = 800;

        private static void Main(string[] args)
        {
            var buffer = Render();
            var picture = GetImageData(buffer);
            picture.Save("result.png", ImageFormat.Png);
            Console.WriteLine("Результат трассировки записан в файл result.png");
        }

        private static Color[] Render()
        {
            var frameBuffer = new Color[Height * Width];

            var tasks = new List<Task>();
            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
            {
                var localX = x;
                var localY = y;
                var task = new Task((() =>
                {
                    var rayDirection = GetRayViewportDirection(localX, localY);
                    frameBuffer[localX + localY * Width] = TraceRay(Scene.CameraPosition, rayDirection, 3);
                }));

                tasks.Add(task);
                task.Start();
            }

            Task.WaitAll(tasks.ToArray());
            return frameBuffer;
        }

        private static Color TraceRay(Vector3 origin, Vector3 direction, int depth)
        {
            var intersection = GetClosestIntersection(origin, direction);
            if (intersection.Sphere == null)
                return Scene.BackgroundColor;

            var closestSphere = intersection.Sphere;
            var closestT = intersection.T;

            var point = Scene.CameraPosition + closestT * direction;

            var normal = point - closestSphere.Center;
            normal = Vector3.Normalize(normal);

            var view = -1 * direction;
            var lighting = ComputeLighting(point, normal, view, closestSphere.Specular);
            var color = lighting * closestSphere.Color.ToVector3();

            if (closestSphere.Reflective <= 0 || depth <= 0)
                return color.ToColor();

            var reflectedRay = ReflectRay(view, normal);
            var reflectedColor = TraceRay(origin, reflectedRay, depth - 1).ToVector3();

            return ((1 - closestSphere.Reflective) * color + closestSphere.Reflective * reflectedColor).ToColor();
        }

        private static float ComputeLighting(Vector3 point, Vector3 normal, Vector3 view, float specular)
        {
            var intensity = Scene.AmbientLight.Intensity;
            var normalLen = 1.0f;
            var viewLen = view.Length();

            foreach (var light in Scene.Lights)
            {
                Vector3 vectorL;
                float maxT;

                if (light.LightType == LightType.Point)
                {
                    vectorL = light.Position - point;
                    maxT = 1.0f;
                }
                else
                {
                    // LightType.Directional
                    vectorL = light.Position;
                    maxT = float.MaxValue;
                }

                // Проверка тени
                var blocker = GetClosestIntersection(point, vectorL, maxT);
                if (blocker.Sphere != null) continue;

                // Диффузное отражение
                var normalDotL = Vector3.Dot(normal, vectorL);
                if (normalDotL > 0) intensity += light.Intensity * normalDotL / (normalLen * vectorL.Length());

                // Зеркальное отражение
                if (specular != -1)
                {
                    var vectorR = ReflectRay(vectorL, normal);
                    var rDotV = Vector3.Dot(vectorR, view);
                    if (rDotV > 0)
                        intensity += light.Intensity * (float)Math.Pow(rDotV / (vectorR.Length() * viewLen), specular);
                }
            }

            return intensity;
        }

        private static Vector3 ReflectRay(Vector3 v1, Vector3 v2)
        {
            return ((2 * Vector3.Dot(v1, v2)) * v2) - v1;
        }

        private static IntersectionData GetClosestIntersection(Vector3 origin, Vector3 direction,
            float maxT = float.MaxValue)
        {
            var closestT = float.MaxValue;
            Sphere closestSphere = null;

            foreach (var sphere in Scene.Objects)
                if (sphere.TryGetIntersectionWithRay(origin, direction, out var t) && t < closestT)
                {
                    closestSphere = sphere;
                    closestT = t;
                }

            return closestT > maxT
                ? new IntersectionData {Sphere = null, T = float.NaN}
                : new IntersectionData {Sphere = closestSphere, T = closestT};
        }

        private static Vector3 GetRayViewportDirection(int x, int y)
        {
            return new Vector3((x - Width / 2) * Scene.ViewportSize / Width,
                (y - Height / 2) * Scene.ViewportSize / Height,
                Scene.ZProjection);
        }

        private static Bitmap GetImageData(Color[] frameBuffer)
        {
            var picture = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                picture.SetPixel(x, y, frameBuffer[x + y * Width]);

            return picture;
        }
    }
}