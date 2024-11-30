using System;
using UnityEngine;

[Serializable]
public class MyPlane
{
    [SerializeField] public Vector3 normal;

    /// <summary>
    /// Guardo el triangulo que me ayuda a determinar donde el plano limita el espacio (en qu� ubicacion, no quiere decir
    /// que estos vertices sean sus limites, ya que los planos son infinitos)
    /// </summary>
    public Vector3 verA;
    public Vector3 verB;
    public Vector3 verC;

    //distance from 0,0,0
    [SerializeField] public float distance;

    public MyPlane()
    {}

    ///// <summary>
    ///// Plane Constructor
    ///// Create the normal/distance of the Plane from 3 vertices
    ///// </summary>
    ///// <param name="vertex1"></param>
    ///// <param name="vertex2"></param>
    ///// <param name="vertex3"></param>
    public MyPlane(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
    {
        SetNormalAndPosition(vertex1, vertex2, vertex3);
    }

    public void SetNormalAndPosition(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
    {
        this.normal = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).normalized;

        this.distance = Vector3.Dot(this.normal, vertex1);

        verA = vertex1;
        verB = vertex2;
        verC = vertex3;
    }

    public Vector3 Normal { get { return normal; } }
    public float Distance { get { return distance; } }

    public MyPlane(Vector3 point, Vector3 normal)
    {
        SetNormalAndPosition(normal, point);
    }

    public bool GetSide(Vector3 pointToCheck)
    {
        //en cierta forma se transforma a la normal en coordenadas globales donde se hace el vector.
        //Al ser una direccion le tengo que sumar la distancia que tiene desde el punto 0,0,0 al plano para obtener la coordenada real.

        // El Dot es el producto escalar, es el coseno de un angulo.
        // Si esta inclinado para un lado (el coseno da distinto de 0, por lo que es menor o mayor a 90 grados) y da positivo, esta del lado positivo del plano.

        return (Vector3.Dot(this.normal, pointToCheck) + distance > 0);
        //return MyTools.DotProduct(normal, pointToCheck) + distance > 0;
    }

    public void SetNormalAndPosition(Vector3 point, Vector3 normal)
    {
        this.normal = normal.normalized;

        //this.normal = normal.normalized;}

        this.distance = -Vector3.Dot(this.normal, point);

        //distance = -MyTools.DotProduct(this.normal, point);
    }
}
