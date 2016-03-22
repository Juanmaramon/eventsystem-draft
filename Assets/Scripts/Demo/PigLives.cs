using UnityEngine;
using System.Collections;

public class PigLives : MonoBehaviour {

    [SerializeField]
    int lives;

    void OnEnable()
    {
        EventManager.StartListening<BasicEvent>("setDamage", Damaged);
    }

    void OnDisable()
    {
        EventManager.StopListening<BasicEvent>("setDamage", Damaged);
    }

    void Damaged(BasicEvent e)
    {
        Debug.Log("Setting damage of " + e.Data + " points");

        lives -= (int)e.Data;

        // Ouch...
        if (lives <= 0)
            Bacon();
    }

    void Bacon()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        // Send explosion event, no parameters required
        EventManager.TriggerEvent("explosion", null);
    }
}
