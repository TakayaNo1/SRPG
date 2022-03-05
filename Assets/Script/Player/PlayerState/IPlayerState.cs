public interface IPlayerState
{
    /**
     * param name="Controller"
     * returns 次の状態
     */
    IPlayerState Next(GameController Controller);
}
