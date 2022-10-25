using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// float para controlar la velocidad
    /// </summary>
    public float speed = 3.8f;
    /// <summary>
    /// controlar la posici�n del objeto
    /// </summary>
    private Transform target;
    /// <summary>
    /// �ndice de cada uno de los "waypoints" al que se dirije
    /// </summary>
    private int waypointIndex = 0;

    /// <summary>
    /// 
    /// </summary>
    public float rotationSpeed=150;

    GameController gc;
   
    private void Start()
    {
        target = WaypointController.points[0];//empieza en el punto 0 (spawn)

    }
    private void Update()
    {

       Vector3 dir = target.position - transform.position;//coge la direcci�n hacia donde se dirigir� con un vector3

       Quaternion rotation = Quaternion.LookRotation(dir);//busca la rotaci�n del waypoint

       transform.rotation=Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);//giro enemigo

       transform.Translate(dir.normalized*speed*Time.deltaTime,Space.World);//Cada frame controlar� hacia donde se est� moviendo

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)//Al estar cerca del target, aumentar� el index de los puntos de control
        {
            GetNextWayPoint();
        }
    }

    /// <summary>
    /// esta funci�n aumenta el index del "waypoint" actual en el que se encuentra el enemigo
    /// si el enemigo supera los l�mites del array de "waypoints" se destruye
    /// </summary>
    void GetNextWayPoint()
    {
        if (waypointIndex >= WaypointController.points.Length - 1)
        {
            
            Destroy(gameObject);
            waypointIndex = 0;
                       
        }
        waypointIndex++;
        target = WaypointController.points[waypointIndex];//se le vuelve a asignar la posici�n del pr�ximo "waypoint"
    }

}
