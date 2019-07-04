using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUtils
{
    static public Vector3Int MousePosToGrid(Grid _grid)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // get the collision point of the ray with the z = 0 plane
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);

        return _grid.WorldToCell(worldPoint);
    }

    static public int ConvertDim(int _row, int _col, int _width)
    {
        return _row + _col * _width;
    }

}

public class TileUtils
{
    public enum TileType
    {
        RuleTile,
        Tile,
        Count
    }

    public static TileType ParseTileTypeName(string _tileTypeName)
    {
        string tilebaseType = Utils.ParseTypeName(_tileTypeName);

        switch (tilebaseType)
        {
            case "RuleTile":
                return TileType.RuleTile;

            case "Tile":
                return TileType.Tile ;

            default:
                Debug.LogError("Tile type not found : " + tilebaseType);
                return TileType.Count;
        }
    }
}