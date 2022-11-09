using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionAnim;
    public ParticleSystem cepiritAnim;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce = 20;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver) {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound);

            var main = cepiritAnim.main;
            main.simulationSpeed = 0.8f;
            main.startSize = 1;
            main.gravityModifier = 30;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {   
        var main = cepiritAnim.main;
        if(collision.gameObject.CompareTag("Ground")) {
            isOnGround = true;
            main.simulationSpeed = 0.5f;
            main.startSize = 0.5f;
            main.gravityModifier = 15;
        } else if(collision.gameObject.CompareTag("Obstacle")) {
            isGameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            cepiritAnim.Stop();
            explosionAnim.Play();
            playerAudio.PlayOneShot(crashSound);
        }
    }
}
