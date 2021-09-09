using System.Windows;

namespace Tetris.Figures
{
    class RightHorse:Figure
    {
        enum Position { FIRST, SECOND, THIRD, FORTH };
        Position _position;
        public RightHorse(Point startingPoint)
        {
            FigureCoordinates[0] = startingPoint;
            FigureCoordinates[1] = startingPoint;
            FigureCoordinates[1].X -= step;
            FigureCoordinates[2] = FigureCoordinates[1];
            FigureCoordinates[2].Y += step;
            FigureCoordinates[3] = FigureCoordinates[2];
            FigureCoordinates[3].Y += step;
            UpdateCanvasLocation();
            _position = Position.FIRST;
        }

        public override void Rotate()
        {
            Point[] point = new Point[4];
            switch (_position)
            {
                case Position.FIRST:
                    {
                        point[0] = FigureCoordinates[0];
                        point[0].Y += step;
                        point[1] = point[0];
                        point[1].Y -= step;
                        point[2] = point[1];
                        point[2].X -= step;
                        point[3] = point[2];
                        point[3].X -= step;

                        if (CheckOverlapping(point) == false)
                        {
                            FigureCoordinates = point;
                            _position = Position.SECOND;
                        }
                        break;
                    }
                case Position.SECOND:
                    {
                        point[0] = FigureCoordinates[0];
                        point[0].X -= step;
                        point[1] = point[0];
                        point[1].X += step;
                        point[2] = point[1];
                        point[2].Y -= step;
                        point[3] = point[2];
                        point[3].Y -= step; ;

                        if (CheckOverlapping(point) == false)
                        {
                            FigureCoordinates = point;
                            _position = Position.THIRD;
                        }
                        break;
                    }
                case Position.THIRD:
                    {
                        point[0] = FigureCoordinates[0];
                        point[0].Y -= step;
                        point[1] = point[0];
                        point[1].Y += step;
                        point[2] = point[1];
                        point[2].X += step;
                        point[3] = point[2];
                        point[3].X += step;

                        if (CheckOverlapping(point) == false)
                        {
                            FigureCoordinates = point;
                            _position = Position.FORTH;
                        }
                        break;
                    }

                case Position.FORTH:
                    {
                        point[0] = FigureCoordinates[0];
                        point[0].Y -= step;
                        point[0].X += step;
                        point[1] = point[0];
                        point[1].X -= step;
                        point[2] = point[1];
                        point[2].Y += step;
                        point[3] = point[2];
                        point[3].Y += step;
                        if (CheckOverlapping(point) == false)
                        {
                            FigureCoordinates = point;
                            _position = Position.FIRST;
                        }
                        break;
                    }
            }
            Adjust();
            UpdateCanvasLocation();
        }
    }
}
