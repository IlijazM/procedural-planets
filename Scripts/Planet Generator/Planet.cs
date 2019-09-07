using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGenerator
{
    public class Planet
    {
        public PlanetStats stats;

        public virtual void Init()
        {
            if (stats.resolution <= 0 || stats.resolution >= 255)
            {
                stats.resolution = 10;
            }
        }

        public virtual void UpdateMesh()
        {

        }

        public virtual void GenerateTerrain()
        {

        }

        public virtual Mesh[] GetMeshes()
        {
            return null;
        }
    }
}

