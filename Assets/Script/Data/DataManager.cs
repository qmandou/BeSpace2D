using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Variable

    static DataManager m_singleton = null;

    //DataBase<MapData> m_mapDataBase = null;

    #endregion

    #region Accessor

    public static DataManager Instance
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
            //m_mapDataBase = new DataBase<MapData>();

            m_singleton = this;
        }
    }

    #endregion

    #region Public Methods

    //public MapData LoadMapData(string _mapDataName)
    //{
    //    if(FileAlreadyExist(_mapDataName))
    //    {
    //        //Debug.Log("file Loaded exist");
    //        return m_mapDataBase.LoadData(_mapDataName);
    //    }

    //    return null;
    //}

    //public void SaveMapData(string _mapDataName, int _width, int _height, int _layer, GridManager _manager)
    //{
    //    m_mapDataBase.SaveData(_mapDataName, new MapData(_mapDataName, _width, _height, _layer, _manager));
    //}

    //public void SaveMapData(MapData _data)
    //{
    //    m_mapDataBase.SaveData(_data.m_id, _data);
    //}

    public bool FileAlreadyExist(string _mapDataName)
    {
        return File.Exists(Application.streamingAssetsPath + "/Save/" + _mapDataName + ".xml");
    }

    #endregion

    #region Private Methods

    #endregion

}
