using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridEditor))]
public class Ship : MonoBehaviour
{
    #region Variable

    int m_maxWidth = 0;
    int m_maxHeight = 0;

    [SerializeField] ShipData m_shipData;

    GridEditor editor;

    #endregion

    #region Constructor

    public Ship(ShipData _data)
    {
        Init(_data);
    }

    #endregion

    #region Accessor

    public int Width
    {
        get
        {
            return m_maxWidth;
        }
    }

    public int Height
    {
        get
        {
            return m_maxHeight;
        }
    }

    #endregion

    #region Unity Methods

    void Start()
    {
        editor = GetComponent<GridEditor>();
        Init(m_shipData);
    }

    #endregion

    public void Init(ShipData _data)
    {
        m_maxWidth = _data.m_maxWidth;
        m_maxHeight = _data.m_maxHeight;
    }
}
