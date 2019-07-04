using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using UnityEngine;


public class DataBase<T> where T : Data
{
    #region Variable

    Dictionary<string, T> m_database;

    #endregion

    #region Accessor

    #endregion

    #region Constructor

    public DataBase()
    {
        m_database = new Dictionary<string, T>();
    }

    #endregion

    #region Public Methods
        
    public T LoadData(string _id)
    {   
        if (m_database.ContainsKey(_id))
        {
            return m_database[_id];
        }
        else
        {
            return LoadFromDatabase(_id);
        }
    }

    public void SaveData(string _id, T _data)
    {
        m_database[_id] = _data;
        XmlUtils.Serialize(m_database[_id], Application.streamingAssetsPath + "/Save/" + _id + ".xml");
    }

    #endregion

    #region Private Methods

    T LoadFromDatabase(string _id)
    {
        //Debug.Log("Load " + _id + " at " + Application.streamingAssetsPath + "/Save/" + _id + ".xml");
        T data = XmlUtils.Deserialize<T>(Application.streamingAssetsPath + "/Save/" + _id + ".xml");
        m_database[_id] = data;
        return m_database[_id];
    }

    #endregion
}
    