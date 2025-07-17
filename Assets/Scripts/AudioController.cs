using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    public AudioSource music, ambianceSource, firePit, effect, footstepSource;
    public AudioClip foot1, foot2, foot3, foot4;
    public AudioClip ambiance, firePlace;
    public AudioClip win, loose, correct1, correct2, wrong1, wrong2;

    private AudioClip[] footClips;
    private float footstepDelay = 0.5f;
    private float footstepTimer = 0f;
    private bool isMoving = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        footClips = new AudioClip[] { foot1, foot2, foot3, foot4 };
        firePit.loop = true;
        firePit.clip = firePlace;
        firePit.Play();
    }

    private void Update()
    {
        HandleFootsteps();
    }

    // Ayak sesi kontrolü: dýþarýdan karakter hareketi bildirilecek
    public void SetMovementState(bool moving)
    {
        isMoving = moving;
    }

    private void HandleFootsteps()
    {
        if (!isMoving)
        {
            footstepSource.Stop();            
            return;
        }


        if(isMoving&& !footstepSource.isPlaying)
        {
            // AudioClip randomFoot = footClips[Random.Range(0, footClips.Length)];
            footstepSource.clip = foot1;//randomFoot;
            footstepSource.Play();
        }
         
    }

    // Tek seferlik efekt sesi oynatýcý (önceki sesi keser)
    public void PlayEffect(AudioClip clip)
    {
        if (clip == null) return;

        effect.Stop();
        effect.clip = clip;
        effect.Play();
    }
}
