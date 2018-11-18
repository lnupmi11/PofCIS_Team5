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
using RealEstateBrokerage.DAL.DataTypes;
using RealEstateBrokerage.DAL.IOTypes;
using RealEstateBrokerage.BLL;

namespace RealEstateBrokerage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Instance of class wthat contain business logic.
        /// </summary>
        private RealEstateBrokerageManager _manager;

        // Search filters
        private int _currCity = -1;
        private int _currDistrict = -1;
        private double _minPrice = 0;
        private double _maxPrice = 0;
        private int _rooms = 0;
        private int _baths = 0;
        private bool _terrace = false;
        private bool _views = false;
        private bool _penthouse = false;
        private List<RealEstate> _serchResult;

        /// <summary>
        /// Constructor without parameters.
        /// </summary>
        public MainWindow()
        {
            _manager = new RealEstateBrokerageManager();
            InitializeComponent();
            CitiesCB.ItemsSource = _manager.Cities.AllCities.Select(x => x.Name);
            FillTable(_manager.RealEstate.AllRealEstate);
        }
        /// <summary>
        /// Method what handles selection of cityCB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CitiesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currCity = _manager.GetCityByName(CitiesCB.SelectedValue.ToString()).Id;
            DistrictsCB.ItemsSource = _manager.GetDistrictsByCityId(_currCity).Select(x => x.Name);
        }

        /// <summary>
        /// Method what handles selection of DistrictCB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistrictsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currDistrict = _manager.GetDistrictByName(DistrictsCB.SelectedValue.ToString()).Id;
        }

        /// <summary>
        /// Mathod that read values from all TextBoxes.
        /// </summary>
        private void ReadInput()
        {
            _minPrice = Double.Parse(MinTB.Text);
            _maxPrice = Double.Parse(MaxTB.Text);
            _rooms = Int32.Parse(RoomsTB.Text);
            _baths = Int32.Parse(BathsTB.Text);
            _views = (bool)ViewsCB.IsChecked;
            _terrace = (bool)TerraceCB.IsChecked;
            _penthouse = (bool)PenthouseCB.IsChecked;
        }
        
        /// <summary>
        /// Method wich handle find button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindBtn_Click(object sender, RoutedEventArgs e)
        {
            ReadInput();
            _serchResult = _manager.SearchRealEstates(_minPrice, _maxPrice, _currCity, _currDistrict, _views, _terrace, _penthouse);
            FillTable(_serchResult);
        }

        /// <summary>
        /// Method what fills table of flats by data of list realEstate.
        /// </summary>
        /// <param name="realEstate">List of RealEstate which contains data about flats</param>
        private void FillTable(List<RealEstate> realEstate)
        {
            Table.Items.Clear();
            foreach (var re in realEstate)
            {
                RealEstateViewModel row = new RealEstateViewModel(re.Id, _manager.GetCityById(re.CityId).Name, _manager.GetDistrictById(re.DistrictId).Name,
                  re.NumOfRooms, re.NumOfBaths, re.IsWithTerrace, re.IsWithViews, re.IsPenthouse, re.Price);
                Table.Items.Add(row);
            }
        }

        /// <summary>
        /// Method wich handle add button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddNewRealEstate tmp = new AddNewRealEstate(_manager);
            tmp.ShowDialog();
            FillTable(_manager.RealEstate.AllRealEstate);
        }

        /// <summary>
        /// Method wich handle delete button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            RealEstateViewModel realEstateView = (RealEstateViewModel)Table.SelectedItem;
            _manager.DeleteById(realEstateView.Id);
            FillTable(_manager.RealEstate.AllRealEstate);
        }
    }

    /// <summary>
    /// Class what contains data about flat that should be represented.
    /// </summary>
    public class RealEstateViewModel
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="city"></param>
        /// <param name="district"></param>
        /// <param name="numOfRooms"></param>
        /// <param name="numOfBaths"></param>
        /// <param name="isWithTerrace"></param>
        /// <param name="isWithViews"></param>
        /// <param name="isPenthouse"></param>
        /// <param name="price"></param>
        public RealEstateViewModel(int id, string city, string district, int numOfRooms, int numOfBaths, bool isWithTerrace, bool isWithViews, bool isPenthouse, double price)
        {
            Id = id;
            City = city;
            District = district;
            NumOfRooms = numOfRooms;
            NumOfBaths = numOfBaths;
            IsWithTerrace = isWithTerrace;
            IsWithViews = isWithViews;
            IsPenthouse = isPenthouse;
            Price = price;
        }

        /// <summary>
        /// Flats Id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// City where flats is situated.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// District where flats is situated.
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// Number of rooms in flat.
        /// </summary>
        public int NumOfRooms { get; set; }
        /// <summary>
        /// Number of baths in flat.
        /// </summary>
        public int NumOfBaths { get; set; }
        /// <summary>
        /// Contatins information if flat is with terrace.
        /// </summary>
        public bool IsWithTerrace { get; set; }
        /// <summary>
        /// Contatins information if flat is with view. 
        /// </summary>
        public bool IsWithViews { get; set; }
        /// <summary>
        /// Contatins information if flat is pent house.
        /// </summary>
        public bool IsPenthouse { get; set; }
        /// <summary>
        /// Price of flat.
        /// </summary>
        public double Price { get; set; }
    }

}
