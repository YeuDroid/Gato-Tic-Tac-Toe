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
    public class Frg_quienInica : DialogFragment
    {
        Button iniciaH = null;
        Button iniciaM = null;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            base.OnCreateView(inflater, container, savedInstanceState);
            var view =  inflater.Inflate(Resource.Layout.QuienInicia, container, false);

            this.iniciaH =  view.FindViewById<Button>(Resource.Id.btn_iniciaH);
            this.iniciaM = view.FindViewById<Button>(Resource.Id.btn_iniciaM);

            //events
            this.iniciaH.Click += IniciaH_Click;
            this.iniciaM.Click += IniciaM_Click;

            return view;

            
        }

        private void IniciaM_Click(object sender, EventArgs e)
        {
           
        }

        private void IniciaH_Click(object sender, EventArgs e)
        {
            
        }
    }
}