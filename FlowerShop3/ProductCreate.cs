
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace FlowerShop3
{
    [Activity(Label = "ProductCreate")]
    public class ProductCreate : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ProductCreate);

            Button btnCreateProduct = FindViewById<Button>(Resource.Id.btnCreate);
            btnCreateProduct.Click += delegate {

                try
                {
                    string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                    var db = new SQLiteConnection(dpPath);
                    db.CreateTable<Product>();

                    Product tbl = new Product();
                    tbl.pName = FindViewById<EditText>(Resource.Id.txtName).Text;
                    tbl.pType = FindViewById<EditText>(Resource.Id.txtType).Text;
                    tbl.pPrice = FindViewById<EditText>(Resource.Id.txtPrice).Text;
                    db.Insert(tbl);

                    Toast.MakeText(this, "Product added", ToastLength.Long).Show();
                    StartActivity(typeof(ProductList));
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                }

            };
        }
    }
}