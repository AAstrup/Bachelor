namespace GameEngine
{
    public interface ITarget
    {
        void Damage(int amount,string damageReason);
        int GetDamage();
        void CheckForDeath();
        PlayerBoardState GetOwner();
        string GetNameType();
    }
}