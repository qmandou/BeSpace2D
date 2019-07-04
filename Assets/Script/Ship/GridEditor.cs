using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using UnityEngine.Tilemaps;

public enum LayerDepth
{
    Tile,
    Object,
    Debug,
    Count
}

public class GridEditor : MonoBehaviour
{
    #region Variable

    Grid m_grid = null;

    List<GameObject> m_layers;
    
    static public Color lineColor = Color.white;

    Vector3Int oldMouse;

    Ship m_ship = null;

    #endregion

    #region Accessor

    public Grid Grid
    {
        get
        {
            return m_grid;
        }
    }

    public Ship Ship
    {
        get
        {
            return m_ship;
        }
    }

    #endregion

    #region Constructor

    #endregion

    #region MonoBehaviour Methods

    // Start is called before the first frame update
    void Awake()
    {
        m_grid = GetComponent<Grid>();
        m_layers = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            m_layers.Add(transform.GetChild(i).gameObject);
        }
    }

    #endregion

    #region Public Methods

    public Tilemap GetTileMap(int _count)
    {
        if (_count >= 0 && _count <= m_layers.Count)
        {
            GameObject g = GetLayer(_count);

            if (g == null)
            {
                Debug.LogError("Layer gameobject is null");
                return null;
            }

            return g.GetComponent<Tilemap>();
        }
        else
        {
            Debug.LogError(_count + " must be between [ 0, " + (m_layers.Count - 1) + "]");
            return null;
        }
    }

    public TilemapRenderer GetTileMapRenderer(int _count)
    {
        if (_count >= 0 && _count <= m_layers.Count)
        {
            GameObject g = GetLayer(_count);

            if (g == null)
            {
                Debug.LogError("Layer gameobject is null");
                return null;
            }

            return g.GetComponent<TilemapRenderer>();
        }
        else
        {
            Debug.LogError(_count + " must be between [ 0, " + (m_layers.Count - 1) + "]");
            return null;
        }
    }

    public GameObject GetLayer(int _count)
    {
        if (_count >= 0 && _count <= m_layers.Count)
        {
            return m_layers[_count];
        }
        else
        {
            Debug.LogError(_count + " must be between [ 0, " + (m_layers.Count - 1) + "]");
            return null;
        }
    }

    public Vector3Int GridMousePosition()
    {
        return MapUtils.MousePosToGrid(m_grid);
    }

    public bool MouseIsMoving()
    {
        Vector3Int mouse = GridMousePosition();
        if (oldMouse != mouse)
        {
            oldMouse = mouse;
            return true;
        }
        else
            return false;
    }

    public void InitGridDisplay(ref Image _gridDisplay)
    {
        m_ship = GetComponent<Ship>();
        if (m_ship == null)
        {
            Debug.LogError("Ship of GridEditor is null");
            return;
        }

        Vector2Int mapSize = new Vector2Int(m_ship.Width * 32, m_ship.Height * 32);
        Texture2D texture = new Texture2D(mapSize.x, mapSize.y, TextureFormat.ARGB32, false);

        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                // Inverse Y line because y = 0 is set at right bot of screen
                if (i % 32 == 0 ||
                    j % 32 == 0 ||
                    i == 0 || j == 0 ||
                    i == mapSize.x - 1 ||
                    j == mapSize.y - 1)
                {
                    texture.SetPixel(i, j, lineColor);
                }
                else
                {
                    texture.SetPixel(i, j, new Color(0, 0, 0, 0));
                }
            }
        }

        texture.Apply();
        _gridDisplay.sprite = Sprite.Create(texture, new Rect(0, 0, mapSize.x, mapSize.y), new Vector2(mapSize.x / 2, mapSize.y / 2));
        _gridDisplay.sprite.name = "Grid";
        _gridDisplay.rectTransform.sizeDelta = new Vector2((float)mapSize.x / 100.0f, (float)mapSize.y / 100.0f);
    }

    #endregion

    #region Private Methods

    #endregion

}
