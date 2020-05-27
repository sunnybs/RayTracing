using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public class Light
    {
        public LightType LightType { get; }
        public float Intensity { get; }
        public Vector3 Position { get; }

        public Light(LightType type, float intens, Vector3 pos = default(Vector3))
        {
            LightType = type;
            Intensity = intens;
            Position = pos;
        }

    }

    public enum LightType
    {
        Ambient,
        Point,
        Directional
    }
}
