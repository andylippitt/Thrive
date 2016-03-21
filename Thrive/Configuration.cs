namespace Thrive
{
    using Thrive.Geometry;

    public class Configuration
    {
        public Dimensions Dimensions = new Dimensions
        {
            Width = 800,
            Height = 600
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

        public int MaxCellsPerPlayer = 16;

    }
}
