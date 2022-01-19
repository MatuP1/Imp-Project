using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private float horizontalInput, forwardInput;
    public Animator animator;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        //move the vehicule forward
        transform.Translate(Vector2.up * Time.deltaTime * speed * forwardInput);
        //move the vehicule right and left
        transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput) + Mathf.Abs(forwardInput));
    }
}
