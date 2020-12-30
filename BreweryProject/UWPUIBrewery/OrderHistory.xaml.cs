using BreweryLibraryClasses.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPUIBrewery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderHistory : Page
    {
        private HttpDataService service2;

        public IngredientInventoryAddition ViewModel { get; set; }
        public string supplierIdCompare;
        public ObservableCollection<IngredientInventoryAddition> IngredientsAdditions { get; private set; } = new ObservableCollection<IngredientInventoryAddition>();
        public List<IngredientInventoryAddition> IngredientsAdditions2 { get; private set; } = new List<IngredientInventoryAddition>();
      


        public OrderHistory()
        {
            
            this.InitializeComponent();
             this.ViewModel = new IngredientInventoryAddition();
        }
      
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           


        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            supplierIdCompare = e.Parameter.ToString();
            if (supplierIdCompare is string && !string.IsNullOrWhiteSpace(supplierIdCompare))
            {
                // greeting.Text = $"Hi, {e.Parameter.ToString()}";
               
                service2 = new HttpDataService("http://localhost:5000/api");
                // list ready for order history

                List<IngredientInventoryAddition> ingredientsAdditions = await service2.GetAsync<List<IngredientInventoryAddition>>("ingredientinventoryadditions");
                foreach (IngredientInventoryAddition s in ingredientsAdditions)
                    if (supplierIdCompare == s.SupplierId.ToString())
                    {
                      this.IngredientsAdditions.Add(s);
                    }
                //end order history stuff

              


            }
            else
            {
               // greeting.Text = "Hi!";
            }
            base.OnNavigatedTo(e);
        }



        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
            //this.close ;
        }
        
    }
}
