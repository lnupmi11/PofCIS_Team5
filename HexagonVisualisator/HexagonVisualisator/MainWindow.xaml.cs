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
using System.Threading;

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
        /// Saves points for new hexagon.
        /// </summary>
        private Hexagon _newHexagon;

        /// <summary>
        /// Saves selected hexagon.
        /// </summary>
        private Hexagon _selectedHexagon;

        /// <summary>
        /// Indicates if left mouse click is new point.
        /// </summary>
        private bool _isAddingNewHexagon;

        /// <summary>
        /// Indicates if hexagon coordinates will be changed.
        /// </summary>
        private bool _isChangingCordinates;

        /// <summary>
        /// Indicates if hexagon should move after mouse.
        /// </summary>
        private bool _isMovingMode;

        /// <summary>
        /// Allows to pick color.
        /// </summary>
        private ColorDialog _colorDialog;

        private Thread _movingMode;

        /// <summary>
        /// Method what initialize main window.
        /// </summary>
        public MainWindow()
        {
            _hexagons = new List<Hexagon>();
            _hexagonSerializer = new HexagonSerializer();
            _isAddingNewHexagon = false;
            _isChangingCordinates = false;
            _isMovingMode = false;
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
            AddNewHexagonButton.IsEnabled = true;
            SavePaintButton.IsEnabled = true;
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
            AddNewHexagonButton.IsEnabled = true;
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
                ClearCanvasPanel();
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
                Polygon p = new Polygon();
                p.Points = new PointCollection();
                for (int j = 0; j < _hexagons[i].Vertexes.Count; j++)
                {
                    p.Points.Add(new System.Windows.Point(_hexagons[i].Vertexes[j % 6].X, _hexagons[i].Vertexes[j % 6].Y));
                }
                p.Fill = new SolidColorBrush(_hexagons[i].Color);
                this.MainCanvas.Children.Add(p);
            }
        }

        /// <summary>
        /// Processes clicks on canvas.
        /// </summary>
        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = Mouse.GetPosition(this.MainCanvas);
            HexagonVisualisatorDAL.Models.Point point = new HexagonVisualisatorDAL.Models.Point(p.X, p.Y);
            if( _isAddingNewHexagon == true )
            {
                if( _newHexagon == null )
                {
                    _newHexagon = new Hexagon();
                }
                _newHexagon.Vertexes.Add(point);
                if(_newHexagon.Vertexes.Count == 6)
                {
                    _hexagons.Add(_newHexagon);

                    _colorDialog = new ColorDialog();
                    _colorDialog.Owner = this;
                    if(_colorDialog.ShowDialog() == true)
                    {
                        _newHexagon.Color = _colorDialog.SelectedColor;
                    }
                    else
                    {
                        _hexagons.RemoveAt(_hexagons.Count - 1);
                    }
                    PrintHexagons();
                    FeedContextMenu();
                    CancelAllActions();
                }
            }
            else
            {
                if(_isChangingCordinates == true)
                {
                    MoveSelecteHexagon(p);
                }
                if(_isMovingMode == true)
                {
                    _isMovingMode = false;
                    _movingMode.Join();
                    EnableButtons();
                    AddNewHexagonButton.IsEnabled = true;
                }
                else
                {
                    var hex = FindPolygonByPoint(p);
                    if (hex != null)
                    {
                        _selectedHexagon = hex;
                        EnableButtons();
                    }
                }
            }
        }

        /// <summary>
        /// Processes creation of new hexagon.
        /// </summary>
        private void AddNewHexagonButton_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            CancelActionButton.IsEnabled = true;
            _isAddingNewHexagon = true; 
            _selectedHexagon = null;
        }

        /// <summary>
        /// Processes color change of hexagon.
        /// </summary>
        private void ChangeColorOfHexagonButton_Click(object sender, RoutedEventArgs e)
        {
            _colorDialog = new ColorDialog();
            _colorDialog.Owner = this;
            if(_colorDialog.ShowDialog() == true)
            {
                _selectedHexagon.Color = _colorDialog.SelectedColor;
            }
            else
            {
                CancelAllActions();     
            }
            PrintHexagons();
        }

        /// <summary>
        /// Processes changing of coordinates for hexagon.
        /// </summary>
        private void ChangeCordinatesOfHexagonButton_Click(object sender, RoutedEventArgs e)
        {
            _isChangingCordinates = true;
            CancelActionButton.IsEnabled = true;
        }

        /// <summary>
        /// Processes hexagon deletion.
        /// </summary>
        private void DeleteExistingButton_Click(object sender, RoutedEventArgs e)
        {
            _hexagons.Remove(_selectedHexagon);
            PrintHexagons();
            FeedContextMenu();
            _selectedHexagon = null;
            DisableButtons();
        }

        /// <summary>
        /// Diselects hexagon.
        /// </summary>
        private void CancelActionButton_Click(object sender, RoutedEventArgs e)
        {
            CancelAllActions();
        }

        /// <summary>
        /// Clears all progress.
        /// </summary>
        private void CancelAllActions()
        {
            if (_movingMode != null && _movingMode.IsAlive)
            {
                _isMovingMode = false;
                _movingMode.Join();
                EnableButtons();
                AddNewHexagonButton.IsEnabled = true;
            }
            DisableButtons();
            _isAddingNewHexagon = false;
            _isChangingCordinates = false;
            _selectedHexagon = null;
            _newHexagon = null;
        }

        /// <summary>
        /// Disables buttons for hexagon changes.
        /// </summary>
        private void DisableButtons()
        {
            ChangeColorOfHexagonButton.IsEnabled = false;
            ChangeCordinatesOfHexagonButton.IsEnabled = false;
            DeleteExistingButton.IsEnabled = false;
            CancelActionButton.IsEnabled = false;
        }

        /// <summary>
        /// Enables buttons for hexagon changes.
        /// </summary>
        private void EnableButtons()
        {
            AddNewHexagonButton.IsEnabled = true;
            ChangeColorOfHexagonButton.IsEnabled = true;
            ChangeCordinatesOfHexagonButton.IsEnabled = true;
            DeleteExistingButton.IsEnabled = true;
        }

        /// <summary>
        /// Finds selected polygon.
        /// </summary>
        private Hexagon FindPolygonByPoint(System.Windows.Point p)
        {
            foreach(var hex in _hexagons)
            {
                if(hex.IsInPolygon(new HexagonVisualisatorDAL.Models.Point(p.X, p.Y)) == true)
                {
                    return hex;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds moves polygon center to new point.
        /// </summary>
        private void MoveSelecteHexagon(System.Windows.Point p)
        {
            var center = _selectedHexagon.GetCentroid();
            var dx = p.X - center.X;
            var dy = p.Y - center.Y;
            foreach (var v in _selectedHexagon.Vertexes)
            {
                v.X += dx;
                v.Y += dy;
            }
            _isChangingCordinates = false;
            PrintHexagons();
        }

        /// <summary>
        /// Dynamically populates context menu with shapes on canvas.
        /// </summary>
        private void FeedContextMenu()
        {
            ShapesButton.ContextMenu.Items.Clear();
            for(int i = 0; i < _hexagons.Count; ++i)
            {
                MenuItem newMenuItem = new MenuItem();
                newMenuItem.Header = "Hexagon " + i;
                newMenuItem.Click += ContextMenuItem_Click;
                ShapesButton.ContextMenu.Items.Add(newMenuItem);
            }
        }

        /// <summary>
        /// Handles clicks from context menu.
        /// </summary>
        private void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem obMenuItem = e.OriginalSource as MenuItem;
            if( obMenuItem != null )
            {
                int hexIndex = Int32.Parse(obMenuItem.Header.ToString().Split(' ')[1]);
                _selectedHexagon = _hexagons[hexIndex];
                EnableButtons();
                _isMovingMode = true;
                DisableButtons();
                AddNewHexagonButton.IsEnabled = false;
                CancelActionButton.IsEnabled = true;
                _movingMode = new Thread(StartMovingMode);
                _movingMode.SetApartmentState(ApartmentState.STA);
                _movingMode.Start();
            }
        }

        /// <summary>
        /// Gets mouse position on window.
        /// </summary>
        private System.Windows.Point GetMousePositionWindowsForms()
        {
            System.Windows.Point pointToWindow = Mouse.GetPosition(this);
            return pointToWindow;
        }

        /// <summary>
        /// Goes into separete thread for hexagon movement.
        /// </summary>
        private void StartMovingMode()
        {
            while(_isMovingMode == true)
            {
                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    MoveSelecteHexagon(GetMousePositionWindowsForms());
                    PrintHexagons();
                }));
                Thread.Sleep(100);
            }
        }
    }
}
