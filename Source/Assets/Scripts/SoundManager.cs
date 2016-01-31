using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

  private AudioSource[] sfxSources = null;
	public AudioClip[] wordsDevil;
  public AudioClip[] wordsBabe;
  public AudioClip[] itensGet;
  public AudioClip[] itensFall;

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

	public float PlayDevilWordSound(string word){
		foreach(AudioClip ac in wordsDevil){
      if (ac.name.ToLower() == word.ToLower()){
//                Debug.Log("found the word: "+word);
        PlaySfx( ac );
        return ac.length;
      }
    }
    return 0;
	}


  public float PlayItemGetSound(string word){
//    Debug.Log("sound of word: "+word);
    word = word.Remove( word.IndexOf("(Clone)") );
    word = (word+"_get");

    foreach(AudioClip ac in itensGet){
      if (ac.name == word ){
//        Debug.Log("found the word: "+word );
        PlaySfx( ac );
        return ac.length;
      }
    }
    return 0;
  }


  public float PlayItemFallSound(string word){
//    Debug.Log("sound of word: "+word);
    word = word.Remove( word.IndexOf("(Clone)") );
    word = (word+"_fall");

    foreach(AudioClip ac in itensFall){
      if (ac.name == word ){
        //        Debug.Log("found the word: "+word);
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
