using System.Collections.Generic;
using UnityEngine;
using static FrustumDrawer;

//3

[ExecuteAlways]
public class FrustrumCulling : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Vector2 aspectRatio;
    [SerializeField] private List<AABB> gos;
    [SerializeField] private float fovY, zNear, zFar;
    [SerializeField] public Vector3[] vertices;

    public Frustrum frustrum = new();

    public enum Vertex
    {
        nearTopLeft,
        nearTopRight,
        nearBottomRight,
        nearBottomLeft,

        farTopLeft,
        farTopRight,
        farBottomRight,
        farBottomLeft,
    }

    private void Update()
    {
        UpdateFrustrum();

        UpdateVertices();

        foreach (AABB go in gos)
        {
            go.GetComponent<MeshRenderer>().enabled = IsPointOnPlane(go);
        }
    }

    private Vector3 IntersectThreePlanes(MyPlane plane1, MyPlane plane2, MyPlane plane3)
    {
        Vector3 normal1 = plane1.Normal, normal2 = plane2.Normal, normal3 = plane3.Normal;

        //El cross es la formula para obtener la intersección entre dos planos (interseccion plano-plano) que te devuelve la recta en la que intersectan
        //El dot es la formula para obtener la intersección recta-plano
        float determinant = MyTools.DotProduct(normal1, MyTools.CrossProduct(normal2, normal3));

        if (Mathf.Abs(determinant) < Vector3.kEpsilon)
        {
            Debug.LogWarning("Planes do not intersect at a single point.");
            return Vector3.zero;
        }

        Vector3 intersectPoint = (
            (-plane1.Distance * MyTools.CrossProduct(normal2, normal3)) +
            (-plane2.Distance * MyTools.CrossProduct(normal3, normal1)) +
            (-plane3.Distance * MyTools.CrossProduct(normal1, normal2))
            ) / determinant;

        return intersectPoint;
    }

    public void UpdateVertices()
    {
        vertices = new Vector3[8];

        vertices[(int)Vertex.nearTopLeft] = IntersectThreePlanes(frustrum.leftFace, frustrum.topFace, frustrum.nearFace); // Near Top Left
        vertices[(int)Vertex.nearTopRight] = IntersectThreePlanes(frustrum.rightFace, frustrum.topFace, frustrum.nearFace); // Near Top Right
        vertices[(int)Vertex.nearBottomRight] = IntersectThreePlanes(frustrum.rightFace, frustrum.bottomFace, frustrum.nearFace); // Near Bottom Right
        vertices[(int)Vertex.nearBottomLeft] = IntersectThreePlanes(frustrum.leftFace, frustrum.bottomFace, frustrum.nearFace); // Near Bottom Left

        vertices[(int)Vertex.farTopLeft] = IntersectThreePlanes(frustrum.leftFace, frustrum.topFace, frustrum.farFace); // Far Top Left
        vertices[(int)Vertex.farTopRight] = IntersectThreePlanes(frustrum.rightFace, frustrum.topFace, frustrum.farFace); // Far Top Right
        vertices[(int)Vertex.farBottomRight] = IntersectThreePlanes(frustrum.rightFace, frustrum.bottomFace, frustrum.farFace); // Far Bottom Right
        vertices[(int)Vertex.farBottomLeft] = IntersectThreePlanes(frustrum.leftFace, frustrum.bottomFace, frustrum.farFace); // Far Bottom Left

    }

    private bool IsPointOnPlane(AABB go)
    {
        Vector3[] vertices = go.BoxVertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            int count = 0;

            foreach (var item in frustrum.GetPlanes())
            {
                if (GetSide(item, vertices[i]))
                {
                    count++;
                }
            }

            if (count == 6)
            {
                return true;
            }
        }

        return false;
    }

    public bool GetSide(MyPlane plane, Vector3 position)
    {
        return plane.IsInPlane(position);
    }


    private void UpdateFrustrum()
    {
        frustrum.SetData(mainCam.transform, aspectRatio.x / aspectRatio.y, fovY, zNear, zFar);
    }
}