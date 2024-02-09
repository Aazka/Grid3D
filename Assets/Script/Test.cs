using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject testItem;
    private Camera cam;
    private Grid<GridObject> grid;
    private GridObject gridObject;
    private void Start()
    {
        cam = Camera.main;
        grid = new Grid<GridObject>(4, 2,10,Vector3.zero,(Grid<GridObject>g,int x,int z,int v)=>new GridObject(g,x,z,v));
        gridObject = new GridObject(grid,4,2,0);
    }
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        var mousePos = Input.mousePosition;
    //        mousePos.z = 30;
    //        grid.SetGridValue(Helper.GetMouseWorldPosition(cam, mousePos), true);
    //    }
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        var mousePos = Input.mousePosition;
    //        mousePos.z = 30;
    //        Debug.Log(grid.ReadGridValue(Helper.GetMouseWorldPosition(cam, mousePos)));
    //    }
    //}
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            mousePos.z = -cam.transform.position.z;
            grid.GetXY(Helper.GetMouseWorldPositionIn_3D(cam, mousePos), out int x, out int y);
            if(grid.CheckIsItemCanDrop(x,y))
            {
                gridObject = grid.ReadGridValue(grid.GetWorldPosition(x, y));
                if(gridObject.value==0)
                {
                    Instantiate(testItem, grid.GetWorldPosition(x,y), Quaternion.identity);
                    gridObject.value = 1;
                    grid.SetGridValue(grid.GetWorldPosition(x, y), gridObject);
                }
            }
        }
    }
    public class GridObject
    {
        public Grid<GridObject> grid;
        public int x, z,value;
        public GridObject(Grid<GridObject> grid, int x, int z,int value)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
            this.value = value;
        }
        public override string ToString()
        {
            return x + "," + z + "," + value;
        }
    }
}
