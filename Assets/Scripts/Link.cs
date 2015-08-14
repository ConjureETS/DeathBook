using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Link : MonoBehaviour
{
    public Color HighlightedColor = new Color(1f, 1f, 1f, 0.5f);

    [SerializeField]
    private Transform StartPoint;

    [SerializeField]
    private Transform EndPoint;

    [SerializeField]
    private LineRenderer BeamLine;
    //public ParticleSystem BeamParticles;

    [SerializeField]
    private Transform StartObject;

    [SerializeField]
    private Transform EndObject;

    private float LIFETIME_RATIO = 0.025f;

    private Renderer _renderer;
    private Color _defaultColor;

    void Awake()
    {
        // Set the importance (weight) of the link here
        BeamLine.SetWidth(0.2f, 0.2f);

        _renderer = BeamLine.GetComponent<Renderer>();

        _renderer.material = Instantiate(_renderer.material);

        _defaultColor = _renderer.material.GetColor("_TintColor");

        //Activate(false);
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

    public void Highlight(bool state, float weight)
    {
        // For now, the weight does nothing but it should eventually influence the intensity and size of the link

        _renderer.material.SetColor("_TintColor", state ? HighlightedColor : _defaultColor);
    }
}
