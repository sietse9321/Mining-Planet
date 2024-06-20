using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    public TileBase destroyedTile;
    public TileBase LampTile;
    //public for (upgrades?)
    public float drillingTime = 1f;
    [SerializeField] float drillTimer = 0;
    [SerializeField] Transform playerTransform;
    [SerializeField] LayerMask whatLayer;
    [SerializeField] Image chargeBar;
    [SerializeField] GameObject orePrefab;
    OreInfo oreInfo;

    [SerializeField] Inventory inventory;

    Vector3Int currentTile;
    Vector3Int coloredTile;

    Tilemap tileMap;

    Item.ItemType oreType;

    void SetTileColor()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f, whatLayer);
        Vector3Int tileToColor = tileMap.WorldToCell(hit.point + (Vector2)transform.right * 0.01f);
        if (hit.collider == null)
        {
            tileMap.SetColor(coloredTile, Color.white);
            return;
        }

        if (coloredTile != tileToColor)
        {
            tileMap.SetColor(coloredTile, Color.white);
            coloredTile = tileToColor;
        }
        else
        {
            tileMap.SetColor(tileToColor, Color.red);
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
        #if UNITY_EDITOR
        Debug.DrawRay(transform.position, dir * 20f, Color.red);
        #endif
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
        Vector3Int tilePosition = tileMap.WorldToCell(hit.point + (Vector2)transform.right * 0.01f);
        if (tilePosition == currentTile)
        {
            if ((drillTimer += Time.deltaTime) >= drillingTime && tileMap.GetTile(tilePosition) != null)
            {
                tileMap.SetTile(tilePosition, destroyedTile);
                Debug.Log("Tile destroyed at: " + tilePosition);
                drillTimer = 0f;
                //ItemWorld.SpawnItemWorld(hit.point + (Vector2)transform.right * 0.01f, new Item { itemType = oreType, amount = 1 });
                //GameObject ore = Instantiate(orePrefab);
                //oreInfo = ore.gameObject.GetComponent<OreInfo>();
                //ore.transform.position = hit.point + (Vector2)transform.right * 0.01f;
                OreGenerator();
                inventory.AddItem(new Item(oreType, 1));
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


        switch (percentage)
        {
            case > 0 and <= 50:
                oreType = Item.ItemType.Stone;
                Debug.Log($"<color={"#808080"}>[STONE]</color>");
                break;
            case > 50 and <= 60:
                oreType = Item.ItemType.Copper;
                Debug.Log($"<color={"#FFA500"}>[COPPER]</color>");
                break;
            case > 60 and <= 85:
                oreType = Item.ItemType.Iron;
                Debug.Log($"<color={"#FFFFFF"}>[IRON]</color>");
                break;
            case > 85 and <= 95:
                oreType = Item.ItemType.Malachite;
                Debug.Log($"<color={"#00FFFF"}>[Malachite]</color>");
                break;
            case > 95 and <= 100:
                oreType = Item.ItemType.Titanium;
                Debug.Log($"<color={"#FFFFFF"}>[TITANIUM]</color>");
                break;
            default:
                Debug.LogWarning("Big oopsie no lootie" + percentage);
                break;
        }
    }

    void SnapDrillToPlayer()
    {
        transform.position = playerTransform.transform.position;
    }
    void Start()
    {
        GameObject mineObject = GameObject.FindWithTag("Mine");
        tileMap = mineObject.GetComponent<Tilemap>();
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            print("Doing the placing");
            Vector3 playerPos = playerTransform.position;

            Vector3Int cellPos = tileMap.WorldToCell(playerPos);
            print("Cellpos is empty");
            tileMap.SetTile(cellPos, LampTile);
            
        }
    }
}