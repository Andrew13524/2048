using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public TileGridLayout TileGrid;
    public GameObject TileFill;

    private Tile[] tiles;

    private void Awake()
    {
        tiles = gameObject.GetComponentsInChildren<Tile>();
    }
    private void Start()
    {
        //StartCoroutine(InitialFill());
        SpawnTileFill(2);
    }

    public IEnumerator InitialFill()
    {
        while(TileGrid.TilesSpawned == false)
        {
            yield return new WaitForSeconds(0.25f);
        }
        SpawnTileFill(2);
    }
    public void SpawnTileFill(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            var x = Random.Range(0, 4);
            var y = Random.Range(0, 4);

            var fillCoord = new Coordinate(x, y);

            print(fillCoord);

            var fillParent = FindTile(fillCoord);

            Instantiate(TileFill, fillParent.gameObject.transform);
        }
    }
    private Tile FindTile(Coordinate coord)
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            var tile = tiles[i];
            if (tile.Coordinate.X == coord.X && tile.Coordinate.Y == coord.Y)
            {
                return tiles[i];
            }
        }
        throw new KeyNotFoundException();
    }
}
