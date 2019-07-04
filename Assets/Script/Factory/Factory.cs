using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Factory<T> where T : Object
{

    #region Variable

    Dictionary<string, T> m_factory = null;

    #endregion

    #region Accessor

    #endregion

    #region Constructor

    public Factory()
    {
        m_factory = new Dictionary<string, T>();
    }

    #endregion

    #region Public Methods

    public T GetObject(string _key)
    {
        if(m_factory != null && m_factory.ContainsKey(_key))
        {
            return m_factory[_key];
        }
        else
        {
            Debug.LogError("That factory not contain " + _key);
            return null;
        }
    }

    public T AddObject(string _key, T _object)
    {
        if (m_factory != null && !m_factory.ContainsKey(_key))
        {
            return m_factory[_key] = _object;
        }
        else
        {
            Debug.LogError("That factory not contain " + _key);
            return null;
        }
    }

    public string[] GetKeys()
    {
        return m_factory.Keys.ToArray();
    }
    #endregion

    #region Private Methods

    #endregion
}
