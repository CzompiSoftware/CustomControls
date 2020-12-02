using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace hu.czompisoftware.customcontrols.CuttingCorners
{
    public class TextBox : System.Windows.Controls.TextBox
    {
        static TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
            //Console.WriteLine(ColorModifier("#3F6D2A", 40));
        }

        #region DogEar
        public double DogEar
        {
            get { return (double)GetValue(DogEarProperty); }
            set { SetValue(DogEarProperty, value); }
        }

        public static readonly DependencyProperty DogEarProperty = DependencyProperty.Register("DogEar", typeof(double), typeof(TextBox), new UIPropertyMetadata(8.0, DogEarPropertyChanged));

        private static void DogEarPropertyChanged(
            DependencyObject obj,
            DependencyPropertyChangedEventArgs e)
        {
            ((TextBox)obj).InvalidateVisual();
        }
        #endregion

        #region IsMirroredDogEar
        public bool IsMirroredDogEar
        {
            get { return (Boolean)GetValue(IsMirroredDogEarProperty); }
            set { SetValue(IsMirroredDogEarProperty, value); }
        }

        public static readonly DependencyProperty IsMirroredDogEarProperty = DependencyProperty.Register("IsMirroredDogEar", typeof(Boolean), typeof(TextBox), new UIPropertyMetadata(false, IsMirroredDogEarPropertyChanged));

        private static void IsMirroredDogEarPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((TextBox)obj).InvalidateVisual();
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

        public static readonly DependencyProperty MergeSideProperty = DependencyProperty.Register("MergeSide", typeof(MergeSideObject), typeof(TextBox), new UIPropertyMetadata(default(MergeSideObject), MergeSidePropertyChanged));

        private static void MergeSidePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((TextBox)obj).InvalidateVisual();
        }
        #endregion

        public TextBox()
        {
            this.SizeChanged += new SizeChangedEventHandler(TextBox_SizeChanged);
        }

        void TextBox_SizeChanged(object sender, SizeChangedEventArgs e)
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
