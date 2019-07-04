using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class EditorManager : MonoBehaviour
{

    #region Variable

    static EditorManager m_singleton = null;

    string m_selectedTileID = "BlueIndoor";

    [SerializeField] LayerDepth m_currentLayerDepth = LayerDepth.Tile;
    LayerDepth m_maxLayerDepth = LayerDepth.Count;

    public GridEditor CurrentShipEditGridManager;
    public Ship currentShipEdit;

    #endregion

    #region Accessor

    public static EditorManager Instance
    {
        get
        {
            return m_singleton;
        }
    }

    #endregion

    #region Constructor

    #endregion

    #region MonoBehaviour Methods

    void Awake()
    {
        if(m_singleton == null)
        {
            m_singleton = this;
        }
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            TileBase tile = GetTileAtGridPostion((int)m_currentLayerDepth);

            if(tile != null)
            {
                Debug.Log(tile.name);
            }
        }

        if (Input.GetMouseButton(1))
        {
            switch (m_currentLayerDepth)
            {
                case LayerDepth.Tile:
                    SetTileAtGridPostion(AssetManager.Instance.GetTile(m_selectedTileID), (int)m_currentLayerDepth);
                    break;
                case LayerDepth.Object:
                    SetTileAtGridPostion(AssetManager.Instance.GetObject(m_selectedTileID), (int)m_currentLayerDepth);
                    break;
            }
        }
    }

    #endregion

    #region Public Methods

    public TileBase GetTileAtGridPostion(int _layerDepth, Vector2Int? _position = null)
    {
        Vector3Int gridPosition;

        if (_position == null)
        {
            gridPosition = MapUtils.MousePosToGrid(CurrentShipEditGridManager.Grid);
        }
        else
        {
            gridPosition = (Vector3Int)_position;
        }

        Tilemap tileMap = CurrentShipEditGridManager.GetTileMap(_layerDepth);
        return tileMap.GetTile(gridPosition);
    }

    public void SetTileAtGridPostion(TileBase _tile, int _layerDepth, Vector2Int? _position = null)
    {
        Vector3Int gridPosition;

        if (_position == null)
        {
            gridPosition = MapUtils.MousePosToGrid(CurrentShipEditGridManager.Grid);
        }
        else
        {
            gridPosition = (Vector3Int)_position;
        }

        if (IsInMap(gridPosition))
        {
            if(_tile)
            {
                Tilemap tileMap = CurrentShipEditGridManager.GetTileMap(_layerDepth);
                tileMap.SetTile(gridPosition, _tile);
            }
            else // if eraser
            {
                for(int i = 0; i < (int)m_maxLayerDepth; i++)
                {
                    Tilemap tileMap = CurrentShipEditGridManager.GetTileMap(i);
                    tileMap.SetTile(gridPosition, _tile);
                }
            }
        }
        else
        {
            Debug.Log("Out of map " + gridPosition);
        }
    }

    public void ClearLayer(int _layerDepth)
    {
        Tilemap tileMap = CurrentShipEditGridManager.GetTileMap(_layerDepth);
        for (int i = 0; i < currentShipEdit.Width; i++)
        {
            for (int j = 0; j < currentShipEdit.Height; j++)
            {
                tileMap.SetTile(new Vector3Int( j, i, _layerDepth), null);
            }
        }
    }

    public void SelectEraser()
    {
        m_selectedTileID = null;
        UIEditorManager.SendUIEvent(UIEditorManager.UIEvent.SetEraser);
    }

    public void SetTileSelectedName(string _selectedTileName, LayerDepth _layerDepth)
    {
        m_selectedTileID = _selectedTileName;
        m_currentLayerDepth = _layerDepth;
    }

    #endregion

    #region Private Methods

    bool IsInMap(Vector3Int position)
    {
        if (position.x >= 0 && position.x < currentShipEdit.Width &&
            position.y >= 0 && position.y < currentShipEdit.Height)
        {
            return true;
        }
        return false;
    }


    #endregion

}
