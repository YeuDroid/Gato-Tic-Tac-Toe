using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Gato_Tic_Tac_Toe
{
    public class DialogFragment1 : DialogFragment
    {
        public static DialogFragment1 NewInstance(Bundle bundle)
        {
            DialogFragment1 fragment = new DialogFragment1();
            fragment.Arguments = bundle;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.AlertLayout, container, false);
            Button button = view.FindViewById<Button>(Resource.Id.btncerrardialog);
            TextView txt_dialog = view.FindViewById<TextView>(Resource.Id.tvalertwiner);
            txt_dialog.Text = Arguments.GetString("texto_main");
            button.Click += delegate {
                Dismiss();
             };

            return view;
        }
    }
}