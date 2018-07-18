using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Gato_Tic_Tac_Toe
{
    [Activity(Label = "Super Gato", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        int secretContador = 0;
        int secretContador2 = 0;

        ImageView[] imgCasillas = new ImageView[10];
        TextView tv_devolped = null;
        //int medida = -1; para segunda version
        int M = 1;
        int H = 2;
        public  int[] casillas = { 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
        Jurassic.ScriptEngine motorJS = new Jurassic.ScriptEngine();
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainLayout);

            // JURRASIC ENCRIPTER
            var txt_raw = Resources.GetString(Resource.String.CODEJS);
            var jsraw = Base64.Base64Decode(txt_raw);
            // message(jsraw);
            motorJS.Evaluate(jsraw);

            //ids
            imgCasillas[0] = FindViewById<ImageView>(Resource.Id.imageView1);
            //la casilla de arriba n ose usa es para precaucion de null 

            imgCasillas[1] = FindViewById<ImageView>(Resource.Id.imageView1);
            imgCasillas[2] = FindViewById<ImageView>(Resource.Id.imageView2);
            imgCasillas[3] = FindViewById<ImageView>(Resource.Id.imageView3);

            imgCasillas[4] = FindViewById<ImageView>(Resource.Id.imageView4);
            imgCasillas[5] = FindViewById<ImageView>(Resource.Id.imageView5);
            imgCasillas[6] = FindViewById<ImageView>(Resource.Id.imageView6);

            imgCasillas[7] = FindViewById<ImageView>(Resource.Id.imageView7);
            imgCasillas[8] = FindViewById<ImageView>(Resource.Id.imageView8);
            imgCasillas[9] = FindViewById<ImageView>(Resource.Id.imageView9);

            this.tv_devolped = FindViewById<TextView>(Resource.Id.tv_devolped);

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
            //quien inicia
            lanzarEscogerQuienInicia();


        }
        public void lanzarEscogerQuienInicia()
        {
            AlertDialog alerta = new AlertDialog.Builder(this).Create();
            alerta.SetTitle("Escoja");
            alerta.SetMessage("¿Quien inicia la partida?");
            alerta.SetButton("Yo,Humano", (a, b) => { message("Empiese la jugada Humano"); });
            alerta.SetButton2("Maquina", (a, b) => {
                gatoInicia();
                message("Escoja... espero tu respuesta perdedor");
            });
            alerta.Show();
        }
        public  bool yaPerdioIA()
        {
           return false;
        }
        public void guardarEStadoPerdido()
        { }
        public void applicarConfigPerdido()
        {
             
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            //Toast.MakeText(this, requestCode.ToString(), ToastLength.Short).Show();
        }
        void labeledOcupado()
        {
            message("Casilla ocupada, Usa la caveza no las patas para pensar!!!");
            //vibrar

        }
        void labeledJugando()
        {
         // message("Que siga el juego...");
        }
        void GATOIACORE()
        {
            motorJS.CallGlobalFunction("IAGatoMovs");
        }
        int quienGana()
        {
            return motorJS.CallGlobalFunction<int>("quienGana");
        }
        void accionHumanoLO(int cas)
        {
            if (estaFree(cas))
            {
                labeledJugando();
                pintaO(cas);

                //maquina
                if (logicaQuienGana() == 0)
                {
                    GATOIACORE();
                    getArray();
                    logicaQuienGana();
                }

            }
            else labeledOcupado();
        }
        private void pintaO(int casilla)
        {
            imgCasillas[casilla].SetImageResource(Resource.Drawable.o);
            casillas[casilla] = H;
            setArray();
        }
        private void pintaX(int casilla)
        {
            imgCasillas[casilla].SetImageResource(Resource.Drawable.x);
            casillas[casilla] = M;
        }
        Boolean estaFree(int casilla)
        {
            return motorJS.CallGlobalFunction<Boolean>("estaLibre", casilla);
        }
        void message(string txt)
        { Toast.MakeText(this,txt,ToastLength.Short).Show(); }
        void resetVals()
        {
            
            this.secretContador = 0;
            this.secretContador2 = 0;
            // casillas
            this.casillas[0] = 0;
            this.casillas[1] = 0;
            this.casillas[2] = 0;

            this.casillas[3] = 0;
            this.casillas[4] = 0;
            this.casillas[5] = 0;

            this.casillas[6] = 0;
            this.casillas[7] = 0;
            this.casillas[8] = 0;

            this.casillas[9] = 0;
            setArray();

            foreach (var imageView in this.imgCasillas)
            {
                imageView.SetImageResource(Resource.Drawable.nulo);
            }

            lanzarEscogerQuienInicia();

        }
        void renderImagenes()
        {

        }
        public Bitmap redimensionarImagenMaximo(Bitmap mBitmap, float newWidth, float newHeigth)
        {
            //Redimensionamos
            int width = mBitmap.Width;
            int height = mBitmap.Height;
            float scaleWidth = ((float)newWidth) / width;
            float scaleHeight = ((float)newHeigth) / height;
            // create a matrix for the manipulation
            Matrix matrix = new Matrix();
            // resize the bit map
            matrix.PostScale(scaleWidth, scaleHeight);
            // recreate the new Bitmap
            return Bitmap.CreateBitmap(mBitmap, 0, 0, width, height, matrix, true);
        }
        void pintaMaquina(int casilla)
        {
            //maquina x
            this.imgCasillas[casilla].SetImageResource(Resource.Drawable.o);

        }
        int logicaQuienGana()
        {

            //maquina
            if (quienGana() == M)
            {
                message("GANO MAQUINA");
                resetVals();
                return M;
            }

            //humano        		
            if (quienGana() == H)
            {
                message("GANO HUMANO");

                Intent intent = new Intent(this, typeof(ActivitySectret));
                StartActivityForResult(intent, 2323);
                applicarConfigPerdido();
                guardarEStadoPerdido();
                resetVals();
                return H;
            }

            if (quienGana() == 3)
            {
                message("NADIE GANO...");
                resetVals();
                return 3;
            }

            return 0;

        }
        void setArray()
        {
            motorJS.CallGlobalFunction<int>("setCasilla", 0, casillas[0]);
            motorJS.CallGlobalFunction<int>("setCasilla", 1, casillas[1]);
            motorJS.CallGlobalFunction<int>("setCasilla", 2, casillas[2]);
            motorJS.CallGlobalFunction<int>("setCasilla", 3, casillas[3]);
            motorJS.CallGlobalFunction<int>("setCasilla", 4, casillas[4]);
            motorJS.CallGlobalFunction<int>("setCasilla", 5, casillas[5]);
            motorJS.CallGlobalFunction<int>("setCasilla", 6, casillas[6]);
            motorJS.CallGlobalFunction<int>("setCasilla", 7, casillas[7]);
            motorJS.CallGlobalFunction<int>("setCasilla", 8, casillas[8]);
            motorJS.CallGlobalFunction<int>("setCasilla", 9, casillas[9]);

        }
        void gatoInicia()
        {
            motorJS.CallGlobalFunction("gatoInicia");
            getArray();

        }
        void getArray()
        {
            int[] array = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            array[0] = motorJS.CallGlobalFunction<int>("getCasilla", 0);
            array[1] = motorJS.CallGlobalFunction<int>("getCasilla", 1);
            array[2] = motorJS.CallGlobalFunction<int>("getCasilla", 2);
            array[3] = motorJS.CallGlobalFunction<int>("getCasilla", 3);
            array[4] = motorJS.CallGlobalFunction<int>("getCasilla", 4);
            array[5] = motorJS.CallGlobalFunction<int>("getCasilla", 5);
            array[6] = motorJS.CallGlobalFunction<int>("getCasilla", 6);
            array[7] = motorJS.CallGlobalFunction<int>("getCasilla", 7);
            array[8] = motorJS.CallGlobalFunction<int>("getCasilla", 8);
            array[9] = motorJS.CallGlobalFunction<int>("getCasilla", 9);
            this.casillas = array;

            if (casillas[1] == M) pintaX(1);
            if (casillas[2] == M) pintaX(2);
            if (casillas[3] == M) pintaX(3);
            if (casillas[4] == M) pintaX(4);
            if (casillas[5] == M) pintaX(5);
            if (casillas[6] == M) pintaX(6);
            if (casillas[7] == M) pintaX(7);
            if (casillas[8] == M) pintaX(8);
            if (casillas[9] == M) pintaX(9);

            if (casillas[1] == H) pintaO(1);
            if (casillas[2] == H) pintaO(2);
            if (casillas[3] == H) pintaO(3);
            if (casillas[4] == H) pintaO(4);
            if (casillas[5] == H) pintaO(5);
            if (casillas[6] == H) pintaO(6);
            if (casillas[7] == H) pintaO(7);
            if (casillas[8] == H) pintaO(8);
            if (casillas[9] == H) pintaO(9);

            logicaQuienGana();

        }
        
        private void Click1(object sender, System.EventArgs e)
        {
            analisaSecretContador();
            this.secretContador++;
            accionHumanoLO(1);
        }
        public void analisaSecretContador()
        {
            if (this.secretContador >= 3 && this.secretContador2 >= 3)
            {
                this.casillas[1] = H;
                this.casillas[2] = H;
                this.casillas[3] = H;

                setArray();
                logicaQuienGana();
            }
        }
        private void Click2(object sender, System.EventArgs e)
        {
            
            accionHumanoLO(2);
        }
        private void Click3(object sender, System.EventArgs e)
        {
            accionHumanoLO(3);
        }
        private void Click4(object sender, System.EventArgs e)
        {
            
            //accionHumanoLO(4);
        }
        private void Click5(object sender, System.EventArgs e)
        {
            analisaSecretContador();
            this.secretContador2++;
            accionHumanoLO(5);
        }
        private void Click6(object sender, System.EventArgs e)
        {
            accionHumanoLO(6);
        }
        private void Click7(object sender, System.EventArgs e)
        {
            accionHumanoLO(7);
        }
        private void Click8(object sender, System.EventArgs e)
        {
            accionHumanoLO(8);
        }
        private void Click9(object sender, System.EventArgs e)
        {
            accionHumanoLO(9);
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

