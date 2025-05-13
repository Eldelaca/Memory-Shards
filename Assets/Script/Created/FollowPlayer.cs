using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        
        transform.rotation = Quaternion.Lerp(transform.rotation, playerTransform.rotation, Time.deltaTime * 3);
    }
}
