using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float SpeedRotation;
    [SerializeField]
    float gravity;
    float horizontal;
    float vertical;
    [SerializeField]
    Camera cameras;
    Vector3 moveHorizontal;
    Vector3 moveForward;
    Vector3 moveDirection;
    CharacterController controller;
    [SerializeField] [SyncVar] int playerID = -1;
    [SerializeField]
    GameObject gameOverUi;

    [SerializeField][SyncVar]
    int health;

    public int PlayerID
    {
        get
        {
            return playerID;
        }

        set
        {
            playerID = value;
        }
    }

    public int getHealth()
    {
        return health;
    }

    public int SetHealth()
    {
        return health -= 10;
    }

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //mouvement de la caméra avec la souris
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * SpeedRotation);
        cameras.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) * Time.deltaTime * SpeedRotation);

        moveHorizontal = transform.right * speed * horizontal;
        moveForward = transform.forward * speed * vertical;

        if (controller.isGrounded == false)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection);
        controller.SimpleMove(moveHorizontal + moveForward);

    }
    // change de couleur les joueurs et active la caméra
    public override void OnStartLocalPlayer()
    {
        cameras.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            SetHealth();
        }
    }

}
