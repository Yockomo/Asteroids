using UnityEngine;

[CreateAssetMenu(fileName = "AudioFiles", menuName = "ScriptableObjects/AudioFiles", order = 1)]
public class AudioFiles : ScriptableObject
{
    public AudioClip ShootingSound;
    public AudioClip SpeedUpSound;
    public AudioClip ExplosionSound;
}
