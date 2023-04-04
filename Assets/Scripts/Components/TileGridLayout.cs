using Assets.Scripts.Models;
using static Assets.Scripts.Models.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileGridLayout : LayoutGroup
{
    public int Padding;
    public float Spacing;
    public bool TileCoordsSet;

    private Vector2 tileSize;

    protected override void Awake()
    {
        TileCoordsSet = false;
    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float tileWidth = (parentWidth / TILE_COLUMNS) - (Spacing * (TILE_COLUMNS - 1)) / TILE_COLUMNS - (Padding / TILE_COLUMNS) * 2;
        float tileHeight = (parentHeight / TILE_ROWS) - (Spacing * (TILE_ROWS - 1)) / TILE_ROWS - (Padding / TILE_ROWS) * 2;

        tileSize.x = tileWidth;
        tileSize.y = tileHeight;

        AlignTiles();
    }

    public void AlignTiles()
    {
        int rowCount;
        int columnCount;
        var tiles = gameObject.GetComponentsInChildren<Tile>();
        for (int i = 0; i < rectChildren.Count; i++)
        {
            var tile = rectChildren[i];

            rowCount = i / TILE_COLUMNS;
            columnCount = i % TILE_COLUMNS;

            var xPos = (tileSize.x * columnCount) + (Spacing * columnCount) + Padding;
            var yPos = (tileSize.y * rowCount) + (Spacing * rowCount) + Padding;

            tiles[i].Coordinate = new Coordinate(columnCount, rowCount);

            SetChildAlongAxis(tile, 0, xPos, tileSize.x);
            SetChildAlongAxis(tile, 1, yPos, tileSize.y);
        }

        TileCoordsSet = true;
    }

    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {
        
    }

    public override void SetLayoutVertical()
    {
        
    }
}
