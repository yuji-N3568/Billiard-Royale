using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;     // 加速の大きさ
    [SerializeField] private float maxSpeed = 10.0f;  // 最大速度
    [SerializeField] private float impactTime = 0.5f;  // 衝突してからふっとぶ時間

    private Rigidbody rb; 
    private float movementX;
    private float movementY;
    
    private bool impact = false;   // 衝突判定
    private float impactTimer = 0f;      // 時間測定用

    void Start()
    {
        rb = GetComponent <Rigidbody>();    // 物理演算を取得
    }

    void OnMove(InputValue movementValue)
    {
        // 入力からベクトルを得る
        Vector2 movementVector = movementValue.Get<Vector2>(); 

        movementX = movementVector.x;   // 左右方向(A, Dキー) 
        movementY = movementVector.y;   // 前後方向(W, Sキー)
    }

    void OnCollisionEnter(Collision collision)
    {
        impact = true;
        impactTimer = impactTime;
    }

    void FixedUpdate() 
    {
        Vector3 cameraX = Camera.main.transform.right;  // カメラ基準の左右方向
        Vector3 cameraY = Camera.main.transform.forward;    // カメラ基準の前後方向

        // y方向(上下)を無効化し、正規化(向きの基準であるため)
        cameraX.y = 0;
        cameraY.y = 0;
        cameraX.Normalize();
        cameraY.Normalize();

        // x方向とz方向に入力を反映させ、正規化(x+zが大きくなるため)
        Vector3 movement = cameraX * movementX + cameraY * movementY;   
        movement.Normalize();

        if(impact)  // 衝突しているとき
        {
            impactTimer -= Time.fixedDeltaTime; // 経過時間を減算

            if(impactTimer <= 0)
            {
                impact = false; // 衝突処理を終了
            }
        }
        else
        {
            rb.AddForce(movement * speed);  // Playerに力を加える(=加速する)
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);    // 最大速度の処理
        }
    }
}
