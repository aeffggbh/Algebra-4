using System;
using UnityEngine;

[Serializable]
public class MyPlane
{
    [SerializeField] public Vector3 normal;

    //distance from 0,0,0
    [SerializeField] public float distance;

    public MyPlane()
    {}

    public Vector3 Normal { get { return normal; } }
    public float Distance { get { return distance; } }

    public MyPlane(Vector3 point, Vector3 normal)
    {
        SetNormalAndPosition(normal, point);
    }

    public bool IsInPlane(Vector3 pointToCheck)
    {
        // en cierta forma se transforma a la normal en coordenadas globales donde se hace el vector.
        // Al ser una direccion le tengo que sumar la distancia que tiene desde el punto 0,0,0 al plano para obtener la coordenada real.

        // El Dot es el producto escalar, es el coseno de un angulo.
        // Si esta inclinado para un lado (el coseno da distinto de 0, por lo que es menor o mayor a 90 grados) y da positivo, esta del lado positivo del plano.

        return (Vector3.Dot(this.normal, pointToCheck) + distance > 0);
    }

    public void SetNormalAndPosition(Vector3 point, Vector3 normal)
    {
        this.normal = normal.normalized;

        this.distance = -Vector3.Dot(this.normal, point);
    }
}
