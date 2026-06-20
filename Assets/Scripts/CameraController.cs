using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject player;   // プレイヤーの追従
    
    [SerializeField] private float mouseSensitivity = 0.1f; // マウス感度
    private Vector3 offset;
    private float yaw;  // x軸回転
    private float pitch;    // y軸回転

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();   // マウス入力を取得

        yaw += mouseDelta.x * mouseSensitivity;
        pitch -= mouseDelta.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -20f, 20f);  // 真上・真下を向いてひっくり返るのを防ぐ

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f); // 回転を適用

        transform.position = player.transform.position + rotation * offset;    // プレイヤーとの距離を固定
        transform.LookAt(player.transform); //
    }
}
