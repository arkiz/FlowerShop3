﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FlowerShop3
{
    public class ScreenAdapter : BaseAdapter<TableItem>
    {
        List<TableItem> items;
        Activity context;
        public ScreenAdapter(Activity context, List<TableItem> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override TableItem this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.list, null);
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.str_item1;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.str_item2;
            view.FindViewById<TextView>(Resource.Id.Text3).Text = item.str_item3;
            view.FindViewById<TextView>(Resource.Id.Text4).Text = item.str_item4;
            view.FindViewById<TextView>(Resource.Id.Text5).Text = item.str_item5;


            return view;
        }
    }
}