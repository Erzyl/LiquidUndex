using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

	[SerializeField]
    private AudioClip[] clips;
	[HideInInspector]
    public float delayBetweenClips = 1f;

    float timer;
	bool canPlay;
	AudioSource source;

	void Start () {
		source = GetComponent<AudioSource>();
		canPlay = true;
	}


	public void Play(){

        timer = timer < delayBetweenClips ? timer + Time.deltaTime : delayBetweenClips;
        canPlay = timer == delayBetweenClips ? true : false;

        if (!canPlay)
            return;

        timer = 0;

        int clipIndex = Random.Range(0, clips.Length);
		AudioClip clip = clips[clipIndex];
		source.PlayOneShot(clip);
	}
}

