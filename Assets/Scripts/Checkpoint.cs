﻿using System;
using System.Collections.Generic;   
using UnityEngine;
using System.Collections;


public class Checkpoint : MonoBehaviour
{
    private List<IPlayerRespawnListener> _listeners;
    
    public void Awake()
    {
        _listeners = new List<IPlayerRespawnListener>();
    }

    public void PlayerHitCheckpoint()
    {
        StartCoroutine(PlayerHitCheckpointCo(LevelManager.Instance.CurrentTimeBonus));
    }

    private IEnumerator PlayerHitCheckpointCo(int bonus)
    {
       FloatingText.Show("Checkpoint", "CheckpointText", new CenterTextPositioner(.5f));
       yield return new WaitForSeconds(.5f);
       FloatingText.Show(string.Format(" +{0} time bonus", bonus), "CheckpointText", new CenterTextPositioner(.5f));
    }

    public void PlayerLeftCheckpoint()
    {

    }

    public void SpawnPlayer(Player player)
    {
        player.RespawnAt(transform);
        foreach (var listener in _listeners)
            listener.OnPlayerRespawnInThisCheckpoint(this, player);
    }

    public void AssignObjectToCheckpoint(IPlayerRespawnListener listener)
    {
        Debug.Log("added");
        _listeners.Add(listener);
    }
}

