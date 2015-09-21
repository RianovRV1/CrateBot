using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class PermaSwitch : MonoBehaviour
{
    public bool _pressed = false;
    public bool action = false;
    public GameObject attached;
    public void Update()
    {
        var raycastBody = Physics2D.Raycast(transform.position, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("body"));
        var raycastCrate = Physics2D.Raycast(transform.position, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("Crates"));
        var raycastPlayer = Physics2D.Raycast(transform.position, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("Player"));
  
       
        if (raycastBody || raycastCrate || raycastPlayer)
            _pressed = true;
        if (_pressed)
        {
            if(attached != null)
                attached.SetActive(!action);
        }
    }
}

