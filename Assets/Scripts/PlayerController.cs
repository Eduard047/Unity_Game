using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float tripleJumpForce;
    [SerializeField] private float fallDeathHeight = -10f;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private int remainingJumps;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        remainingJumps = 3; // Изменено на 3 для максимального количества прыжков
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Check if the player is falling below the death height.
        if (transform.position.y < fallDeathHeight)
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
                Jump(jumpForce);
            else if (remainingJumps > 0)
                Jump(doubleJumpForce);
            else if (remainingJumps == 0)
                Jump(tripleJumpForce);
        }

        // Sets animation parameters.
        anim.SetBool("run", Mathf.Abs(horizontalInput) > 0.01f);
        anim.SetBool("grounded", grounded);
    }

    private void Jump(float jumpForce)
    {
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        anim.SetTrigger("jump");
        grounded = false;
        remainingJumps--;

        if (remainingJumps < 0)
            remainingJumps = 0;
    }

    private void Die()
    {
        // Можете добавить здесь код, который будет выполняться при смерти персонажа.
        // Пример: Перезапуск уровня
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Сброс количества прыжков
        remainingJumps = 3; // или любое другое начальное значение
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            remainingJumps = 3; // Восстановление доступных прыжков при касании земли
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
