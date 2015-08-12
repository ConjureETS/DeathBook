using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class PersonTest : MonoBehaviour
{
    // Temporary, for test
    private List<FriendshipLink> _links;
    private bool _highlighted = false;

    void Awake()
    {
        _links = new List<FriendshipLink>();
    }

    public void AddLink(FriendshipLink link)
    {
        _links.Add(link);
    }

    void OnMouseOver()
    {
        Debug.Log("abc");
        if (!_highlighted)
        {
            _highlighted = true;

            foreach (FriendshipLink link in _links)
            {
                link.Highlight(true, 1f);
            }
        }
    }

    void OnMouseExit()
    {
        _highlighted = false;

        foreach (FriendshipLink link in _links)
        {
            link.Highlight(false, 1f);
        }
    }
}
