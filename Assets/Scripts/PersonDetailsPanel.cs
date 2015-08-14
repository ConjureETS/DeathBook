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
        _model = model;
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
