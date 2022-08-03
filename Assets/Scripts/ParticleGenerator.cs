using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    //[SerializeField] Transform _particleParent;

    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _particleSpeed;
    [SerializeField] private float _spawnerRadius;
    [SerializeField] private float _particleDrag;
    

    private float _nextSpawnTime;

   


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
        Vector2 pos = Random.insideUnitCircle * _spawnerRadius + (Vector2)transform.position;

        //create particle based on prefab we created and based on position we just created and a quaternion that corresponds to "no rotation"
        GameObject particle = Instantiate(_particlePrefab, pos, Quaternion.identity);

           
        //return the value of the particle
        return particle;
    }

    private void LaunchParticle(GameObject particle)
    {
        Rigidbody2D rb2d = particle.GetComponent<Rigidbody2D>();
        rb2d.drag = _particleDrag;
        rb2d.velocity = transform.right * _particleSpeed;
    }





}
