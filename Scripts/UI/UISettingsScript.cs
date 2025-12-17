using StarterAssets;
using UnityEngine;
using UnityEngine.Rendering;

public class UISettingsScript : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private AudioClip openSound;
    [SerializeField]
    private AudioClip closeSound;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField] 
    private FirstPersonController playerController;
    
    [SerializeField]
    private KeyCode toggleKey = KeyCode.Escape;

    void Start()
    {
        Cursor.visible = settingsMenu.activeSelf;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
            Cursor.lockState = settingsMenu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = settingsMenu.activeSelf;
            if (closeSound && audioSource && openSound)
            {
                audioSource.PlayOneShot(settingsMenu.activeSelf ? openSound : closeSound);
            }
            if (playerController)
            {
                playerController.enabled = !settingsMenu.activeSelf;
            }
        }
    }
    
}
