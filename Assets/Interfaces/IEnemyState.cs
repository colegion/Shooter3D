using EnemySystem;

namespace Interfaces
{
    public interface IEnemyState
    {
        public void EnterState(Enemy enemy);
        public void ExitState(Enemy enemy);
    }
}
