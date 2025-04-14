using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] float adjustSpeedAmount = -2f;

    const string hitString = "Hit";

    float cooldownTimer = 0f;

    LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = FindAnyObjectByType<LevelGenerator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (cooldownTimer < collisionCooldown) return;

        levelGenerator.ChangeChunkSpeed(adjustSpeedAmount);
        animator.SetTrigger(hitString);
        cooldownTimer = 0f;
    }
}
