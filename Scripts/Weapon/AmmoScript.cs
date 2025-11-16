using UnityEngine;
using UnityEngine.UI;

public class AmmoScript : MonoBehaviour
{
    [SerializeField]
    private Text ammoText;
    public void SetAmmo(int currentAmmo, int ammoLeft)
    {
        ammoText.text = + currentAmmo + " / " + ammoLeft;
    }
}

