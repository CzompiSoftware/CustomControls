using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace hu.czompisoftware.customcontrols.CuttingCorners
{
    public class Label : System.Windows.Controls.Label
    {
        static Label()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Label), new FrameworkPropertyMetadata(typeof(Label)));
            //Console.WriteLine(ColorModifier("#3F6D2A", 40));
        }
        public static String ColorModifier(String color, int amt)
        {
            var col = int.Parse(color.TrimStart('#'), System.Globalization.NumberStyles.HexNumber);
            return String.Format("#{0:X6}", (((col & 0x0000FF) + amt) | ((((col >> 8) & 0x00FF) + amt) << 8) | (((col >> 16) + amt) << 16)));
        }

        #region DogEar
        public double DogEar
        {
            get { return (double)GetValue(DogEarProperty); }
            set { SetValue(DogEarProperty, value); }
        }

        public static readonly DependencyProperty DogEarProperty = DependencyProperty.Register("DogEar", typeof(double), typeof(Label), new UIPropertyMetadata(8.0, DogEarPropertyChanged));

        private static void DogEarPropertyChanged(
            DependencyObject obj,
            DependencyPropertyChangedEventArgs e)
        {
            ((Label)obj).InvalidateVisual();
        }
        #endregion

        #region IsMirroredDogEar
        public Boolean IsMirroredDogEar
        {
            get { return (Boolean)GetValue(IsMirroredDogEarProperty); }
            set { SetValue(IsMirroredDogEarProperty, value); }
        }

        public static readonly DependencyProperty IsMirroredDogEarProperty = DependencyProperty.Register("IsMirroredDogEar", typeof(Boolean), typeof(Label), new UIPropertyMetadata(false, IsMirroredDogEarPropertyChanged));

        private static void IsMirroredDogEarPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((Label)obj).InvalidateVisual();
        }
        #endregion

        #region MergeSide
        public enum MergeSideObject
        {
            None,
            Both,
            Left,
            Right
        };
        public MergeSideObject MergeSide
        {
            get { return (MergeSideObject)GetValue(MergeSideProperty); }
            set { SetValue(MergeSideProperty, value); }
        }

        public static readonly DependencyProperty MergeSideProperty = DependencyProperty.Register("MergeSide", typeof(MergeSideObject), typeof(Label), new UIPropertyMetadata(default(MergeSideObject), MergeSidePropertyChanged));

        private static void MergeSidePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((Label)obj).InvalidateVisual();
        }
        #endregion

        public Label()
        {
            this.SizeChanged += new SizeChangedEventHandler(Label_SizeChanged);
        }

        void Label_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var clip = new PathGeometry();
            clip.Figures = new PathFigureCollection();

            #region IsMirroredDogEar
            if (IsMirroredDogEar)
            {
                var items = new List<PathSegment>();
                var start = new Point(DogEar, 0);
                switch (MergeSide)
                {
                    case MergeSideObject.Left:
                        start = new Point(0, 0);
                        items.Add(new LineSegment(new Point(this.ActualWidth, 0), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth, this.ActualHeight - DogEar), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth - DogEar, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(0, this.ActualHeight), true));
                        break;
                    case MergeSideObject.Right:
                        items.Add(new LineSegment(new Point(this.ActualWidth, 0), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(0, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(0, DogEar), true));
                        break;
                    case MergeSideObject.Both:
                        start = new Point(0, 0);
                        items.Add(new LineSegment(new Point(this.ActualWidth, 0), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(0, this.ActualHeight), true));
                        break;
                    case MergeSideObject.None:
                    default:
                        items.Add(new LineSegment(new Point(this.ActualWidth, 0), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth, this.ActualHeight - DogEar), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth - DogEar, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(0, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(0, DogEar), true));
                        break;
                }
                clip.Figures.Add(
                    new PathFigure(start, items, true)
                );
            }
            else
            {
                var items = new List<PathSegment>();
                switch (MergeSide)
                {
                    case MergeSideObject.Left:
                        items.Add(new LineSegment(new Point(this.ActualWidth - DogEar, 0), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth, DogEar), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(0, this.ActualHeight), true));
                        break;
                    case MergeSideObject.Right:
                        items.Add(new LineSegment(new Point(this.ActualWidth, 0), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(DogEar, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(0, this.ActualHeight - DogEar), true));
                        break;
                    case MergeSideObject.Both:
                        items.Add(new LineSegment(new Point(this.ActualWidth, 0), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(0, this.ActualHeight), true));
                        break;
                    case MergeSideObject.None:
                    default:
                        items.Add(new LineSegment(new Point(this.ActualWidth - DogEar, 0), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth, DogEar), true));
                        items.Add(new LineSegment(new Point(this.ActualWidth, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(DogEar, this.ActualHeight), true));
                        items.Add(new LineSegment(new Point(0, this.ActualHeight - DogEar), true));
                        break;
                }
                clip.Figures.Add(
                    new PathFigure(new Point(0, 0), items, true)
                );
            }
            #endregion

            this.Clip = clip;
        }
    }
}
