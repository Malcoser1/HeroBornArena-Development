using UnityEngine;

public class PlayerBehav : MonoBehaviour
{
    
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;

    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private GameBehavior _gameManager;

    private CapsuleCollider _col;

    void Start()
    {
        
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }



    // Update is called once per frame
    void Update()
    {
      
        vInput = Input.GetAxis("Vertical") * moveSpeed;

       
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            
            Rigidbody bulletRB =
            newBullet.GetComponent<Rigidbody>();
            
            bulletRB.velocity = this.transform.forward * bulletSpeed;
        }
    }

void FixedUpdate()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
        
        Vector3 rotation = Vector3.up * hInput;
       
        Quaternion angleRot = Quaternion.Euler(rotation *
        Time.fixedDeltaTime);
    
        _rb.MovePosition(this.transform.position +
        this.transform.forward * vInput * Time.fixedDeltaTime);
         
        _rb.MoveRotation(_rb.rotation * angleRot);
    }
    private bool IsGrounded()
    {
        // 7
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,_col.bounds.min.y, _col.bounds.center.z);
        // 8
        bool grounded = Physics.CheckCapsule(_col.bounds.center,capsuleBottom, distanceToGround, groundLayer,QueryTriggerInteraction.Ignore);
        // 9
        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 4
        if (collision.gameObject.name == "Enemy")
        {
            // 5
            _gameManager.HP -= 1;
        }
    }

}