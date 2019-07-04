using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class UIEditorManager : MonoBehaviour
{
    public enum UIEvent
    {
        SetEraser
    }

    #region Variable
    [SerializeField] Image gridDisplay;
    [SerializeField] GameObject showGridButton;

    [SerializeField] GameObject[] headerLeftButtons = null;
    [SerializeField] GameObject[] headerRightButtons = null;
    [SerializeField] GameObject loadScrollViewContent = null;

    [SerializeField] GameObject ButtonPrefabs = null;
    List<GameObject> loadScrollViewList = null;

    [SerializeField] GameObject loadPanel = null;
    [SerializeField] GameObject savePanel = null;

    [SerializeField] InputField mapNameSaveInputField = null;
    [SerializeField] InputField mapNameLoadInputField = null;

    [SerializeField] bool playMode = false;
    
    [SerializeField] GameObject[] footerLeftButtons = null;
    [SerializeField] GameObject footerButtonPrefabs = null;
    [SerializeField] GameObject tileButtonScrollViewContent = null;
    [SerializeField] GameObject objectScrollViewContent = null;

    [SerializeField] Text textPosition = null;

    [SerializeField] Image selectedTileViewer = null;
    [SerializeField] Sprite eraser = null;

    [SerializeField] UIGameManager uiGameManager = null;

    static UIEditorManager instance;

    GridEditor CurrentShipGridEditor;
    #endregion

    #region Accessor

    #endregion

    #region Constructor

    #endregion

    #region MonoBehaviour Methods

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            loadScrollViewList = new List<GameObject>();
            InitScrollViewButton(LayerDepth.Tile);
            InitScrollViewButton(LayerDepth.Object);
            CurrentShipGridEditor = EditorManager.Instance.CurrentShipEditGridManager;
            CurrentShipGridEditor.InitGridDisplay(ref gridDisplay);
        }
        else
        {
            Debug.LogError("Only one UIEditorManager");
            Destroy(this);
        }
    }

    private void Update()
    {
        UpdateTextPosition();
    }

    public static void SendUIEvent(UIEvent _event)
    {
        switch(_event)
        {
            case UIEvent.SetEraser:
                instance.SetEraserOnTileViewer();
                break;
            default:
                break;
        }
    }

    #endregion

    #region Public Methods

        #region Header Left

    public void EditToPlayMode()
    {
        playMode = !playMode;
        Transform canvasTransform = GetComponentInChildren<Canvas>().transform;

        if (playMode)
        {
            for (int i = 0; i < canvasTransform.childCount; i++)
            {
                GameObject g = canvasTransform.GetChild(i).gameObject;
                if (g.tag != "MainBar")
                {
                    g.SetActive(false);
                }
            }

            uiGameManager.gameObject.SetActive(true);

            for (int i = 0; i < headerLeftButtons.Length - 1; i++)
            {
                headerLeftButtons[i].SetActive(false);
            }

            headerLeftButtons[headerLeftButtons.Length - 1].GetComponentInChildren<Text>().text = "Edit";
        }
        else
        {
            for (int i = 0; i < canvasTransform.childCount; i++)
            {
                canvasTransform.GetChild(i).gameObject.SetActive(true);
            }

            uiGameManager.gameObject.SetActive(false);

            for (int i = 0; i < headerLeftButtons.Length - 1; i++)
            {
                headerLeftButtons[i].SetActive(true);
            }

            headerLeftButtons[headerLeftButtons.Length - 1].GetComponentInChildren<Text>().text = "Play";
        }
    }

    /*
    public void SaveMap()
    {
        if (mapNameSaveInputField.text == null)
        {
            Debug.Log("Error :: input field text is null");
        }
        else if (DataManager.Instance.FileAlreadyExist(mapNameSaveInputField.text))
        {
            Debug.Log("File already exist To Do Choice Erase or Break");
            MapData data = EditorManager.Instance.mapData;
            data.UpdateMap(currentShipEditGrid);
            data.m_id = mapNameSaveInputField.text;
            DataManager.Instance.SaveMapData(data);
        }
        else
        {
            MapData data = EditorManager.Instance.mapData;
            data.m_id = mapNameSaveInputField.text;
            DataManager.Instance.SaveMapData(data);
        }
    }
    */

    /*
    public void LoadMap()
    {
        MapData data = DataManager.Instance.LoadMapData(mapNameLoadInputField.text);

        if (data != null)
        {
            EditorManager.Instance.mapData = data;
        }
        else
        {
            Debug.LogWarning("Data loaded is null");
        }
    }
    */


    #endregion

        #region Header Nav Panel

    public void NavPanelMode(int _mode)
    {
        switch (_mode)
        {
            case 1:
                if (loadPanel.activeSelf)
                {
                    loadPanel.SetActive(false);
                    savePanel.SetActive(false);
                }
                else
                {
                    RefreshLoadScrollViewContent();
                    loadPanel.SetActive(true);
                    savePanel.SetActive(false);
                }
                break;
            case 2:
                if (savePanel.activeSelf)
                {
                    loadPanel.SetActive(false);
                    savePanel.SetActive(false);
                }
                else
                {
                    loadPanel.SetActive(false);
                    savePanel.SetActive(true);
                }
                break;
        }
    }

    #endregion

        #region Header Right
    #endregion

        #region Footer Left

    public void SetEraserOnTileViewer()
    {
        selectedTileViewer.sprite = eraser;
    }

    #endregion

        #region Footer Left Nav Panel

    public void ShowFooterPanel(int _panelID)
    {
        for (int i = 0; i < footerLeftButtons.Length; i++)
        {
            if (i == _panelID)
            {
                footerLeftButtons[i].SetActive(!footerLeftButtons[i].activeSelf);
            }
            else
            {
                footerLeftButtons[i].SetActive(false);
            }
        }
    }

    #endregion

        #region Footer Right
    #endregion

        #region Footer Right Panel

    public void UpdateTextPosition()
    {
        if (textPosition)
        {
            Vector3Int gridMousePosition = CurrentShipGridEditor.GridMousePosition();
            textPosition.text = "X : " + gridMousePosition.x + " Y : " + gridMousePosition.y + "\nZ : " + gridMousePosition.z;
        }
    }

    #endregion

    // Set on/off gridDisplay
    public void ShowGrid()
    {
        gridDisplay.gameObject.SetActive(!gridDisplay.gameObject.activeSelf);

        if (gridDisplay.gameObject.activeSelf)
        {
            showGridButton.GetComponentInChildren<Text>().text = "Grid ON";
        }
        else
        {
            showGridButton.GetComponentInChildren<Text>().text = "Grid OFF";
        }
    }

    #endregion

    #region Private Methods

    void RefreshLoadScrollViewContent()
    {
        SeekMapFilesSavedInStreamingAsset();
    }

    /// <summary>
    /// Look the directory and make a button if correct file is founded
    /// </summary>
    void SeekMapFilesSavedInStreamingAsset()
    {
        string[] files = Directory.GetFiles(Application.streamingAssetsPath + "/Save/");
        ClearLoadScrollViewButton();

        foreach (string file in files)
        {
            MakeShipButton(file);
        }
    }

    /// <summary>
    /// if the path is a xml file 
    /// I create a button in the scroll view with callback on the setter of mapNameLoadInputField
    /// </summary>
    /// <param name="_path"></param>
    void MakeShipButton(string _path)
    {
        if (CheckPath(_path))
        {
            string name = CheckName(_path);
            GameObject newButton = Instantiate(ButtonPrefabs, loadScrollViewContent.transform);
            newButton.GetComponentInChildren<Text>().text = name;
            Button button = newButton.GetComponent<Button>();
            button.onClick.AddListener(delegate { SetLoadInputFieldName(name); } );

            loadScrollViewList.Add(newButton);
        }
    }

    /// <summary>
    /// Check if the path given a xml file
    /// </summary>
    /// <param name="_path"></param>
    /// <returns></returns>
    bool CheckPath(string _path)
    {
        return _path.Substring(_path.Length - 3, 3) == "xml";
    }

    /// <summary>
    /// Check name at the end of path
    /// </summary>
    /// <param name="_path"></param>
    /// <returns></returns>
    string CheckName(string _path)
    {
        for(int i = _path.Length - 1; i >= 0; i--)
        {
            if(_path[i] == '/')
            {
                string toReturn = _path.Substring(i + 1, _path.Length - (i + 1));
                return toReturn.Substring(0, toReturn.Length - 4);
            }
        }
        return null;
    }

    // Set field name of loaded map by user
    void SetLoadInputFieldName(string _name)
    {
        mapNameLoadInputField.text = _name;
    }

    void ClearLoadScrollViewButton()
    {
        foreach(GameObject g in loadScrollViewList)
        {
            Destroy(g);
        }

        loadScrollViewList.Clear();
    }

    /// <summary>
    /// Init all button with assetbundle tile
    /// /// </summary>
    void InitScrollViewButton(LayerDepth _layerDepth)
    {
        TileBase[] tilesBase = null;
        bool isFirst = true;

        switch (_layerDepth)
        {
            case LayerDepth.Tile:
                tilesBase = AssetManager.Instance.GetAllTile();
                break;
            case LayerDepth.Object:
                tilesBase = AssetManager.Instance.GetAllObject();
                break;
        }

        if(tilesBase == null)
        {
            Debug.LogAssertion("InitScrollViewButton :: tilesBase == null");
            return;
        }

        foreach (TileBase tilebase in tilesBase)
        {
            // Set GameObject
            GameObject gButton = GameObject.Instantiate(footerButtonPrefabs, SelectTileViewParent(_layerDepth));
            Button uiButton = gButton.GetComponent<Button>();
            gButton.transform.name = SelectTileViewName(_layerDepth) + tilebase.name;
            gButton.GetComponentInChildren<Text>().text = tilebase.name;

            // Get TileType
            TileUtils.TileType tileType = TileUtils.ParseTileTypeName(tilebase.GetType().ToString());

            // Set button callback :: warning layerdepth define by tiletype it can be source of bug in the future if more tiletype will add
            uiButton.onClick.AddListener(delegate { SetSelectTileName(tilebase.name, _layerDepth); });

            Sprite sprite = null;
            AssetManager.SetSpriteByType(tilebase, out sprite);

            if (sprite)
            {
                gButton.GetComponentInChildren<Image>().sprite = sprite;

                if (isFirst)
                {
                    selectedTileViewer.sprite = sprite;
                    isFirst = false;
                }
            }
            else
            {
                Debug.LogError(tilebase.name + " sprite is null");
            }
        }
    }

    /// <summary>
    /// Set the container of tile button with type condition
    /// </summary>
    /// <param name="_layerDepth"></param>
    /// <returns></returns>
    Transform SelectTileViewParent(LayerDepth _layerDepth)
    {
        switch (_layerDepth)
        {
            case LayerDepth.Tile:
                return tileButtonScrollViewContent.transform;
            case LayerDepth.Object:
                return objectScrollViewContent.transform;
            default:
                return null;
        }
    }

    /// <summary>
    /// Return the generic name with LayerDepth condition
    /// </summary>
    /// <param name="_layerDepth"></param>
    /// <returns></returns>
    string SelectTileViewName(LayerDepth _layerDepth)
    {
        switch (_layerDepth)
        {
            case LayerDepth.Tile:
                return "TileButton_";
            case LayerDepth.Object:
                return "ObjectButton_";
            default:
                return "UnkonwLayer";
        }
    }

    /// <summary>
    /// Set the selected tile since AssetManager with that name
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_layerDepth"></param>
    void SetSelectTileName(string _name, LayerDepth _layerDepth)
    {
        TileBase tileBase = null;
        switch (_layerDepth)
        {
            case LayerDepth.Tile:
                tileBase = AssetManager.Instance.GetTile(_name);
                break;
            case LayerDepth.Object:
                tileBase = AssetManager.Instance.GetObject(_name);
                break;
        }
        EditorManager.Instance.SetTileSelectedName(_name, _layerDepth);

        Sprite sprite = null;
        AssetManager.SetSpriteByType(tileBase, out sprite);

        if(sprite)
        {
            selectedTileViewer.sprite = sprite;
        }
        else
        {
            Debug.LogError("Selected " + name + " tile is null ");
        }
    }

    #endregion
}