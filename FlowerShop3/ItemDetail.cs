
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
    [Activity(Label = "ItemDetail")]
    public class ItemDetail : Activity
    {
        TextView edt_prd_id;
        EditText edt_prd_nm;
        EditText edt_prd_type;
        EditText edt_prd_price;
        TextView edt_prd_qty;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ItemDetail);

            string prd_id = Intent.GetStringExtra("prd_id") ?? "Data not available";

            edt_prd_id = FindViewById<TextView>(Resource.Id.edt_prd_id);
            edt_prd_nm = FindViewById<EditText>(Resource.Id.edt_prd_nm);
            edt_prd_type = FindViewById<EditText>(Resource.Id.edt_prd_type);
            edt_prd_price = FindViewById<EditText>(Resource.Id.edt_prd_price);
            edt_prd_qty = FindViewById<TextView>(Resource.Id.edt_prd_qty);

            Toast.MakeText(this, prd_id, ToastLength.Short).Show();

            try  
            {  
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);  
                var data = db.Table < Product > (); //Call Table  
                int ID = int.Parse(prd_id);
                var data1 = data.Where(x => x.id == ID).FirstOrDefault(); //Linq Query  
                if (data1 != null)  
                {
                    Toast.MakeText(this, data1.pName, ToastLength.Short).Show();
                    edt_prd_id.Text = data1.id.ToString();
                    edt_prd_nm.Text = data1.pName;
                    edt_prd_type.Text = data1.pType;
                    edt_prd_price.Text = data1.pPrice;
                    edt_prd_qty.Text = data1.pQty;

                }  
                else  
                {  
                    Toast.MakeText(this, "Data not available", ToastLength.Short).Show();  
                }  
            }  
            catch (Exception ex)  
            {  
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();  
            }


            Button btn_plus = FindViewById<Button>(Resource.Id.btn_plus);
            Button btn_minus = FindViewById<Button>(Resource.Id.btn_minus);
            Button btn_update = FindViewById<Button>(Resource.Id.btn_update);

            int qty;
            btn_plus.Click += delegate {
                if(edt_prd_qty.Text == "") {
                    qty = 0;
                }else {
                    qty = int.Parse(edt_prd_qty.Text);
                }
                if (qty < 100)
                {
                    edt_prd_qty.Text = (qty + 1).ToString();
                }
            };

            btn_minus.Click += delegate {
                if (edt_prd_qty.Text == "")
                {
                    qty = 0;
                }
                else
                {
                    qty = int.Parse(edt_prd_qty.Text);
                }
                if (qty > 0)
                {
                    edt_prd_qty.Text = (qty - 1).ToString();
                }
            };

            btn_update.Click += delegate (object sender, EventArgs e)
            {
                updateProductQuantity(sender, e);
            };

        }

        private void updateProductQuantity(object sender, EventArgs e)
        {
            try   
            {  
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");  
                var db = new SQLiteConnection(dpPath);  
                db.CreateTable < Product > ();  
                Product tbl = new Product();
                tbl.id = int.Parse(edt_prd_id.Text);

                tbl.pName = edt_prd_nm.Text;
                tbl.pType = edt_prd_type.Text;
                tbl.pPrice = edt_prd_price.Text;
                tbl.pQty = edt_prd_qty.Text;

                db.Update(tbl);  
                Toast.MakeText(this, "Record Update Successfully...,", ToastLength.Short).Show();
                    StartActivity(typeof(MainActivity));
            } catch (Exception ex) {  
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();  
            }  
        }
    }
}
