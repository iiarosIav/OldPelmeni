using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public float speedMain;

    private Rigidbody2D rb;
    public Vector2 moveInput;
    public Vector2 moveVelocity;

    private SpriteRenderer spriteRenderer;


    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;    


    // лан, комменты будут. Это скрипт движения + изменение спрайтов


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speedMain;

    }
    
    private void FixedUpdate()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);

        // ну тут как раз мы вертим спрайты.

        if (vertical > 0.1f) 
        {
            spriteRenderer.sprite = upSprite;
        }
        else if (vertical < -0.1f) 
        {
            spriteRenderer.sprite = downSprite;
        }
        else if (horizontal < -0.1f) 
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (horizontal > 0.1f) 
        {
            spriteRenderer.sprite = rightSprite;
        }




    }

}
