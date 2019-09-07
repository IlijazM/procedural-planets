using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGenerator
{
    public class Sphere : Planet
    {
        public Vector3[] directions;

        public SpherePart[] parts;

        public override void Init()
        {
            base.Init();

            directions = new Vector3[]
            {
                Vector3.up,
                Vector3.down,
                Vector3.right,
                Vector3.left,
                Vector3.forward,
                Vector3.back
            };
        }

        public override void UpdateMesh()
        {
            base.UpdateMesh();

            parts = new SpherePart[directions.Length];

            for (int i = 0; i < directions.Length; i++)
            {
                parts[i] = new SpherePart(this, directions[i]);
                parts[i].UpdateMesh();
            }
        }

        public override Mesh[] GetMeshes()
        {
            Mesh[] meshes = new Mesh[parts.Length];

            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i] = parts[i].mesh;
            }

            return meshes;
        }
    }

    public class SpherePart
    {
        public Sphere sphere;
        public PlanetStats stats;

        public Mesh mesh;

        public Vector3 localX;
        public Vector3 localY;
        public Vector3 localZ;

        public SpherePart(Sphere sphere, Vector3 localY)
        {
            this.sphere = sphere;
            stats = sphere.stats;

            this.localY = localY;
            localX = new Vector3(localY.y, localY.z, localY.x);
            localZ = Vector3.Cross(localX, localY);
        }

        public void UpdateMesh()
        {
            Vector3[] vertices = new Vector3[stats.resolution * stats.resolution];

            int triCount = (int)Mathf.Pow(stats.resolution - 1, 2) * 6;
            int[] triangles = new int[triCount];

            int triIndex = 0;
            for (int x = 0; x < stats.resolution; x++)
            {
                for (int y = 0; y < stats.resolution; y++)
                {
                    int i = y + x * stats.resolution;

                    Vector2 planePosition = new Vector2(x, y) / (stats.resolution - 1);

                    Vector3 pointOnCube = localY + (planePosition.x - 0.5f) * 2 * localX + (planePosition.y - 0.5f) * 2 * localZ;
                    Vector3 pointOnSphere = pointOnCube.normalized;

                    vertices[i] = pointOnSphere;

                    if (x != stats.resolution - 1 && y != stats.resolution - 1)
                    {
                        triangles[triIndex] = i;
                        triangles[triIndex + 1] = i + stats.resolution + 1;
                        triangles[triIndex + 2] = i + stats.resolution;
                        triangles[triIndex + 3] = i;
                        triangles[triIndex + 4] = i + 1;
                        triangles[triIndex + 5] = i + stats.resolution + 1;

                        triIndex += 6;
                    }
                }
            }

            mesh = new Mesh();
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
    }

}
