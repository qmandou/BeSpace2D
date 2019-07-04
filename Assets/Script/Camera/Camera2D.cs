using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    #region Variable

    InputManager m_input;

    #endregion

    #region Accessor

    [SerializeField] float cameraSpeed = 100;

    #endregion

    #region Constructor

    #endregion

    #region MonoBehaviour Methods

    void Start()
    {
        m_input = InputManager.Instance;
    }

    void Update()
    {
        CameraMove();
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    void CameraMove()
    {
        if(Input.GetKey(m_input.cameraUp))
        {
            transform.Translate(transform.up * Time.deltaTime * cameraSpeed);
        }
        else if(Input.GetKey(m_input.cameraDown))
        {
            transform.Translate(-transform.up * Time.deltaTime * cameraSpeed);
        }

        if (Input.GetKey(m_input.cameraRight))
        {
            transform.Translate(transform.right * Time.deltaTime * cameraSpeed);
        }
        else if (Input.GetKey(m_input.cameraLeft))
        {
            transform.Translate(-transform.right * Time.deltaTime * cameraSpeed);
        }
    }

    #endregion

}
