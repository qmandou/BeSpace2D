using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;



public class AssetManager : MonoBehaviour
{
    #region Variable

    static AssetManager m_singleton = null;

    AssetBase<TileBase> tileBaseAsset = null;
    AssetBase<TileBase> objectBaseAsset = null;

    #endregion

    #region Accessor

    static public AssetManager Instance
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
        if (m_singleton == null)
        {
            tileBaseAsset = new AssetBase<TileBase>("tile");
            objectBaseAsset = new AssetBase<TileBase>("object");
            m_singleton = this;
        }
        else
        {
            Debug.LogError("Only one Asset Manager");
            Destroy(gameObject);
        }
    }

    #endregion

    #region Public Methods

    public TileBase GetTile(string _tileID)
    {
        if (_tileID == null) return null;
        return tileBaseAsset.GetData(_tileID);
    }

    public TileBase GetObject(string _tileID)
    {
        if (_tileID == null) return null;
        return objectBaseAsset.GetData(_tileID);
    }

    public TileBase[] GetAllTile()
    {
        return tileBaseAsset.GetAllData();
    }

    public TileBase[] GetAllObject()
    {
        return objectBaseAsset.GetAllData();
    }

    public Ttile[] GetAllTypedTile<Ttile>() where Ttile : TileBase
    {
        return tileBaseAsset.GetAllData().Where(x => x is Ttile).Select(x => x as Ttile).ToArray();
    }

    public static void SetSpriteByType(TileBase _tileBase, out Sprite _sprite)
    {
        TileUtils.TileType tileType = TileUtils.ParseTileTypeName(_tileBase.GetType().ToString());

        switch (tileType)
        {
            case TileUtils.TileType.RuleTile:
                RuleTile ruteTile = (RuleTile)_tileBase;
                _sprite = ruteTile.m_DefaultSprite;
                break;

            case TileUtils.TileType.Tile:
                Tile tile = (Tile)_tileBase;
                _sprite = tile.sprite;
                break;

            default:
                Debug.LogError("Tile type is null " + tileType);
                _sprite = null;
                break;
        }
    }


    #endregion

    #region Private Methods

    #endregion

}