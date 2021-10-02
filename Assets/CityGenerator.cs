using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public PlayerController player;
    public Transform roadPrefab;
    public Transform buildingPrefab;
    public Transform playerCellHighlightPrefab;
    public Transform wormContainer;
    private const int WormLenght = 6;
    private const int CellScale = 3;
    private readonly List<Worm> worms = new List<Worm>();
    private Transform highlightCell;

    private void Start()
    {
        RegenerateMap();
        RenderWorms();
    }

    private void Update()
    {
        RenderHighlightedCell();

        var isAtTheEndOfAWorm = worms.Any(worm => worm.Cells.Last() == GetPlayerCell());
        if (isAtTheEndOfAWorm)
        {
            RegenerateMap();
            RenderWorms();
        }
    }

    private Vector2Int GetPlayerCell()
    {
        return WorldCenterPositionToCell(player.transform.position);
    }

    private void RegenerateMap()
    {
        worms.Clear();

        worms.Add(new Worm(GetPlayerCell(), Direction.Up, WormLenght));
        worms.Add(new Worm(GetPlayerCell(), Direction.Left, WormLenght));
        worms.Add(new Worm(GetPlayerCell(), Direction.Down, WormLenght));
        worms.Add(new Worm(GetPlayerCell(), Direction.Right, WormLenght));
    }

    private void RenderWorms()
    {

        for (var i = 0; i< wormContainer.childCount; ++i)
        {
            Destroy(wormContainer.GetChild(i).gameObject);
        }

        foreach (var wormCell in worms.SelectMany(worm => worm.Cells))
        {
            Instantiate(
                roadPrefab,
                CellToWorldCenterPosition(wormCell),
                Quaternion.identity,
                wormContainer
            );
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
