//.NetSystemCollections
using System.Collections;
using System.Collections.Generic;

//Unity Library
using UnityEngine;
using VirtualExpo.MainArea.PlayerUICustomized;

//PhotonPunLibrarySDK
using Photon.Pun;
using Photon.Realtime;


namespace VirtualExpo.Player
{

    public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
    {

        #region PrivateVariable(s)

        [Header("Player Needed Variable")]
        private string thisPlayerName;
        private int thisPlayerId;
        [SerializeField] private float walkSpeed = 6f;
        [SerializeField] private float grafity = -9.8f;
        [SerializeField] private bool isGrounded;
        [SerializeField] private float jumpHeight = 3f;

        [Space(5)]
        [Header("Position and Transform Movement")]
        [SerializeField] private CharacterController playerController;
        private float horizontalInput;
        private float verticalInput;

        [SerializeField] private Transform playerCam;
        [SerializeField] private GameObject cinemaMachineComponent;

        private float turnSmoothTime;
        private float turnSmoothVelocity;

        [SerializeField] private Transform groundCheck;
        private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;

        private Vector3 velocity;

        [Space(5)]
        [Header("Player Animation")]
        [SerializeField] private Animator anim;
        //[SerializeField] private float directionDampTime = 0.25f;

        private PlayerUI uiForPlayer;

        #endregion

        #region Public Variable

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #endregion


        #region Core Unity Function

        private void Awake()
        {

            //playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
            if (photonView.IsMine)
            {
                PlayerMovement.LocalPlayerInstance = this.gameObject;
            }

            thisPlayerName = photonView.Owner.NickName;
            uiForPlayer = this.GetComponent<PlayerUI>();
            uiForPlayer.DefaultUI(thisPlayerName);

            DontDestroyOnLoad(this.gameObject);

        }

        // Update is called once per frame
        void Update()
        {

            if (!photonView.IsMine && PhotonNetwork.IsConnected)
            {
                playerCam.gameObject.SetActive(false);
                cinemaMachineComponent.SetActive(false);
                return;
            }
            else
            {
                playerCam.gameObject.SetActive(true);
                cinemaMachineComponent.SetActive(true);
                MovementWalk(walkSpeed);
            }

        }

        #endregion

        #region Player Transform / Movement Method(s)

        void MovementWalk(float speed)
        {

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {

                velocity.y = -2f;

            }

            //Jump with Space Pressed
            Jumping(jumpHeight, grafity);

            //gravity
            velocity.y += grafity * Time.deltaTime;
            playerController.Move(velocity * Time.deltaTime);

            //Get Horizontal & Vertical Input
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            //Walk / Run Animation
            WalkAnimation(horizontalInput, verticalInput);

            //Define dir from that input
            Vector3 dir = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            if (dir.magnitude >= 0.1f)
            {

                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //move based on that direction
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                playerController.Move(moveDir.normalized * speed * Time.deltaTime);

            }

        }

        void Jumping(float jumpPower, float playerGrafity)
        {

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpPower * -2 * playerGrafity);

                //jumpAnimation Trigger
                JumpAnimation();
            }

        }

        #endregion

        #region Trigger and Collider Event Method(s)

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Teleporter")
            {

                string sceneName = other.GetComponent<Teleporter>().sceneName;
                PhotonNetwork.Destroy(this.gameObject);
                PhotonNetwork.LoadLevel(sceneName);

            }

        }

        #endregion

        #region Animation

        void WalkAnimation(float horizontal, float vertical)
        {

            anim.SetFloat("Speed", horizontal * horizontal + vertical * vertical);
            //anim.SetFloat("Direction", horizontal, directionDampTime, Time.deltaTime);

        }

        void JumpAnimation()
        {

            anim.SetTrigger("Jump");

        }

        #endregion

        #region Photon Pun IPunObservable Implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                //any data (gameobject that activated or other data)
            }
            else
            {
                // Network player, receive data
                //any data (gameobject that activated or other data)
            }

        }

        #endregion

        #region Photon Pun Callbacks Methods



        #endregion

    }

}