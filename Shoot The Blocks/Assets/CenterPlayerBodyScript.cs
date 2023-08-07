using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CenterPlayerBodyScript : MonoBehaviour
{

    public UnityEvent<bool> submitGameOverEvent;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Enemy")
        {
            submitGameOverEvent.Invoke(true);
        }
    }
}
