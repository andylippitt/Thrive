namespace Thrive
{
    using Thrive.Geometry;

    public class Configuration
    {
        public Dimensions Dimensions = new Dimensions
        {
            Width = 1024,
            Height = 768
        };
        public int MinimumSplitMass = 10;

        public double SplitCost = 0.05;

        public double SeparationSpeed = 0.5;
        public double EatMassThreshold = 1.25;
        public double EatDistanceThreshold = 0.25;

        public double EatCost = 0.05;

        public double CellMaxSpeed = 10;
        public double CellMassSpeedPenalty = 0.0004;

        public double CellMaxMass = 20000;

        public double CellHunger = 0.02 / 20;

        public int MaxCellsPerPlayer = 16;

        public int FoodMinSize = 20;
        public int FoodMaxSize = 80;

        public int TotalGameMass = 20000;
        public int TotalGameMassTolerance = 40;

        public int HunterMin = 6;
    }
}
