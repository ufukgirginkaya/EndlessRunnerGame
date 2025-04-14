using UnityEngine;

public class Apple : Pickup
{

    LevelGenerator levelGenerator;

    [SerializeField] float increaseSpeed = 3f;

    

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator; 
    }
    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkSpeed(increaseSpeed);

        Debug.Log("Powerup!");
        
    }
}
