using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    [SerializeField] private float _minimumSpeed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_rigidbody.velocity.magnitude < _minimumSpeed)
        {
            gameObject.SetActive(false);
        }
    }
}
