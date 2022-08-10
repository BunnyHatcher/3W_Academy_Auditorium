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
            
            //set cursor to Move sprite if it hits a target with the "Move" tag
            if(hit.collider.CompareTag("Move"))
            {
                Cursor.SetCursor(_mouseMoveTexture, new Vector2(16f, 16f), CursorMode.Auto);
            }
            //set cursor to Move sprite if it hits a target with the "Resize" tag
            else if (hit.collider.CompareTag("Resize"))
            {
                Cursor.SetCursor(_mouseResizeTexture, new Vector2(16f, 16f), CursorMode.Auto);
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
    }
}
