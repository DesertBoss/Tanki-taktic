public interface IInitiable<T1> {
	public void Init (T1 obj);
}

public interface IInitiable<T1, T2> {
	public void Init (T1 obj1, T2 obj2);
}

public interface IInitiable<T1, T2, T3> {
	public void Init (T1 obj1, T2 obj2, T3 obj3);
}

public interface IInitiable<T1, T2, T3, T4> {
	public void Init (T1 obj1, T2 obj2, T3 obj3, T4 obj4);
}