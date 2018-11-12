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
using RealEstateBrokerage.DAL.DataTypes;
using RealEstateBrokerage.DAL.IOTypes;
using RealEstateBrokerage.BLL;

namespace RealEstateBrokerage
{
    /// <summary>
    /// Interaction logic for AddNewRealEstate.xaml
    /// </summary>
    public partial class AddNewRealEstate : Window
    {
        private RealEstateBrokerageManager _manager;
        private string _currCity;
        private string _currDistrict;
        private double _price = 0;
        private int _rooms = 0;
        private int _baths = 0;
        private bool _terrace = false;
        private bool _views = false;
        private bool _penthouse = false;


        public AddNewRealEstate(RealEstateBrokerageManager mn)
        {
            _manager = mn;
            InitializeComponent();
        }

        private void ReadInput()
        {
            _price = Double.Parse(Price.Text);
            _rooms = Int32.Parse(RoomsCount.Text);
            _baths = Int32.Parse(BathroomsCount.Text);
            _views = (bool)ViewsCB.IsChecked;
            _terrace = (bool)TerraceCB.IsChecked;
            _penthouse = (bool)PenthouseCB.IsChecked;
            _currCity = City.Text;
            _currDistrict = District.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReadInput();
            _manager.AddNewAccommodation(_price,_rooms,_baths,_views,_terrace,_penthouse,_currCity,_currDistrict);
            this.Close();
        }
    }
}
