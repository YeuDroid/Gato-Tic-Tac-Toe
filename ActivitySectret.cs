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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            // Create your application here
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PierdeGato);

        }
    }
}