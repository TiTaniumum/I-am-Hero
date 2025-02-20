using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace I_am_Hero_WPF.Views
{
    public partial class MainPage : Page
    {
        private bool _isDragging;
        private Point _startPoint;
        private Grid _draggedElement;
        private readonly Dictionary<Grid, (Point, Point)> _relativePositions = new Dictionary<Grid, (Point, Point)>();
        private readonly Dictionary<Grid, Point> _originalPositions = new Dictionary<Grid, Point>();
        private readonly int _columns = 10;
        private readonly int _rows = 8;
        private readonly double _thumbMargin = 10;

        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Block_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid grid)
            {
                _draggedElement = grid;
                _isDragging = true;
                _startPoint = e.GetPosition(this);
                if (!_originalPositions.ContainsKey(grid))
                {
                    _originalPositions[grid] = new Point(Canvas.GetLeft(grid), Canvas.GetTop(grid));
                }
                _draggedElement.CaptureMouse();
            }
        }

        private void Block_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging || _draggedElement == null) return;
            Point currentPoint = e.GetPosition(this);
            double newLeft = Canvas.GetLeft(_draggedElement) + (currentPoint.X - _startPoint.X);
            double newTop = Canvas.GetTop(_draggedElement) + (currentPoint.Y - _startPoint.Y);

            // Проверка на выход за границы
            newLeft = Math.Max(0, Math.Min(MainCanvas.ActualWidth - _draggedElement.Width, newLeft));
            newTop = Math.Max(0, Math.Min(MainCanvas.ActualHeight - _draggedElement.Height, newTop));

            Canvas.SetLeft(_draggedElement, newLeft);
            Canvas.SetTop(_draggedElement, newTop);
            _startPoint = currentPoint;
        }

        private void Block_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_draggedElement == null) return;
            _isDragging = false;
            _draggedElement.ReleaseMouseCapture();

            double left = Canvas.GetLeft(_draggedElement);
            double top = Canvas.GetTop(_draggedElement);
            Point nearestTopLeft = GetNearestGridPoint(left, top);
            Point correctedBottomRight = GetNearestGridPoint(left + _draggedElement.Width, top + _draggedElement.Height);

            if (!IsPositionOccupied(_draggedElement, nearestTopLeft))
            {
                _relativePositions[_draggedElement] = (new Point(nearestTopLeft.X / MainCanvas.ActualWidth, nearestTopLeft.Y / MainCanvas.ActualHeight),
                                                       new Point(correctedBottomRight.X / MainCanvas.ActualWidth, correctedBottomRight.Y / MainCanvas.ActualHeight));
                Canvas.SetLeft(_draggedElement, nearestTopLeft.X);
                Canvas.SetTop(_draggedElement, nearestTopLeft.Y);
                _originalPositions[_draggedElement] = new Point(Canvas.GetLeft(_draggedElement), Canvas.GetTop(_draggedElement));
            }
            else if (_originalPositions.TryGetValue(_draggedElement, out Point originalPos))
            {
                Canvas.SetLeft(_draggedElement, originalPos.X);
                Canvas.SetTop(_draggedElement, originalPos.Y);
            }
            _draggedElement = null;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb thumb)
            {
                Grid grid = thumb.TemplatedParent as Grid ?? thumb.Parent as Grid;
                if (grid == null) return;

                double left = Canvas.GetLeft(grid);
                double top = Canvas.GetTop(grid);
                double newWidth = Math.Max(grid.Width + e.HorizontalChange, 50);
                double newHeight = Math.Max(grid.Height + e.VerticalChange, 50);
                Point nearestBottomRight = GetNearestGridPoint(left + newWidth, top + newHeight);

                if (!IsPositionOccupied(grid, new Point(left, top), newWidth, newHeight))
                {
                    grid.Width = nearestBottomRight.X - left - _thumbMargin;
                    grid.Height = nearestBottomRight.Y - top - _thumbMargin;
                    grid.Clip = new RectangleGeometry(new Rect(0, 0, grid.Width, grid.Height), 10, 10);
                    double canvasWidth = MainCanvas.ActualWidth;
                    double canvasHeight = MainCanvas.ActualHeight;
                    _relativePositions[grid] = (new Point(left / canvasWidth, top / canvasHeight),
                                                new Point(nearestBottomRight.X / canvasWidth, nearestBottomRight.Y / canvasHeight));
                }
            }
        }
        private bool IsPositionOccupied(Grid element, Point newPosition, double width = 0, double height = 0)
        {
            if (width == 0) width = element.Width;
            if (height == 0) height = element.Height;

            foreach (var grid in MainCanvas.Children.OfType<Grid>())
            {
                if (grid == element) continue;
                double gridLeft = Canvas.GetLeft(grid);
                double gridTop = Canvas.GetTop(grid);

                if (newPosition.X < gridLeft + grid.Width && newPosition.X + width > gridLeft &&
                    newPosition.Y < gridTop + grid.Height && newPosition.Y + height > gridTop)
                {
                    return true;
                }
            }
            return false;
        }

        private void MainCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGridLines();
            AlignBlocksToGrid();
            SaveRelativePositions();
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawGridLines();
            RestorePositions();
            AlignBlocksToGrid();
        }

        private void DrawGridLines()
        {
            if (MainCanvas == null) return;
            var savedGrids = MainCanvas.Children.OfType<Grid>().ToList();
            MainCanvas.Children.Clear();
            double width = MainCanvas.ActualWidth, height = MainCanvas.ActualHeight;

            for (int i = 1; i < _columns; i++) AddGridLine(i * width / _columns, 0, i * width / _columns, height);
            for (int i = 1; i < _rows; i++) AddGridLine(0, i * height / _rows, width, i * height / _rows);
            savedGrids.ForEach(grid => MainCanvas.Children.Add(grid));
        }

        private void AddGridLine(double x1, double y1, double x2, double y2)
        {
            MainCanvas.Children.Add(new Line
            {
                X1 = x1,
                X2 = x2,
                Y1 = y1,
                Y2 = y2,
                Stroke = Brushes.Gray,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection { 2, 2 }
            });
        }

        private Point GetNearestGridPoint(double x, double y)
        {
            double columnStep = MainCanvas.ActualWidth / _columns;
            double rowStep = MainCanvas.ActualHeight / _rows;
            return new Point(Math.Round(x / columnStep) * columnStep, Math.Round(y / rowStep) * rowStep);
        }

        private void AlignBlocksToGrid()
        {
            foreach (var grid in MainCanvas.Children.OfType<Grid>())
            {
                double left = Canvas.GetLeft(grid);
                double top = Canvas.GetTop(grid);
                Point nearestTopLeft = GetNearestGridPoint(left, top);
                Point nearestBottomRight = GetNearestGridPoint(left + grid.Width, top + grid.Height);

                grid.Width = nearestBottomRight.X - nearestTopLeft.X - _thumbMargin;
                grid.Height = nearestBottomRight.Y - nearestTopLeft.Y - _thumbMargin;
                Canvas.SetLeft(grid, nearestTopLeft.X);
                Canvas.SetTop(grid, nearestTopLeft.Y);
                grid.Clip = new RectangleGeometry(new Rect(0, 0, grid.Width, grid.Height), 10, 10);
            }
        }

        private void SaveRelativePositions()
        {
            double width = MainCanvas.ActualWidth, height = MainCanvas.ActualHeight;
            _relativePositions.Clear();
            foreach (var grid in MainCanvas.Children.OfType<Grid>())
                _relativePositions[grid] = (new Point(Canvas.GetLeft(grid) / width, Canvas.GetTop(grid) / height),
                                            new Point((Canvas.GetLeft(grid) + grid.Width) / width, (Canvas.GetTop(grid) + grid.Height) / height));
        }

        private void RestorePositions()
        {
            double width = MainCanvas.ActualWidth, height = MainCanvas.ActualHeight;
            foreach (var kvp in _relativePositions)
            {
                Grid grid = kvp.Key;
                (Point relTopLeft, Point relBottomRight) = kvp.Value;
                Point newTopLeft = GetNearestGridPoint(relTopLeft.X * width, relTopLeft.Y * height);
                Point newBottomRight = GetNearestGridPoint(relBottomRight.X * width, relBottomRight.Y * height);
                grid.Width = newBottomRight.X - newTopLeft.X;
                grid.Height = newBottomRight.Y - newTopLeft.Y;
                Canvas.SetLeft(grid, newTopLeft.X);
                Canvas.SetTop(grid, newTopLeft.Y);
                grid.Clip = new RectangleGeometry(new Rect(0, 0, grid.Width, grid.Height), 10, 10);
            }
        }
    }
}
