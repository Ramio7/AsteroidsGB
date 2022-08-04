using UnityEngine;

public interface IHaveCooldowns
{
    public bool Cooldown(float cooldownStart, float cooldownAmount, out float newCooldownStart);
}
