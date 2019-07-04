using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Variable

    [SerializeField] public KeyCode cameraUp;
    [SerializeField] public KeyCode cameraDown;
    [SerializeField] public KeyCode cameraLeft;
    [SerializeField] public KeyCode cameraRight;

    static InputManager m_singleton = null;

    #endregion

    #region Accessor

    static public InputManager Instance
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
        
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    #endregion

}
