using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DeathBook.Model;

public class PersonDetailsPanel : MonoBehaviour, IObserver
{
    public Image ProfilePicture;
    public Text Name;
    public GameObject FriendsPanel;
    public Button KillButton;
    public Button WatchButton;
    public Button XButton;

    private Person _model;

    public void SetModel(Person model)
    {
        if (_model != null)
        {
            _model.UnSubscribe(this);
        }

        _model = model;

        _model.Subscribe(this);

        UpdateInfo();
    }

    public void Notify()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        Name.text = _model.Name;
    }
}
