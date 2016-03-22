using UnityEngine;
using System.Collections;

public class BigExplosion : MonoBehaviour
{
    void OnEnable()
    {
		EventManager.StartListening<BasicEvent>("explosion", MakeIt);
    }

    void OnDisable()
    {
		EventManager.StopListening<BasicEvent>("explosion", MakeIt);
    }

	void MakeIt(BasicEvent e)
    {
        Debug.Log("YIHAAAA!!!");
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
    }
}
