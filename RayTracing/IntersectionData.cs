using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracing
{
    public class IntersectionData
    {
        public Sphere Sphere { get; set; }

        /// <summary>
        /// Коэффициент, при котором луч пересекает сферу
        /// </summary>
        public float T { get; set; }
    }
}
