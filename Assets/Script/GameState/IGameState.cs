public interface IGameState
{
    /**
     * param name="Controller"
     * returns 次の状態
     */
    IGameState Next(GameController Controller);
}
