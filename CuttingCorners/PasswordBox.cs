using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace hu.czompisoftware.customcontrols.CuttingCorners
{
    public class PasswordBox : System.Windows.Controls.TextBox
    {
        #region Member Variables
        /// <summary>
        ///   Dependency property to hold watermark for PasswordBox
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
          DependencyProperty.Register(
            "Password", typeof(SecureString), typeof(PasswordBox), new UIPropertyMetadata(new SecureString()));

        public static readonly DependencyProperty HiddenTextProperty =
          DependencyProperty.Register(
            "HiddenText", typeof(string), typeof(PasswordBox), new UIPropertyMetadata(string.Empty));

        /// <summary>
        ///   Private member holding mask visibile timer
        /// </summary>
        private readonly DispatcherTimer _maskTimer;
        #endregion

        static PasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBox), new FrameworkPropertyMetadata(typeof(PasswordBox)));
            //Console.WriteLine(ColorModifier("#3F6D2A", 40));
        }

        #region DogEar
        public double DogEar
        {
            get { return (double)GetValue(DogEarProperty); }
            set { SetValue(DogEarProperty, value); }
        }

        public static readonly DependencyProperty DogEarProperty = DependencyProperty.Register("DogEar", typeof(double), typeof(PasswordBox), new UIPropertyMetadata(8.0, DogEarPropertyChanged));

        private static void DogEarPropertyChanged(
            DependencyObject obj,
            DependencyPropertyChangedEventArgs e)
        {
            ((PasswordBox)obj).InvalidateVisual();
        }
        #endregion

        #region IsMirroredDogEar
        public bool IsMirroredDogEar
        {
            get { return (Boolean)GetValue(IsMirroredDogEarProperty); }
            set { SetValue(IsMirroredDogEarProperty, value); }
        }

        public static readonly DependencyProperty IsMirroredDogEarProperty = DependencyProperty.Register("IsMirroredDogEar", typeof(Boolean), typeof(PasswordBox), new UIPropertyMetadata(false, IsMirroredDogEarPropertyChanged));

        private static void IsMirroredDogEarPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PasswordBox)obj).InvalidateVisual();
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

        public static readonly DependencyProperty MergeSideProperty = DependencyProperty.Register("MergeSide", typeof(MergeSideObject), typeof(PasswordBox), new UIPropertyMetadata(default(MergeSideObject), MergeSidePropertyChanged));

        private static void MergeSidePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PasswordBox)obj).InvalidateVisual();
        }
        #endregion

        #region SizeChanged
        void PasswordBox_SizeChanged(object sender, SizeChangedEventArgs e)
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
        #endregion

        #region Constructors
        /// <summary>
        ///   Initialises a new instance of the LifeStuffPasswordBox class.
        /// </summary>
        public PasswordBox()
        {
            PreviewTextInput += OnPreviewTextInput;
            PreviewKeyDown += OnPreviewKeyDown;
            SizeChanged += PasswordBox_SizeChanged;
            CommandManager.AddPreviewExecutedHandler(this, PreviewExecutedHandler);
            _maskTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            _maskTimer.Tick += (sender, args) => MaskAllDisplayText();
        }
        #endregion

        #region Commands & Properties
        /// <summary>
        ///   Gets or sets dependency Property implementation for Password
        /// </summary>
        public SecureString Password
        {
            get
            {
                return (SecureString)GetValue(PasswordProperty);
            }

            set
            {
                SetValue(PasswordProperty, value);
            }
        }

        public string HiddenText
        {
            get
            {
                return (string)GetValue(HiddenTextProperty);
            }

            set
            {
                SetValue(HiddenTextProperty, value);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        ///   Method to handle PreviewExecutedHandler events
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="executedRoutedEventArgs">Event Text Arguments</param>
        private static void PreviewExecutedHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Command == ApplicationCommands.Copy ||
                executedRoutedEventArgs.Command == ApplicationCommands.Cut ||
                executedRoutedEventArgs.Command == ApplicationCommands.Paste)
            {
                executedRoutedEventArgs.Handled = true;
            }
        }

        /// <summary>
        ///   Method to handle PreviewTextInput events
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="textCompositionEventArgs">Event Text Arguments</param>
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs textCompositionEventArgs)
        {
            AddToSecureString(textCompositionEventArgs.Text);
            textCompositionEventArgs.Handled = true;
        }

        /// <summary>
        ///   Method to handle PreviewKeyDown events
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="keyEventArgs">Event Text Arguments</param>
        private void OnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            Key pressedKey = keyEventArgs.Key == Key.System ? keyEventArgs.SystemKey : keyEventArgs.Key;
            switch (pressedKey)
            {
                case Key.Space:
                    AddToSecureString(" ");
                    keyEventArgs.Handled = true;
                    break;
                case Key.Back:
                case Key.Delete:
                    if (SelectionLength > 0)
                    {
                        RemoveFromSecureString(SelectionStart, SelectionLength);
                    }
                    else if (pressedKey == Key.Delete && CaretIndex < Text.Length)
                    {
                        RemoveFromSecureString(CaretIndex, 1);
                    }
                    else if (pressedKey == Key.Back && CaretIndex > 0)
                    {
                        int caretIndex = CaretIndex;
                        if (CaretIndex > 0 && CaretIndex < Text.Length)
                            caretIndex = caretIndex - 1;
                        RemoveFromSecureString(CaretIndex - 1, 1);
                        CaretIndex = caretIndex;
                    }

                    keyEventArgs.Handled = true;
                    break;
            }
        }

        /// <summary>
        ///   Method to add new text into SecureString and process visual output
        /// </summary>
        /// <param name="text">Text to be added</param>
        private void AddToSecureString(string text)
        {
            if (SelectionLength > 0)
            {
                RemoveFromSecureString(SelectionStart, SelectionLength);
            }

            foreach (char c in text)
            {
                int caretIndex = CaretIndex;
                Password.InsertAt(caretIndex, c);
                HiddenText = HiddenText.Insert(caretIndex, c.ToString());
                MaskAllDisplayText();
                if (caretIndex == Text.Length)
                {
                    _maskTimer.Stop();
                    _maskTimer.Start();
                    Text = Text.Insert(caretIndex++, c.ToString());
                }
                else
                {
                    Text = Text.Insert(caretIndex++, "*");
                }
                CaretIndex = caretIndex;
            }
        }

        /// <summary>
        ///   Method to remove text from SecureString and process visual output
        /// </summary>
        /// <param name="startIndex">Start Position for Remove</param>
        /// <param name="trimLength">Length of Text to be removed</param>
        private void RemoveFromSecureString(int startIndex, int trimLength)
        {
            int caretIndex = CaretIndex;
            for (int i = 0; i < trimLength; ++i)
            {
                Password.RemoveAt(startIndex);
                HiddenText = HiddenText.Remove(startIndex, 1);
            }

            Text = Text.Remove(startIndex, trimLength);
            CaretIndex = caretIndex;
        }

        private void MaskAllDisplayText()
        {
            _maskTimer.Stop();
            int caretIndex = CaretIndex;
            Text = new string('*', Text.Length);
            CaretIndex = caretIndex;
        }
        #endregion
    }
}