using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HexagonVisualisator
{
    /// <summary>
    /// Interaction logic for ColorDialog.xaml
    /// </summary>
    public partial class ColorDialog : Window
    {
        #region Constructors

        /// <summary>
        /// Default constructor initializes to Black.
        /// </summary>
        public ColorDialog()
          : this(Colors.Black)
        { }

        /// <summary>
        /// Constructor with an initial color.
        /// </summary>
        /// <param name="initialColor">Color to set the ColorPicker to.</param>
        public ColorDialog(Color initialColor)
        {
            InitializeComponent();
            colorPicker.InitialColor = initialColor;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets/sets the ColorDialog color.
        /// </summary>
        public Color SelectedColor
        {
            get { return colorPicker.SelectedColor; }
            set { colorPicker.InitialColor = value; }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Close ColorDialog, accepting color selection.
        /// </summary>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        ///  Close ColorDialog, rejecting color selection.
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion
    }
}
