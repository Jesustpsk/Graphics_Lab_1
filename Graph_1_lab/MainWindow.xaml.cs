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
        private static bool _axes = true, _rules = true, _grid = true, _designations = true;
        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show("Выполнил: Кузьмин Дмитрий\nВариант: 11\nГрафик: Гиперболическая спираль",
                "Лабораторная работа №1");
        }

        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {
            StartDraw();
        }

        private void StartDraw()
        {
            if (_axes)
            {
                DrawAxes();
                DrawAxes2();
            }

            if (_grid)
            {
                DrawElipces();
                DrawGrid();
            }

            if (_rules)
            {
                DrawRule();
                DrawRule2();
            }

            if (_designations)
            {
                DrawDesignations();
                DrawDesignations2();
            }
            
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
        private void DrawAxes2()
        {
            var centerX = (int)Canvas2.ActualWidth / 2;
            var centerY = (int)Canvas2.ActualHeight / 2;
            var radius = Math.Min(centerX, centerY) - 10;
            
            #region VERTICALAXE
            var verticalAxis = new Line
            {
                Stroke = Brushes.Black,
                X1 = centerX,
                Y1 = centerY - radius,
                X2 = centerX,
                Y2 = centerY + radius,
                StrokeThickness = 2,
            };
            Canvas2.Children.Add(verticalAxis);
            
            var verticalArrowLeft = new Line
            {
                Stroke = Brushes.Black,
                X1 = centerX,
                Y1 = 10,
                X2 = centerX - 5,
                Y2 = 20,
                StrokeThickness = 2
            };
            Canvas2.Children.Add(verticalArrowLeft);
            
            var verticalArrowRight = new Line
            {
                Stroke = Brushes.Black,
                X1 = centerX,
                Y1 = 10,
                X2 = centerX + 5,
                Y2 = 20,
                StrokeThickness = 2
            };
            Canvas2.Children.Add(verticalArrowRight);
            #endregion

            #region  HORIZONTALAXE
            var horizontalAxis = new Line
            {
                Stroke = Brushes.Black,
                X1 = centerX - radius,
                Y1 = centerY,
                X2 = centerX + radius,
                Y2 = centerY,
                StrokeThickness = 2
            };
            Canvas2.Children.Add(horizontalAxis);
            
            var horizontalArrowLeft = new Line
            {
                Stroke = Brushes.Black,
                X1 = centerX * 2 - 10,
                Y1 = centerY,
                X2 = centerX * 2 - 20,
                Y2 = centerY - 5,
                StrokeThickness = 2
            };
            Canvas2.Children.Add(horizontalArrowLeft);
            
            var horizontalArrowRight = new Line
            {
                Stroke = Brushes.Black,
                X1 = centerX * 2 - 10,
                Y1 = centerY,
                X2 = centerX * 2 - 20,
                Y2 = centerY + 5,
                StrokeThickness = 2
            };
            Canvas2.Children.Add(horizontalArrowRight);
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
        private void DrawGrid()
        {
            #region VERTICALGRID
            for (var i = 10; i < Canvas2.Width; i += 10)
            {
                var line = new Line()
                {
                    Stroke = Brushes.Black,
                    X1 = i,
                    Y1 = 10,
                    X2 = i,
                    Y2 = Canvas2.Height - 10,
                    StrokeThickness = 0.5,
                    StrokeDashArray = new DoubleCollection() { 4, 4 }
                };
                Canvas2.Children.Add(line);
            }
            #endregion

            #region HORIZONTALGRID
            for (var i = 10; i < Canvas2.Height; i += 10)
            {
                var line = new Line()
                {
                    Stroke = Brushes.Black,
                    X1 = 10,
                    Y1 = i,
                    X2 = Canvas2.Width - 10,
                    Y2 = i,
                    StrokeThickness = 0.5,
                    StrokeDashArray = new DoubleCollection() { 4, 4 }
                };
                Canvas2.Children.Add(line);
            }
            #endregion
        }
        private void DrawRule()
        {
            var centerX = new Point(Canvas.Width / 2, Canvas.Height / 2 - 5);
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
        private void DrawRule2()
        {
            var center = new Point(Canvas.Width / 2, Canvas.Height / 2);
            #region VERTICALRULE
            for (var i = 20; i < Canvas2.Width - 20; i += 10)
            {
                var line = new Line()
                {
                    Stroke = Brushes.Black,
                    X1 = i,
                    Y1 = center.Y - 5,
                    X2 = i,
                    Y2 = center.Y + 5,
                    StrokeThickness = 1
                };
                Canvas2.Children.Add(line);
            }
            #endregion

            #region HORIZONTALRULE
            for (var i = 30; i < Canvas2.Height - 10; i += 10)
            {
                var line = new Line()
                {
                    Stroke = Brushes.Black,
                    X1 = center.X - 5,
                    Y1 = i,
                    X2 = center.X + 5,
                    Y2 = i,
                    StrokeThickness = 1
                };
                Canvas2.Children.Add(line);
            }
            #endregion
        }
        private void DrawDesignations()
        {
            var r0 = new TextBlock()
            {
                FontSize = 12,
                Text = "0"
            };
            Canvas.SetTop(r0, 225);
            Canvas.SetRight(r0, 10);
            Canvas.Children.Add(r0);
            
            var r30 = new TextBlock()
            {
                FontSize = 12,
                Text = "30"
            };
            Canvas.SetTop(r30, 110);
            Canvas.SetRight(r30, 30);
            Canvas.Children.Add(r30);
            
            var r60 = new TextBlock()
            {
                FontSize = 12,
                Text = "60"
            };
            Canvas.SetTop(r60, 23);
            Canvas.SetRight(r60, 120);
            Canvas.Children.Add(r60);
            
            var r90 = new TextBlock()
            {
                FontSize = 12,
                Text = "90"
            };
            Canvas.SetTop(r90, 0);
            Canvas.SetRight(r90, 253);
            Canvas.Children.Add(r90);
            
            var r120 = new TextBlock()
            {
                FontSize = 12,
                Text = "120"
            };
            Canvas.SetTop(r120, 23);
            Canvas.SetLeft(r120, 120);
            Canvas.Children.Add(r120);
            
            var r150 = new TextBlock()
            {
                FontSize = 12,
                Text = "150"
            };
            Canvas.SetTop(r150, 110);
            Canvas.SetLeft(r150, 30);
            Canvas.Children.Add(r150);
            
            var r180 = new TextBlock()
            {
                FontSize = 12,
                Text = "180"
            };
            Canvas.SetTop(r180, 225);
            Canvas.SetLeft(r180, 0);
            Canvas.Children.Add(r180);
            
            var r210 = new TextBlock()
            {
                FontSize = 12,
                Text = "210"
            };
            Canvas.SetBottom(r210, 110);
            Canvas.SetLeft(r210, 30);
            Canvas.Children.Add(r210);
            
            var r240 = new TextBlock()
            {
                FontSize = 12,
                Text = "240"
            };
            Canvas.SetBottom(r240, 23);
            Canvas.SetLeft(r240, 120);
            Canvas.Children.Add(r240);
            
            var r270 = new TextBlock()
            {
                FontSize = 12,
                Text = "270"
            };
            Canvas.SetBottom(r270, 0);
            Canvas.SetLeft(r270, 253);
            Canvas.Children.Add(r270);
            
            var r300 = new TextBlock()
            {
                FontSize = 12,
                Text = "300"
            };
            Canvas.SetBottom(r300, 23);
            Canvas.SetRight(r300, 120);
            Canvas.Children.Add(r300);
            
            var r330 = new TextBlock()
            {
                FontSize = 12,
                Text = "330"
            };
            Canvas.SetBottom(r330, 110);
            Canvas.SetRight(r330, 30);
            Canvas.Children.Add(r330);
        }
        private void DrawDesignations2()
        {
            //
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
            var startPoint = new Point(center.X + _a * Math.Cos(angle) / angle, center.Y - _a * Math.Sin(angle) / angle);
            angle += _step;
            var pathFigure = new PathFigure
            {
                StartPoint = startPoint
            };
            pathGeometry.Figures.Add(pathFigure);
            path.Data = pathGeometry;
            Canvas.Children.Add(path);
            if(_step != 0 && _phi != 0 && _phi > 0)
                while (angle < _phi)
                {
                    var x = center.X + _a *  Math.Cos(angle)/ angle;
                    var y = center.Y - _a * Math.Sin(angle) / angle;
                    var lineSegment = new LineSegment(new Point(x, y), true);
                    pathFigure.Segments.Add(lineSegment);
  
                    angle += _step;
                }
        }
        private void DrawHyperbolicSpiral2(string a, string stepCount, string step)
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
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };

            var pathGeometry = new PathGeometry();
            var startPoint = new Point(center.X + _a * Math.Cos(angle) / angle, center.Y - _a * Math.Sin(angle) / angle);
            angle += _step;
            var pathFigure = new PathFigure
            {
                StartPoint = startPoint
            };
            pathGeometry.Figures.Add(pathFigure);
            path.Data = pathGeometry;
            Canvas2.Children.Add(path);
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
                if (Canvas2 != null)
                {
                    Canvas2.Children.Clear();
                    StartDraw();
                    DrawHyperbolicSpiral2(TextBoxA.Text, TextBoxB.Text, TextBoxStep.Text);
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
                if (Canvas2 != null)
                {
                    Canvas2.Children.Clear();
                    StartDraw();
                    DrawHyperbolicSpiral2(alpha, TextBoxB.Text, TextBoxStep.Text);
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
            if (Int32.TryParse(TextBoxB.Text, out int num))
            {
                Slider.Maximum = String.IsNullOrEmpty(TextBoxB.Text) ? 0 : Convert.ToInt32(TextBoxB.Text) * 200;
                if (String.IsNullOrEmpty(TextBoxA.Text) || String.IsNullOrEmpty(TextBoxB.Text) ||
                    String.IsNullOrEmpty(TextBoxStep.Text))
                {
                    Slider.IsEnabled = false;
                    Slider.Value = 0;
                }
                else
                {
                    Slider.IsEnabled = true;
                    Slider.Value = Slider.Minimum;
                }
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
        private void CBD_OnChecked(object sender, RoutedEventArgs e)
        {
            _designations = true;
            CompleteChanges();
        }

        private void CBD_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _designations = false;
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
