using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    [SerializeField] float _bounce = 10f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _bounce , ForceMode2D.Impulse);
        }
    }
}
