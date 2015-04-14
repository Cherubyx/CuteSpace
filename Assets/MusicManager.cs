using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour {
    [System.Serializable]
    struct AudioInfo {
        public string name;
        public AudioClip clip;
        public bool loop;
        public bool playOnAwake;
        public float volume;
    }

    [SerializeField]
    private AudioInfo[] audioClips;

    private Dictionary<string, AudioSource> audioSources;

    private static MusicManager instance;

    public static MusicManager Instance {
        get {
            // If an instance has not been assigned,
            // find one and assign it
            if (instance == null) {
                instance = GameObject.FindObjectOfType<MusicManager>();
				if(instance != null){
					DontDestroyOnLoad(instance.gameObject);
				}                
            }
            return instance;
        }
    }

    protected void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            // If this is the case, then another instance exists
            // and we should destroy this one
            if (this != instance) Destroy(gameObject);
        }

        // Create an audio sources dictionary for quick access
        audioSources = new Dictionary<string, AudioSource>();
        for (int i = 0; i < audioClips.Length; i++) {
            audioSources[audioClips[i].name] = CreateAudioSource(audioClips[i]);
        }
    }

    private AudioSource CreateAudioSource(AudioInfo audioInfo) {
        GameObject o = new GameObject(audioInfo.name);

        // Make the gameobject our child
        o.transform.parent = transform;

        AudioSource a = o.AddComponent<AudioSource>();
        SetAudioSourceSettings(a, audioInfo);

        return a;
    }

    private void SetAudioSourceSettings(AudioSource audioSource, AudioInfo audioInfo) {
        audioSource.clip = audioInfo.clip;
        audioSource.loop = audioInfo.loop;
        audioSource.playOnAwake = audioInfo.playOnAwake;
        audioSource.volume = audioInfo.volume;
    }


    public void Play(string audioName) {
        GetAudioSource(audioName).Play();        
    }

    public void PlayDelayed(string audioName, float delay) {
        GetAudioSource(audioName).PlayDelayed(delay);
    }

    public void Stop(string audioName) {
        GetAudioSource(audioName).Stop();
    }

    public void Pause(string audioName) {
        GetAudioSource(audioName).Pause();
    }

    public void UnPause(string audioName) {
        GetAudioSource(audioName).UnPause();
    }

    public AudioSource GetAudioSource(string audioName) {
        AudioSource a;
        if (!audioSources.TryGetValue(audioName, out a)) Debug.LogError("Audio does not exist");

        return a;
    }
}
