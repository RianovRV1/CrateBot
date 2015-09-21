using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;

    public float MaxSpeed = 8;

    internal float JumpMultiplier = 1;
    private float 
        _defaultSpeed = 8,
        _defaultJump = 1,
        _defaultSpeedAccelerationOnGround = 10f,
        _defaultSpeedAcceleratonInAir = 5f;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;
    public int MaxHealth = 100;
    public GameObject OuchEffect;
    
    public AudioClip PlayerHitSound;
    public AudioClip JumpSound;
    //public Animator Animator;
    public int Health { get; private set; }
    public bool IsDead { get; private set; }
    internal bool _pushing = false;
    public GameObject Deadbody;
    internal bool _died = false;
    
    public void Awake() //using start causes the level manager to have their start method to start first, causing a null in _controller when using RespawnAt
    {
        
        _controller = GetComponent<CharacterController2D>();
        _isFacingRight = transform.localScale.x > 0;
        Health = MaxHealth;

    }

    public void Update()
    {


       
        
        if(!IsDead)
            HandleInput();

        var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
        
        
        if (IsDead)
            _controller.SetHorizontalForce(0);
        else
            _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

        //Animator.SetBool("IsGrounded", _controller.State.IsGrounded);
        //Animator.SetBool("IsDead", IsDead);
        //Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
    }
    public void FinishLevel()
    {
        enabled = false;
        _controller.enabled = false;
        collider2D.enabled = false;
    }
    public void Kill()
    {
        if(Deadbody != null)
        {
            Instantiate(Deadbody, transform.position, transform.rotation);
        }
        
        if (OuchEffect != null)
            Instantiate(OuchEffect, transform.position, transform.rotation);
        if (PlayerHitSound != null)
            AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);
        _controller.HandleCollisions = false;
        collider2D.enabled = false;
        IsDead = true;
        //ResetStats();
        Health = 0;
        _died = true;
        _controller.ResetParameters();
        
        
    }

    public void ResetStats()
    {
        if (JumpMultiplier != _defaultJump || SpeedAccelerationInAir != _defaultSpeedAcceleratonInAir || MaxSpeed != _defaultSpeed || SpeedAccelerationOnGround != _defaultSpeedAccelerationOnGround)
        {
            JumpMultiplier = _defaultJump;
            SpeedAccelerationInAir = _defaultSpeedAcceleratonInAir;
            MaxSpeed = _defaultSpeed;
            SpeedAccelerationOnGround = _defaultSpeedAccelerationOnGround;
            //FloatingText.Show("Stats back to normal", "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
        }
        else
            return;
    }

    
    public void PowerUp(float jump, float groundspeed, float airspeed, float maxspeed, float duration)
    {
        
        JumpMultiplier = jump;
        SpeedAccelerationOnGround = groundspeed;
        _defaultSpeedAcceleratonInAir = airspeed;
        MaxSpeed = maxspeed;
        Invoke("ResetStats", duration);
    }

    public void RespawnAt(Transform spawnPoint)
    {
        if (!_isFacingRight)
            Flip();

        IsDead = false;
        collider2D.enabled = true;
        _controller.HandleCollisions = true;
        Health = MaxHealth;

        transform.position = spawnPoint.position;
    }



    
  

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _normalizedHorizontalSpeed = 1;
            if (!_isFacingRight)
                Flip();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _normalizedHorizontalSpeed = -1;
                if (_isFacingRight)
                    Flip();
        }

        else
        {
            _normalizedHorizontalSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            
            LevelManager.Instance.KillPlayer();
        }

        if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpSound != null && !_controller._swimming)
                AudioSource.PlayClipAtPoint(JumpSound, transform.position);
            _controller.Jump(JumpMultiplier);
        }
  
    }
    
    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }


}
