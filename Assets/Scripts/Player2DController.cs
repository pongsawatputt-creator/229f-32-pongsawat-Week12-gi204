using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 450; // Jump

    private Rigidbody2D _rb;
    private float _moveInputValue; // A=(-1), D=1
    private bool _isGrounded;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Keyboard.current != null)
        {
            _moveInputValue = (Keyboard.current.dKey.isPressed ? 1 : 0) // กด A, D เพื่อกำหนดทิศทาง
                - (Keyboard.current.aKey.isPressed ? 1 : 0);
        }
        _rb.linearVelocity = new Vector2(_moveInputValue * speed, _rb.linearVelocity.y); //ใส่แรง X คงแรง Y

        if (_moveInputValue < 0) { _spriteRenderer.flipX = true; }
        else if (_moveInputValue > 0) { _spriteRenderer.flipX = false; }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && _isGrounded) // กด spacebar พร้อมเช็คติดพื้น 
        {
            _rb.AddForce(new Vector2(_rb.linearVelocity.x, jumpForce)); // ใส่แรงแกน Y คงแรงแกน X
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}