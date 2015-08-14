using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DeathBook.Model;
using System;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]
public class PersonTest : MonoBehaviour
{
    public Action<PersonTest> OnClicked;

    public Color NormalColor;
    public Color SelectedColor;

    private List<FriendshipLink> _links;
    private bool _highlighted = false;
    private bool _selected = false;

    private Person _model;
    private Renderer _renderer;

    public Person Model
    {
        set { _model = value; }
        get { return _model; }
    }

    void Awake()
    {
        _links = new List<FriendshipLink>();
        _renderer = GetComponent<Renderer>();
    }

    public void AddLink(FriendshipLink link)
    {
        _links.Add(link);
    }

    public void Select(bool state)
    {
        _selected = state;
        UpdateLinks(state);
        _renderer.material.color = state ? SelectedColor : NormalColor;
    }

    private void UpdateLinks(bool state)
    {
        foreach (FriendshipLink link in _links)
        {
            link.Highlight(state, 1f);
        }
    }

    void OnMouseEnter()
    {
        if (!_selected && !_highlighted)
        {
            UpdateLinks(true);
        }

        _highlighted = true;
    }

    void OnMouseExit()
    {
        if (!_selected)
        {
            UpdateLinks(false);
        }

        _highlighted = false;
    }

    void OnMouseDown()
    {
        // The sphere should be subscribed to this event and update the data accordingly
        if (OnClicked != null)
        {
            OnClicked(this);
        }

        Debug.Log("clicked");
    }
}
