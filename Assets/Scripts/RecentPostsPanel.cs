using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DeathBook.Model;

public class RecentPostsPanel : MonoBehaviour 
{
    public Text Content;

    PostGenerator _postGenerator;

    private float _elapsedTime = 0f;

    void Start()
    {
        _postGenerator = new PostGenerator();
    }

	void Update ()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= 15f)
        {
            Status status = _postGenerator.generateStatus();

            Content.text = status.Text;

            _elapsedTime = 0f;
        }
	}
}
