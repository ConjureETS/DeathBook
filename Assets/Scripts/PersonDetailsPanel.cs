using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DeathBook.Model;
using DeathBook.Util;
using System.Collections.Generic;

public class PersonDetailsPanel : MonoBehaviour, IObserver
{
    public Image ProfilePicture;
    public Text Name;
    public Text FriendsTitle;
    public GameObject FriendsPanel;
    public Button KillButton;
    public Button WatchButton;
    public Button XButton;
    public GameObject Container;
    public RatioProgression AwarenessBar;
    public RatioProgression FriendAwarenessBar;

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

        AwarenessBar.SetCompletedRatio(_model.AwarenessLevel);

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

        // We copy the list so we can sort it without affecting the model data
        List<Friendship> list = new List<Friendship>(_model.FriendList);
        list.Sort();

        FriendsTitle.text = string.Concat("Friends (", list.Count, ")");

        for (int i = 0; i < list.Count; i++)
        {
            Person friend = list[i].Friend;

            float minY = 1f - (height - 0.01f) * (i + 1) - i * 0.01f;
            float maxY = 1f - height * i;

            // Friend picture
            UIFriendPicture friendPicture = Instantiate(FriendPicture) as UIFriendPicture;
            Image picture = friendPicture.Picture;

            friendPicture.Model = friend;
            picture.sprite = friend.Picture;
            picture.transform.SetParent(FriendsPanel.transform);
            picture.rectTransform.anchorMin = new Vector2(0.022f, minY);
            picture.rectTransform.anchorMax = new Vector2(0.26f, maxY);
            picture.rectTransform.offsetMin = Vector2.zero;
            picture.rectTransform.offsetMax = Vector2.zero;

            // Awareness bar
            RatioProgression awarenessBar = Instantiate(FriendAwarenessBar) as RatioProgression;
            RectTransform barRectTrans = awarenessBar.GetComponent<RectTransform>();

            awarenessBar.SetCompletedRatio(friend.AwarenessLevel);

            awarenessBar.transform.SetParent(FriendsPanel.transform);
            barRectTrans.anchorMin = new Vector2(0.28f, minY);
            barRectTrans.anchorMax = new Vector2(1f, maxY);
            barRectTrans.offsetMin = Vector2.zero;
            barRectTrans.offsetMax = Vector2.zero;
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
