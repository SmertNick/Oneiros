using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Theme theme = Theme.Happy;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float force = 300f;
    [SerializeField] private int damage = 1;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        theme = GameManager.Instance.theme;
        spriteRenderer.sprite = sprites[(int) theme];
        
        Events.OnThemeChange += HandleThemeChange;
    }

    private void HandleThemeChange(Theme newTheme)
    {
        theme = newTheme;
        spriteRenderer.sprite = sprites[(int) theme];
    }

    private void OnEnable()
    {
        body.AddForce(transform.right * force, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //TODO add sparks particle system
        gameObject.SetActive(false);
        if (other.gameObject.GetComponent<IDamageable>() == null) return;
        other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
    }

    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
    }
}
