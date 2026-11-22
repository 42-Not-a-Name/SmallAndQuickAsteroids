using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource SoundDead;
    public AudioSource SoundClick;
    public AudioSource SoundExpload;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SoundClick.pitch = Random.Range(0.6f, 1.8f);
            SoundClick.Play();
        }
    }
}
