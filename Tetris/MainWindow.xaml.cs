using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Threading;
using Tetris.Figures;
using Figure = Tetris.Figures.Figure;
using Line = Tetris.Figures.Line;

namespace Tetris
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        enum Figures { LINE = 0, BOX, RIGHT_HORSE, LEFT_HORSE, RIGHT_SNAKE, LEFT_SNAKE, PENNIS };
        enum Move { LEFT, RIGHT };

        private readonly Random _random = new Random();
        private DispatcherTimer Timer = new DispatcherTimer();
        public static List<Point> FilledCubes = new List<Point>();
        private Figure _figure;
        private int _score;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _score = 0;
            Timer.Interval = TimeSpan.FromSeconds(0.5);
            Timer.Tick += Timer_tick;
            _figure = new Box(new Point(160, 0));
            for (int i = 0; i < 4; i++)
            {
                GameField.Children.Add(_figure.Rectangles[i].Rect);
            }
            Timer.Start();
        }

        public void Timer_tick(object sender, EventArgs e)
        {
            if (_figure.MoveDown() == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dispatcher.Invoke(() => _figure.Rectangles[i].Rect.Fill = Brushes.Green);
                }
                RefreshField();
            }
        }

        private void RefreshField()
        {
            for (int i = 0; i < 4; i++)
            {
                FilledCubes.Add(new Point(_figure.FigureCoordinates[i].X, _figure.FigureCoordinates[i].Y));
            }

            //checking
            for (int i = 0; i < 16; i++)
            {
                if (CheckRow(i * 40))
                {
                    DeleteRow(i);
                }
            }

            //add new figure
            _figure = CreateFigure((Figures)_random.Next(0,6), new Point(160, 0));
            for (int i = 0; i < 4; i++)
            {
                GameField.Children.Add(_figure.Rectangles[i].Rect);
            }

            if (CheckLost())
            {
                try
                {
                    MessageBox.Show("You are looser.\nYour scored " + _score, "Important message");
                    Application.Current.Shutdown();
                }
                catch (Exception)
                {

                }
            }

        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    {
                        bool movedDown = _figure.MoveDown();
                        while (movedDown)
                        {
                            movedDown = _figure.MoveDown();
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            Dispatcher.Invoke(() => _figure.Rectangles[i].Rect.Fill = Brushes.Green);
                        }
                        RefreshField();
                        break;
                    }
                case Key.Left:
                    {
                        _figure.MoveLeft();
                        break;
                    }
                case Key.Right:
                    {
                        _figure.MoveRight();
                        break;
                    }
                case Key.R:
                    {
                        _figure.Rotate();
                        break;
                    }
            }
        }

        private bool CheckRow(int y)
        {
            for (int k = 0; k < 12; k++)
            {
                if (!FilledCubes.Exists(p => p.X == k * 40 && p.Y == y))
                {
                    return false;
                }
            }
            return true;
        }

        private void DeleteRow(int n)
        {
            var children = GameField.Children.OfType<UIElement>().ToList();
            for (int i = 0; i < 12; i++)
            {
                foreach (UIElement child in children)
                {
                    if (Canvas.GetLeft(child) == i * 40 && Canvas.GetTop(child) == n * 40)
                    {
                        GameField.Children.Remove(child);
                        FilledCubes.Remove(new Point(i * 40, n * 40));
                        _score += 1;
                    }
                }

            }
            children = GameField.Children.OfType<UIElement>().ToList();
            foreach (UIElement child in children)
            {
                if (Canvas.GetTop(child) < n * 40)
                    Canvas.SetTop(child, Canvas.GetTop(child) + 40);
            }
            for (int i = 0; i < FilledCubes.Count; i++)
            {
                if (FilledCubes[i].Y < n * 40)
                    FilledCubes[i] = new Point(FilledCubes[i].X, FilledCubes[i].Y + 40);
            }
        }

        private bool CheckLost()
        {
            foreach (Point p in FilledCubes)
            {
                if (p.Y == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private static Figure CreateFigure(Figures figure, Point coordinates)
        { 
            switch (figure){
                case Figures.BOX:
                {
                    return new Box(coordinates);
                }
                case Figures.LEFT_HORSE:
                {
                    return new LeftHorse(coordinates);
                }
                case Figures.RIGHT_HORSE:
                {
                    return new RightHorse(coordinates);
                }
                case Figures.LEFT_SNAKE:
                {
                    return new LeftSnake(coordinates);
                }
                case Figures.RIGHT_SNAKE:
                {
                    return new RightSnake(coordinates);
                }
                case Figures.LINE:
                {
                    return new Line(coordinates);
                }
                case Figures.PENNIS:
                {
                    return new Pennis(coordinates);
                }
            }
            return new Box(coordinates);
        }

    }
}