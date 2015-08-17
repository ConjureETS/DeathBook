using DeathBook.Util;
using System.Collections.Generic;
using UnityEngine;

namespace DeathBook.Model
{
	public class PostGenerator
	{
        private string[] _posts;

        public PostGenerator()
        {
            TextAsset postsFile = Resources.Load("TextFiles/FacebookPosts") as TextAsset;

            _posts = postsFile.text.Split('\n');
        }

		public Status generateStatus(/*put stuff here*/)
		{
            if (LevelManager.Instance.GameLevel.NumDead == LevelManager.Instance.GameLevel.People.Count)
            {
                return new Status() { Text = "" };
            }

            Person person = null;

            // May be a bottleneck if unlucky, needs to be tested
            do
            {
                person = LevelManager.Instance.GameLevel.People[UnityEngine.Random.Range(0, LevelManager.Instance.GameLevel.People.Count)];
            } while (!person.Alive);

            Status status = new Status()
            {
                Text = "<b>" + person.Name + "</b>" + ": " + _posts[UnityEngine.Random.Range(0, _posts.Length)]
            };

			return status;
		}

		public Headline generateHeadline(/*put stuff here*/)
		{
			//and here...
			return null;
		}
	}
}
