using System.Windows;

namespace Tetris.Figures
{
    class Box : Figure
    {
        public Box(Point startingPoint)
        {
            FigureCoordinates[0] = startingPoint;
            FigureCoordinates[1].X = startingPoint.X + step;
            FigureCoordinates[1].Y = startingPoint.Y;
            FigureCoordinates[2].X = startingPoint.X;
            FigureCoordinates[2].Y = startingPoint.Y + step;
            FigureCoordinates[3].X = startingPoint.X + step;
            FigureCoordinates[3].Y = startingPoint.Y + step;
            UpdateCanvasLocation();
        }

        override public void Rotate() { }
    }
}
