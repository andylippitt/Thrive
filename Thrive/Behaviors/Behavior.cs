namespace Thrive.Behaviors
{
    public abstract class Behavior
    {
        public Game Game { get; set; }
        public abstract void Step();
        public virtual void Initialize() { }
    }
}
