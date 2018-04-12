using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Tag_Ball)
        {
            other.transform.parent.parent.SendMessage("HitBallDoor", SendMessageOptions.RequireReceiver);
            int tempIndex = 0;
            if (other.transform.position.x > 0)
            {
                tempIndex = 2;
            }
            else if (other.transform.position.x < 0)
            {
                tempIndex = -2;
            }
            gameObject.transform.parent.parent.SendMessage("ShootAGoal", tempIndex);
        }
    }
}
