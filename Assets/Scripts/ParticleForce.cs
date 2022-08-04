using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleForce : MonoBehaviour
{

    public Rigidbody2D rb;
    public Vector2 f;


    private void Start()
    {
        rb.AddForce(f, 0);
    }



}
