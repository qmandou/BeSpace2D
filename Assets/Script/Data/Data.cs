using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data : object
{
    public string m_id;

    public Data()
    {
    }

    public Data(string _id)
    {
        m_id = _id;
    }
}
