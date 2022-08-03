using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] Transform _particleParent;

    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _particleSpeed;
    [SerializeField] private float _spawnerRadius;
    

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
        Vector2 pos = Random.insideUnitCircle * _spawnerRadius;
        GameObject particle = Instantiate(_particlePrefab, _particleParent);
        particle.transform.position = pos;
        return particle;
    }

    private void LaunchParticle(GameObject particle)
    {
        Rigidbody2D rb2d = particle.GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.right * _particleSpeed;
    }





}
