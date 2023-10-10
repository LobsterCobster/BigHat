using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshCollider : MonoBehaviour
{
    SkinnedMeshRenderer meshRenderer;
    MeshCollider meshcollider;

    public void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        meshcollider = GetComponent<MeshCollider>();
    }
    public void Update()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        meshcollider.sharedMesh = null;
        meshcollider.sharedMesh = colliderMesh;
    }
}
