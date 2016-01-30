using UnityEngine;
using System.Collections;
/// <summary>
/// Plays an intro clip only once, and then loops another one. /// </summary>
public class PartialLoopPlayer : MonoBehaviour {
	
	public AudioSource introSource;
	public AudioSource loopSource;
	private bool started = false;
	
	void Start(){
		if( !started )
			StartPlaying();
	}
	
	public void SetMusic(AudioClip intro, AudioClip loop){
		introSource.Stop();
		loopSource.Stop();
		
		introSource.clip = intro;
		loopSource.clip = loop;
		
		StartPlaying();
	}
	
	public void StartPlaying(){
		if( introSource.clip!=null ){
			introSource.Play();
			if( introSource.clip!=null ){
				loopSource.PlayDelayed(introSource.clip.length);
			}
		}else if( loopSource.clip!=null ){
			loopSource.Play();
		}
		started = true;
	}
	
}
