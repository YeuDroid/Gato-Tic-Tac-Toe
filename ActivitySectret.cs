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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            // Create your application here
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PierdeGato);

            this.btn_salir = FindViewById<Button>(Resource.Id.btn_salir);
            this.btn_salir.Click += Btn_salir_Click;
        }

        private void Btn_salir_Click(object sender, EventArgs e)
        {
            this.SetResult(Result.Ok);
            this.Finish();
        }
       
    }
}