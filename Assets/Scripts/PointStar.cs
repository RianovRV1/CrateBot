using System;
using System.Collections.Generic;

using UnityEngine;


public class PointStar : PickUp
{
   
    public int PointsToAdd = 10;
   
    public Animator Animator;
    public SpriteRenderer Renderer;
    public override void GiveToPlayer(Player player)
    {
        GameManager.Instance.AddPoints(PointsToAdd);
        
        //FloatingText.Show(string.Format("+{0}!", PointsToAdd), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));// can set some parameters to easily changes these in the inspector

        _isCollected = true;
        Renderer.enabled = false;
        Animator.SetTrigger("Collect");
    }


    public void FinishAnimationEvent()
    {
        Renderer.enabled = false;
        Animator.SetTrigger("Reset");
    }

    public override void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        if (!player._died)
        {
            _isCollected = false;
            Renderer.enabled = true;
        }
        else if(player._died)
        {
            _isCollected = true;
            Renderer.enabled = false;
        }
    }
    
}

