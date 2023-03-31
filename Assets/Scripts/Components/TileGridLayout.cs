using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileGridLayout : LayoutGroup
{
    public int Padding;
    public float Spacing;
    public bool TilesSpawned;

    private Vector2 tileSize;
    private int rows;
    private int columns;

    protected override void Awake()
    {
        TilesSpawned = false;
        rows = 4;
        columns = 4;
    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float tileWidth = (parentWidth / columns) - (Spacing * (columns - 1)) / columns - (Padding / columns) * 2;
        float tileHeight = (parentHeight / rows) - (Spacing * (rows - 1)) / rows - (Padding / rows) * 2;

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

            rowCount = i / columns;
            columnCount = i % columns;

            var xPos = (tileSize.x * columnCount) + (Spacing * columnCount) + Padding;
            var yPos = (tileSize.y * rowCount) + (Spacing * rowCount) + Padding;

            tiles[i].Coordinate = new Coordinate(columnCount, rowCount);

            SetChildAlongAxis(tile, 0, xPos, tileSize.x);
            SetChildAlongAxis(tile, 1, yPos, tileSize.y);
        }

        TilesSpawned = true;
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
