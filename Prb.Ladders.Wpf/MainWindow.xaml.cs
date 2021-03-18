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
using Prb.Ladders.Core;
using System.Linq;
namespace Prb.Ladders.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<Ladder> ladders;
        List<Ladder> filteredLadders;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoSeeding();
            PopulateListbox();           
            DoStats();
            PopulateCombobox();
            cmbBrand.SelectedIndex = 0;
            rdbAll.IsChecked = true;
        }
        private void DoSeeding()
        {
            ladders = new List<Ladder>();
            ladders.Add(new Ladder("Alfa", 1, 10, 1.8, 55M, 3));
            ladders.Add(new Ladder("Beta", 1, 10, 1.8, 65M, 2));
            ladders.Add(new Ladder("Tango", 1, 10, 1.8, 75M, 3));
            ladders.Add(new Ladder("Alfa", 2, 10, 2.9, 99.99M, 5));
            ladders.Add(new Ladder("Beta", 2, 10, 2.9, 107.99M, 1));
            ladders.Add(new Ladder("Tango", 2, 10, 2.9, 120M, 5));
            ladders.Add(new Ladder("Alfa", 3, 10, 4.1, 125M, 3));
            ladders.Add(new Ladder("Beta", 3, 10, 4.1, 140M, 4));
            ladders.Add(new Ladder("Tango", 3, 10, 4.1, 160M, 5));
            ladders.Add(new Ladder("Alfa", 1, 12, 2.1, 65M, 4));
            ladders.Add(new Ladder("Beta", 1, 12, 2.1, 75M, 4));
            ladders.Add(new Ladder("Tango", 1, 12, 2.1, 85M, 1));
            ladders.Add(new Ladder("Alfa", 2, 12, 3.5, 109M, 0));
            ladders.Add(new Ladder("Beta", 2, 12, 3.5, 117M, 1));
            ladders.Add(new Ladder("Tango", 2, 12, 3.5, 130M, 5));
            ladders.Add(new Ladder("Alfa", 3, 12, 4.9, 135M, 3));
            ladders.Add(new Ladder("Beta", 3, 12, 4.9, 150M, 1));
            ladders.Add(new Ladder("Tango", 3, 12, 4.9, 170M, 7));
            ladders = ladders.OrderBy(l => l.Brand).ThenBy(l => l.Sections).ThenBy(l => l.StepsPerSection).ToList();
        }
        private void PopulateListbox()
        {
            ClearControls();
            filteredLadders = new List<Ladder>();
            string brandFilter = "";
            if (cmbBrand.SelectedIndex > 0)
            {
                brandFilter = cmbBrand.SelectedItem.ToString();
            }
            int sectionFilter = 0;
            if (rdb1.IsChecked == true) sectionFilter = 1;
            else if (rdb2.IsChecked == true) sectionFilter = 2;
            else if (rdb3.IsChecked == true) sectionFilter = 3;

            filteredLadders = ladders;
            if(brandFilter != "")
                filteredLadders = ladders.Where(l => l.Brand == brandFilter).ToList();
            if (sectionFilter != 0)
                filteredLadders = filteredLadders.Where(l => l.Sections == sectionFilter).ToList();

            //foreach(Ladder ladder in ladders)
            //{
            //    if (brandFilter == "")
            //    {                    
            //        if(sectionFilter == 0)
            //        {
            //            filteredLadders.Add(ladder);
            //        }
            //        else
            //        {
            //            if(ladder.Sections == sectionFilter)
            //            {
            //                filteredLadders.Add(ladder);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (brandFilter == ladder.Brand)
            //        {
            //            if (sectionFilter == 0)
            //            {
            //                filteredLadders.Add(ladder);
            //            }
            //            else
            //            {
            //                if (ladder.Sections == sectionFilter)
            //                {
            //                    filteredLadders.Add(ladder);
            //                }
            //            }
            //        }
            //    }
            //}

            lstLadders.ItemsSource = null;
            lstLadders.ItemsSource = filteredLadders;
        }
        private void ClearControls()
        {
            btnSale.Visibility = Visibility.Hidden;
            lblBrand.Content = "";
            lblMaxHeight.Content = "";
            lblSalePrice.Content = "";
            lblSections.Content = "";
            lblStepsPerSection.Content = "";
            lblStock.Content = "";
            lblStockValue.Content = "";
        }
        private void PopulateControls(Ladder ladder)
        {
            lblBrand.Content = ladder.Brand;
            lblMaxHeight.Content = ladder.MaxHeight;
            lblSalePrice.Content = ladder.SalePrice.ToString("€#,##0.00");
            lblSections.Content = ladder.Sections;
            lblStepsPerSection.Content = ladder.StepsPerSection;
            lblStock.Content = ladder.Stock;
            lblStockValue.Content = (ladder.Stock * ladder.SalePrice).ToString("€#,##0.00");
            if(ladder.Stock > 0)
            {
                btnSale.Visibility = Visibility.Visible;
            }
        }
        private void DoStats()
        {
            int totalStock = 0;
            decimal totalStockValue = 0M;
            foreach(Ladder ladder in ladders)
            {
                totalStock += ladder.Stock;
                totalStockValue += (ladder.Stock * ladder.SalePrice);
            }
            lblTotalStock.Content = totalStock;
            lblTotalStockValue.Content = totalStockValue.ToString("€#,##0.00");
        }
        private void lstLadders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if(lstLadders.SelectedItem != null)
            {
                Ladder ladder =(Ladder)lstLadders.SelectedItem;
                PopulateControls(ladder);
            }
        }


        private void PopulateCombobox()
        {
            List<string> brandNames = new List<string>();
            brandNames.Add("<alle merken>");
            foreach(Ladder ladder in ladders)
            {
                bool found = false;
                foreach(string brand in brandNames)
                {
                    if(brand == ladder.Brand)
                    {
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    brandNames.Add(ladder.Brand);
                }
            }
            cmbBrand.ItemsSource = brandNames;
        }
        private void cmbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateListbox();
        }

        private void rdbAll_Checked(object sender, RoutedEventArgs e)
        {
            PopulateListbox();
        }

        private void rdb1_Checked(object sender, RoutedEventArgs e)
        {
            PopulateListbox();
        }

        private void rdb2_Checked(object sender, RoutedEventArgs e)
        {
            PopulateListbox();
        }

        private void rdb3_Checked(object sender, RoutedEventArgs e)
        {
            PopulateListbox();
        }

        private void btnSale_Click(object sender, RoutedEventArgs e)
        {
            if(lstLadders.SelectedItem != null)
            {
                Ladder ladder = (Ladder)lstLadders.SelectedItem;
                ladder.Stock--;

                lblStock.Content = ladder.Stock;
                lblStockValue.Content = (ladder.Stock * ladder.SalePrice).ToString("€#,##0.00");

                DoStats();

                if (ladder.Stock <= 0)
                    btnSale.Visibility = Visibility.Hidden;

            }
        }
    }
}
