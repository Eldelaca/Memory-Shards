using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        
        transform.rotation = Quaternion.Lerp(transform.rotation, playerTransform.rotation, Time.deltaTime * 3);
    }
}
