using BasicVector;

namespace Thrive.Geometry
{
    public class Point
    {
        public int X;
        public int Y;

        public Vector ToVector()
        {
            return new Vector(X, Y);
        }
    }
}
