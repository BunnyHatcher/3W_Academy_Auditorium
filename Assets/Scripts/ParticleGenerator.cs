using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    /*[SerializeField] private GameObject _particlePrefab;
     * With the particle pool created, we don't need to
     * declare the prefab anymore and can use the pool directly...
     */
    [SerializeField] private ParticlePool _pool;

    [Header("Spawner Parameters")]
    [Tooltip("Parent folder for storing particles")]
    [SerializeField] private Transform _particleContainer;

    
    [SerializeField] private float _spawnDelay;
    //allows to add a slider with a range of values 
    [Range(0.1f,+10f)]
    [SerializeField] private float _spawnerRadius;

    [Header("Particle Parameters")]
    [Tooltip("Initial Speed of the particle in m/s")]
    [SerializeField] private float _particleSpeed;

    [Tooltip("Friction applied to particules")]
    [SerializeField] private float _particleDrag;


    [Header("Gizmos")]
    [SerializeField] private bool _drawGizmos;
    [SerializeField] private Color _gizmosColor;
    

    private float _nextSpawnTime;

    //Tipp: Put tranform into cache to save resources
    //1. declare variable
    private Transform _transform;

    
    //2. call variable on Awake and give it the value of transform
    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if(Time.time > _nextSpawnTime)
        {
            //Spawn particle
            GameObject newParticle = SpawnParticle();

            
            // Launch particle
            LaunchParticle(newParticle);

            _nextSpawnTime = Time.time + _spawnDelay;
        }
    }

    private GameObject SpawnParticle()
    {
        //define random position within a given radius and add the transform position of the spawner
        //3. use variable "_tranform" instead of transform
        Vector2 pos = Random.insideUnitCircle * _spawnerRadius + (Vector2)_transform.position;

        /*create particle based on prefab and position we just created
        as well as a quaternion that corresponds to "no rotation" and a parent prefab to store my particles
        
        GameObject particle = Instantiate(_particlePrefab, pos, Quaternion.identity, _particleContainer);
        
        --> Instead of instantiating the particles in the Generator script, we can just bring over 
        the GetParticle method from the ParticlePool script
        */
        GameObject particle = _pool.GetParticle();

        //make sure that particle is not null to avoid errors
        if (particle != null)
        {
                   
            //reactivate the particle so we can use it
            particle.SetActive(true);

            //and tell it to spawn at the right position, i.e. the Vector 2 pos
            particle.transform.position = pos;

            //access the trail renderer for the new particle and delete the trail in the history
            //to avoid treail artefacts from the previous position of the particle
            particle.GetComponent<TrailRenderer>().Clear();
        }
        
        //return the value of the particle
        return particle;
    }

    
   
    private void LaunchParticle(GameObject particle)
    {
        //add condition to check if there are actually particles available in the pool 

        if (particle != null)
        { 
        Rigidbody2D rb2d = particle.GetComponent<Rigidbody2D>();
        rb2d.drag = _particleDrag;

        //3. use variable "_tranform" instead of transform
        rb2d.velocity = _transform.right * _particleSpeed;
        }
    }



    private void OnDrawGizmos()
    {
        if (_drawGizmos)
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(transform.position, _spawnerRadius);
            Gizmos.DrawRay(transform.position, transform.right * _particleSpeed);
        }
        
         
    }





}
