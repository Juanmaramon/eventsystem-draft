using UnityEngine;
using System.Collections;

public class Xoxo
{
	public string lame;

	public Xoxo (string lame)
	{
		this.lame = lame;
	}
}

public class EventTriggerTest : MonoBehaviour {


    void Update () {
        if (Input.GetKeyDown ("q"))
        {
			EventManager.TriggerEvent ("test", new SomethingHappenedEvent("String q"));
        }

        if (Input.GetKeyDown ("o"))
        {
			EventManager.TriggerEvent ("Spawn", new SomethingHappenedEvent(2));
        }

        if (Input.GetKeyDown ("p"))
        {
			EventManager.TriggerEvent ("Destroy", new SomethingHappenedEvent(new Xoxo("lolo")));
        }

        if (Input.GetKeyDown ("x"))
        {
//            EventManager.TriggerEvent ("Junk");
        }
    }
}