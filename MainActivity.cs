using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace Gato_Tic_Tac_Toe
{
    [Activity(Label = "Super Gato", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        ImageView[] imgCasillas = new ImageView[10];
        int medida = -1;
        int M = 1;
        int H = 2;
        int[] casillas = { 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
        Jurassic.ScriptEngine motorJS = new Jurassic.ScriptEngine();
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainLayout);
            //ids
            imgCasillas[0] = FindViewById<ImageView>(Resource.Id.imageView1);
            imgCasillas[1] = FindViewById<ImageView>(Resource.Id.imageView1);
            imgCasillas[2] = FindViewById<ImageView>(Resource.Id.imageView2);
            imgCasillas[3] = FindViewById<ImageView>(Resource.Id.imageView3);

            imgCasillas[4] = FindViewById<ImageView>(Resource.Id.imageView4);
            imgCasillas[5] = FindViewById<ImageView>(Resource.Id.imageView5);
            imgCasillas[6] = FindViewById<ImageView>(Resource.Id.imageView6);

            imgCasillas[7] = FindViewById<ImageView>(Resource.Id.imageView7);
            imgCasillas[8] = FindViewById<ImageView>(Resource.Id.imageView8);
            imgCasillas[9] = FindViewById<ImageView>(Resource.Id.imageView9);

            //events
            this.imgCasillas[1].Click += Click1;
            this.imgCasillas[2].Click += Click2;
            this.imgCasillas[3].Click += Click3;

            this.imgCasillas[4].Click += Click4;
            this.imgCasillas[5].Click += Click5;
            this.imgCasillas[6].Click += Click6;

            this.imgCasillas[7].Click += Click7;
            this.imgCasillas[8].Click += Click8;
            this.imgCasillas[9].Click += Click9;

            //

            int ancho = this.getAnchoPantalla();
            int medida = (ancho / 3) - 5;

            //no render mas de 1024
            if (medida >= 1024) medida = 1024;


            //
            foreach (var imageView in this.imgCasillas)
            {
                imageView.SetImageResource(Resource.Drawable.nulo);
            }
            //

        }

        void resetValues()
        {

        }
        void renderImagenes()
        {

        }
        void pintaHumano(int casilla)
        {
            //humano o
            this.imgCasillas[casilla].SetImageResource(Resource.Drawable.o);
        }
        void pintaMaquina(int casilla)
        {
            //maquina x
            this.imgCasillas[casilla].SetImageResource(Resource.Drawable.o);

        }
        private void Click1(object sender, System.EventArgs e)
        {
            pintaHumano(1);
        }
        private void Click2(object sender, System.EventArgs e)
        {
            pintaHumano(2);
        }
        private void Click3(object sender, System.EventArgs e)
        {
            pintaHumano(3);
        }
        private void Click4(object sender, System.EventArgs e)
        {
            pintaHumano(4);
        }
        private void Click5(object sender, System.EventArgs e)
        {
            pintaHumano(5);
        }
        private void Click6(object sender, System.EventArgs e)
        {
            pintaHumano(6);
        }
        private void Click7(object sender, System.EventArgs e)
        {
            pintaHumano(7);
        }
        private void Click8(object sender, System.EventArgs e)
        {
            pintaHumano(8);
        }
        private void Click9(object sender, System.EventArgs e)
        {
            pintaHumano(9);
        }
        int getAnchoPantalla()
        {
            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            int width = metrics.WidthPixels; // ancho absoluto en pixels 
            int height = metrics.HeightPixels; // alto absoluto en pixels 

            return width;
        }
    }
}

