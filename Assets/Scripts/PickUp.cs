using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class PickUp : MonoBehaviour, IPlayerRespawnListener
{
    public GameObject Effect;

    public AudioClip SoundFX;
    internal bool _isCollected; // internal declaration so only child can access it through code, invisible to unity inspector 
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (_isCollected)
            return;
        var player = other.GetComponent<Player>();
        if (player == null)
            return;
        
        if (SoundFX!= null)
            AudioSource.PlayClipAtPoint(SoundFX, transform.position);

        if(Effect != null)
            Instantiate(Effect, transform.position, transform.rotation);
        GiveToPlayer(player);

        
    }

    public virtual void GiveToPlayer(Player player)
    {
        //FloatingText.Show("a thing", "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50)); // whatever method you need to do to affect the player on pickup
        _isCollected = true;
        gameObject.SetActive(false);
    }

    public virtual void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        _isCollected = false;
        gameObject.SetActive(true);
    }
}

