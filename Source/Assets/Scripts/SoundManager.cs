using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	private AudioSource[] sfxSource;
	public AudioClip[] words;

	public AudioClip[] confirmSound;
	public AudioClip[] bigConfirmSound;
	public AudioClip[] cancelSound;
	public AudioClip[] cursorSound;
	public AudioClip[] forbiddenSound;
	public static SoundManager instance = null;

  public PartialLoopPlayer loopPlayer;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;


	void Awake () {
		if(instance == null)
			instance = this;
		else if(instance!=this)
			Destroy(gameObject);

    DontDestroyOnLoad(gameObject);
    sfxSource = GetComponents<AudioSource>();
	}

	public void PlaySfx(AudioClip clip){
		foreach( AudioSource audio in sfxSource ){
			if( !audio.isPlaying ){
				audio.pitch = 1f;
				audio.clip = clip;
				audio.Play();
				break;
			}
		}
	}
	
	public void PlayRandomPitchSfx(AudioClip clip){
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);
		
		foreach( AudioSource audio in sfxSource ){
			if( !audio.isPlaying ){
				audio.pitch = randomPitch;
				audio.clip = clip;
				audio.Play();
				break;
			}
		}
	}

	public void RandomizeSfx(AudioClip[] clips){
		int randomIndex = Random.Range(0, clips.Length);
		PlaySfx( clips[randomIndex] );
	}
	
	public void RandomPitchedSfx(AudioClip[] clips){
		int randomIndex = Random.Range(0, clips.Length);
		PlayRandomPitchSfx( clips[randomIndex] );
	}

	public float PlayWordSound(string word){
		foreach(AudioClip ac in words){
            if (ac.name.ToLower() == word.ToLower()){
                Debug.Log("found the word: "+word);
                PlaySfx( ac );
                return ac.length;
            }
        }
        return 0;
	}


	public void PlayConfirmSound(){
		SoundManager.instance.RandomizeSfx(confirmSound);
	}

	public void PlayBigConfirmSound(){
		SoundManager.instance.RandomizeSfx(bigConfirmSound);
	}
	
	public void PlayCancelSound(){
		SoundManager.instance.RandomizeSfx(cancelSound);
	}
	
	public void PlayCursorSound(){
		SoundManager.instance.RandomizeSfx(cursorSound);
	}
	
	public void PlayForbiddenSound(){
		SoundManager.instance.RandomizeSfx(forbiddenSound);
	}
	
}
