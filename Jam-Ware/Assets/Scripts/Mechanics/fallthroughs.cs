using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Core;
using Platformer.Mechanics;


public class fallthroughs : MonoBehaviour
{

    internal Collider2D _collider;
    public PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.velocity.y > 0 || Input.GetKey("s"))
        {
            _collider.enabled = false;
        }
        else if (_player.velocity.y < 0)
        {
            _collider.enabled = true;
        }
        
    }

    
}
