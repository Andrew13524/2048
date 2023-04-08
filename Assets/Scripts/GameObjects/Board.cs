using Assets.Scripts.GameObjects;
using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Assets.Scripts.Models.Constants;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public TileGridLayout TileGrid;
    public GameObject TileFill;
    public GameObject GameOverScreen;

    private Tile[,] tiles;

    private void Start()
    {
        tiles = GetTiles();
        StartCoroutine(InitialFill());
    }
    private void Update()
    {
        if (DirectionalKeysPressed())
        {
            if(GetFilledTiles().Count == TILE_COLUMNS * TILE_ROWS)
            {
                if (!AnyLegalMoves()) 
                {
                    SetAlpha(0.3f);
                    GameOverScreen.SetActive(true); 
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                ShiftTiles(Direction.UP);
                
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                ShiftTiles(Direction.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                ShiftTiles(Direction.RIGHT);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                ShiftTiles(Direction.LEFT);
            }
            SpawnTileFill(1);
        }
    }

    public IEnumerator InitialFill()
    {
        while(TileGrid.TileCoordsSet == false)
        {
            yield return new WaitForSeconds(0.5f);
        }
        SpawnTileFill(2);
    }
    public void SpawnTileFill(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            var availableTiles = GetAvailableTiles();

            if (availableTiles.Count == 0) return;

            var tile = availableTiles[Random.Range(0, availableTiles.Count)];

            Instantiate(TileFill, tile.transform);
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void ShiftTiles(Direction direction)
    {
        for (int i = 0; i < GRID_SIDE_LENGTH; i++)
        {
            var emptyTiles = new Queue<Tile>();
            Tile mergeableTile = null;

            for (int j = GRID_SIDE_LENGTH - 1; j >= 0; j--)
            {
                Tile currentTile;

                if (direction == Direction.RIGHT)     currentTile = tiles[j, i];
                else if (direction == Direction.DOWN) currentTile = tiles[i, j];
                else if (direction == Direction.UP)   currentTile = tiles[i, Math.Abs(j - (GRID_SIDE_LENGTH - 1))];
                else                                  currentTile = tiles[Math.Abs(j - (GRID_SIDE_LENGTH - 1)), i];

                if (currentTile.HasFill)
                {
                    if(mergeableTile != null)
                    {
                        var mergeableTileFill = mergeableTile.GetComponentInChildren<TileFill>();
                        var currentTileFill = currentTile.GetComponentInChildren<TileFill>();

                        if (mergeableTileFill.Level == currentTileFill.Level)
                        {
                            currentTileFill.transform.SetParent(mergeableTile.transform);
                            mergeableTileFill.Level += 1;
                            emptyTiles.Enqueue(currentTile);
                            mergeableTile = null;
                            continue;
                        }
                    }

                    if (emptyTiles.Count > 0)
                    {
                        var tileFill = currentTile.GetComponentInChildren<TileFill>();
                        tileFill.transform.SetParent(emptyTiles.Dequeue().transform);
                        emptyTiles.Enqueue(currentTile);
                        currentTile = tileFill.GetComponentInParent<Tile>();
                    }
                    mergeableTile = currentTile;
                    
                }
                else
                {
                    emptyTiles.Enqueue(currentTile);
                }
            }
        }
    }
        
    private Tile[,] GetTiles()
    {
        var tiles = new Tile[TILE_COLUMNS, TILE_ROWS];
        var children = gameObject.GetComponentsInChildren<Tile>();

        for (int i = 0; i < TILE_COLUMNS; i++)
        {
            for(int j = 0; j < TILE_ROWS; j++)
            {
                tiles[i, j] = children[(j * TILE_ROWS) + i];
            }
        }
        return tiles;
    }

    private List<Tile> GetFilledTiles()
    {
        var filledTiles = new List<Tile>();
        foreach(var tile in tiles)
        {
            if (tile.HasFill == true) filledTiles.Add(tile);
        }
        return filledTiles;
    }

    private List<Tile> GetAvailableTiles()
    {
        var availableTiles = new List<Tile>();
        foreach(var tile in tiles)
        {
            if (!tile.HasFill) availableTiles.Add(tile);
        }
        return availableTiles;
    }

    private bool AnyLegalMoves()
    {
        for(int i = 0; i < 2; i++)
        {
            for (int j = 0; j < tiles.GetLength(0); j++)
            {
                Tile lastTile = null;
                for (int k = 0; k < tiles.GetLength(1); k++)
                {
                    Tile currentTile;

                    if (i == 0) currentTile = tiles[j, k];
                    else currentTile = tiles[k, j];

                    if (k == 0)
                    {
                        lastTile = currentTile;
                        continue;
                    }

                    if (lastTile.GetComponentInChildren<TileFill>().Level == currentTile.GetComponentInChildren<TileFill>().Level)
                    {
                        return true;
                    }

                    lastTile = currentTile;
                }
            }
        }
        return false;
    }
    private bool DirectionalKeysPressed()
    {
        foreach(var key in DIRECTIONAL_KEYS)
        {
            if (Input.GetKeyDown(key)) return true;
        }
        return false;
    }
    private void SetAlpha(float alpha)
    {
        Image[] children = GetComponentsInChildren<Image>();
        foreach (var child in children)
        {
            Color newColor = child.color;
            newColor.a = alpha;
            child.color = newColor;
        }

        TextMeshProUGUI[] textChildren = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI textChild in textChildren)
        {
            Color newTextColor = textChild.color;
            newTextColor.a = alpha;
            textChild.color = newTextColor;
        }
    }
}
