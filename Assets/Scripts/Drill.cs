using UnityEngine;
using UnityEngine.Tilemaps;

public class Drill : MonoBehaviour
{
    public TileBase destroyedTile;
    //public for (upgrades?)
    public float drillingTime = 1f;
    [SerializeField] Transform playerTransform;
    [SerializeField] LayerMask whatLayer;

    float drillTimer = 0;
    Vector3Int currentTile;

    Tilemap tilemap;

    void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
    }

    void Update()
    {
        SnapDrillToPlayer();
        MoveDrill();

        if (Input.GetMouseButton(0))
        {
            DrillTiles();
        }
        else
        {
            drillTimer = 0;
        }
    }

    /// <summary>
    /// Handles the rotation for drill to look at mouse
    /// </summary>
    void MoveDrill()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 target = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        Vector3 dir = target - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //debug ray
        Debug.DrawRay(transform.position, dir * 20f, Color.red);
    }

    void DrillTiles()
    {
        //shoots raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f, whatLayer);
        //if no collider return
        if (hit.collider == null)
        {
            currentTile = Vector3Int.zero;
            drillTimer = 0f;
            return;
        }
        Vector3Int tilePosition = tilemap.WorldToCell(hit.point + (Vector2)transform.right * 0.01f);
        if (tilePosition == currentTile)
        {
            if ((drillTimer += Time.deltaTime) >= drillingTime && tilemap.GetTile(tilePosition) != null)
            {
                tilemap.SetTile(tilePosition, destroyedTile);
                Debug.Log("Tile destroyed at: " + tilePosition);
                drillTimer = 0f;
            }
        }
        else
        {
            currentTile = tilePosition;
            drillTimer = 0f;
        }
    }
    void SnapDrillToPlayer()
    {
        transform.position = playerTransform.transform.position;
    }
}
