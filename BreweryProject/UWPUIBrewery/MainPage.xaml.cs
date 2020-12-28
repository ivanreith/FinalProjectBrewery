using System;
using System.Collections.Generic;
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
using UWPUIBrewery;

using System.Collections.ObjectModel;
using Windows.UI.Popups;
using System.Net.Http;
using BreweryLibraryClasses.Models;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPUIBrewery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Supplier selected = null;
        private IngredientInventoryAddition ingredientAdded;
        private HttpDataService service;
        public Supplier Selected
        {
            get { return selected; }
            set { selected = value; }
        }
        public IngredientInventoryAddition IngredientAdded
        {
            get { return ingredientAdded; }
            set { ingredientAdded = value; }
        }
        public ObservableCollection<Supplier> Suppliers { get; private set; } = new ObservableCollection<Supplier>();
        public ObservableCollection<IngredientInventoryAddition> IngredientsAdditions { get; private set; } = new ObservableCollection<IngredientInventoryAddition>();

        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            service = new HttpDataService("http://localhost:5000/api");
            List<Supplier> suppliers = await service.GetAsync<List<Supplier>>("Suppliers");
            foreach (Supplier s in suppliers)
                this.Suppliers.Add(s);
            //ClearCustomerDetails();
           // EnableFields(false);
           // EnableButtons("pageLoad");
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // adding
            if (Selected == null)
            {
                this.supplierIdTxt.IsEnabled = true;
                ClearSupplierDetails();
                EnableFields(true);
                EnableButtons("pageLoad");
            }
            // editing
            else
            {
                DisplayProviderDetails();
                this.supplierIdTxt.IsEnabled = true;
                EnableButtons("found");
            }

        }

        private void DisplayProviderDetails()
        {
            this.supplierIdTxt.Text = Selected.SupplierId.ToString();
            this.supplierNameTxt.Text = Selected.Name;
            this.supplierAddressTxt.Text = Selected.SupplierAddress.ToString();
            this.supplierPhoneTxt.Text = Selected.Phone;
            this.supplierWebsiteTxt.Text = Selected.Website;
            this.supplierContactPersonNameTxt.Text = Selected.ContactFirstName + Selected.ContactLastName;
           // int stateIndex = this.FindStateIndex(Selected.StateCode);
           // this.customerStateCBox.SelectedIndex = stateIndex;
            EnableFields(false);
        }
        private void ClearSupplierDetails()
        {
            this.supplierIdTxt.Text = "";
            this.supplierNameTxt.Text = "";
            this.supplierAddressTxt.Text = "";
            this.supplierPhoneTxt.Text = "";
            this.supplierEmailTxt.Text = "";
            this.supplierWebsiteTxt.Text = "";
            this.supplierContactPersonNameTxt.Text = "";
            this.supplierContactPersonLastNameTxt.Text = "";
            this.supplierContactPhoneNumberTxt.Text = "";
            this.supplierContactEmailTxt.Text = "";
            EnableFields(false);
        }
        private void EnableFields(bool enabled = true)
        {
            this.supplierIdTxt.IsEnabled = enabled;
            this.supplierNameTxt.IsEnabled = enabled;
            this.supplierAddressTxt.IsEnabled = enabled;
            this.supplierEmailTxt.IsEnabled = enabled;
            this.supplierWebsiteTxt.IsEnabled = enabled;
            this.supplierContactPersonNameTxt.IsEnabled = enabled;
            this.supplierContactPersonLastNameTxt.IsEnabled = enabled;
            this.supplierContactPhoneNumberTxt.IsEnabled = enabled;
            this.supplierContactEmailTxt.IsEnabled = enabled;
        }
        private void EnableButtons(string state)
        {
            switch (state)
            {
                case "pageLoad":
                    this.SearchSupplierBtn.IsEnabled = false;
                    this.EditSupplierBtn.IsEnabled = false;
                    this.DeleteSupplierBtn.IsEnabled = false;
                    this.CreateSupplierBtn.IsEnabled = false;
                    this.OrderHistoryBtn.IsEnabled = false;
                    this.CancelBtn.IsEnabled = true;
                    this.SaveSupplierBtn.IsEnabled = false;
                    break;
                case "editing":
                case "adding":
                    this.DeleteSupplierBtn.IsEnabled = false;
                    this.EditSupplierBtn.IsEnabled = false;
                    this.CreateSupplierBtn.IsEnabled = false;
                    this.SearchSupplierBtn.IsEnabled = false;
                    this.SaveSupplierBtn.IsEnabled = true;
                    this.CancelBtn.IsEnabled = true;
                    this.OrderHistoryBtn.IsEnabled = false;
                    break;
                case "found":
                    this.OrderHistoryBtn.IsEnabled = true;
                    this.DeleteSupplierBtn.IsEnabled = true;
                    this.EditSupplierBtn.IsEnabled = true;
                    this.CreateSupplierBtn.IsEnabled = false;
                    this.SearchSupplierBtn.IsEnabled = false;
                    this.SaveSupplierBtn.IsEnabled = false;
                    this.CancelBtn.IsEnabled = false;
                    break;
            }

        }
        private async void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (Selected != null)
            {
                int supplierId = Selected.SupplierId;
                if (await service.DeleteAsync("customers\\" + supplierId))
                {
                    Selected = null;
                    this.supplierIdTxt.Text = "";
                    ClearSupplierDetails();
                    EnableButtons("pageLoad");
                    var messageDialog = new MessageDialog("Customer was deleted.");
                    await messageDialog.ShowAsync();
                }
                else
                {
                    var messageDialog = new MessageDialog("There was a problem deleting that Provider.");
                    await messageDialog.ShowAsync();
                }
            }
        }
        private void CreateSupplier_Click(object sender, RoutedEventArgs e)
        { 
        
        }

        private void SaveSupplier_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SearchSupplier_Click(object sender, RoutedEventArgs e)
        {

        }
        private void OrderHistory_Click(object sender, RoutedEventArgs e)
        {

        }
        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {

        }
        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }


    }// END PARTIAL CLASS
}
