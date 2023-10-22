using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Minesweeper.Constants;

public class Gameplay : Control
{
    [Export] public PackedScene CellScene;

    private bool _firstMove = true;
    private int _gridWidth = 9;
    private int _gridHeight = 9;
    private int _numberOfMines = 10;

    private Cell[,] _board;

    public override void _Ready()
    {
        var gridContainer = new GridContainer();

        gridContainer.Columns = _gridWidth;
        Vector2 textureSize = Vector2.Zero;

        var cells = new Cell[_gridWidth, _gridHeight];

        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                var cell = CellScene.Instance<Cell>();
                cell.GridPosition = new Vector2(x, y);
                cell.Connect("pressed", this, "OnCellPressed", new Godot.Collections.Array(cell));
                textureSize = cell.TextureNormal.GetSize();
                cells[x, y] = cell;
                gridContainer.AddChild(cell);
            }
        }

        _board = cells;

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

    private void InitBoard(Vector2 exclude)
    {
        int CountNeighboringMines(int x, int y, bool[,] knownMineLocations)
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

                if (knownMineLocations[cx, cy])
                {
                    mineCount++;
                }
            }

            return mineCount;
        }

        // pass 1: set mines
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

        var mineLocations = new bool[_gridWidth, _gridHeight];
        var uniqueMineList = uniqueMines.ToList();

        foreach (var mine in uniqueMineList)
        {
            mineLocations[(int)mine.x, (int)mine.y] = true;
        }

        // pass 2: set neighbors
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                if (mineLocations[x, y])
                {
                    _board[x, y].SetType(CellType.Mine);
                }
                else
                {
                    var count = CountNeighboringMines(x, y, mineLocations);

                    if (count == 0) { _board[x,y].SetType(CellType.Safe0); }
                    if (count == 1) { _board[x,y].SetType(CellType.Safe1); }
                    if (count == 2) { _board[x,y].SetType(CellType.Safe2); }
                    if (count == 3) { _board[x,y].SetType(CellType.Safe3); }
                    if (count == 4) { _board[x,y].SetType(CellType.Safe4); }
                    if (count == 5) { _board[x,y].SetType(CellType.Safe5); }
                    if (count == 6) { _board[x,y].SetType(CellType.Safe6); }
                    if (count == 7) { _board[x,y].SetType(CellType.Safe7); }
                    if (count == 8) { _board[x,y].SetType(CellType.Safe8); }
                }
            }
        }
    }

    private void OnCellPressed(Cell cell)
    {
        if (_firstMove)
        {
            InitBoard(cell.GridPosition);
            _firstMove = false;
        }

        var position = cell.GridPosition.ToIntVector();
        cell.Reveal();

        if (cell.Type == CellType.Safe0)
        {
            RevealNeighboringZeros(position.Item1, position.Item2);
        }
    }

    private void RevealNeighboringZeros(int x, int y)
    {

    }
}
