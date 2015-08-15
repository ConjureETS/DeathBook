using UnityEngine;
using System.Collections;
using DeathBook.Util;
using DeathBook.Model;

[RequireComponent(typeof(LineRenderer))]
public class Link : MonoBehaviour, IObserver
{
	private float highlightAlpha = 0.8f;
	private float defaultAlpha = 0.5f;

	private Color currentDefaultColor;
	private Color currentHighlightColor;

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
			GetColors(Model.Awareness);
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
		GetColors(Model.Awareness);
		UpdateBeam();
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

	private void GetColors(float level)
	{
		//If level is 0.0, green    [0,1,0].
		//If level is 0.5, yellow [1,1,0].
		//If level is 1.0, red      [1,0,0].

		float r = 1f;
		float g = 1f;

		if (level < 0.5f)
			r = Mathf.Lerp(0, 1, level*2);
		else
			g = Mathf.Lerp(1, 0, level * 2 - 1);

		currentDefaultColor = new Color(r, g, 0f, defaultAlpha);
		currentHighlightColor = new Color(r, g, 0f, highlightAlpha);
	}

	private void UpdateBeam()
	{
		float width = isHighlighted ? hightlightScale : defaultScale;
		BeamLine.SetWidth(width, width);
		
		_renderer.material.SetColor("_TintColor", isHighlighted ? currentHighlightColor : currentDefaultColor);
	}
}
