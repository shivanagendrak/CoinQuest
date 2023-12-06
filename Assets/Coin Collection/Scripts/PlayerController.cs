using UnityEngine;


public class PlayerController : MonoBehaviour {
	
	public float speed;
	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();

	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			GameManager.Instance.CollectCoin();

        }
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.CompareTag("Enemy"))
        {
			GameManager.Instance.GameOver(false);
        }
    }


}