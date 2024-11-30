using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class FrustumDrawer : MonoBehaviour
{
    public FrustrumCulling frustrumCulling;

    private void OnDrawGizmos()
    {
        if (frustrumCulling.frustrum != null)
            DrawFrustrum(frustrumCulling.frustrum);
    }

    private void DrawFrustrum(Frustrum frustrum)
    {
        if (frustrum.GetPlanes().Length <= 0) return;
        
        //near-far connections

        Gizmos.color = Color.green;
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearTopLeft], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farTopLeft]);
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearTopRight], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farTopRight]);

        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearBottomRight], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farBottomRight]);
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearBottomLeft], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farBottomLeft]);
   
        //near
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearTopLeft], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearTopRight]);
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearTopLeft], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearBottomLeft]);
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearTopRight], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearBottomRight]);
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearBottomLeft], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.nearBottomRight]);

        //far
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farTopLeft], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farTopRight]);
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farTopLeft], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farBottomLeft]);
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farTopRight], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farBottomRight]);
        Gizmos.DrawLine(frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farBottomLeft], frustrumCulling.corners[(int)FrustrumCulling.CornersEn.farBottomRight]);
    }
}