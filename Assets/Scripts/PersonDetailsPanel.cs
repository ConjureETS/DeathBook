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

    public Image UIFriendPicture;

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

        foreach (Transform picture in FriendsPanel.transform)
        {
            Destroy(picture.gameObject);
        }

        //FriendsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(90f, 21 * _model.FriendList.Count);

        RectTransform panelTrans = FriendsPanel.GetComponent<RectTransform>();

        panelTrans.anchorMin = new Vector2(0f, -0.3125f * _model.FriendList.Count);
        panelTrans.anchorMax = new Vector2(1f, 1f);
        panelTrans.offsetMin = Vector2.zero;
        panelTrans.offsetMax = Vector2.zero;

        float height = 1f / _model.FriendList.Count;

        for (int i = 0; i < _model.FriendList.Count; i++)
        {
            // Temporary, until the model changes
            Person friend = _model.FriendList[i].friend1 == _model ? _model.FriendList[i].friend2 : _model.FriendList[i].friend1;

            Image friendPicture = Instantiate(UIFriendPicture) as Image;

            friendPicture.sprite = friend.Picture;

            friendPicture.transform.SetParent(FriendsPanel.transform);
            friendPicture.rectTransform.anchorMin = new Vector2(0.022f, 1f - (height - 0.01f) * (i + 1) - i * 0.01f);
            friendPicture.rectTransform.anchorMax = new Vector2(0.26f, (1f - height * i));
            friendPicture.rectTransform.offsetMin = Vector2.zero;
            friendPicture.rectTransform.offsetMax = Vector2.zero;

            if (i == _model.FriendList.Count - 1)
            {
                Debug.Log(friendPicture.rectTransform.position);
            }
            else if (i == 0)
            {
                Debug.Log(friendPicture.rectTransform.position);
            }
        }


    }
}
