using HalFiyatlari.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HalFiyatlari.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
   
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Customer> Items { get; set; }
        public MainPage()
        {
            InitializeComponent();


           
           
                MyListView.ItemsSource = (Customer.GetCustomer());
          
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            await Navigation.PushModalAsync(new PriceList(e.Item as Customer));
        }
    }
}
