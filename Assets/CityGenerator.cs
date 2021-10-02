using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public PlayerController player;
    public Transform roadPrefab;
    public Transform buildingPrefab;
    private const int GridSize = 5;
    private const int CellScale = 3;
    private CityTile[] grid;

    private void Start()
    {
        GenerateBuildings();
        RenderCity();
    }

    private void Update()
    {
        // print("current player cell: " + GetPlayerCell());
    }

    private Vector2 GetPlayerCell()
    {
        var playerPosition = player.transform.position;
        return new Vector2(Mathf.Floor(playerPosition.x), Mathf.Floor(playerPosition.y));
    }

    private void GenerateBuildings()
    {
        grid = new CityTile[GridSize * GridSize];
        for (var i = 0; i < grid.Length; ++i)
        {
            grid[i] = Random.value > 0.9 ? CityTile.Building : CityTile.Road;
        }
    }

    private void RenderCity()
    {
        for (var x = 0; x < GridSize; ++x)
        {
            for (var y = 0; y < GridSize; ++y)
            {
                var cell = GetPlayerCell() + new Vector2(x * CellScale - 2, y * CellScale - 2);
                var prefab = grid[x * GridSize + y] == CityTile.Road ? roadPrefab : buildingPrefab;
                Instantiate(prefab, cell, Quaternion.identity, transform);
            }
        }
    }
}
