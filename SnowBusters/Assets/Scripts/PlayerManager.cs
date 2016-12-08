using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    float MovingTurnSpeed = 360;
    [SerializeField]
    float StationaryTurnSpeed = 180;
    [SerializeField]
    float JumpPower = 12f;
    [SerializeField]
    float GravityMultiplier = 2f;
    [SerializeField]
    float RunCycleLegOffset = 0.2f;
    [SerializeField]
    float MoveSpeedMultiplier = 1f;
    [SerializeField]
    float AnimSpeedMultiplier = 1f;
    [SerializeField]
    float GroundCheckDistance = 0.1f;

    Rigidbody RB;
    Animator Anim;
    bool IsGrounded;
    float OrigGroundCheckDistance;
    const float k_Half = 0.5f;
    float TurnAmount;
    float ForwardAmount;
    Vector3 GroundNormal;
    float CapsuleHeight;
    Vector3 CapsuleCenter;
    CapsuleCollider Capsule;
    bool Crouching;
    // Use this for initialization
    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
        RB = gameObject.GetComponent<Rigidbody>();
        Capsule = gameObject.GetComponent<CapsuleCollider>();
        CapsuleHeight = Capsule.height;
        CapsuleCenter = Capsule.center;
        RB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        OrigGroundCheckDistance = GroundCheckDistance;
    }
    public void Move(Vector3 move, bool crouch, bool jump)
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, GroundNormal);
        TurnAmount = Mathf.Atan2(move.x, move.z);
        ForwardAmount = move.z;
        ApplyExtraTurnRotation();

        if (IsGrounded)
        {
            HandleGroundedMovement(crouch, jump);
        }
        else
        {
            HandleAirborneMovement();
        }
        ScaleCapsuleForCrouching(crouch);
        PreventStandingInLowHeadroom();
        UpdateAnimator(move);
    }
    void ScaleCapsuleForCrouching(bool crouch)
    {
        if (IsGrounded && crouch)
        {
            if (Crouching) return;
            Capsule.height = Capsule.height / 2f;
            Capsule.center = Capsule.center / 2f;
            Crouching = true;
        }
        else
        {
            Ray crochRay = new Ray(RB.position + Vector3.up * Capsule.radius * k_Half, Vector3.up);
            float crouchRayLength = CapsuleHeight - Capsule.radius * k_Half;
            if (Physics.SphereCast(crochRay, Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                Crouching = true;
                return;
            }
            Capsule.height = CapsuleHeight;
            Capsule.center = CapsuleCenter;
            Crouching = false;
        }
    }
    void PreventStandingInLowHeadroom()
    {
        if (Crouching)
        {
            Ray crochRay = new Ray(RB.position + Vector3.up * Capsule.radius * k_Half, Vector3.up);
            float crouchRayLength = CapsuleHeight - Capsule.radius * k_Half;
            if (Physics.SphereCast(crochRay, Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                Crouching = true;
            }
        }
    }
    
    void UpdateAnimator(Vector3 move)
    {

        // update the animator parameters
        Anim.SetFloat("Forward", ForwardAmount, 0.1f, Time.deltaTime);
        Anim.SetFloat("Turn", TurnAmount, 0.1f, Time.deltaTime);
        Anim.SetBool("Crouch", Crouching);
        Anim.SetBool("OnGround", IsGrounded);
        if (!IsGrounded)
        {
            Anim.SetFloat("Jump", RB.velocity.y);
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        float runCycle =
            Mathf.Repeat(
                Anim.GetCurrentAnimatorStateInfo(0).normalizedTime + RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * ForwardAmount;
        if (IsGrounded)
        {
            Anim.SetFloat("JumpLeg", jumpLeg);
        }

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (IsGrounded && move.magnitude > 0)
        {
            Anim.speed = AnimSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
            Anim.speed = 1;
        }
    }
    
    void HandleAirborneMovement()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * GravityMultiplier) - Physics.gravity;
        RB.AddForce(extraGravityForce);

        GroundCheckDistance = RB.velocity.y < 0 ? OrigGroundCheckDistance : 0.01f;
    }


    void HandleGroundedMovement(bool crouch, bool jump)
    {
        // check whether conditions are right to allow a jump:
        if (jump && !crouch && Anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            // jump!
            RB.velocity = new Vector3(RB.velocity.x, JumpPower, RB.velocity.z);
            IsGrounded = false;
            Anim.applyRootMotion = false;
            GroundCheckDistance = 0.1f;
        }
    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
        transform.Rotate(0, TurnAmount * turnSpeed * Time.deltaTime, 0);
    }


    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (IsGrounded && Time.deltaTime > 0)
        {
            Vector3 v = (Anim.deltaPosition *MoveSpeedMultiplier) / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = RB.velocity.y;
            RB.velocity = v;
        }
    }


    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the characte
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, GroundCheckDistance))
        {
            GroundNormal = hitInfo.normal;
            IsGrounded = true;
            Anim.applyRootMotion = true;
        }
        else
        {
            IsGrounded = false;
            GroundNormal = Vector3.up;
            Anim.applyRootMotion = false;
        }
    }
}
