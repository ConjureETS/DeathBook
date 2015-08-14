using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DeathBook.Model;
using DeathBook.Util;
using System;

[RequireComponent(typeof(Collider))]
public class PersonNode : MonoBehaviour, IObserver
{
    public Action<PersonNode> OnClicked;

    public Color SelectedColor = Color.blue;

    public Color StartColor = Color.green;
    public Color MiddleColor = Color.yellow;
    public Color EndColor = Color.red;

    public Renderer internQuad;
    public Renderer xQuad;

    private List<Link> _links;
    private bool _highlighted = false;
    private bool _selected = false;

    private Person _model;
    private Renderer _renderer;
    private Transform _transform;

    public Person Model
    {
        get { return _model; }
        set
        {
            _model = value;
            _model.Subscribe(this);
            UpdateInfo();
        }
    }

    void Awake()
    {
        _links = new List<Link>();
        _renderer = GetComponent<Renderer>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        // Find another way to do it if it lags to much
        _transform.LookAt(new Vector3(_transform.position.x, _transform.position.y, _transform.position.z + 1));
    }

    public void AddLink(Link link)
    {
        _links.Add(link);
    }

    public void Select(bool state)
    {
        _selected = state;
        UpdateLinks(state);

        if (state)
        {
            _renderer.material.color = SelectedColor;
        }
        else
        {
            UpdateInfo();
        }
    }

    private void UpdateLinks(bool state)
    {
        foreach (Link link in _links)
        {
            link.Highlight(state, 1f);
        }
    }

    public void Notify()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        //If dead -> set offline until all friends are aware, then add a big red X to profile pic
        if (_model.Alive)
        {
            xQuad.enabled = false;
            SetColors();
        }
        else
        {
            xQuad.enabled = true;
            gameObject.GetComponent<Renderer>().material.color = new Color32(50, 50, 50, 1);
            UpdateLinks(false);
        }
    }

    private void SetColors()
    {
        //set greyed out for offline
        if (_model.Online)
        {
            internQuad.material.color = new Color32(255, 255, 255, 1);
        }
        else
        {
            internQuad.material.color = new Color32(80, 80, 80, 1);
        }

        if (_model.AwarenessLevel < 0.5)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.Lerp(StartColor, MiddleColor, _model.AwarenessLevel * 2);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.Lerp(MiddleColor, EndColor, _model.AwarenessLevel * 2 - 1);
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
