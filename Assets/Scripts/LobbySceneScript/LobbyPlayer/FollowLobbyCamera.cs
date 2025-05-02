using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLobbyCamera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            return;
        }
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        desiredPosition.z = transform.position.z;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);


        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime*smoothSpeed);
        //Vector3 pos = transform.position;
        //pos.x = target.position.x;
        //pos.y = target.position.y;
        //transform.position = pos;


    }
}
