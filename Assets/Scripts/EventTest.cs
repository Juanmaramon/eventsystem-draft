using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// Public declaration of UnityEvent with arguments required
public class SomethingHappenedEvent : GameEvent
{
	private object data;

	public object Data
	{
		get{ return data; }
		set { data = value; }
	}

	public SomethingHappenedEvent(object data)
	{
		this.data = data;
	}
}

public class EventTest : MonoBehaviour {

    private UnityAction someListener;

    void Awake ()
    {
        //someListener = new UnityAction (SomeFunction);
    }

    void OnEnable ()
    {
		EventManager.StartListening<SomethingHappenedEvent> ("test", SomeFunction);
		EventManager.StartListening<SomethingHappenedEvent> ("Spawn", SomeOtherFunction);        
		EventManager.StartListening<SomethingHappenedEvent> ("Destroy", SomeThirdFunction);
		//EventManager.StartListening ("WithParams", SomeFunctionWithParameters);
    }

    void OnDisable ()
    {
		EventManager.StopListening<SomethingHappenedEvent> ("test", SomeFunction);
		EventManager.StopListening<SomethingHappenedEvent> ("Spawn", SomeOtherFunction);
		EventManager.StopListening<SomethingHappenedEvent> ("Destroy", SomeThirdFunction);
		//EventManager.StopListening ("WithParams", SomeFunctionWithParameters);
    }

	void SomeFunction (SomethingHappenedEvent e)
    {
		Debug.Log ("Some Function was called! " + e.Data + " " + e.Data.GetType());
    }
    
	void SomeOtherFunction (SomethingHappenedEvent e)
    {
		Debug.Log ("Some Other Function was called! " + e.Data + " " + e.Data.GetType());
    }
    
	void SomeThirdFunction (SomethingHappenedEvent e)
    {
		Xoxo instance = (Xoxo)e.Data;
		Debug.Log ("Some Third Function was called! " + instance.lame);
    }

	void SomeFunctionWithParameters (string name, int number)
	{
		Debug.Log ("Some fourth function with parameters " + name + " " + number);
	}
}