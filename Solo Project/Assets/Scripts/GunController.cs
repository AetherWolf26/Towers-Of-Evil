using UnityEngine;

public class GunController : MonoBehaviour
{
    public Animator gunAnimator; // Assign your gun's Animator in the Inspector
    public bool isGunEquipped = false; // Set this based on your game logic


    void Update()
    {
        if (isGunEquipped)
        {
            // Play equip animation or ensure in equip state if not firing
            // (You might have a separate logic for the equip animation itself)

            if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
            {
                gunAnimator.SetBool("IsFiring", true);
            }
            else if (Input.GetMouseButtonUp(0)) // Left mouse button released
            {
                gunAnimator.SetBool("IsFiring", false);
            }
        }
        else
        {
            // If the gun is not equipped, ensure firing animation is stopped
            gunAnimator.SetBool("IsFiring", false);
            // Optionally, set the animator to an idle state for unequipped weapon
        }
    }
}