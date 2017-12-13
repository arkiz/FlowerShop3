
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
    [Activity(Label = "ProductList")]
    public class ProductList : Activity
    {
        ListView listtable;
        List<TableItem> tableItems = new List<TableItem>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProductList);
            // Create your application here

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
            var db = new SQLiteConnection(dpPath);
            var data = db.Table<Product>();


            var records = new List<string>();
            foreach (var listing in data)
            {
                records.Add(listing.pName + "   -   " + listing.pPrice);
                tableItems.Add(new TableItem() { str_item1 = listing.pName, str_item2 = "Quantity : (" + listing.pQty+ ")",
                    str_item3 = "type" + listing.pType,
                    str_item4 = "$"+ listing.pPrice,
                    str_item5 = listing.id.ToString()

                                            });

            }

            listtable = (ListView)FindViewById(Resource.Id.ProductList);
            //listtable.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, records.ToArray());
            listtable.Adapter = new ScreenAdapter(this, tableItems);
            listtable.ItemClick += OnListItemClick;

        }
        protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {
            //Get our item from the list adapter
            //var item = listtable.Adapter.GetItem(e.Position);

            var t = tableItems[e.Position];


            var ItemDetailActivity = new Intent(this, typeof(ItemDetail));
            ItemDetailActivity.PutExtra("prd_id", t.str_item5);
            StartActivity(ItemDetailActivity);
        }
    }
}