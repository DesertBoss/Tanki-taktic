using System;
using System.Collections.Generic;

public struct Event<TData> {
	public TData data;
}

static public class EventDispatcher<EventType> {
	static public event Action<Event<EventType>> OnEvent;
	static public void Broadcast (EventType data) {
		OnEvent?.Invoke (new Event<EventType> { data = data });
	}
}

static public class EventDispatcherExt {
	static public void Broadcast<T> (this T data) where T : struct {
		EventDispatcher<T>.Broadcast (data);
	}
}


// -----------------------

public class Player {
	public struct OnPlayerDiedEvent {
		public Player player;
	}

	public Player () {
		EventDispatcher<OnPlayerDiedEvent>.OnEvent += OnSomePlayerDied;
	}

	void OnSomePlayerDied (Event<OnPlayerDiedEvent> obj) {
		
	}

	public void OnDie () {
		new OnPlayerDiedEvent () { player = this }.Broadcast ();
	}
}

// -------------------------

public static class ListExtensions {
	public static void RemoveUnorderedAt<T> (this List<T> list, int index) {
		list[index] = list[list.Count - 1];
		list.RemoveAt (list.Count - 1);
	}

	public static void RemoveUnordered<T> (this List<T> list, T obj) {
		int index = list.IndexOf (obj);
		list.RemoveUnorderedAt (index);
	}
}