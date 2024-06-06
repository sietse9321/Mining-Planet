using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    public TileBase destroyedTile;
    //public for (upgrades?)
    public float drillingTime = 1f;
    [SerializeField] float drillTimer = 0;
    [SerializeField] Transform playerTransform;
    [SerializeField] LayerMask whatLayer;
    [SerializeField] Image chargeBar;
    [SerializeField] GameObject orePrefab;
    OreInfo oreInfo;

    Vector3Int currentTile;
    Vector3Int coloredTile;

    Tilemap tilemap;


    void SetTileColor()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f, whatLayer);
        Vector3Int tileToColor = tilemap.WorldToCell(hit.point + (Vector2)transform.right * 0.01f);
        if (hit.collider == null)
        {
            tilemap.SetColor(coloredTile, Color.white);
            return;
        }

        if (coloredTile != tileToColor)
        {
            tilemap.SetColor(coloredTile, Color.white);
            coloredTile = tileToColor;
        }
        else
        {
            tilemap.SetColor(tileToColor, Color.red);
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
                //Debug.Log("Tile destroyed at: " + tilePosition);
                drillTimer = 0f;
                GameObject ore = Instantiate(orePrefab);
                oreInfo = ore.gameObject.GetComponent<OreInfo>();
                ore.transform.position = hit.point + (Vector2)transform.right * 0.01f;
                OreGenerator();
            }
        }
        else
        {
            currentTile = tilePosition;
            drillTimer = 0f;
        }
    }
    void OreGenerator()
    {
        int percentage = Random.Range(1, 101);

        if (percentage > 0 && percentage <= 50)
        {
            oreInfo.AssignValues("Stone", 1, Color.white);
            Debug.Log($"<color={"#808080"}><b>[STONE]</b></color>");
        }
        else if (percentage > 50 && percentage <= 60)
        {
            oreInfo.AssignValues("Copper", 1, Color.red);
            Debug.Log($"<color={"#FFA500"}><b>[COPPER]</b></color>");
        }
        else if (percentage > 60 && percentage <= 85)
        {
            oreInfo.AssignValues("Iron", 1, Color.grey);
            Debug.Log($"<color={"#FFFFFF"}><b>[IRON]</b></color>");
        }
        else if (percentage > 85 && percentage <= 95)
        {
            oreInfo.AssignValues("Some random material", 1, Color.cyan);
            Debug.Log($"<color={"#00FFFF"}><b>[SOME RANDOM SHIT]</b></color>");
        }
        else if (percentage > 95 && percentage <= 100)
        {
            oreInfo.AssignValues("Titanium", 1, Color.yellow);
            
            Debug.Log($"<color={"#FFFFFF"}><b>[TITANIUM]</b></color>");
        }
        else
        {
            Debug.LogWarning("Big oopsie no lootie" + percentage);
        }
    }

    void SnapDrillToPlayer()
    {
        transform.position = playerTransform.transform.position;
    }
    void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
    }

    void Update()
    {
        SnapDrillToPlayer();
        MoveDrill();
        SetTileColor();
        chargeBar.fillAmount = drillTimer / drillingTime;

        if (Input.GetMouseButton(0))
        {
            DrillTiles();
        }
        else
        {
            drillTimer = 0;
        }
    }
}