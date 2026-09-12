using UnityEngine;

public class ZombieFlag : Enemy
{
    bool zombieFlag = true;
    public override void Serang()
    {
        Debug.Log("Zombie Flag Menyerang");
    }
}
