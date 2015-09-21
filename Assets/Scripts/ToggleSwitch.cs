using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



public class ToggleSwitch : MonoBehaviour
{
    
    public bool _pressed = false;
    public bool action = false;
    public GameObject attached;


    public void Update()
    {
        var rayone = new Vector3(transform.position.x - .2f, transform.position.y);
        var raytwo = new Vector3(transform.position.x, transform.position.y);
        var raythree = new Vector3(transform.position.x + .2f, transform.position.y);

        var raycastBody1 = Physics2D.Raycast(rayone, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("body"));
        var raycastCrate1 = Physics2D.Raycast(rayone, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("Crates"));
        var raycastPlayer1 = Physics2D.Raycast(rayone, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("Player"));
        var raycastBody2 = Physics2D.Raycast(raytwo, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("body"));
        var raycastCrate2 = Physics2D.Raycast(raytwo, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("Crates"));
        var raycastPlayer2 = Physics2D.Raycast(raytwo, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("Player"));
        var raycastBody3 = Physics2D.Raycast(raythree, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("body"));
        var raycastCrate3 = Physics2D.Raycast(raythree, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("Crates"));
        var raycastPlayer3 = Physics2D.Raycast(raythree, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("Player"));


        if (raycastBody1 || raycastCrate1 || raycastPlayer1 || raycastBody2 || raycastCrate2 || raycastPlayer2 || raycastBody3 || raycastCrate3 || raycastPlayer3)
            _pressed = true;

        if (!raycastBody1 && !raycastCrate1 && !raycastPlayer1 && !raycastBody2 && !raycastCrate2 && !raycastPlayer2 && !raycastBody3 && !raycastCrate3 && !raycastPlayer3)
            _pressed = false;

        if (_pressed)
        {
            if(attached != null)
                attached.SetActive(!action);
        }
        if (!_pressed)
        {
            if(attached != null)
                attached.SetActive(action);
        }
    }
}


