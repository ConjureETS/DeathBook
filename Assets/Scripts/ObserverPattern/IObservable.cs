using System.Collections.Generic;

namespace DeathBook.Util
{
	// Using an abstract class to avoid repeating code, but could be implemented as an interface if inheritance is somehow needed for the subjects
	public abstract class Observable
	{
		private List<IObserver> observers;

		public Observable()
		{
			observers = new List<IObserver>();
		}

		public void Subscribe(IObserver observer)
		{
			observers.Add(observer);
		}

		public void UnSubscribe(IObserver observer)
		{
			observers.Remove(observer);
		}

		public void NotifyObservers()
		{
			foreach (IObserver observer in observers)
			{
				observer.Notify();
			}
		}
	}
}
