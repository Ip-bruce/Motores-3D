using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_carroTudo : MonoBehaviour
{
    [SerializeField] float velocidadeX;
    [SerializeField] float velocidadeZ;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject mesh_player;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        float velX = 0;
        float velZ = 0;

        if (Input.GetKey(KeyCode.W))
        {
            velZ += velocidadeZ;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velZ -= velocidadeZ;
        }

        if (Input.GetKey(KeyCode.D))
        {
            velX += velocidadeX;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velX -= velocidadeX;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        //GetComponent<Rigidbody>().velocity = new Vector3(velX, GetComponent<Rigidbody>().velocity.y, velZ);
        GetComponent<Rigidbody>().velocity = transform.right * velX + transform.forward * velZ + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
        mesh_player.GetComponent<Animator>().SetBool("_walk", Mathf.Abs(GetComponent<Rigidbody>().velocity.x) > 0 || Mathf.Abs(GetComponent<Rigidbody>().velocity.z) > 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barril")
        {
            GetComponent<ParticleSystem>().Stop();
            collision.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ParticleAtivator"))
        {
            GetComponent<ParticleSystem>().Play();
            Destroy(other);
        }
    }
}