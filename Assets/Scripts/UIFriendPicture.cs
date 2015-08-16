using UnityEngine;
using System.Collections;
using DeathBook.Model;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIFriendPicture : MonoBehaviour
{
    private Person _model;

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
        _picture = GetComponent<Image>();
    }

    public void OnClick()
    {
        _model.SelectNode();
    }
}
