using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource _audio;

    [SerializeField] AudioClip _popSound;
    [SerializeField] AudioClip _blastSoundBig;
    [SerializeField] AudioClip _blastSoundSubtle;
    
    void Start() {
        _audio = GetComponent<AudioSource>();
    }

    void Update() {

    }

    private void PlayClipOnce(AudioClip clip) {
        _audio.PlayOneShot(clip);
    }
    public void PlayPopSound() {
        PlayClipOnce(_popSound);
    }
    public void PlayBlastSound(int id) {
        AudioClip clip = id == 1 ? _blastSoundBig : _blastSoundSubtle;
        PlayClipOnce(clip);
    }
    public void PlayBlastSound() {
        PlayClipOnce(_blastSoundBig);
    }
}
