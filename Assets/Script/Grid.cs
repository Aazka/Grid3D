using System;
using UnityEngine;

public class Grid<TGridGeneric> 
{
    private int gridWidth,gridHeight,gridCellSize;
    private TGridGeneric[,] gridArray;
    private Vector3 gridOriginePos;
    private TextMesh[,] debugArray;
    public Grid(int width,int height,int cellSize,Vector3 origin,Func<Grid<TGridGeneric>,int,int,int,TGridGeneric> createGrid)
    {
        this.gridHeight = height;
        this.gridWidth = width;
        this.gridCellSize = cellSize;
        this.gridOriginePos = origin;

        gridArray = new TGridGeneric[gridWidth, gridHeight];
        debugArray = new TextMesh[gridWidth, gridHeight];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                gridArray[x, z] = createGrid(this, x, z,0);
            }
        }

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugArray[x, y] = Helper.CreateWorldText(null, gridArray[x, y].ToString(), GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, Color.white);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100);
            }
            Debug.DrawLine(GetWorldPosition(0, gridHeight), GetWorldPosition(gridWidth, gridHeight), Color.white, 100);
            Debug.DrawLine(GetWorldPosition(gridWidth, 0), GetWorldPosition(gridWidth, gridHeight), Color.white, 100);
        }
    }
    public Vector3 GetWorldPosition(int x,int y)
    {
        Vector3 worldPoint = new Vector3(x,0, y) * gridCellSize + gridOriginePos;
        return worldPoint;
    }
    private void SetPosition(int selectedIndexX,int selectedIndexY, TGridGeneric value)
    {
        if(selectedIndexX>=0 && selectedIndexY>=0 && selectedIndexX<gridWidth && selectedIndexY<gridHeight)
        {
            gridArray[selectedIndexX, selectedIndexY] = value;
            debugArray[selectedIndexX, selectedIndexY].text = gridArray[selectedIndexX, selectedIndexY].ToString();
        }
    }
    public bool CheckIsItemCanDrop(int selectedIndexX, int selectedIndexY)
    {
        return (selectedIndexX >= 0 && selectedIndexY >= 0 && selectedIndexX < gridWidth && selectedIndexY < gridHeight);
    }
    public void GetXY(Vector3 worldPos,out int x,out int y)
    {
        x = Mathf.FloorToInt((worldPos- gridOriginePos).x / gridCellSize);
        y= Mathf.FloorToInt((worldPos- gridOriginePos).z / gridCellSize);
    }
    public void SetGridValue(Vector3 worldPosition, TGridGeneric value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetPosition(x, y, value);
    }
    private TGridGeneric GetValue(int selectedIndexX,int selectedIndexY)
    {
        if (selectedIndexX >= 0 && selectedIndexY >= 0 && selectedIndexX < gridWidth && selectedIndexY < gridHeight)
        {
            return gridArray[selectedIndexX, selectedIndexY];
        }
        return default(TGridGeneric);
    }
    public TGridGeneric ReadGridValue(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetValue(x, y);
    }
}
