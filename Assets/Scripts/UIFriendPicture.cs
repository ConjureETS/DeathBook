using UnityEngine;
using System.Collections;
using DeathBook.Model;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIFriendPicture : MonoBehaviour
{
    private Person _model;
    private Level lvl;

    public Person Model
    {
        get { return _model; }
        set { _model = value; }
    }

    private Image _picture;

    public Image Picture
    {
        get { return _picture; }
    }

    void Awake()
    {
        lvl = LevelManager.Instance.GameLevel;
        _picture = GetComponent<Image>();
    }

    public void OnClick()
    {
        if (lvl.tutorialInt == 3)
            lvl.allowNext = true;
        _model.SelectNode();
        _model.SelectNode();
    }
}
