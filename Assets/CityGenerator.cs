using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public Transform roadPrefab;
    public Transform buildingPrefab;
    public Transform playerCellHighlightPrefab;
    public Transform diamondPrefab;
    private const int WormLength = 6;
    private const int CellScale = 3;
    private readonly List<Worm> worms = new List<Worm>();
    private Transform highlightCell;

    private void Start()
    {
        PlayerController.OnDiamondCollected += OnRegenerateMap;
        RegenerateMap();
        RenderWorms();
    }

    private void Update()
    {
        // RenderHighlightedCell();
    }

    private void OnRegenerateMap(int _)
    {
        RegenerateMap();
        RenderWorms();
    }

    private static Vector2Int GetPlayerCell()
    {
        return WorldCenterPositionToCell(FindObjectOfType<PlayerController>().transform.position);
    }

    private void RegenerateMap()
    {
        worms.Clear();
        worms.Add(new Worm(GetPlayerCell(), Direction.Up, WormLength));
        worms.Add(new Worm(GetPlayerCell(), Direction.Left, WormLength));
        worms.Add(new Worm(GetPlayerCell(), Direction.Down, WormLength));
        worms.Add(new Worm(GetPlayerCell(), Direction.Right, WormLength));
    }

    private void RenderWorms()
    {
        var wormContainer = GameObject.Find("WormContainer").transform;
        for (var i = 0; i < wormContainer.childCount; ++i)
        {
            Destroy(wormContainer.GetChild(i).gameObject);
        }

        foreach (var worm in worms)
        {
            for (var i = 0; i < worm.Cells.Count; ++i)
            {
                var isLastCell = i == worm.Cells.Count - 1;
                if (isLastCell)
                {
                    var diamondGo = Instantiate(
                        diamondPrefab,
                        CellToWorldCenterPosition(worm.Cells[i]),
                        Quaternion.identity,
                        wormContainer
                    );
                    diamondGo.GetComponent<Diamond>().Init(200);
                }

                Instantiate(
                    roadPrefab,
                    CellToWorldCenterPosition(worm.Cells[i]),
                    Quaternion.identity,
                    wormContainer
                );
            }
        }

        var wormCells = worms.Aggregate(new List<Vector2Int>(),
            (wormCells, worm) => wormCells.Concat(worm.Cells).ToList());
        var playerCell = GetPlayerCell();
        for (var x = -10; x <= 10; ++x)
        {
            for (var y = -10; y <= 10; ++y)
            {
                var cell = new Vector2Int(playerCell.x + x, playerCell.y + y);
                if (!wormCells.Contains(cell))
                {
                    Instantiate(
                        buildingPrefab,
                        CellToWorldCenterPosition(cell),
                        Quaternion.identity,
                        wormContainer
                    );
                }
            }
        }
    }

    private void RenderHighlightedCell()
    {
        if (highlightCell)
        {
            Destroy(highlightCell.gameObject);
        }

        highlightCell = Instantiate(
            playerCellHighlightPrefab,
            CellToWorldCenterPosition(GetPlayerCell()),
            Quaternion.identity,
            transform
        );
    }

    private static Vector3 CellToWorldCenterPosition(Vector2Int cell)
    {
        return new Vector3(cell.x * CellScale, cell.y * CellScale, 0) + new Vector3(1.5f, 1.5f, 0);
    }

    private static Vector2Int WorldCenterPositionToCell(Vector3 worldCenterPosition)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldCenterPosition.x / CellScale),
            Mathf.FloorToInt(worldCenterPosition.y / CellScale)
        );
    }
}
