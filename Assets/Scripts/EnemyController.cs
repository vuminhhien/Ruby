using System.Threading;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vartical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;

    bool broken = true;

    Rigidbody2D rigidbody2D;
    Animator animator;
    float timer;
    int direction = 1;
    AudioSource audioSource;
    public AudioClip clipPlayerHit;
    public AudioClip clipRobotFix;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

         if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        UnityEngine.Vector2 position = rigidbody2D.position;

        if(vartical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else{
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        rigidbody2D.MovePosition(position); 
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
            audioSource.PlayOneShot(clipPlayerHit);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        audioSource.Stop();
        audioSource.PlayOneShot(clipRobotFix);
    }
}
