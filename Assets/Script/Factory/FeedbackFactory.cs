using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FeedbackFactory : MonoBehaviour
{
    #region Variable
    [SerializeField] TileBase feedback_Valid;
    [SerializeField] TileBase feedback_Bad;
    [SerializeField] TileBase feedback_GoldSelect;
    [SerializeField] TileBase feedback_Select;

    static FeedbackFactory singleton = null;

    // Debug
    public Factory<TileBase> feedbackFactory;

    #endregion

    #region Accessor

    public static FeedbackFactory Instance
    {
        get
        {
            return singleton;
        }
    }


    #endregion

    #region Constructor

    #endregion

    #region MonoBehaviour Methods

    void Awake()
    {
        if (singleton == null)
        {
            feedbackFactory = new Factory<TileBase>();
            Prototype_InitAtHand();

            singleton = this;
        }
        else
        {
            Debug.LogError("Only one GridFeedbackManager, delete this on " + transform.name);
            Destroy(this);
        }
    }
    
    #endregion

    #region Public Methods

    void Prototype_InitAtHand()
    {
        feedbackFactory.AddObject("Valid", feedback_Valid);
        feedbackFactory.AddObject("Bad", feedback_Bad);
        feedbackFactory.AddObject("GoldSelect", feedback_GoldSelect);
        feedbackFactory.AddObject("Select", feedback_Select);
    }

    #endregion

    #region Private Methods

    #endregion
}
