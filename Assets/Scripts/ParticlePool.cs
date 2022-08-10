using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{

    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private int _amountOfParticles;

    private GameObject[] _particles;

    private void Awake()
    {
        //Initiate array with defined size
        _particles = new GameObject[_amountOfParticles];
        
        //Then instantiate an array of the defined size using a for loop
        for (int i = 0; i < _amountOfParticles; i++)
        {
            _particles[i] = Instantiate(_particlePrefab, transform);
            // deactivate particles immediatly
            _particles[i].SetActive(false);
        }
    }

    public GameObject GetParticle()
    {   
        //check the defined amount of particles in the array...
        for (int i = 0; i < _amountOfParticles; i++)
        
        {   
            //each particle that is not active in the hierarchy..
            if(!_particles[i].activeInHierarchy)
            {   
                // ... will be returned
                return _particles[i];
            }
        }

        // if no inactive particle is found, return null
        return null;
    }

}
