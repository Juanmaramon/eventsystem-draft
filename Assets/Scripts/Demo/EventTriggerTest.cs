using UnityEngine;

public class EventTriggerTest : MonoBehaviour {
    void Update () {
        // Make it bacon
        if (Input.GetKeyDown ("q"))
        {
			EventManager.TriggerEvent ("setDamage", new BasicEvent(Random.Range(1, 5)));
        }
    }
}