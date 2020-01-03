using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource[] sfxSource;
	public AudioClip[] moveSound;
	public AudioClip[] femaleDieSound;
	public AudioClip[] maleAttackSound;
	public AudioClip[] maleHappySound;
	public AudioClip[] kissSound;
	public AudioClip[] lockSound;

	public AudioClip[] confirmSound;
	public AudioClip[] bigConfirmSound;
	public AudioClip[] cancelSound;
	public AudioClip[] cursorSound;
	public AudioClip[] forbiddenSound;
	public static SoundManager instance = null;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;


	void Awake () {
		if(instance == null)
			instance = this;
		else if(instance!=this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public void PlaySfx(AudioClip clip){
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);
		
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

	public void PlayMoveSound(){
		SoundManager.instance.RandomizeSfx(moveSound);
	}

	public void PlayFemaleDieSound(){
		SoundManager.instance.RandomPitchedSfx(femaleDieSound);
	}
	
	public void PlayMaleAttackSound(){
		SoundManager.instance.RandomizeSfx(maleAttackSound);
	}
	
	public void PlayCriticalKissSound(){
		SoundManager.instance.PlaySfx(kissSound[4]);
	}

	public void PlayLevelUpSound(){
		SoundManager.instance.RandomizeSfx(bigConfirmSound);
	}

	public void PlayKissSound(){
		SoundManager.instance.RandomizeSfx(kissSound);
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

	public void PlayLockSound(){
		SoundManager.instance.RandomizeSfx(lockSound);
	}
	
}
