using System.Collections.Generic;

namespace DeathBook.Model
{
	public class Status : Post
	{
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
	}
}