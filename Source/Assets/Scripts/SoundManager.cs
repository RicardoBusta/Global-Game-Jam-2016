using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

  private AudioSource[] sfxSources = null;
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


  void Start () {
    if(instance == null){
      instance = this;
      DontDestroyOnLoad(gameObject);
    }else if(instance!=this){
			Destroy(gameObject);
    }
	}

  public static SoundManager GetInstance(){
    if(instance!=null){
      Debug.Log("had the reference");
      return instance;
    }else{
      Debug.Log("did not have the reference");
      instance = GameObject.FindWithTag("Sound Manager").GetComponent<SoundManager>();
      return instance;
    }
  }

	public void PlaySfx(AudioClip clip){
    if(sfxSources == null){
      sfxSources = GetComponents<AudioSource>();
    }

		foreach( AudioSource audio in sfxSources ){
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
		
		foreach( AudioSource audio in sfxSources ){
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
		RandomizeSfx(confirmSound);
	}

	public void PlayBigConfirmSound(){
		RandomizeSfx(bigConfirmSound);
	}
	
	public void PlayCancelSound(){
		RandomizeSfx(cancelSound);
	}
	
	public void PlayCursorSound(){
		RandomizeSfx(cursorSound);
	}
	
	public void PlayForbiddenSound(){
		RandomizeSfx(forbiddenSound);
	}
	
}
