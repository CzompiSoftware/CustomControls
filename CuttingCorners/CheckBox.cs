using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace hu.czompisoftware.customcontrols.CuttingCorners
{
    public class CheckBox : System.Windows.Controls.CheckBox
    {
        static CheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBox), new FrameworkPropertyMetadata(typeof(CheckBox)));
            //Console.WriteLine(ColorModifier("#3F6D2A", 40));
        }
        public double DogEar
        {
            get { return (double)GetValue(DogEarProperty); }
            set { SetValue(DogEarProperty, value); }
        }

        public static readonly DependencyProperty DogEarProperty =
            DependencyProperty.Register("DogEar",
            typeof(double),
            typeof(CheckBox),
            new UIPropertyMetadata(8.0, DogEarPropertyChanged));

        private static void DogEarPropertyChanged(
            DependencyObject obj,
            DependencyPropertyChangedEventArgs e)
        {
            ((CheckBox)obj).InvalidateVisual();
        }

        public CheckBox()
        {
            this.SizeChanged += new SizeChangedEventHandler(CheckBox_SizeChanged);
        }

        void CheckBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            /*var clip = new PathGeometry();
            clip.Figures = new PathFigureCollection();
            clip.Figures.Add(
                new PathFigure(
                    new Point(0, 0),
                    new[] {
                        new LineSegment(new Point(16 - DogEar, 0), true),
                        new LineSegment(new Point(16, DogEar), true),
                        new LineSegment(new Point(16, 16), true),
                        new LineSegment(new Point(DogEar, 16), true),
                        new LineSegment(new Point(0, 16-DogEar), true),
                    },
                    true)
            );
            this.Clip = clip;*/
        }
    }
}
