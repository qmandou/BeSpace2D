using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static string ParseTypeName(string _typeName)
    {
        int tileBaseIndex = -1;
        for (int i = _typeName.Length - 1; i > 0; i--)
        {
            if (_typeName[i] == '.')
            {
                tileBaseIndex = i + 1;
                i = 0;
            }
        }

        if (tileBaseIndex == -1)
        {
            return _typeName;
        }
        else
        {
            return _typeName.Substring(tileBaseIndex, _typeName.Length - tileBaseIndex); //
        }
    }


    static public void SetActiveRecursiveTransform(Transform _parent)
    {
        for (int i = 0; i < _parent.childCount; i++)
        {
            Transform t = _parent.GetChild(i);
            t.gameObject.SetActive(true);
            SetActiveRecursiveTransform(t);
        }
    }
}
