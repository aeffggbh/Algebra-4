using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//4
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
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearTopLeft], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farTopLeft]);
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearTopRight], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farTopRight]);

        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearBottomRight], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farBottomRight]);
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearBottomLeft], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farBottomLeft]);
   
        //near
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearTopLeft], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearTopRight]);
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearTopLeft], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearBottomLeft]);
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearTopRight], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearBottomRight]);
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearBottomLeft], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.nearBottomRight]);

        //far
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farTopLeft], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farTopRight]);
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farTopLeft], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farBottomLeft]);
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farTopRight], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farBottomRight]);
        Gizmos.DrawLine(frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farBottomLeft], frustrumCulling.vertices[(int)FrustrumCulling.Vertex.farBottomRight]);
    }
}