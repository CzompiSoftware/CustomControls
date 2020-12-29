using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace hu.czompisoftware.customcontrols.CuttingCorners
{
    public class ToggleButton : System.Windows.Controls.Primitives.ToggleButton
    {
        static ToggleButton()
        {
        }

        #region HoverBackground property
        private static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register(
                "HoverBackground", typeof(Brush), typeof(ToggleButton),
                new PropertyMetadata(default(Brush)));

        public Brush? HoverBackground
        {
            get => (Brush?)GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }
        #endregion

    }
}
