using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{

    [SerializeField] GameObject _particlePrefab;
    [SerializeField] Transform _particleSpawner;
    

    [SerializeField] float __particleSpeed;

    //Setting shot intervall
    [SerializeField] float _delayBetweenParticles;
    private float _nextSpawnTime;





    private void SpawnParticle()
    {
        GameObject newParticle = Instantiate(_particlePrefab, _particleSpawner.position, _particleSpawner.rotation);
        
    }








}
