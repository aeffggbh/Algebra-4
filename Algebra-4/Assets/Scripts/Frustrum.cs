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
    public void SetData(Transform testTransform, float aspect, float fovY, float nearDistInput, float farDistInput)
    {
        Transform cam = testTransform;

        fovY *= Mathf.Deg2Rad;

        //tan = opuesto/ady
        // ady = fardist
        // opuesto = halfVSide
        float halfVSide = farDistInput * Mathf.Tan(fovY * 0.5f);
        // aspect = 16/9
        // 16 (width) = 9 (height) * aspect
        float halfHSide = halfVSide * aspect;

        Vector3 farForwardDistance = farDistInput * cam.forward;
        Vector3 nearForwardDistance = nearDistInput * cam.forward;

        nearFace.SetNormalAndPosition(cam.position + nearForwardDistance, cam.forward);
        farFace.SetNormalAndPosition(cam.position + farForwardDistance, -cam.forward);

        rightFace.SetNormalAndPosition(cam.position, MyTools.CrossProduct(farForwardDistance + cam.right * halfHSide, cam.up));
        leftFace.SetNormalAndPosition(cam.position, MyTools.CrossProduct(cam.up, farForwardDistance - cam.right * halfHSide));

        //hacerlo perpendicular al right de la camara ayuda a que quede mirando para arriba/abajo
        topFace.SetNormalAndPosition(cam.position, MyTools.CrossProduct(cam.right, farForwardDistance + cam.up * halfVSide));
        bottomFace.SetNormalAndPosition(cam.position, MyTools.CrossProduct(farForwardDistance - cam.up * halfVSide, cam.right));

    }

    public MyPlane[] GetPlanes()
    {
        return new MyPlane[6] { nearFace, farFace, rightFace, leftFace, topFace, bottomFace };
    }
}