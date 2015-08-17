using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DeathBook.Model;
using DeathBook.Util;
using System;

[RequireComponent(typeof(Collider))]
public class PersonNode : MonoBehaviour, IObserver
{
	private const float UpdateFrequency = 0.5f;
	private float time = 0;

    public Action<PersonNode> OnClicked;

    public Color SelectedColor = Color.blue;

    public Color StartColor = Color.green;
    public Color MiddleColor = Color.yellow;
    public Color EndColor = Color.red;

    public Renderer internQuad;
    public float KillHoldDuration = 2f;
    public RatioProgression xMarkLeft;
    public RatioProgression xMarkRight;
	public Renderer bloodSplatter;
	public Renderer shoutBubble;

    private List<Link> _links;
    private bool _highlighted = false;
    private bool _selected = false;

    private Person _model;
    private Renderer _renderer;
    private Transform _transform;

    private float _holdDuration;

    public Person Model
    {
        get { return _model; }
        set
        {
            _model = value;
            _model.Subscribe(this);
            _model.OnSelected += () => { OnClicked(this); };
            UpdateInfo();
            SetProfilePicture();
        }
    }

    private void SetProfilePicture()
    {
        internQuad.material.mainTexture = _model.Picture.texture;
        internQuad.material.SetTexture("_MainTex", _model.Picture.texture);
    }

    void Awake()
    {
        _links = new List<Link>();
        _renderer = GetComponent<Renderer>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
		time += Time.deltaTime;
		if (time > UpdateFrequency)
		{
			_model.Update(time);
			time = 0;
		}

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

    private void UpdateLinks(bool isHighlighted)
    {
        foreach (Link link in _links)
        {
            link.Highlight(isHighlighted);
        }
    }

	public void Kill()
	{
        if (_model.Kill())
        {
            StartCoroutine(SplashBlood());
        }
	}

    private IEnumerator SplashBlood()
    {
        bloodSplatter.gameObject.SetActive(true);

        float ratio = 0f;

        Vector3 finalScale = Vector3.one * 1.7f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / 0.4f;

            bloodSplatter.transform.localScale = Vector3.Lerp(Vector3.zero, finalScale, ratio);

            yield return null;
        }

        ratio = 0f;

        Color initialColor = bloodSplatter.material.color;
        Color finalColor = initialColor;
        finalColor.a = 0f;

        // Fade out
        while (ratio < 1f)
        {
            ratio += Time.deltaTime / 1f;

            bloodSplatter.material.color = Color.Lerp(initialColor, finalColor, ratio);

            yield return null;
        }

        bloodSplatter.gameObject.SetActive(false);
    }

    public void Notify()
    {
		//Debug.Log("Received notification! " + Model.AwarenessLevel);
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        //If dead -> set offline until all friends are aware, then add a big red X to profile pic
        if (_model.Alive)
        {
			shoutBubble.gameObject.SetActive(_model.CurrentStatus != null);

            SetColors();
        }
        else
        {
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
        _holdDuration = 0f;

        // The sphere should be subscribed to this event and update the data accordingly
        if (OnClicked != null)
        {
            OnClicked(this);
        }
    }

    void OnMouseDrag()
    {
        if (!_model.Alive) return;

        _holdDuration += Time.deltaTime;
        
        xMarkLeft.SetCompletedRatio(Mathf.Clamp(_holdDuration - 0.025f, 0f, 1f));
        xMarkRight.SetCompletedRatio(Mathf.Clamp(_holdDuration - 1.025f, 0f, 1f));

        if (_holdDuration >= KillHoldDuration)
        {
            Kill();
        }
    }

    void OnMouseUp()
    {
        if (_model.Alive)
        {
            xMarkLeft.SetCompletedRatio(0f);
            xMarkRight.SetCompletedRatio(0f);
        }
    }
}
