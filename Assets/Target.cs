using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]Transform middlePos;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            float distance = Vector3.Distance(middlePos.position, collision.transform.position);
            Debug.Log($"{distance}");
            if(distance>0.1f)
            {
                AudioManager.Instance.PlayEffect("GetScore");
            }
            else
            {
                AudioManager.Instance.PlayEffect("GetXScore");
            }
        }
    }
}
