using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Base class with event data
public abstract class GameEvent
{
}

public class EventManager : MonoBehaviour {

    #region Attributes

	// Generic event delegate
    public delegate void EventDelegate<T> (T e) where T : GameEvent;
	// Event delegate with data received
	private delegate void EventDelegate (GameEvent e);

	private Dictionary <string, EventDelegate> eventDictionary;
	private Dictionary <System.Delegate, EventDelegate> eventLookup;

    private static EventManager eventManager;

    #endregion

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError ("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init (); 
					DontDestroyOnLoad (eventManager);
				}
            }

            return eventManager;
        }
    }

    void Init ()
    {
        if (eventDictionary == null)
        {
			eventDictionary = new Dictionary<string, EventDelegate>();
			eventLookup = new Dictionary<System.Delegate, EventDelegate> ();
        }
    }

    #region Public methods

    public static void StartListening<T> (string eventName, EventDelegate<T> listener) where T : GameEvent
    {
		// Early-out if we've already registered this delegate
		if (instance.eventLookup.ContainsKey(listener))
			return;
		
		// Create a new non-generic delegate which calls our generic one.
		// This is the delegate we actually invoke.
		EventDelegate internalDelegate = (e) => listener((T)e);
		instance.eventLookup[listener] = internalDelegate;

		EventDelegate tempDelegate;
		if (instance.eventDictionary.TryGetValue(eventName, out tempDelegate))
		{
			instance.eventDictionary[eventName] = tempDelegate += internalDelegate; 
		}
		else
		{
			instance.eventDictionary[eventName] = internalDelegate;
		}
    }
		
	public static void StopListening<T> (string eventName, EventDelegate<T> listener) where T : GameEvent
    {
		// If there is no event manager exit
		if (eventManager == null) return;
        
        // Try to get listener in order to remove it
		EventDelegate internalDelegate;
		if (instance.eventLookup.TryGetValue (listener, out internalDelegate))
        {
			EventDelegate tempDelegate;
			if (instance.eventDictionary.TryGetValue(eventName, out tempDelegate))
			{
				tempDelegate -= internalDelegate;
				if (tempDelegate == null)
				{
					instance.eventDictionary.Remove(eventName);
				}
				else
				{
					instance.eventDictionary[eventName] = tempDelegate;
				}
			}

			instance.eventLookup.Remove (listener);
        }
    }

	public static void TriggerEvent (string eventName, GameEvent e)
    {
        // Try to get event to invoke it
		EventDelegate tempDelegate = null;
		if (instance.eventDictionary.TryGetValue (eventName, out tempDelegate))
        {
			tempDelegate.Invoke (e);
        }
    }

    #endregion
}