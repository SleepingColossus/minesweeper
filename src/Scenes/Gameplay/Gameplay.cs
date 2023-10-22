using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Godot.Collections;
using Minesweeper.Constants;

public class Gameplay : Control
{
    [Export] public PackedScene CellScene;

    private int _gridWidth = 9;
    private int _gridHeight = 9;
    private int _numberOfMines = 10;

    private bool[,] _mineLocations = null;

    public override void _Ready()
    {
        var gridContainer = new GridContainer();

        gridContainer.Columns = _gridWidth;
        Vector2 textureSize = Vector2.Zero;

        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                var cell = CellScene.Instance<Cell>();
                cell.GridPosition = new Vector2(x, y);
                cell.Connect("pressed", this, "OnCellPressed", new Godot.Collections.Array(cell));
                textureSize = cell.TextureNormal.GetSize();

                gridContainer.AddChild(cell);
            }
        }

        // center grid container
        gridContainer.AnchorLeft = 0.5f;
        gridContainer.AnchorRight = 0.5f;
        gridContainer.AnchorTop = 0.5f;
        gridContainer.AnchorBottom = 0.5f;

        var totalTextureSize = new Vector2(textureSize.x * _gridWidth, textureSize.y * _gridHeight);

        gridContainer.MarginLeft = -totalTextureSize.x / 2;
        gridContainer.MarginRight = totalTextureSize.x / 2;
        gridContainer.MarginTop = -totalTextureSize.y / 2;
        gridContainer.MarginBottom = totalTextureSize.y / 2;

        AddChild(gridContainer);
    }

    private void InitMines(Vector2 exclude)
    {
        var uniqueMines = new HashSet<Vector2>();
        var rng = new Random();

        while (uniqueMines.Count < _numberOfMines)
        {
            var x = rng.Next(0, _gridWidth);
            var y = rng.Next(0, _gridHeight);

            var mine = new Vector2(x, y);

            if (mine == exclude)
            {
                continue;
            }

            if (uniqueMines.Contains(mine))
            {
                continue;
            }

            uniqueMines.Add(mine);
        }

        _mineLocations = new bool[_gridWidth, _gridHeight];
        var uniqueMineList = uniqueMines.ToList();

        foreach (var mine in uniqueMineList)
        {
            _mineLocations[(int)mine.x, (int)mine.y] = true;
        }
    }

    private void OnCellPressed(Cell cell)
    {
        // initialize mines if first click
        if (_mineLocations == null)
        {
            InitMines(cell.GridPosition);
        }

        var position = cell.GridPosition.ToIntVector();

        bool isMine = _mineLocations[position.Item1, position.Item2];

        if (isMine)
        {
            cell.SetTexture(CellType.MineLose);
        }
        else
        {
            var cellType = CountNeighboringMines(position.Item1, position.Item2);
            cell.SetTexture(cellType);
        }
    }

    private CellType CountNeighboringMines(int x, int y)
    {
        var mineCount = 0;

        var coordsToCheck = new Tuple<int, int>[]
        {
            new Tuple<int, int>(x - 1, y - 1),  // top left
            new Tuple<int, int>(x    , y - 1),  // top
            new Tuple<int, int>(x + 1, y - 1),  // top right
            new Tuple<int, int>(x - 1, y),      // left
            new Tuple<int, int>(x + 1, y),      // right
            new Tuple<int, int>(x - 1, y + 1),  // bottom left
            new Tuple<int, int>(x    , y + 1),  // bottom
            new Tuple<int, int>(x + 1, y + 1),  // bottom right
        };

        foreach (var coordinates in coordsToCheck)
        {
            var cx = coordinates.Item1;
            var cy = coordinates.Item2;

            // skip if cells are out of bounds
            if (cx < 0 || cx >= _gridWidth) { continue; }
            if (cy < 0 || cy >= _gridHeight) { continue; }

            if (_mineLocations[cx, cy])
            {
                mineCount++;
            }
        }

        if (mineCount == 0) { return CellType.Safe0; }
        if (mineCount == 1) { return CellType.Safe1; }
        if (mineCount == 2) { return CellType.Safe2; }
        if (mineCount == 3) { return CellType.Safe3; }
        if (mineCount == 4) { return CellType.Safe4; }
        if (mineCount == 5) { return CellType.Safe5; }
        if (mineCount == 6) { return CellType.Safe6; }
        if (mineCount == 7) { return CellType.Safe7; }
        if (mineCount == 8) { return CellType.Safe8; }

        throw new InvalidEnumArgumentException();
    }
}
