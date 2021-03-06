using System.Windows;

namespace Tetris.Figures
{
    class Line : Figure
    {
        enum Position { HORIZONTAL, VERTICAL };
        Position _position;
        public Line(Point startingPoint)
        {
            for (int i = 0; i < 4; i++)
            {
                FigureCoordinates[i] = startingPoint;
                FigureCoordinates[i].X += step * i;
            }
            UpdateCanvasLocation();
            _position = Position.HORIZONTAL;
        }

        public override void Rotate()
        {
            Point[] point = new Point[4];

            if (_position == Position.HORIZONTAL)
            {
                point[0] = FigureCoordinates[1];
                point[1].X = FigureCoordinates[1].X;
                point[1].Y = FigureCoordinates[1].Y + step;
                point[2].X = FigureCoordinates[2].X - step;
                point[2].Y = FigureCoordinates[2].Y + (step * 2);
                point[3].X = FigureCoordinates[3].X - (step * 2);
                point[3].Y = FigureCoordinates[3].Y + (step * 3);
                if (CheckOverlapping(point) == false)
                {
                    FigureCoordinates = point;
                    _position = Position.VERTICAL;
                }
            }
            else
            {
                point[1] = FigureCoordinates[0];
                point[0].X = FigureCoordinates[0].X - step;
                point[0].Y = FigureCoordinates[0].Y;
                point[2].X = FigureCoordinates[2].X + step;
                point[2].Y = FigureCoordinates[2].Y - (step * 2);
                point[3].X = FigureCoordinates[3].X + (step * 2);
                point[3].Y = FigureCoordinates[3].Y - (step * 3);
                if (CheckOverlapping(point) == false)
                {
                    FigureCoordinates = point;
                    _position = Position.HORIZONTAL;
                }
            }
            Adjust();
            UpdateCanvasLocation();
        }

    }
}
