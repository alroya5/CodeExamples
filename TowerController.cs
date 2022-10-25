using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    /// <summary>
    /// Objetivos del disparo
    /// </summary>
    List<Target> targets;

    /// <summary>
    /// Punto de emisi�n de las part�culas
    /// </summary>
    public Transform muzzle;

    /// <summary>
    /// Torreta
    /// </summary>
    public Transform turret;

    /// <summary>
    /// Material de las part�culas
    /// </summary>
    Material particleMaterial;

    /// <summary>
    /// Componente emisor de part�culas
    /// </summary>
    ParticleSystem _ps;

    /// <summary>
    /// M�dulo principal del sistema de part�culas
    /// </summary>
    ParticleSystem.MainModule mainModule;

    /// <summary>
    /// M�dulo shape
    /// </summary>
    ParticleSystem.ShapeModule shapeModule;

    /// <summary>
    /// M�dulo de emisi�n
    /// </summary>
    ParticleSystem.EmissionModule emissionModule;

    /// <summary>
    /// M�dulo de renderer
    /// </summary>
    ParticleSystemRenderer particleRenderer;
    ParticleSystemRenderMode renderMode = ParticleSystemRenderMode.Mesh;

    /// <summary>
    /// M�dulo de colisi�n
    /// </summary>
    ParticleSystem.CollisionModule collisionModule;

    /// <summary>
    /// Velocidad de rotaci�n
    /// </summary>
    public float rotationSpeed = 90;

    /// <summary>
    /// Estado del ca��n
    /// </summary>
    enum GunState { ready, cooldown, pointing }
    GunState gunState;

    /// <summary>
    /// Indica si el ca��n est� apuntando al target
    /// </summary>
    bool pointing;

    /// <summary>
    /// Tiempo de reutilizaci�n
    /// </summary>
    public float coolDown = 1;

    /// <summary>
    /// Precisi�n de apuntado
    /// </summary>
    private float anglePosition = 5;

    /// <summary>
    /// Identificador del personaje sale del �reea de influencia de la torreta
    /// </summary>
    int exitId;

    /// <summary>
    /// Le da el valor de da�o que hara la torre
    /// </summary>
    public float damage = 10;

    public Material Bullet;

    public Mesh Arrow;

    public float BulletSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        //Se carga el material de las part�culas
        //particleMaterial = Resources.Load<Material>("Particle Material");
        particleMaterial = Bullet;

        //Asigana el emisor de part�culas
        _ps = gameObject.AddComponent<ParticleSystem>();

        //Ajusta el tama�o de la part�cula
        mainModule = _ps.main;
        mainModule.startSize = 0.5f;

        //Ajusta la velocidad de la part�cula
        mainModule.startSpeed = BulletSpeed;

        //Ajusta el shape de sistema de part�culas
        shapeModule = _ps.shape;
        shapeModule.angle = 0;
        shapeModule.radius = 0;

        //Detiene la emisi�n autom�tica de part�culas
        emissionModule = _ps.emission;
        emissionModule.rateOverTime = 0;

        //se accede al m�dulo de renderer
        particleRenderer = GetComponent<ParticleSystemRenderer>();
        particleRenderer.material = particleMaterial;
        particleRenderer.mesh = Arrow;

        //se accede al m�dulo de colisi�n
        collisionModule = _ps.collision;
        //se habilita el m�dulo de colisi�n
        collisionModule.enabled = true;
        //Tipo World
        collisionModule.type = ParticleSystemCollisionType.World;
        //Permite recibir mensaje de la colisi�n de part�culas
        collisionModule.sendCollisionMessages = true;
        //Las part�culas pierden el 100% de su vida al colisionar
        collisionModule.lifetimeLoss = 1;

        //Puede disparar
        gunState = GunState.ready;

        //Crea la lista de Targets
        targets = new List<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        particleRenderer.renderMode = renderMode;

        if (targets.Count > 0)
        {
            //Actualiza la posici�n shapeModule respecto del muzzle
            shapeModule.position = muzzle.position - transform.position;

            if (targets[0].healthSubject.healthState == HealthModule.HealthState.death)
            {
                targets.RemoveAt(0);
                return;
            }

            //Direcci�n que une a la torreta con el target   
           
                Vector3 direction = targets[0].targetSubject.GetChild(0).position - turret.position;
                //Rotaci�n necesaria para mirar al target
                Quaternion rotation = Quaternion.LookRotation(direction);
            //Rotaci�n interpolada
                Vector3 rotation_ = Quaternion.Lerp(muzzle.rotation, rotation, Time.deltaTime * rotationSpeed).eulerAngles;
                turret.rotation = rotation;//Quaternion.RotateTowards(turret.rotation, rotation, rotationSpeed * Time.deltaTime);

            //Vector3 direction = targets[0].targetSubject.GetChild(0).position - turret.position;
            ////Rotaci�n necesaria para mirar al target
            //Quaternion rotation = Quaternion.LookRotation(direction);
            ////Rotaci�n interpolada
            //Vector3 rotation_ = rotation.eulerAngles; // Quaternion.Lerp(muzzle.rotation, rotation, Time.deltaTime * rotationSpeed).eulerAngles;
            //turret.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);//Quaternion.RotateTowards(turret.rotation, rotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(turret.rotation, rotation) < anglePosition)
                    pointing = true;
                else
                    pointing = false;

            ///Modifica la rotaci�n del shape
            shapeModule.rotation = turret.rotation.eulerAngles;

            //Comprueba si puede disparar
            if (gunState == GunState.ready && pointing)
            {
                //Emite una particula
                _ps.Emit(1);
                //Cambia de estado
                gunState = GunState.cooldown;
                //Reset cooldown
                Invoke("ResetCooldown", coolDown);
            }
        }
    }


    void ResetCooldown()
    {
        //Cambia de estado
        gunState = GunState.ready;

    }

    /// <summary>
    /// M�todo invocado por Unity
    /// cunado se produce una colisi�n de una part�cula
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        
    }

    /// <summary>
    /// Detecta los eventos de trigger(enter)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        AgentController subject = other.GetComponent<AgentController>();


        if (subject != null)
        {
            Target t = new Target(subject.transform, subject.GetComponent<HealthModule>(), subject);
            targets.Add(t);
        }
    }


    /// <summary>
    /// Detecta los eventos de trigger(exit)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        AgentController subject = other.GetComponent<AgentController>();


        if (subject != null)
        {
            exitId = subject.iD;
            Target targetFounded = targets.Find(GetTargetId);
            targets.Remove(targetFounded);
        }

    }

    /// <summary>
    /// M�todo de comparaci�n
    /// Se llama de forma autom�tica desde el m�todo Find,
    /// una vez por cada elemento de la colecci�n.
    /// </summary>
    /// <param name="currentElement"></param>
    /// <returns></returns>
    private bool GetTargetId(Target currentElement)
    {
        if (exitId == currentElement.agentController.iD)
            return true;

        return false;
    }

    ///// <summary>
    ///// Dibuja una primitiva
    ///// </summary>
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, 7.5f);
    //}
}

/// <summary>
/// Representa a un target
/// </summary>
public class Target
{
    public Transform targetSubject;
    public AgentController agentController;
    public HealthModule healthSubject;

    public Target() { }
    public Target(Transform tr, HealthModule hm, AgentController ac)
    {
        targetSubject = tr;
        agentController = ac;
        healthSubject = hm;
    }
}