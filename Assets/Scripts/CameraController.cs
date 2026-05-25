using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;   // プレイヤーの追従

    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;    // プレイヤーとの位置関係を固定
    }
}
