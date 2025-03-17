using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace I_am_Hero_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        private bool _isDragging;
        private Point _startPoint;
        private Grid _draggedElement;
        private readonly Dictionary<Grid, (Point, Point)> _relativePositions = new Dictionary<Grid, (Point, Point)>();
        private readonly Dictionary<Grid, Point> _originalPositions = new Dictionary<Grid, Point>();

        private int _rows = 6;
        private int _columns = 9;
        private readonly int MinRows = 6;
        private readonly int MinColumns = 9;
        private const double MinGridWidth = 180;
        private const double MinGridHeight = 120;
        private readonly double _thumbMargin = 10;
        private DashboardViewModel ViewModel => DataContext as DashboardViewModel;

        public DashboardView()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel(this);
        }

        private void Block_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel?.IsEditMode == false) return;

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
            if (ViewModel?.IsEditMode == false) return;

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
            if (ViewModel?.IsEditMode == false) return;

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
            LayoutManager.SaveLayout(MainCanvas);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (ViewModel?.IsEditMode == false) return;

            if (sender is Thumb thumb)
            {
                Grid grid = thumb.TemplatedParent as Grid ?? thumb.Parent as Grid;
                if (grid == null) return;

                double left = Canvas.GetLeft(grid);
                double top = Canvas.GetTop(grid);
                double newWidth = Math.Max(grid.Width + e.HorizontalChange, MainCanvas.ActualWidth / _columns - _thumbMargin);
                double newHeight = Math.Max(grid.Height + e.VerticalChange, MainCanvas.ActualHeight / _rows - _thumbMargin);
                Point nearestBottomRight = GetNearestGridPoint(left + newWidth, top + newHeight);

                if (!IsPositionOccupied(grid, new Point(left, top), newWidth, newHeight))
                {
                    grid.Width = Math.Max(MainCanvas.ActualWidth / _columns - _thumbMargin, nearestBottomRight.X - left - _thumbMargin);
                    grid.Height = Math.Max(MainCanvas.ActualHeight / _rows - _thumbMargin, nearestBottomRight.Y - top - _thumbMargin);
                    grid.Clip = new RectangleGeometry(new Rect(0, 0, grid.Width, grid.Height), 10, 10);
                    double canvasWidth = MainCanvas.ActualWidth;
                    double canvasHeight = MainCanvas.ActualHeight;
                    _relativePositions[grid] = (new Point(left / canvasWidth, top / canvasHeight),
                                                new Point(nearestBottomRight.X / canvasWidth, nearestBottomRight.Y / canvasHeight));
                    LayoutManager.SaveLayout(MainCanvas);
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
            double blockWidth = MainCanvas.ActualWidth * 0.33;
            double blockHeight = MainCanvas.ActualHeight * 0.5;

            Canvas.SetLeft(ProfileBlock, 10);
            Canvas.SetTop(ProfileBlock, 10);

            Canvas.SetLeft(SkillsBlock, MainCanvas.ActualWidth * 0.33);
            Canvas.SetTop(SkillsBlock, 10);

            Canvas.SetLeft(AttributesBlock, MainCanvas.ActualWidth * 0.66);
            Canvas.SetTop(AttributesBlock, 10);

            Canvas.SetLeft(CalendarBlock, 10);
            Canvas.SetTop(CalendarBlock, MainCanvas.ActualHeight * 0.5);

            Canvas.SetLeft(QuestsBlock, MainCanvas.ActualWidth * 0.33);
            Canvas.SetTop(QuestsBlock, MainCanvas.ActualHeight * 0.5);

            ProfileBlock.Width = blockWidth;
            ProfileBlock.Height = blockHeight;

            SkillsBlock.Width = blockWidth;
            SkillsBlock.Height = blockHeight;

            AttributesBlock.Width = blockWidth;
            AttributesBlock.Height = blockHeight;

            CalendarBlock.Width = blockWidth;
            CalendarBlock.Height = blockHeight;

            QuestsBlock.Width = blockWidth;
            QuestsBlock.Height = blockHeight;

            AlignBlocksToGrid();

            LayoutManager.LoadLayout(MainCanvas); 
            SaveRelativePositions();
            var scrollViewer = FindParent<ScrollViewer>(MainCanvas);
            if (scrollViewer != null)
            {
                scrollViewer.SizeChanged += ScrollViewer_SizeChanged;
            }            
        }
        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawGridLinesAdaptive();
            RestorePositions();
            AlignBlocksToGrid();
            LayoutManager.SaveLayout(MainCanvas);
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawGridLinesAdaptive();
            RestorePositions();
            AlignBlocksToGrid();
            LayoutManager.SaveLayout(MainCanvas);
        }

        private void OnEditModeButtonClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.InvokeAsync(() =>
            {
                DrawGridLinesAdaptive();
            }, System.Windows.Threading.DispatcherPriority.Background);
        }

        //private void DrawGridLines()
        //{
        //    if (MainCanvas == null) return;
        //    var savedGrids = MainCanvas.Children.OfType<Grid>().ToList();
        //    MainCanvas.Children.Clear();
        //    double width = MainCanvas.ActualWidth, height = MainCanvas.ActualHeight;
        //    if (ViewModel?.IsEditMode == true)
        //    {
        //        for (int i = 1; i < _columns; i++) AddGridLine(i * width / _columns, 0, i * width / _columns, height);
        //        for (int i = 1; i < _rows; i++) AddGridLine(0, i * height / _rows, width, i * height / _rows);
        //    }
        //    savedGrids.ForEach(grid => MainCanvas.Children.Add(grid));

        //    MainCanvas.UpdateLayout();
        //    Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Render);
        //}
        private void DrawGridLinesAdaptive()
        {
            if (MainCanvas == null || ScrollViewerContainer == null) return;

            var savedGrids = MainCanvas.Children.OfType<Grid>().ToList();
            MainCanvas.Children.Clear();

            double width = MainCanvas.ActualWidth;
            double scrollViewerHeight = ScrollViewerContainer.ActualHeight;
            double minRowHeight = 50;
            double requiredHeight = _rows * minRowHeight;

            MainCanvas.Height = Math.Max(scrollViewerHeight, requiredHeight);

            if (ViewModel?.IsEditMode == true)
            {
                for (int i = 1; i < _columns; i++)
                {
                    double x = i * width / _columns;
                    AddGridLine(x, 0, x, MainCanvas.Height);
                }

                for (int i = 1; i < _rows; i++)
                {
                    double height = Math.Max(minRowHeight, scrollViewerHeight / _rows);
                    double y = i * height;
                    AddGridLine(0, y, width, y);
                }
            }

            savedGrids.ForEach(grid => MainCanvas.Children.Add(grid));
            MainCanvas.UpdateLayout();
            Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Render);
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

        private void AlignBlocksToGrid() // Align blocks to grid after replacing
        {
            foreach (var grid in MainCanvas.Children.OfType<Grid>())
            {
                double left = Canvas.GetLeft(grid);
                double top = Canvas.GetTop(grid);
                Point nearestTopLeft = GetNearestGridPoint(left, top);
                Point nearestBottomRight = GetNearestGridPoint(left + grid.Width, top + grid.Height);

                grid.Width = Math.Max(MainCanvas.ActualWidth / _columns - _thumbMargin, nearestBottomRight.X - nearestTopLeft.X - _thumbMargin);
                grid.Height = Math.Max(MainCanvas.ActualHeight / _rows - _thumbMargin, nearestBottomRight.Y - nearestTopLeft.Y - _thumbMargin);
                Canvas.SetLeft(grid, nearestTopLeft.X);
                Canvas.SetTop(grid, nearestTopLeft.Y);
                grid.Clip = new RectangleGeometry(new Rect(0, 0, grid.Width, grid.Height), 10, 10);
            }
        }

        private void SaveRelativePositions() // Save blocks positions before resizing
        {
            double width = MainCanvas.ActualWidth, height = MainCanvas.ActualHeight;
            _relativePositions.Clear();
            foreach (var grid in MainCanvas.Children.OfType<Grid>())
            {
                _relativePositions[grid] = (new Point(Canvas.GetLeft(grid) / width, Canvas.GetTop(grid) / height),
                                            new Point((Canvas.GetLeft(grid) + grid.Width) / width, (Canvas.GetTop(grid) + grid.Height) / height));
            }
        }

        private void RestorePositions() // Restore blocks positions after resizing
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

        public int GetRows() => _rows;
        public int GetColumns() => _columns;
        public double GetMainCanvasWidth() => MainCanvas.ActualWidth;
        public double GetMainCanvasHeight() => MainCanvas.ActualHeight;

        public void SetRows(int value)
        {
            _rows = value;
            DrawGridLinesAdaptive();
            RestorePositions();
            AlignBlocksToGrid();
        }
        public void SetColumns(int value)
        {
            _columns = value;
            DrawGridLinesAdaptive();
            RestorePositions();
            AlignBlocksToGrid();
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int newNumber))
            {
                e.Handled = true;
                return;
            }

            var textBox = sender as TextBox;
            string fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            if (textBox.Tag is int minValue && int.TryParse(fullText, out int result) && result < minValue)
            {
                e.Handled = true;
            }
        }
        private void NumericTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;

            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (textBox.Text.Length == 1 ||
                    (int.TryParse(textBox.Text, out int value) &&
                     textBox.Tag is int minValue && value <= minValue))
                {
                    e.Handled = true;
                }
            }
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null)
            {
                if (parent is T correctlyTyped)
                    return correctlyTyped;
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

    }
}
