namespace Thrive.Actors
{
    using Thrive.Players;

    public class Cell : Actor
    {
        public Player Player { get; set; }

        public Cell(Game game) : base(game)
        {
        }

        public override void Die()
        {
            base.Die();
            Player.Actors.Remove(this);
        }

        public override bool CanBeEatenBy(Actor actor)
        {
            if (actor is Cell)
            {
                var cell = actor as Cell;
                if (cell.Player == this.Player)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }
    }
}
