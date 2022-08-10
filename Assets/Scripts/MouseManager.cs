using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

    [SerializeField] private LayerMask _layerMask;

    [Header("Cursor Texture")]
    [SerializeField] private Texture2D _mouseMoveTexture;
    [SerializeField] private Texture2D _mouseResizeTexture;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity , _layerMask );

        if( hit.collider != null )
        {
            Debug.DrawRay(ray.origin, ray.direction * 150f, Color.green);
            Debug.Log("Touchdown!");
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 150f, Color.red);
        }
    }
}
