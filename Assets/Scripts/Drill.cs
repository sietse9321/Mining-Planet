using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Drill : MonoBehaviour
{
    // How destroyed tiles should look.
    public TileBase destroyedTile;

    Tilemap tilemap;

    void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Drilling();
        }
    }

    void Drilling()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 target = new Vector3(mousePos.x, mousePos.y, transform.position.z); // Maintain the same z coordinate as the object

        Vector3 dir = target - transform.position;
        dir.Normalize();

        // Angle of object to mouse pos
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        float maxDistance = 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, maxDistance);
        Debug.DrawRay(transform.position, dir * maxDistance, Color.red);

        if (hit.collider != null)
        {
            Debug.Log("Hit collider: " + hit.collider.gameObject.name);
            Debug.Log("Hit point: " + hit.point);

            // Get the position of the hit point
            Vector3Int pos = tilemap.WorldToCell(hit.point);
            Debug.Log("Tilemap position: " + pos);

            // Check if the tilemap has a tile at this position
            if (tilemap.HasTile(pos))
            {
                Debug.Log("Tile exists at: " + pos);
                tilemap.SetTile(pos, destroyedTile);
                Debug.Log("Tile destroyed at: " + pos);
            }
            else
            {
                Debug.Log("No tile at: " + pos);
            }
        }
        else
        {
            Debug.Log("No collider hit.");
        }
    }
}








//// Mouse pos to tile pos
//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
//Vector3Int pos = tilemap.WorldToCell(hit.point);

//// Replace tile with destroyed tile
//tilemap.SetTile(pos, destroyedTile);
