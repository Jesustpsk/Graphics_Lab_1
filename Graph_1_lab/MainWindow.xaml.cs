using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Graph_1_lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static double _a = 0, _phi = 0, _step = 0;
        private static bool _axes = true, _rules = true, _grid = true;
        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show("Выполнил: Кузьмин Дмитрий\nВариант: 11\nГрафик: Гиперболическая спираль",
                "Лабораторная работа №1");
        }

        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {
            StartDraw();
            ScaleTransform scaleTransform = new ScaleTransform(1.0, 1.0);
            Canvas.LayoutTransform = scaleTransform;
        }

        private void StartDraw()
        {
            if (_axes) DrawAxes();
            if (_grid) DrawElipces();
            if(_rules) DrawRule();
        }
        private Line Rotate(Line line, int angle)
        {
            var r = new Point(250, 250);
            var ang = angle;
            var angleRadian = ang * Math.PI / 180;
            return new Line
            {
                Stroke = Brushes.Black,
                X1 = (line.X1 - r.X) * Math.Cos(angleRadian) - (line.Y1 - r.Y) * Math.Sin(angleRadian) + r.X, 
                Y1 = (line.X1 - r.X) * Math.Sin(angleRadian) + (line.Y1 - r.Y) * Math.Cos(angleRadian) + r.Y, 
                X2 = (line.X2 - r.X) * Math.Cos(angleRadian) - (line.Y2 - r.Y) * Math.Sin(angleRadian) + r.X, 
                Y2 = (line.X2 - r.X) * Math.Sin(angleRadian) + (line.Y2 - r.Y) * Math.Cos(angleRadian) + r.Y,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection() { 8, 6 }
            };
        }
        private void DrawAxes()
        {
            var centerX = (int)Canvas.ActualWidth / 2;
            var centerY = (int)Canvas.ActualHeight / 2;
            var radius = Math.Min(centerX, centerY) - 10;
            
            #region VERTICALAXE
            var verticalAxis = new Line
            {
                Stroke = Brushes.Black,
                X1 = centerX,
                Y1 = centerY - radius,
                X2 = centerX,
                Y2 = centerY + radius,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection() { 8, 6 }
            };
            Canvas.Children.Add(verticalAxis);
            #endregion

            #region  HORIZONTALAXE
            var horizontalAxis = new Line
            {
                Stroke = Brushes.Black,
                X1 = centerX,
                Y1 = centerY,
                X2 = centerX + radius,
                Y2 = centerY,
                StrokeThickness = 2
            };
            Canvas.Children.Add(horizontalAxis);
            
            var horizontalArrowLeft = new Line
            {
                Stroke = Brushes.Black,
                X1 = centerX * 2 - 10,
                Y1 = centerY,
                X2 = centerX * 2 - 20,
                Y2 = centerY - 5,
                StrokeThickness = 2
            };
            Canvas.Children.Add(horizontalArrowLeft);
            
            var horizontalArrowRight = new Line
            {
                Stroke = Brushes.Black,
                X1 = centerX * 2 - 10,
                Y1 = centerY,
                X2 = centerX * 2 - 20,
                Y2 = centerY + 5,
                StrokeThickness = 2
            };
            Canvas.Children.Add(horizontalArrowRight);
            #endregion

            #region ROTATEDAXES
            for (var i = 30; i <= 150; i+=30)
            {
                Canvas.Children.Add(Rotate(verticalAxis, i));
            }
            #endregion
        }
        private void DrawElipces()
        {
            var center = new Point(Canvas.Width / 2, Canvas.Height / 2);
            double radius = 10;
            while (radius < Canvas.Width / 2 - 10)
            {
                Ellipse circle = new Ellipse
                {
                    Width = radius * 2,
                    Height = radius * 2,
                    Stroke = Brushes.Black,
                    StrokeThickness = 0.5,
                    StrokeDashArray = new DoubleCollection() { 4, 4 }
                };
                Canvas.SetLeft(circle, center.X - radius);
                Canvas.SetTop(circle, center.Y - radius);

                Canvas.Children.Add(circle);
                radius += 10;
            }
        }
        private void DrawRule()
        {
            Point centerX = new Point(Canvas.Width / 2, Canvas.Height / 2 - 5);
            #region X_AXE
            for (var i = 0; i < Canvas.Width / 2 - 20; i += 10)
            {
                var division = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = centerX.X + i,
                    Y1 = centerX.Y,
                    X2 = centerX.X + i,
                    Y2 = centerX.Y + 10,
                    StrokeThickness = 1
                };
                Canvas.Children.Add(division);
            }
            #endregion
        }
        private void DrawHyperbolicSpiral(string a, string stepCount, string step)
        {
            
            var center = new Point(Canvas.Width / 2, Canvas.Height / 2);

            if (a == "") a = "0";
            if (stepCount == "") stepCount = "0";
            if (step == "") step = "0";
            step = step.Replace('.', ',');
            double.TryParse(a, out _a);
            double.TryParse(stepCount, out _phi);
            double.TryParse(step, out _step);
            double angle =  0.1;

            var path = new Path
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };

            var pathGeometry = new PathGeometry();
            var startPoint = new Point(center.X + _a, center.Y);
            var pathFigure = new PathFigure
            {
                StartPoint = startPoint
            };
            pathGeometry.Figures.Add(pathFigure);
            path.Data = pathGeometry;
            Canvas.Children.Add(path);
            
            if(_step != 0 && _phi != 0)
                while (angle < _phi)
                {
                    var x = center.X + _a *  Math.Cos(angle)/ angle;
                    var y = center.Y - _a * Math.Sin(angle) / angle;
                    var lineSegment = new LineSegment(new Point(x, y), true);
                    pathFigure.Segments.Add(lineSegment);
  
                    angle += _step;
                }
        }

        private void CompleteChanges()
        {
            if (TextBoxA != null && TextBoxB != null && TextBoxStep != null)
            {
                if (Canvas != null)
                {
                    Canvas.Children.Clear();
                    StartDraw();
                    DrawHyperbolicSpiral(TextBoxA.Text, TextBoxB.Text, TextBoxStep.Text);
                }
            }
        }
        
        private void CompleteChanges(string alpha)
        {
            if (TextBoxA != null && TextBoxB != null && TextBoxStep != null)
            {
                if (Canvas != null)
                {
                    Canvas.Children.Clear();
                    StartDraw();
                    DrawHyperbolicSpiral(alpha, TextBoxB.Text, TextBoxStep.Text);
                }
            }
        }

        #region TEXTCHANGE
        private void TextBox_a_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Slider.Minimum = String.IsNullOrEmpty(TextBoxA.Text) ? 0 : Convert.ToInt32(TextBoxA.Text);
            if (String.IsNullOrEmpty(TextBoxA.Text) || String.IsNullOrEmpty(TextBoxB.Text) || String.IsNullOrEmpty(TextBoxStep.Text))
            {
                Slider.IsEnabled = false;
                Slider.Value = 0;
            }
            else
            {
                Slider.IsEnabled = true;
                Slider.Value = Slider.Minimum;
            }
            CompleteChanges();
        }
        private void TextBox_b_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Slider.Maximum = String.IsNullOrEmpty(TextBoxB.Text) ? 0 : Convert.ToInt32(TextBoxB.Text) * 200;
            if (String.IsNullOrEmpty(TextBoxA.Text) || String.IsNullOrEmpty(TextBoxB.Text) || String.IsNullOrEmpty(TextBoxStep.Text))
            {
                Slider.IsEnabled = false;
                Slider.Value = 0;
            }
            else
            {
                Slider.IsEnabled = true;
                Slider.Value = Slider.Minimum;
            }

            CompleteChanges();
        }
        private void TextBox_step_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(TextBoxA.Text) || String.IsNullOrEmpty(TextBoxB.Text) || String.IsNullOrEmpty(TextBoxStep.Text))
            {
                Slider.IsEnabled = false;
                Slider.Value = 0;
            }
            else
            {
                Slider.IsEnabled = true;
                Slider.Value = Slider.Minimum;
            }
            CompleteChanges();
        }
        #endregion

        #region CHECKBOX
        private void CBA_OnChecked(object sender, RoutedEventArgs e)
        {
            _axes = true;
            CompleteChanges();
        }
        private void CBA_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _axes = false;
            CompleteChanges();
        }
        private void CBR_OnChecked(object sender, RoutedEventArgs e)
        {
            _rules = true;
            CompleteChanges();
        }
        private void CBR_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _rules = false;
            CompleteChanges();
        }
        private void CBG_OnChecked(object sender, RoutedEventArgs e)
        {
            _grid = true;
            CompleteChanges();
        }
        private void CBG_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _grid = false;
            CompleteChanges();
        }
        #endregion
        
        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var alpha = Convert.ToInt32(Slider.Value).ToString();
            CompleteChanges(alpha);
        }
    }
}
