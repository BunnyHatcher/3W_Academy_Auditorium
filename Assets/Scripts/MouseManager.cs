using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _forceRatio = 10;

    [SerializeField] private Vector2 _radiusLimit = new Vector2(1f, 5f); // x is the minimum, y is the maximum

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
            
            //set cursor to Move sprite if it hits a target with the "Move" tag
            if(hit.collider.CompareTag("Move"))
            {
                Cursor.SetCursor(_mouseMoveTexture, new Vector2(16f, 16f), CursorMode.Auto);
                if( Input.GetMouseButtonDown(0))
                {
                    _activeEffector = hit.collider.transform.parent;
                }
                // When I click on the Move field of an effector, I move it
                if( _activeEffector != null)
                {
                    //to avoid moving the effector on the z-axis, we have to create a new Vector3 with the z-axis fixed on the original position
                    Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    _activeEffector.transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, _activeEffector.transform.position.z); 
                }
            }
            //set cursor to Resize sprite if it hits a target with the "Resize" tag
            else if (hit.collider.CompareTag("Resize"))
            {
                Cursor.SetCursor(_mouseResizeTexture, new Vector2(16f, 16f), CursorMode.Auto);
                if (Input.GetMouseButtonDown(0))
                {
                    _activeEffector = hit.collider.transform;
                }

                if(_activeEffector != null)
                {
                    float radius = Vector2.Distance(_activeEffector.position, hit.point);

                    radius = Mathf.Clamp(radius, _radiusLimit.x, _radiusLimit.y);

                    _activeEffector.GetComponent<CircleShape>().Radius = radius;
                    _activeEffector.GetComponent<AreaEffector2D>().forceMagnitude = radius * _forceRatio;
                }
            }
            //put an else just in case...
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 150f, Color.red);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        // If you release the Left Mouse Button, nothing is selected
        if( Input.GetMouseButtonUp(0))
        {
            _activeEffector = null;
        }
    }

    private Transform _activeEffector;
}
