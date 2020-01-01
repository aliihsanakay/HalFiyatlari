using HalFiyatlari.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HalFiyatlari.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PriceList : ContentPage
    {
        public PriceList(Customer customer )
        {
            InitializeComponent();
            BindingContext = this;
            lblHalName.Text = customer.Name;
        }
    }
}