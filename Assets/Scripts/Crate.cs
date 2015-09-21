using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour 
{
    public bool _isPushed = false;
    public bool _grounded = false;
    public bool _crateTouchLeft = false;
    public bool _crateTouchRight = false;
    public Transform sidecolliderLeft, sidecolliderRight;
    public LayerMask sideCollisionCrate;
    public LayerMask sideCollisionPlayer;
    public bool _playerOnLeft = false;
    public bool _playerOnRight = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
       
        if (player == null ||  _crateTouchLeft || _crateTouchRight)
            return;
        
        _isPushed = true;
        if(_playerOnLeft)
            GetComponent<Rigidbody2D>().velocity = new Vector2(5, GetComponent<Rigidbody2D>().velocity.y);
        if(_playerOnRight)
            GetComponent<Rigidbody2D>().velocity = new Vector2(-5, GetComponent<Rigidbody2D>().velocity.y);
  


    }

    public void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(sidecolliderLeft.position, 0.3f);
            Gizmos.DrawSphere(sidecolliderRight.position, 0.3f);
        }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Debug.Log("A thing");
            _grounded = true;
        }

        if (collision.gameObject.tag == "Crate")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            _isPushed = false;
           
        }
    }
 
    public void Update()
    {
        _crateTouchLeft = Physics2D.OverlapPoint(sidecolliderLeft.position, sideCollisionCrate);
        _crateTouchRight = Physics2D.OverlapPoint(sidecolliderRight.position, sideCollisionCrate);
        _playerOnLeft = Physics2D.OverlapPoint(sidecolliderLeft.position - new Vector3(0.1f, 0.1f), sideCollisionPlayer);
        _playerOnRight = Physics2D.OverlapPoint(sidecolliderRight.position + new Vector3(0.1f, 0.1f), sideCollisionPlayer);
        if (_crateTouchLeft || _crateTouchRight)
            GetComponent<Rigidbody2D>().isKinematic = true;

        if (GetComponent<Rigidbody2D>().velocity.x != 0)
            _isPushed = true;
        else
            _isPushed = false;
        if (GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            _grounded = false;
        }
        else if (GetComponent<Rigidbody2D>().velocity.y == 0)
            _grounded = true;
        if (!_grounded)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10);

    }
  
}

