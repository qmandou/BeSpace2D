using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AssetBundleManager : MonoBehaviour
{
    #region Variable

    static AssetBundleManager singleton = null;

    Dictionary<string, AssetBundle> m_BundleDico = null;

    #endregion

    #region Constructor

    #endregion

    #region Accessor

    public static AssetBundleManager Instance
    {
        get
        {
            return singleton;
        }
    }

    #endregion

    #region Unity Methods

    void Awake()
    {
        if (singleton == null)
        {
            m_BundleDico = new Dictionary<string, AssetBundle>();
            Directory.GetFiles(Application.streamingAssetsPath).ToList().ForEach(x => AddCorrectPath(x));
            singleton = this;
        }
    }

    #endregion

    #region Public Methods

    public AssetBundle GetBundle(string _bundleName)
    {
        if (m_BundleDico.ContainsKey(_bundleName))
        {
            return m_BundleDico[_bundleName];
        }

        Debug.LogWarning(_bundleName + " bundle key isn't contain");
        return null;
    }

    #endregion

    #region Private Methods

    void AddCorrectPath(string _path)
    {
        if (PathIsCorrect(_path))
        {
            string key = GetNameKey(_path);
            m_BundleDico.Add(key, AssetBundle.LoadFromFile(_path));
            //Debug.Log("Bundle found " + key);
        }
    }

    string GetNameKey(string _path)
    {
        int count = 0;
        for (int i = _path.Length - 1; i >= 0; i--)
        {
            if (_path[i] == '\\')
            {
                string key = _path.Substring(i + 1, count);
                key = key.Substring(0, key.Length - 7);
                return key;
            }
            count++;
        }

        Debug.LogError("Bundle hasn't name ???");
        return null;
    }

    bool PathIsCorrect(string _path)
    {
        return _path.Substring(_path.Length - 6, 6) == "bundle" ? true : false;
    }

    #endregion

}

public class AssetBase<T> where T : Object
{
    Dictionary<string, T> m_dico;
    AssetBundle _aBundle = null;
    bool isLoad = false;

    public AssetBase(string _bundleName)
    {
        m_dico = new Dictionary<string, T>();
        _aBundle = AssetBundleManager.Instance.GetBundle(_bundleName);
    }

    public T GetData(string _assetID)
    {
        if (!m_dico.ContainsKey(_assetID))
        {
            m_dico[_assetID] = _aBundle.LoadAsset<T>(_assetID);
        }

        return m_dico[_assetID];
    }

    public T[] GetAllData()
    {
        if (!isLoad)
        {
            T[] toReturn = _aBundle.LoadAllAssets<T>();

            foreach (T o in toReturn)
            {
                if (!m_dico.ContainsKey(o.name))
                {
                    m_dico[o.name] = o;
                }
            }

            isLoad = true;
            return toReturn;
        }
        else
        {
            return m_dico.Values.ToArray<T>();
        }
    }


}
