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
           

            List<Customer> list = new List<Customer>();
            Customer c = new Customer();
            c.Code = "07";
            c.Name = "Antalya";
            list.Add(c);
            Customer c1 = new Customer();
            c1.Code = "34";
            c1.Name = "İstanbul BB Hali";
            list.Add(c1);

            Customer c2 = new Customer();
            c2.Code = "80";
            c2.Name = "Yalova Büyükşehir Hali";
            list.Add(c2);
            list.Add(c1);
            list.Add(c1);
            list.Add(c);
            list.Add(c);
            MyListView.ItemsSource = list;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            await Navigation.PushModalAsync(new PriceList(e.Item as Customer));
        }
    }
}
