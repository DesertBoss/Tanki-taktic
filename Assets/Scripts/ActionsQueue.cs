using System.Collections.Generic;
using System;
using System.Linq;

public class ActionsQueue
{
	private Queue<Action> _actionsQuaue;
	private object _locker;

	public ActionsQueue ()
	{
		_actionsQuaue = new Queue<Action> ();
		_locker = new object ();
	}

	public void AddAction (Action action)
	{
		lock (_locker)
			_actionsQuaue.Enqueue (action);
	}

	public void ExecuteSingle ()
	{
		if (_actionsQuaue.Count == 0)
			return;

		lock (_locker)
			_actionsQuaue.Dequeue ().Invoke ();
	}

	public void ExecuteAll ()
	{
		if (_actionsQuaue.Count == 0)
			return;

		while (_actionsQuaue.Count > 0)
		{
			lock (_locker)
				_actionsQuaue.Dequeue ().Invoke ();
		}
	}

	public void Destroy ()
	{
		lock (_locker)
		{
			_actionsQuaue.Clear ();
		}

		_actionsQuaue = null;
		_locker = null;
	}
}

public class ActionsQueue<T>
{
	private Queue<Tuple<Action<T>, T>> _actionsQuaue;
	private object _locker;

	public ActionsQueue ()
	{
		_actionsQuaue = new Queue<Tuple<Action<T>, T>> ();
		_locker = new object ();
	}

	public void AddAction (Action<T> action, T arg)
	{
		lock (_locker)
			_actionsQuaue.Enqueue (new Tuple<Action<T>, T> (action, arg));
	}

	public void ExecuteSingle ()
	{
		if (_actionsQuaue.Count == 0)
			return;

		lock (_locker)
		{
			var turple = _actionsQuaue.Dequeue ();
			turple.Item1.Invoke (turple.Item2);
		}
	}

	public void ExecuteAll ()
	{
		if (_actionsQuaue.Count == 0)
			return;

		while (_actionsQuaue.Count > 0)
		{
			lock (_locker)
			{
				var turple = _actionsQuaue.Dequeue ();
				turple.Item1.Invoke (turple.Item2);
			}
		}
	}

	public void Destroy ()
	{
		lock (_locker)
		{
			_actionsQuaue.Clear ();
		}

		_actionsQuaue = null;
		_locker = null;
	}
}