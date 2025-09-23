
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Vector3 cameraOffset = new Vector3(0, .5f, .2f);
    Vector2 cameraRotation = Vector2.zero;
    Transform playercam;
    InputAction lookAxis;
    Rigidbody rb;
    Ray jumpRay;
    Ray interactRay;
    RaycastHit interactHit;
    GameObject pickupObj;

    public PlayerInput input;
    public Transform weaponSlot;
    public Weapon currentWeapon;

    float inputX;
    float inputY;

    public float Xsensitivity = .1f;
    public float Ysensitivity = .1f;
    public float speed = 5f;
    public float jumpHeight = 2;
    public float jumpRayDistance = 1.1f;
    public float camRotationLimit = 90;
    public float interactDistance = 1f;

    public int health = 5;
    public int maxHealth = 5;

    public bool attacking = false;
    public object CameraRotation { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        interactRay = new Ray(transform.position, transform.forward);
        rb = GetComponent<Rigidbody>();
        playercam = GameObject.Find("Main Camera").transform;
        lookAxis = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        weaponSlot = transform.GetChild(0);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Camera Handler
        /* ...
        playercam.transform.position = transform.position + cameraOffset;

        cameraRotation.x += lookAxis.ReadValue<Vector2>().x * Xsensitivity;
        cameraRotation.y += lookAxis.ReadValue<Vector2>().y * Ysensitivity;

        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -camRotationLimit, camRotationLimit);

        playercam.transform.rotation = Quaternion.Euler(-cameraRotation.y, cameraRotation.x, 0);
        */
        Quaternion playerRotation = Quaternion.identity;
        playerRotation.y = playercam.transform.rotation.y;
        playerRotation.w = playercam.transform.rotation.w;
        transform.rotation = playerRotation;
        jumpRay.origin = transform.position;
        jumpRay.direction = -transform.up;

        interactRay.origin = playercam.transform.position;
        interactRay.direction = playercam.transform.forward;

        if (Physics.Raycast(interactRay, out interactHit, interactDistance))
        {
            if (interactHit.collider.tag == "weapon")
            {
                pickupObj = interactHit.collider.gameObject;
            }
        }
        else
            pickupObj = null;

        // Movement System
        Vector3 tempMove = rb.linearVelocity;

        tempMove.x = inputY * speed;
        tempMove.z = inputX * speed;

        rb.linearVelocity = (tempMove.x * transform.forward) + (tempMove.y * transform.up) + (tempMove.z * transform.right);
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (currentWeapon)
        {
            if ((currentWeapon.holdToAttack))
            {
                if (context.ReadValueAsButton())
                    attacking = true;
                else
                    attacking = false;
            }
            else if (context.ReadValueAsButton())
                currentWeapon.fire();
        }
    }
    public void Reload()
    {
        if (currentWeapon)
            currentWeapon.reload();
    }
    public void Interact()
    {
        if (pickupObj)
        {
            if (pickupObj.tag == "weapon")
                pickupObj.GetComponent<Weapon>().equip(this);
        }
        else
            Reload();
    }
    public void DropWeapon()
    {
        if (currentWeapon)
        {
            currentWeapon.GetComponent<Weapon>().unequip();
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 InputAxis = context.ReadValue<Vector2>();

        inputX = InputAxis.x;
        inputY = InputAxis.y;

    }
    public void Jump()
    {
        if (Physics.Raycast(jumpRay, jumpRayDistance))
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KillZone")
        {
            health = 0;
        }
        if ((other.tag == "1 Health") && (health < maxHealth))
        {
            health += 1;
            other.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            health--;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            health--;
        }
        if (collision.gameObject.tag == "1 Health")
        {
            health++;
        }
    }
}