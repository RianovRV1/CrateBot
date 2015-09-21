using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public String LevelName;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == null)
            return;
        LevelManager.Instance.GotoNextLevel(LevelName);
    }
}

