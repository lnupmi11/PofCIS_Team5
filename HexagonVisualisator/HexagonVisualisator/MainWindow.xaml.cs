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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using HexagonVisualisatorDAL.Serializers;
using HexagonVisualisatorDAL.Models;

namespace HexagonVisualisator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Serializer for saving data about our hexagons in xml format. 
        /// </summary>
        private HexagonSerializer _hexagonSerializer;
        
        /// <summary>
        /// List of displayed hexagons.
        /// </summary>
        private List<Hexagon> _hexagons;

        /// <summary>
        /// Method what initialize main window.
        /// </summary>
        public MainWindow()
        {
            _hexagons = new List<Hexagon>();
            _hexagonSerializer = new HexagonSerializer();
            InitializeComponent();
        }

        /// <summary>
        /// Method what occurres when New button is clicked and clear canvas window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New_Paint_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvasPanel();
        }

        /// <summary>
        /// Method what occurres when Open button is clicked and open OpenFileDialog window to choose existing print.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Paint_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "(*.xml)|*.xml",
                RestoreDirectory = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Title = "Choose file"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                _hexagons = _hexagonSerializer.Deserialize(openFileDialog.FileName);
            }
            if (_hexagons != null)
            {
                PrintHexagons();
            }
        }

        /// <summary>
        /// Method what occurres when Save button is clicked and open SaveFileDialog window to save current print.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Paint_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                RestoreDirectory = true,
                DefaultExt = "xml",
                CheckPathExists = true,
                Title = "Save your work",
                ValidateNames = true
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                _hexagonSerializer.Serialize(_hexagons, saveFileDialog.FileName);
            }
        }

        /// <summary>
        /// Method what clear canvas.
        /// </summary>
        private void ClearCanvasPanel()
        {
            this.MainCanvas.Children.Clear();
            this.MainCanvas.Background = new SolidColorBrush(Colors.LightGray);
        }

        /// <summary>
        /// Method for temp initialize default hexagons.
        /// </summary>
        private void TestData()
        {
            _hexagons.Add(new Hexagon(new HexagonVisualisatorDAL.Models.Point(100, 100), new HexagonVisualisatorDAL.Models.Point(100, 10), 30));
            _hexagons.Add(new Hexagon(new HexagonVisualisatorDAL.Models.Point(150, 150), new HexagonVisualisatorDAL.Models.Point(150, 200), 40));
            _hexagons.Add(new Hexagon(new HexagonVisualisatorDAL.Models.Point(200, 200), new HexagonVisualisatorDAL.Models.Point(300, 300), 50));
        }

        /// <summary>
        /// Method wich redraw all hexagons.
        /// </summary>
        private void PrintHexagons()
        {
            ClearCanvasPanel();
            this.MainCanvas.Children.Clear();
            for (int i = 0; i < _hexagons.Count; i++)
            {
                for (int j = 0; j < _hexagons[i].Vertexes.Count - 1; j++)
                {
                    Line line = new Line();
                    line.Stroke = Brushes.Red;
                    line.X1 = _hexagons[i].Vertexes[j].X;
                    line.X2 = _hexagons[i].Vertexes[j + 1].X;
                    line.Y1 = _hexagons[i].Vertexes[j].Y;
                    line.Y2 = _hexagons[i].Vertexes[j + 1].Y;
                    line.StrokeThickness = 2;
                    this.MainCanvas.Children.Add(line);
                }
            }
        }

        // TODO: proccess click on canvas. 
        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = Mouse.GetPosition(this.MainCanvas);
            HexagonVisualisatorDAL.Models.Point point = new HexagonVisualisatorDAL.Models.Point(p.X, p.Y);
        }

        private void AddNewHexagonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeColorOfHexagonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeCordinatesOfHexagonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteExistingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelActionButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
