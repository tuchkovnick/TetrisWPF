using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tetris.Figures
{
    public struct Cube{
        public Rectangle Rect;
        public int Top;
        public int Left;
    } 

    abstract class Figure
    {
        public int step = 40;
        private Point[] _figureCoordinates = new Point[4];
        public  Point[] FigureCoordinates { get { return _figureCoordinates; } set { _figureCoordinates = value; } }
        public Cube [] Rectangles = new Cube[4];

        public Figure()
        {
            for(int i = 0; i < 4; i++)
            {
                Rectangles[i].Rect = new Rectangle();
                Rectangles[i].Rect.Width = step;
                Rectangles[i].Rect.Height = step;
                Rectangles[i].Rect.Fill = Brushes.Red;
            }
        }

        public bool MoveDown()
        {
            
            for (int k = 0; k < 4; k++)
            {
                if (_figureCoordinates[k].Y + step > 600)
                    return false; //нижняя граница

                foreach (Point p in MainWindow.FilledCubes)
                {
                if (_figureCoordinates[k].Y + step == p.Y && _figureCoordinates[k].X == p.X)
                    return false;
                }    
            }

            for (int i = 0; i<4; i++)
            {
                _figureCoordinates[i]. Y += step;
            }
            UpdateCanvasLocation();
            return true;
        }

        public void MoveLeft()
        {
            for(int k = 0; k < 4; k++)
            {
                if (_figureCoordinates[k].X - step < 0) return;
                foreach (Point p in MainWindow.FilledCubes)
                {
                    if (_figureCoordinates[k].X - step == p.X && _figureCoordinates[k].Y == p.Y)
                        return;
                }
            }
        
            for (int i = 0; i < 4; i++)
            {
                _figureCoordinates[i].X -= step;
            }
            UpdateCanvasLocation();
        }

        public void MoveRight()
        {
            for (int k = 0; k < 4; k++)
            {
                if (_figureCoordinates[k].X + step > 440) return;
                foreach (Point p in MainWindow.FilledCubes)
                {
                    if (_figureCoordinates[k].X + step == p.X && _figureCoordinates[k].Y == p.Y)
                        return;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                _figureCoordinates[i].X += step;
            }
            UpdateCanvasLocation();
        }

        public void UpdateCanvasLocation()
        {
            for (int i = 0; i < 4; i++)
            {
                Canvas.SetTop(Rectangles[i].Rect, FigureCoordinates[i].Y);
                Canvas.SetLeft(Rectangles[i].Rect, FigureCoordinates[i].X);
            }
        }

        protected void Adjust()
        {
            while (CheckLeftOuting())
            {
                for (int i = 0; i < 4; i++)
                {
                    FigureCoordinates[i].X += step;
                }
            }

            while (CheckRightOuting())
            {
                for (int i = 0; i < 4; i++)
                {
                    FigureCoordinates[i].X -= step;
                }
            }

            while (CheckDownOuting())
            {
                for (int i = 0; i < 4; i++)
                {
                    FigureCoordinates[i].Y -= step;
                }
            }
        }

        private bool CheckLeftOuting()
        {
            foreach (Point p in FigureCoordinates)
            {
                if (p.X < 0) return true;
            }
            return false;
        }

        private bool CheckRightOuting()
        {
            foreach (Point p in FigureCoordinates)
            {
                if (p.X > 440) return true;
            }
            return false;
        }

        private bool CheckDownOuting()
        {
            foreach (Point p in FigureCoordinates)
            {
                if (p.Y > 600) return true;
            }
            return false;
        }

        protected bool CheckOverlapping(Point[] p)
        {
            for (int i = 0; i<4 ;i++)
            {
                foreach(Point cube in MainWindow.FilledCubes)
                {
                    if (p[i].X == cube.X && p[i].Y == cube.Y)
                        return true;
                }
            }
            return false;
        }

        public abstract void Rotate();

    }
}
