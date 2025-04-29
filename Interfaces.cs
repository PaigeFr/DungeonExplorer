namespace DungeonExplorer
{
    /// <summary>
    /// The interfaces for the dungeon explorer game
    /// </summary>
    public interface IDamageable
    {
        void TakeDamage(int damage);
        int Health { get; set; }
    }

    public interface ICollectible
    {
        void Collect();
    }
}
