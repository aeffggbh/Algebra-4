using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Frustrum
{
    public MyPlane topFace = new();
    public MyPlane bottomFace = new();

    public MyPlane rightFace = new();
    public MyPlane leftFace = new();

    public MyPlane farFace = new();
    public MyPlane nearFace = new();

    public Frustrum()
    { }

    public Frustrum(Transform transform, float aspect, float fovY, float zNear, float zFar)
    {
        SetData(transform, aspect, fovY, zNear, zFar);
    }

    //https://learnopengl.com/Guest-Articles/2021/Scene/Frustum-Culling
    public void SetData(Transform origin, float aspect, float fovY, float nearDistInput, float farDistInput)
    {
        //paso el fovY a radianes para calcular la tangente
        fovY *= Mathf.Deg2Rad;

        //tan = opuesto/ady
        // ady = fardist
        // opuesto = halfVSide
        float halfVSide = farDistInput * Mathf.Tan(fovY * 0.5f);
        // aspect = 16/9
        // 16 (width) = 9 (height) * aspect
        // El fov se va a modificar proporcionalmente al aspect
        float halfHSide = halfVSide * aspect;

        Vector3 farForwardDistance = farDistInput * origin.forward;
        Vector3 nearForwardDistance = nearDistInput * origin.forward;

        nearFace.SetNormalAndPosition(origin.position + nearForwardDistance, origin.forward);
        farFace.SetNormalAndPosition(origin.position + farForwardDistance, -origin.forward);

        rightFace.SetNormalAndPosition(origin.position, MyTools.CrossProduct(farForwardDistance + origin.right * halfHSide, origin.up));
        leftFace.SetNormalAndPosition(origin.position, MyTools.CrossProduct(origin.up, farForwardDistance - origin.right * halfHSide));

        topFace.SetNormalAndPosition(origin.position, MyTools.CrossProduct(origin.right, farForwardDistance + origin.up * halfVSide));
        bottomFace.SetNormalAndPosition(origin.position, MyTools.CrossProduct(farForwardDistance - origin.up * halfVSide, origin.right));
    }

    public MyPlane[] GetPlanes()    
    {
        return new MyPlane[6] { nearFace, farFace, rightFace, leftFace, topFace, bottomFace };
    }
}