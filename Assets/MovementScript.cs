using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float Speed;
    public float rotationSpeed;

    [SerializeField] public Transform chil;

    [SerializeField] public ParticleSystem top;
    [SerializeField] public ParticleSystem bottom;
    [SerializeField] public ParticleSystem left;
    [SerializeField] public ParticleSystem right;

    private void FixedUpdate()
    {
        if (MainGameManager.Started)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * (Speed + (MainGameManager.score / (1500 * MainGameManager.multy))));
                bottom.Play();
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * (Speed + (MainGameManager.score / (1500 * MainGameManager.multy))));
                right.Play();
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * (Speed + (MainGameManager.score / (1500 * MainGameManager.multy))));
                top.Play();
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * (Speed + (MainGameManager.score / (1500 * MainGameManager.multy))));
                left.Play();
            }

            if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
            {
                bottom.Stop();
            }
            if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
            {
                right.Stop();
            }
            if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            {
                top.Stop();
            }
            if (!(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
            {
                left.Stop();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

            }

            chil.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (MainGameManager.Started)
        {
            if (collision.gameObject.layer == 8)
            {
                top.Stop();
                bottom.Stop();
                right.Stop();
                left.Stop();
                MainGameManager.gameOver();
            }
            FindFirstObjectByType<SoundManager>().SoundExpload.Play();
        }
    }
}
