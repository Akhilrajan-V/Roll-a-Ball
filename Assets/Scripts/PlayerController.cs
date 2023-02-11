using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce;
    public AudioSource audioSource;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject ground;

    private Rigidbody rb;
    private int count;
    private float jumpOffset;
    private float movementX;
    private float movementY;
    private float movementZ;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

        // Compute Jump Offset
        jumpOffset = (transform.position.y - ground.transform.position.y)+1;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        // movementZ = movementVector.x;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        } 
    }

    void Update()
    {
           if(transform.position.y - ground.transform.position.y<jumpOffset)
        {
            if(Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count +=1;
            SetCountText();
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
