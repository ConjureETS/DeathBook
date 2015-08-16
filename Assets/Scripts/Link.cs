using UnityEngine;
using System.Collections;
using DeathBook.Util;
using DeathBook.Model;

[RequireComponent(typeof(LineRenderer))]
public class Link : MonoBehaviour, IObserver
{
	private float highlightAlpha = 0.8f;
	private float defaultAlpha = 0.5f;

	private Color color;

	private Color baseColor = new Color(0.3f, 0.7f, 1f);
	private Color inactiveColor = new Color(0.15f, 0.15f, 0.05f);

	private static float defaultScale = 0.03f;
	private float hightlightScale = 0.2f;

	private bool isHighlighted = false;
	
    [SerializeField]
    private Transform StartPoint;

    [SerializeField]
    private Transform EndPoint;

    [SerializeField]
    private LineRenderer BeamLine;
    //public ParticleSystem BeamParticles;

    private Transform StartObject;
    private Transform EndObject;

	private FriendshipLink model;
	public FriendshipLink Model
	{
		get { return model; }
		set
		{
			model = value;
			model.Subscribe(this);

			//Make it between 0.1 and 0.4
			GetColors();
			hightlightScale = Model.Importance * 0.3f + 0.1f;
			Highlight(false);
		}
	}

    private float LIFETIME_RATIO = 0.025f;

    private Renderer _renderer;

    void Awake()
    {
        // Set the importance (weight) of the link here
        BeamLine.SetWidth(0.2f, 0.2f);

        _renderer = BeamLine.GetComponent<Renderer>();

        _renderer.material = Instantiate(_renderer.material);

        //_defaultColor = _renderer.material.GetColor("_TintColor");

        //Activate(false);
    }

	public void Notify()
	{
		GetColors();
		UpdateBeam();
		if (Model.KillCount == 2)
			hightlightScale = 0.1f;
		//TODO SR
	}

    void Update()
    {
		UpdateVisualEffects();
    }

    public void Activate(bool state)
    {
        BeamLine.gameObject.SetActive(state);

        // We need to re-update after changing the state of the visuals since the transform of the targetted robot may have changed
        UpdateVisualEffects();
    }

    private void UpdateVisualEffects()
    {
        StartPoint.position = StartObject.position + new Vector3(0f, 0f, 0f);
        EndPoint.position = EndObject.position + new Vector3(0f, 0f, 0f);

        float angle = Vector3.Angle(EndPoint.position - StartPoint.position, transform.right);

        angle = EndPoint.position.y > StartPoint.position.y ? angle : -angle;

        float distance = Vector3.Magnitude(EndPoint.position - StartPoint.position);

        //BeamParticles.startLifetime = distance * LIFETIME_RATIO;

        StartPoint.eulerAngles = new Vector3(0f, 0f, angle);

        BeamLine.SetPosition(0, StartPoint.position);
        BeamLine.SetPosition(1, EndPoint.position);
    }

    public void AttachToObjects(GameObject origin, GameObject destination)
    {
        StartObject = origin.transform;
        EndObject = destination.transform;
    }

    public void Highlight(bool state)
    {
; isHighlighted = state;
		UpdateBeam();
    }

	private void GetColors()
	{
		if (Model.KillCount == 0)
			color = baseColor;
		else if (Model.KillCount == 2)
			color = inactiveColor;
		else
		color = new Color(1f, Mathf.Lerp(1, 0, Model.Awareness), 0f);
	}

	private void UpdateBeam()
	{
		float width = isHighlighted ? hightlightScale : defaultScale;
		BeamLine.SetWidth(width, width);

		color.a = isHighlighted ? highlightAlpha : defaultAlpha;
		
		_renderer.material.SetColor("_TintColor", color);
	}
}
