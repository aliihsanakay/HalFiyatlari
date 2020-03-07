using HalFiyatlari.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public List<Product> ProductItems { get; set; }
        public PriceList(Customer customer)
        {
            InitializeComponent();
            BindingContext = this;
            lblHalName.Text = customer.Name;
            ProductItems = Product.GetTodayPriceByCustomerId(customer.Id);

            productPriceList.ItemsSource = ProductItems;
            if (ProductItems.Count == 0)
            {
                lblNoData.IsVisible = true;
                productPriceList.IsVisible = false;
            }
            else
            {

                lblNoData.IsVisible = false;
                productPriceList.IsVisible = true;
            }


        }

        private void productPriceList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
        private bool isRowEven;

        private void Cell_OnAppearing(object sender, EventArgs e)
        {
            if (this.isRowEven)
            {
                var viewCell = (ViewCell)sender;
                if (viewCell.View != null)
                {
                    viewCell.View.BackgroundColor = Color.FromHex("#dcdbd9");
                    this.isRowEven = !this.isRowEven;
                }
            }


        }

        private void btnDetail_Clicked(object sender, EventArgs e)
        {
            var button = sender as Xamarin.Forms.Button;
            var productId = Convert.ToInt32(button.CommandParameter);

            var product = ProductItems.FirstOrDefault(x => x.Id == productId);


        }
    }
}