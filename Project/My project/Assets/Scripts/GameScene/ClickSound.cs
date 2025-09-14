using UnityEngine;

public class ClickSound : MonoBehaviour
{
    private AudioSource source;
    
    
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    public void PlayAudio()
    {
        
        source.Play();
    }
}
