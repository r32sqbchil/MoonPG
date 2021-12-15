
public interface IEnemyBody
{
    void OnTurn();
    void InSightOfPlayer();
    void OutSightOfPlayer();
    void OnKnockBack(float direction, float damage);
}
