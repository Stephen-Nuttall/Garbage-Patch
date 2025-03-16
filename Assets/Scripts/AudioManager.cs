using UnityEngine;
using FMODUnity;
using FMOD.Studio;

/*!
\brief Responsible for storing and playing the game's music and sound effects using the FMOD package.
To trigger a sound effect from a script, use "AudioManager.instance.PlaySFX()" or any of the other functions here.

Documentation updated 2/2/2025
\author Stephen Nuttall (old version also authored by Nick Bottari and Alexander Art)
\todo Make pausing/unpausing the game pause/unpause all game sounds.
\todo Stop all sounds when exiting to main menu and play main menu theme.
*/
public class AudioManager : MonoBehaviour
{
    /// To make the object persistent, it needs a reference to itself.
    public static AudioManager instance;
    /// Reference to the music that is currently playing.
    EventReference currentMusicReference;
    // Instance of the music that is currently playing.
    EventInstance currentMusicInstance;

    /// \brief Make this object persistent and set reference to data manager.
    /// If this is the only AudioManager in the scene, don’t destroy it on reload. If there’s another AudioManager in the scene, destroy it.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// Basic function to play a given sound once.
    public void PlaySFX(EventReference sound)
    {
        if (!sound.IsNull)
            RuntimeManager.PlayOneShot(sound);
    }

    /// Plays a short sound once from a given location.
    public void PlayOneShot(EventReference sound, Vector2 worldPos)
    {
        if (!sound.IsNull)
            RuntimeManager.PlayOneShot(sound, worldPos);
    }

    /// Plays a sound (expected to be music) continously, stopping any track currently playing unless it's the same as the parameter.
    public void PlayMusic(EventReference musicRef)
    {
        if (musicRef.Guid != currentMusicReference.Guid)
        {
            currentMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentMusicReference = musicRef;
            currentMusicInstance = RuntimeManager.CreateInstance(musicRef);
            currentMusicInstance.start();
        }
    }
}
