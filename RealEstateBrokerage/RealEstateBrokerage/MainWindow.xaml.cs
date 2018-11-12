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

namespace RealEstateBrokerage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CitiesDB _cities;
        private DistrictsDB _districts;
        private RealEstateDB _realEstate;


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

        public MainWindow()
        {
            InitializeComponent();
            _cities = new CitiesDB("../../DAL/InputData/CitiesData.txt");
            _districts = new DistrictsDB("../../DAL/InputData/DistrictsData.txt");
            _realEstate = new RealEstateDB("../../DAL/InputData/RealEstateData.txt");
            CitiesCB.ItemsSource = _cities.AllCities.Select(x => x.Name);
            FillTable(_realEstate.AllRealEstate);
        }

        private void CitiesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currCity = _cities.GetCityByName(CitiesCB.SelectedValue.ToString()).Id;
            DistrictsCB.ItemsSource = _districts.GetDistrictsByCityId(_currCity).Select(x => x.Name);
        }

        private void DistrictsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currDistrict = _districts.GetDistrictByName(DistrictsCB.SelectedValue.ToString()).Id;
        }

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
        private void FindBtn_Click(object sender, RoutedEventArgs e)
        {
            ReadInput();
            _serchResult = _realEstate.AllRealEstate.Where(x => x.Price >= _minPrice && x.Price <= _maxPrice
            && x.CityId == _currCity && x.DistrictId == _currDistrict && x.IsWithViews == _views
            && x.IsWithTerrace == _terrace && x.IsPenthouse == _penthouse).ToList();
            FillTable(_serchResult);
        }

        private void FillTable(List<RealEstate> realEstate)
        {
            Table.Items.Clear();
            foreach (var re in realEstate)
            {
                RealEstateViewModel row = new RealEstateViewModel(re.Id, _cities.GetCityById(re.CityId).Name, _districts.GetDistrictById(re.DistrictId).Name,
re.NumOfRooms, re.NumOfBaths, re.IsWithTerrace, re.IsWithViews, re.IsPenthouse, re.Price);
                Table.Items.Add(row);
            }
        }
    }

    public class RealEstateViewModel
    {
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

        public int Id { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int NumOfRooms { get; set; }
        public int NumOfBaths { get; set; }
        public bool IsWithTerrace { get; set; }
        public bool IsWithViews { get; set; }
        public bool IsPenthouse { get; set; }
        public double Price { get; set; }
    }

}
