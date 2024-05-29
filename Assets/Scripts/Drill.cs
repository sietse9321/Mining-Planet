using UnityEngine;
using UnityEngine.Tilemaps;

public class Drill : MonoBehaviour
{
    // How destroyed tiles should look.
    public TileBase destroyedTile;

    bool isDrilling = false;
    BoxCollider2D boxCollider;
    Tilemap tilemap;

    void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        MoveDrill();

        if (Input.GetMouseButton(0))
        {
            boxCollider.enabled = true;
            print("boxcollider enabled");
        }
        else
        {
            boxCollider.enabled = false;
        }
    }

    /// <summary>
    /// Handels the rotation for drill to look at mouse
    /// </summary>
    void MoveDrill()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 target = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        Vector3 dir = target - transform.position;
        dir.z = 0;
        dir.Normalize();
        // Angle of object to mouse pos
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Debug.DrawRay(transform.position, dir * 20f, Color.red);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;

        foreach (ContactPoint2D hit in collision.contacts)
        {
            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
            tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
        }
    }
}