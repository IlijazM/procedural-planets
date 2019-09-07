using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Material material;

    PlanetGenerator.Planet planet;

    void Start()
    {
        planet = new PlanetGenerator.Sphere();
        planet.Init();
        planet.UpdateMesh();

        Mesh[] meshes = planet.GetMeshes();

        for (int i = 0; i < meshes.Length; i++)
        {
            Mesh mesh = meshes[i];

            GameObject go = new GameObject("part");
            go.transform.parent = transform;

            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshFilter>();

            MeshRenderer renderer = go.GetComponent<MeshRenderer>();
            renderer.sharedMaterial = material;

            MeshFilter filter = go.GetComponent<MeshFilter>();
            filter.sharedMesh = mesh;
        }
    }

    void Update()
    {
        
    }
}
