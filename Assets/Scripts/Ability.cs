public class Ability
{
    public string name;
    public float cooldownTime;

    public Ability()
    {
        this.name = "Default Ability";
        this.cooldownTime = 0f;
    }

    public virtual void Activate()
    {
        // реализация активной способности
    }

    public virtual void Passive()
    {
        // реализация пассивной способности
    }

    public virtual void BeginCooldown()
    {
        // реализация начала перезарядки способности
    }
}