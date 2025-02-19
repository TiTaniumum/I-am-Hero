using System;
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

                // Получаем текущие координаты блока
                double left = Canvas.GetLeft(_draggedElement);
                double top = Canvas.GetTop(_draggedElement);

                // Привязываем к ближайшей точке сетки
                Point nearestPoint = GetNearestGridPoint(left, top);

                // Устанавливаем новые координаты блока
                Canvas.SetLeft(_draggedElement, nearestPoint.X);
                Canvas.SetTop(_draggedElement, nearestPoint.Y);

                // Сбрасываем переменную _draggedElement
                _draggedElement = null;
            }
        }


        // Изменение размера
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb thumb && thumb.Parent is Border block)
            {
                double newWidth = block.Width + e.HorizontalChange;
                double newHeight = block.Height + e.VerticalChange;

                block.Width = newWidth > 100 ? newWidth : 100; // Минимальная ширина
                block.Height = newHeight > 100 ? newHeight : 100; // Минимальная высота
            }
        }

        private void MainCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGridLines();
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawGridLines();
        }

        private void DrawGridLines()
        {
            if (MainCanvas == null) return;

            var savedBorders = MainCanvas.Children.OfType<Border>().ToList();
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
