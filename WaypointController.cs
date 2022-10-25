using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    /// <summary>
    /// Static para acceder a esta referencia desde cualquier script
    /// Un array de transforms que muestra la posici�n de cada waypoint
    /// </summary>
    public static Transform[] points;
    private void Awake()
    {
        
        points = new Transform[transform.childCount];//la longitud del array depende de la cantidad de hijos que tenga este objeto
        //Recorro el array de points y busca dentro de todos los hijos de este objeto para asignarles a cada "points" su transform
        for(int i = 0; i < points.Length; i++)
        {
            points[i]=transform.GetChild(i);
        }
        //a partir de aqu� ya est�n guardadas todas las posiciones de los hijos de este objeto(waypoints) en un array
    }
}
