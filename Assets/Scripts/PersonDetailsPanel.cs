using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DeathBook.Model;
using DeathBook.Util;

public class PersonDetailsPanel : MonoBehaviour, IObserver
{
    public Image ProfilePicture;
    public Text Name;
    public GameObject FriendsPanel;
    public Button KillButton;
    public Button WatchButton;
    public Button XButton;
    public GameObject Container;

    public UIFriendPicture FriendPicture;

    private PersonNode _node;
    private Person _model;

    void Awake()
    {
        Container.SetActive(false);
    }

    public void SetNode(PersonNode node)
    {
        if (_model != null)
        {
            _model.UnSubscribe(this);
        }
        
        _node = node;
        _model = node.Model;

        _model.Subscribe(this);

        Container.SetActive(true);

        UpdateInfo();
    }

    public void Notify()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        Name.text = _model.Name;

        KillButton.gameObject.SetActive(_model.Alive);
        WatchButton.gameObject.SetActive(_model.Alive);

        foreach (Transform picture in FriendsPanel.transform)
        {
            Destroy(picture.gameObject);
        }

        ProfilePicture.sprite = _model.Picture;
        
        RectTransform panelTrans = FriendsPanel.GetComponent<RectTransform>();

        panelTrans.anchorMin = new Vector2(0f, -0.3125f * _model.FriendList.Count);
        panelTrans.anchorMax = new Vector2(1f, 1f);
        panelTrans.offsetMin = Vector2.zero;
        panelTrans.offsetMax = Vector2.zero;

        float height = 1f / _model.FriendList.Count;

        for (int i = 0; i < _model.FriendList.Count; i++)
        {
            Person friend = _model.FriendList[i].Friend;

            UIFriendPicture friendPicture = Instantiate(FriendPicture) as UIFriendPicture;

            friendPicture.Model = friend;

            Image picture = friendPicture.Picture;

            picture.sprite = friend.Picture;

            picture.transform.SetParent(FriendsPanel.transform);
            picture.rectTransform.anchorMin = new Vector2(0.022f, 1f - (height - 0.01f) * (i + 1) - i * 0.01f);
            picture.rectTransform.anchorMax = new Vector2(0.26f, (1f - height * i));
            picture.rectTransform.offsetMin = Vector2.zero;
            picture.rectTransform.offsetMax = Vector2.zero;
        }
    }

    public void Close()
    {
        Container.SetActive(false);
        _node.Select(false);
    }

    public void KillNode()
    {
        _node.Kill();
    }
}
