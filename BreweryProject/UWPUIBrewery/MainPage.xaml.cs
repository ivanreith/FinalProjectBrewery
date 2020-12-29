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

        private Ingredient ingredient;
       
        private HttpDataService service;
      

        public Ingredient Ingredient
        {
            get { return ingredient; }
            set { ingredient = value; }
        }
      
        public Supplier Selected
        {
            get { return selected; }
            set { selected = value; }
        }
        public IngredientInventoryAddition IngredientAdded
        {
            get { return ingredientAdded; }
            set { ingredientAdded = value; }// I don't think I need to modify it.
        }
        public ObservableCollection<Ingredient> Ingredients { get; private set; } = new ObservableCollection<Ingredient>();
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
          
            ClearSupplierDetails();
            EnableFields(false);
            this.supplierIdTxt.IsEnabled = true;
            EnableButtons("pageLoad");
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
                DisplaySupplierDetails();
                this.supplierIdTxt.IsEnabled = true;
                EnableButtons("found");
            }

        }

        private void DisplaySupplierDetails()
        {
            if (Selected.SupplierId.ToString() != null)
                this.supplierIdTxt.Text = Selected.SupplierId.ToString();
            else
                this.supplierIdTxt.Text = "";
            if (Selected.Name != null)
                this.supplierNameTxt.Text = Selected.Name;
            else
                this.supplierNameTxt.Text = "";
            // this.supplierAddressTxt.Text = Selected.SupplierAddress.ToString();
            if (Selected.Phone != null)
                this.supplierPhoneTxt.Text = Selected.Phone;
            else
                this.supplierPhoneTxt.Text = "";
            if (Selected.Email != null)
                this.supplierEmailTxt.Text = Selected.Email;
            else
                this.supplierEmailTxt.Text = "";
            if (Selected.Website != null)
                this.supplierWebsiteTxt.Text = Selected.Website;
            else
                this.supplierWebsiteTxt.Text = "";
            if (Selected.ContactFirstName != null)
                this.supplierContactPersonNameTxt.Text = Selected.ContactFirstName;
            else
                this.supplierContactPersonNameTxt.Text = "";
            if (Selected.ContactLastName != null)
                this.supplierContactPersonLastNameTxt.Text = Selected.ContactLastName;
            else
                this.supplierContactPersonLastNameTxt.Text = "";
            if (Selected.ContactPhone != null)
                this.supplierContactPhoneNumberTxt.Text = Selected.ContactPhone;
            else
                this.supplierContactPhoneNumberTxt.Text = "";
            if (Selected.ContactEmail != null)
                this.supplierContactEmailTxt.Text = Selected.ContactEmail;
            else
                this.supplierContactEmailTxt.Text = "";
            // int stateIndex = this.FindStateIndex(Selected.StateCode);
            // this.customerStateCBox.SelectedIndex = stateIndex;
            EnableFields(false);
            this.supplierIdTxt.IsEnabled = true;
        }
        private void ClearSupplierDetails()
        {
            this.supplierIdTxt.Text = "";
            this.supplierNameTxt.Text = "";         
            this.supplierPhoneTxt.Text = "";
            this.supplierEmailTxt.Text = "";
            this.supplierWebsiteTxt.Text = "";
            this.supplierContactPersonNameTxt.Text = "";
            this.supplierContactPersonLastNameTxt.Text = "";
            this.supplierContactPhoneNumberTxt.Text = "";
            this.supplierContactEmailTxt.Text = "";
            EnableFields(true);
           // this.supplierIdTxt.IsEnabled = true;
        }
        private void EnableFields(bool enabled = true)
        {
            this.supplierIdTxt.IsEnabled = enabled;
            this.supplierNameTxt.IsEnabled = enabled;           
            this.supplierPhoneTxt.IsEnabled = enabled;
            this.supplierEmailTxt.IsEnabled = enabled;
            this.supplierWebsiteTxt.IsEnabled = enabled;
            this.supplierContactPersonNameTxt.IsEnabled = enabled;
            this.supplierContactPersonLastNameTxt.IsEnabled = enabled;
            this.supplierContactPhoneNumberTxt.IsEnabled = enabled;
            this.supplierContactEmailTxt.IsEnabled = enabled;
            
            this.supplierIdTxt.SelectAll();
        }
        private void EnableButtons(string state)
        {
            switch (state)
            {
                case "pageLoad":
                    this.SearchSupplierBtn.IsEnabled = true;
                    this.EditSupplierBtn.IsEnabled = false;
                    this.DeleteSupplierBtn.IsEnabled = false;
                    this.CreateSupplierBtn.IsEnabled = true;
                    this.OrderHistoryBtn.IsEnabled = false;
                    this.CancelBtn.IsEnabled = false;
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
                    this.CreateSupplierBtn.IsEnabled = true;
                    this.SearchSupplierBtn.IsEnabled = true;
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
                if (await service.DeleteAsync("suppliers\\" + supplierId))
                {
                    Selected = null;
                    this.supplierIdTxt.Text = "";
                    ClearSupplierDetails();
                    EnableButtons("pageLoad");
                    var messageDialog = new MessageDialog("Supplier was deleted.");
                    await messageDialog.ShowAsync();
                }
                else
                {
                    var messageDialog = new MessageDialog("There was a problem deleting that Supplier.");
                    await messageDialog.ShowAsync();
                }
            }
        } // END DELETE
        private void CreateSupplier_Click(object sender, RoutedEventArgs e)
        {
            Selected = null;
            ClearSupplierDetails();
            this.supplierIdTxt.Text = "";    
            EnableFields(true);
            this.supplierIdTxt.IsEnabled = false;
            EnableButtons("adding");
        }// END CREATE

        private async void SaveSupplier_Click(object sender, RoutedEventArgs e)
        {
            // adding
            if (Selected == null)
            {
                Supplier newSupplier = new Supplier();
                newSupplier.Name = this.supplierNameTxt.Text;
                newSupplier.Phone = this.supplierPhoneTxt.Text;
                newSupplier.Email = this.supplierEmailTxt.Text;
                newSupplier.Website = this.supplierWebsiteTxt.Text;
                newSupplier.ContactFirstName = this.supplierContactPersonNameTxt.Text;
                newSupplier.ContactLastName = this.supplierContactPersonLastNameTxt.Text;                
                newSupplier.ContactPhone = this.supplierContactPhoneNumberTxt.Text;
                newSupplier.ContactEmail = this.supplierContactEmailTxt.Text;
                HttpResponseMessage response = await service.PostAsJsonAsync<Supplier>("suppliers", newSupplier, true);
                if (response.IsSuccessStatusCode)
                {
                    // the customer id is at the end of response.Headers.Location.AbsolutePath
                    string url = response.Headers.Location.AbsolutePath;
                    int index = url.LastIndexOf("/");
                    string supplierId = url.Substring(index + 1);
                    newSupplier.SupplierId = int.Parse(supplierId);
                    Selected = newSupplier;
                    this.supplierIdTxt.Text = Selected.SupplierId.ToString();
                    this.supplierIdTxt.IsEnabled = true;
                    DisplaySupplierDetails();
                    EnableButtons("found");
                    var messageDialog = new MessageDialog("Supplier was added.");
                    await messageDialog.ShowAsync();
                }
                else
                {
                    var messageDialog = new MessageDialog("There was a problem adding that Supplier.");
                    await messageDialog.ShowAsync();
                }
            }
            // editing
            else
            {
                Supplier updatedSupplier = new Supplier();
                updatedSupplier.SupplierId = Selected.SupplierId;
                updatedSupplier.Name = this.supplierNameTxt.Text;
                updatedSupplier.Phone = this.supplierPhoneTxt.Text;
                updatedSupplier.Email = this.supplierEmailTxt.Text;
                updatedSupplier.Website = this.supplierWebsiteTxt.Text;
                updatedSupplier.ContactFirstName = this.supplierContactPersonNameTxt.Text;
                updatedSupplier.ContactLastName = this.supplierContactPersonLastNameTxt.Text;
                updatedSupplier.ContactPhone = this.supplierContactPhoneNumberTxt.Text;
                updatedSupplier.ContactEmail = this.supplierContactEmailTxt.Text;

                if (await service.PutAsJsonAsync<Supplier>("suppliers\\" + updatedSupplier.SupplierId, updatedSupplier))
                {
                    Selected = updatedSupplier;
                    DisplaySupplierDetails();
                    this.supplierIdTxt.IsEnabled = true;
                    EnableButtons("found");
                    var messageDialog = new MessageDialog("Supplier was updated.");
                    await messageDialog.ShowAsync();
                }
                else
                {
                    var messageDialog = new MessageDialog("There was a problem updating that Supplier.");
                    await messageDialog.ShowAsync();
                }
            }
        }// End SAVE
        private async void SearchSupplier_Click(object sender, RoutedEventArgs e)
        {
            string supplierId = this.supplierIdTxt.Text;
            try
            {
                Selected = await service.GetAsync<Supplier>("suppliers\\" + supplierId, null, true);
               
                DisplaySupplierDetails();
                EnableButtons("found");

            }
            catch
            {
                var messageDialog = new MessageDialog("A Supplier with that Supplier id cannot be found.");
                await messageDialog.ShowAsync();
                Selected = null;
                ClearSupplierDetails();
                EnableFields(false);
                this.supplierIdTxt.IsEnabled = true;
            }
        }
        private void OrderHistory_Click(object sender, RoutedEventArgs e)
        {

        }//END ORDER HISTORY
        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {
            
            EnableFields(true);
            this.supplierIdTxt.IsEnabled = false;
            EnableButtons("editing");
        } //END EDIT
        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }


    }// END PARTIAL CLASS
}
