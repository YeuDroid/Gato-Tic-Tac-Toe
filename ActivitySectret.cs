using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Gato_Tic_Tac_Toe
{
    [Activity(Label = "ActivitySectret")]
    public class ActivitySectret : Activity
    {
        Button btn_salir=null;
        TextView tv_id=null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            // Create your application here
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PierdeGato);

            this.btn_salir = FindViewById<Button>(Resource.Id.btn_salir);
            this.btn_salir.Click += Btn_salir_Click;
            this.tv_id = FindViewById<TextView>(Resource.Id.tv_id);

            this.tv_id.Text = "-" +MainActivity.casillas[1].ToString() + "-" + MainActivity.casillas[2].ToString() + "-"  + MainActivity.casillas[3].ToString()
                + "-"  + MainActivity.casillas[4].ToString() + "-" + MainActivity.casillas[5].ToString() + "-" +
                MainActivity.casillas[6].ToString() + "-" + MainActivity.casillas[7].ToString() + "-" + "-" + MainActivity.casillas[8].ToString();
        }

        private void Btn_salir_Click(object sender, EventArgs e)
        {
            this.SetResult(Result.Ok);
            this.Finish();
        }
       
    }
}