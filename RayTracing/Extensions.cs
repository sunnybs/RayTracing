using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public static class Extensions
    {
        public static Color ToColor(this Vector3 vector)
        {
            var r = vector.X > 255 ? 255 : (int)vector.X;
            var g = vector.Y > 255 ? 255 : (int)vector.Y;
            var b = vector.Z > 255 ? 255 : (int)vector.Z;

            return Color.FromArgb(r, g, b);
        }

        public static Vector3 ToVector3(this Color color)
        {
            return new Vector3(color.R,color.G,color.B);
        }
    }
}
