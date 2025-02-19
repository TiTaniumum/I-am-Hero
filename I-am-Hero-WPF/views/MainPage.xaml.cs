using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace I_am_Hero_WPF.Views
{
    public partial class MainPage : Page
    {
        private bool _isDragging = false;
        private Point _startPoint;
        private UIElement _draggedElement;
        private Dictionary<UIElement, Point> _relativePositions = new Dictionary<UIElement, Point>(); // Относительные позиции блоков
        private Dictionary<UIElement, Size> _relativeSizes = new Dictionary<UIElement, Size>(); // Относительные размеры блоков


        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        // Начало перетаскивания
        private void Block_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _draggedElement = sender as UIElement;
            if (_draggedElement != null)
            {
                _isDragging = true;
                _startPoint = e.GetPosition(this);
                _draggedElement.CaptureMouse();
            }
        }

        // Перетаскивание
        private void Block_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _draggedElement != null)
            {
                Point currentPoint = e.GetPosition(this);
                double offsetX = currentPoint.X - _startPoint.X;
                double offsetY = currentPoint.Y - _startPoint.Y;

                Canvas.SetLeft(_draggedElement, Canvas.GetLeft(_draggedElement) + offsetX);
                Canvas.SetTop(_draggedElement, Canvas.GetTop(_draggedElement) + offsetY);

                _startPoint = currentPoint;
            }
        }

        // Завершение перетаскивания
        private void Block_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_draggedElement != null)
            {
                _isDragging = false;
                _draggedElement.ReleaseMouseCapture();

                // Текущие размеры Canvas
                double canvasWidth = MainCanvas.ActualWidth;
                double canvasHeight = MainCanvas.ActualHeight;

                // Определяем ближайшую точку сетки
                double left = Canvas.GetLeft(_draggedElement);
                double top = Canvas.GetTop(_draggedElement);
                Point nearestPoint = GetNearestGridPoint(left, top);

                // Обновляем положение элемента
                Canvas.SetLeft(_draggedElement, nearestPoint.X);
                Canvas.SetTop(_draggedElement, nearestPoint.Y);

                // Сохраняем относительное положение
                double relativeX = nearestPoint.X / canvasWidth;
                double relativeY = nearestPoint.Y / canvasHeight;
                _relativePositions[_draggedElement] = new Point(relativeX, relativeY);

                _draggedElement = null;
            }
        }



        // Изменение размера
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb thumb)
            {
                Grid grid = thumb.TemplatedParent as Grid ?? thumb.Parent as Grid;
                if (grid == null) return;

                double newWidth = Math.Max(grid.Width + e.HorizontalChange, 50);
                double newHeight = Math.Max(grid.Height + e.VerticalChange, 50);

                // Получаем текущие координаты блока
                double left = Canvas.GetLeft(grid);
                double top = Canvas.GetTop(grid);

                // Получаем ближайшую точку пересечения
                Point nearestPoint = GetNearestGridPoint(left + newWidth, top + newHeight);

                // Вычисляем новые размеры с учетом привязки
                newWidth = nearestPoint.X - left - 10;
                newHeight = nearestPoint.Y - top - 10;

                grid.Width = newWidth;
                grid.Height = newHeight;

                // Обновляем Clip
                if (grid.Clip is RectangleGeometry clip)
                {
                    clip.Rect = new Rect(0, 0, newWidth, newHeight);
                }
                else
                {
                    grid.Clip = new RectangleGeometry(new Rect(0, 0, newWidth, newHeight), 10, 10);
                }
            }
        }




        private void MainCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGridLines();
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawGridLines();

            double newWidth = e.NewSize.Width;
            double newHeight = e.NewSize.Height;

            foreach (var item in _relativePositions)
            {
                UIElement element = item.Key;
                Point relativePos = item.Value;
                Size relativeSize = _relativeSizes.ContainsKey(element) ? _relativeSizes[element] : new Size(0.2, 0.2); // Размер по умолчанию

                // Вычисляем новые абсолютные координаты и размеры
                double newX = relativePos.X * newWidth;
                double newY = relativePos.Y * newHeight;
                double newW = relativeSize.Width * newWidth;
                double newH = relativeSize.Height * newHeight;

                // Привязываем к ближайшей точке сетки
                Point nearestPoint = GetNearestGridPoint(newX, newY);

                // Устанавливаем новые параметры
                Canvas.SetLeft(element, nearestPoint.X);
                Canvas.SetTop(element, nearestPoint.Y);
                ((FrameworkElement)element).Width = newW;
                ((FrameworkElement)element).Height = newH;
            }
        }

        private void DrawGridLines() // Отрисовка сетки
        {
            if (MainCanvas == null) return;

            var savedBorders = MainCanvas.Children.OfType<Grid>().ToList();
            MainCanvas.Children.Clear(); // Очищаем, чтобы не дублировать линии

            double width = MainCanvas.ActualWidth;
            double height = MainCanvas.ActualHeight;
            int columns = 8; // Количество вертикальных линий
            int rows = 8; // Количество горизонтальных линий

            // Добавляем вертикальные линии
            for (int i = 1; i < columns; i++)
            {
                double x = width * i / columns;

                Line verticalLine = new Line
                {
                    X1 = x,
                    X2 = x,
                    Y1 = 0,
                    Y2 = height,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection { 2, 2 }
                };

                MainCanvas.Children.Add(verticalLine);
            }

            // Добавляем горизонтальные линии
            for (int i = 1; i < rows; i++)
            {
                double y = height * i / rows;

                Line horizontalLine = new Line
                {
                    X1 = 0,
                    X2 = width,
                    Y1 = y,
                    Y2 = y,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection { 2, 2 }
                };

                MainCanvas.Children.Add(horizontalLine);
            }
            // Восстанавливаем сохранённые блоки
            foreach (var border in savedBorders)
            {
                MainCanvas.Children.Add(border);
            }
        }

        private Point GetNearestGridPoint(double x, double y)
        {
            double width = MainCanvas.ActualWidth;
            double height = MainCanvas.ActualHeight;
            int columns = 8; // Количество вертикальных линий
            int rows = 8;    // Количество горизонтальных линий

            // Вычисляем шаг сетки
            double columnStep = width / columns;
            double rowStep = height / rows;

            // Находим ближайшие координаты
            double nearestX = Math.Round(x / columnStep) * columnStep;
            double nearestY = Math.Round(y / rowStep) * rowStep;

            return new Point(nearestX, nearestY);
        }
    }
}
