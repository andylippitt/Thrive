namespace Thrive.Geometry
{
    using System;

    public static class Geo
    {
        public static double Distance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
    }
}
