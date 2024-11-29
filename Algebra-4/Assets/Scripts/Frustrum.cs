using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Frustrum
{
    public MyPlane topFace;
    public MyPlane bottomFace;

    public MyPlane rightFace;
    public MyPlane leftFace;

    public MyPlane farFace;
    public MyPlane nearFace;

    public Frustrum(Transform transform, float aspect, float fovY, float zNear, float zFar)
    {
        SetData(transform, aspect, fovY, zNear, zFar);
    }

    public void SetData(Transform testTransform, float aspect, float fovY, float nearDistInput, float farDistInput)
    {
        Transform cam = testTransform;

        fovY *= Mathf.Deg2Rad;

        float halfVSide = farDistInput * Mathf.Tan(fovY * 0.5f);
        float halfHSide = halfVSide * aspect;

        Vector3 farForwardDistance = farDistInput * cam.forward;
        Vector3 nearForwardDistance = nearDistInput * cam.forward;

        nearFace.SetNormalAndPosition(cam.position + nearForwardDistance, cam.forward);
        farFace.SetNormalAndPosition(cam.position + farForwardDistance, -cam.forward);

        rightFace.SetNormalAndPosition(cam.position, MyTools.CrossProduct(farForwardDistance + cam.right * halfHSide, cam.up));
        leftFace.SetNormalAndPosition(cam.position, MyTools.CrossProduct(cam.up, farForwardDistance - cam.right * halfHSide));

        topFace.SetNormalAndPosition(cam.position, -MyTools.CrossProduct(cam.right, farForwardDistance - cam.up * halfVSide));
        bottomFace.SetNormalAndPosition(cam.position, -MyTools.CrossProduct(farForwardDistance + cam.up * halfVSide, cam.right));

    }

    public MyPlane[] GetPlanes()
    {
        return new MyPlane[6] { nearFace, farFace, rightFace, leftFace, topFace, bottomFace };
    }
}