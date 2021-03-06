﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Content.PM;

namespace Gato_Tic_Tac_Toe
{

    [Activity(Label = "Super Gato", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]

    public class MainActivity : Activity
    {
        int medida = -1;
        int secretContador = 0;
        int secretContador2 = 0;

        //version delayed
        System.Threading.Thread hilo = null;
        Boolean lockActionHuman = false;
        //version delayed

        ImageView[] imgCasillas = new ImageView[10];
        TextView tv_devolped = null;
        Button btn_rst_juego = null;
        
        //int medida = -1; para segunda version
        int M = 1;
        int H = 2;
        
        public static int[] casillas = { 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
        string jsraw = null;
        Jurassic.ScriptEngine motorJS = new Jurassic.ScriptEngine();
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainLayout);

            // JURRASIC ENCRIPTER
            jsraw = CloudFilePtovider.GetFileFromCloud();
            if (jsraw == CloudFilePtovider.ErrorTag)
            {
                jsraw = CloudFilePtovider.GetEnebedFile(this);
                if(jsraw == CloudFilePtovider.ErrorTag)
                {
                    //jsraw = CloudFilePtovider.GetCacheFile();
                }
            }
            // message(jsraw);
            IniciaJurassic();

            //ids
            this.btn_rst_juego = FindViewById<Button>(Resource.Id.btn_reset);
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
            this.btn_rst_juego.Click += Btn_rst_juego_Click;
            this.tv_devolped.LongClick += Tv_devolped_LongClick;

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
            medida = (ancho / 3) - 5;

            //no render mas de 1024
            if (medida >= 1024) medida = 1024;
            this.hilo = new System.Threading.Thread(delayerHilo);

            //
            foreach (var imageView in this.imgCasillas)
            {
                Square.Picasso.Picasso.With(this).Load(Resource.Drawable.nulo).Resize(medida, medida).Into(imageView);
            }
            //quien inicia
            lanzarEscogerQuienInicia();

        }
        public void IniciaJurassic()
        {
            motorJS.Evaluate(jsraw);
        }
        private void Tv_devolped_LongClick(object sender, Android.Views.View.LongClickEventArgs e)
        {
            AlertDialog alerta = new AlertDialog.Builder(this).Create();
            alerta.SetTitle("Information");
           // alerta.SetIcon(Resource.Drawable.gato);
            alerta.SetMessage("JS CODE VERSION: " + GetCodeVersion() + " GUI VERSION: 0.87 -B COMPATIBLE ");
            
            alerta.SetButton2("ok", (a, b) =>
            {
                ;
            });
            alerta.Show();
        }
        private void Btn_rst_juego_Click(object sender, EventArgs e)
        {


            AlertDialog alerta = new AlertDialog.Builder(this).Create();
            alerta.SetTitle("Cobarde");
            //alerta.SetIcon(Resource.Drawable.gato);
            alerta.SetMessage("¿Quieres realmente reiniciar la partida?");
            alerta.SetButton("Si", (a, b) =>
            {
                resetVals();
                message("Juego reiniciado...");
            });
            alerta.SetButton2("No, cancelar", (a, b) =>
            {
                ;
            });
            alerta.Show();


        }
        public string GetCodeVersion()
        {
            try
            {
                var resultado = motorJS.GetGlobalValue<String>("VersionCode");
                if (resultado == "undefined") throw new Exception();
                else return resultado;
            }
            catch (Exception) { return "NOT B SERIES COMPATIBLE"; }

        }
        public void lanzarEscogerQuienInicia()
        {
            
            AlertDialog alerta = new AlertDialog.Builder(this).Create();
            alerta.SetTitle("Escoja");
          //  alerta.SetIcon(Resource.Drawable.gato);
            alerta.SetMessage("¿Quien inicia la partida?");
            alerta.SetButton("Yo,Humano", (a, b) => { message("Empiese la jugada Humano"); });
            alerta.SetButton2("Maquina", (a, b) => {
                gatoInicia();
                message("Escoja... espero tu respuesta perdedor");
            });
            alerta.Show();
        }
        public bool yaPerdioIA()
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
        public  override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            message(requestCode.ToString());
        }
        private void checkPermissions(string[] PERMISSIONS)
        {
            

            foreach (string permiso in PERMISSIONS)
            {
                if (ApplicationContext.CheckSelfPermission(permiso) == Android.Content.PM.Permission.Denied)
                {
                    //solicitud de permiso
                    RequestPermissions(PERMISSIONS, 4000);
            
                }
                else
                {
                    message(permiso + " rechazado");
                    // permiso ya dado
                }
            }


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
        int quienGanak()
        {
            return motorJS.CallGlobalFunction<int>("quienGana");
        }
        void accionHumanoLO(int cas)
        {
            if (this.lockActionHuman) return;

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
            //   imgCasillas[casilla].SetImageResource(Resource.Drawable.o);
            Square.Picasso.Picasso.With(this).Load(Resource.Drawable.o).Resize(medida, medida).Into(imgCasillas[casilla]);
            casillas[casilla] = H;
            setArray();
        }
        private void pintaONeg(int casilla)
        {
            //   imgCasillas[casilla].SetImageResource(Resource.Drawable.o);
            Square.Picasso.Picasso.With(this).Load(Resource.Drawable.o_neg).Resize(medida, medida).Into(imgCasillas[casilla]);
            casillas[casilla] = H;
            setArray();
        }
        private void pintaNulos(int casilla)
        {
            Square.Picasso.Picasso.With(this).Load(Resource.Drawable.nulo).Resize(medida, medida).Into(imgCasillas[casilla]);
        }
        private void pintaX(int casilla)
        {
            //  imgCasillas[casilla].SetImageResource(Resource.Drawable.x);
            Square.Picasso.Picasso.With(this).Load(Resource.Drawable.x).Resize(medida, medida).Into(imgCasillas[casilla]);
            casillas[casilla] = M;
        }
        private void pintaXNeg(int casilla)
        {
            //  imgCasillas[casilla].SetImageResource(Resource.Drawable.x);
            Square.Picasso.Picasso.With(this).Load(Resource.Drawable.x_neg).Resize(medida, medida).Into(imgCasillas[casilla]);
            casillas[casilla] = M;
        }
        Boolean estaFree(int casilla)
        {
            return motorJS.CallGlobalFunction<Boolean>("estaLibre", casilla);
        }
        void message(string txt)
        { Toast.MakeText(this, txt, ToastLength.Short).Show(); }
        void resetVals()
        {
            this.lockActionHuman = false;
            this.secretContador = 0;
            this.secretContador2 = 0;
            // casillas
            casillas[0] = 8;

            casillas[1] = 0;
            casillas[2] = 0;
            casillas[3] = 0;

            casillas[4] = 0;
            casillas[5] = 0;
            casillas[6] = 0;

            casillas[7] = 0;
            casillas[8] = 0;
            casillas[9] = 0;

            //setArray();
            IniciaJurassic();
            foreach (var imageView in this.imgCasillas)
            {
                Square.Picasso.Picasso.With(this).Load(Resource.Drawable.nulo).Resize(medida, medida).Into(imageView);
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
        public void delayerHilo()
        {
            //System.Threading.Thread.Sleep(1000);
            //message("alive");
            //resetVals();
        }
        int logicaQuienGana()
        {
            var estado = quienGana();
            if (estado == 1)
            {
                
            }
            //maquina
            if (estado == M)
            {
                messageAlert("HA GANO LA INTELIGENCIA ARTIFICAL (MAQUINA), HAS PERDIDO HUMANO, ERES UNA VERGUENZA PARA TU RAZA.");

                // debe pausar el codigo 

                resetVals();
                return M;
            }

            //humano        		
            if (estado == H)
            {
                messageAlert("VAYA VAYA HAS PODIDO HAYAR EL ERROR EN EL PROGRAMA, O ERES SUPER INTELIGENTE, ERES UN DIGNO OPONENTE PARA MI CPU");

                Intent intent = new Intent(this, typeof(ActivitySectret));
                StartActivityForResult(intent, 2323);
                applicarConfigPerdido();
                guardarEStadoPerdido();
                resetVals();
                return H;
            }

            if (estado == 3)
            {
                messageAlert("NADIE HA GANADO Y NO CREO QUE ME GANES TE FALTAN MUCHAS NEURONAS PARA QUE PASE ESO...");
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
        public void messageAlert(string txt)
        {
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            //Remove fragment else it will crash as it is already added to backstack
            Fragment prev = FragmentManager.FindFragmentByTag("dialog");
            if (prev != null)
            {
                ft.Remove(prev);
            }

            ft.AddToBackStack(null);

            // Create and show the dialog.
            Bundle bundle = new Bundle();
            bundle.PutString("texto_main",txt);
            DialogFragment1 newFragment = DialogFragment1.NewInstance(bundle);

            //Add fragment
            newFragment.Show(ft, "dialog");
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
            casillas = array;

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
         //   var permisos = new String[] { Android.Manifest.Permission.WriteExternalStorage };
           // checkPermissions(permisos);

            accionHumanoLO(1);
        }
        public void analisaSecretContador()
        {
            if (this.secretContador >= 3 && this.secretContador2 >= 3)
            {
                casillas[1] = H;
                casillas[2] = H;
                casillas[3] = H;

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
            accionHumanoLO(4);
        }
        private void Click5(object sender, System.EventArgs e)
        {
            //analisaSecretContador();
            //this.secretContador2++;
            
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
        public int quienGana()
        {    //delado
                if (casillas[1] == 1 && casillas[2] == 1 && casillas[3] == 1)
               {
                pintaXNeg(1);
                pintaXNeg(2);
                pintaXNeg(3);
                return M;
               }

                if (casillas[4] == 1 && casillas[5] == 1 && casillas[6] == 1)
              {

                this.lockActionHuman = true;

                //new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                //    RunOnUiThread(() => {

                //        System.Threading.Thread.Sleep(500);
                pintaXNeg(4);
                pintaXNeg(5);
                pintaXNeg(6);
                //    });
                //})).Start();

                return M;
            }

            if (casillas[7] == 1 && casillas[8] == 1 && casillas[9] == 1)
            {

                this.lockActionHuman = true;

                //new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                //    RunOnUiThread(() => {

                //        System.Threading.Thread.Sleep(500);
                pintaXNeg(7);
                pintaXNeg(8);
                pintaXNeg(9);
                //    });
                //})).Start();

                return M;
            }

            //acostadas

            if (casillas[1] == 1 && casillas[4] == 1 && casillas[7] == 1)
            {
                this.lockActionHuman = true;

                //new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                //    RunOnUiThread(() => {

                //        System.Threading.Thread.Sleep(500);
                pintaXNeg(1);
                pintaXNeg(4);
                pintaXNeg(7);
                //    });
                //})).Start();

                return M;
            }

            if (casillas[2] == 1 && casillas[5] == 1 && casillas[8] == 1)
            {

                this.lockActionHuman = true;

                //new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                //    RunOnUiThread(() => {

                //        System.Threading.Thread.Sleep(500);
                pintaXNeg(2);
                pintaXNeg(5);
                pintaXNeg(8);
                //    });
                //})).Start();

                return M;
            }

            if (casillas[3] == 1 && casillas[6] == 1 && casillas[9] == 1)
            {

                this.lockActionHuman = true;

                //new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                //    RunOnUiThread(() => {

                //        System.Threading.Thread.Sleep(500);
                pintaXNeg(3);
                pintaXNeg(6);
                pintaXNeg(9);
                //    });
                //})).Start();

                return M;
            }

            //diagonales

            if (casillas[3] == 1 && casillas[5] == 1 && casillas[7] == 1)
            {

                this.lockActionHuman = true;

                // new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                //     RunOnUiThread(() => {

                //         System.Threading.Thread.Sleep(500);
                pintaXNeg(3);
                pintaXNeg(5);
                pintaXNeg(7);
                //     });
                // })).Start();
                //;
                return M;
            }

            if (casillas[1] == 1 && casillas[5] == 1 && casillas[9] == 1)
            {

                this.lockActionHuman = true;

                //new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                //    RunOnUiThread(() => {

                //        System.Threading.Thread.Sleep(500);
                pintaXNeg(1);
                pintaXNeg(5);
                pintaXNeg(9);
                //    });
                //})).Start();

                return M;
            }

            //humano

            //delado
            if (casillas[1] == 2 && casillas[2] == 2 && casillas[3] == 2)
            {
                pintaONeg(1);
                pintaONeg(2);
                pintaONeg(3);
                return H;
            }

            if (casillas[4] == 2 && casillas[5] == 2 && casillas[6] == 2)
            {
                pintaONeg(4);
                pintaONeg(5);
                pintaONeg(6);
                return H;
            }

            if (casillas[7] == 2 && casillas[8] == 2 && casillas[9] == 2)
            {
                pintaONeg(7);
                pintaONeg(8);
                pintaONeg(9);
                return H;
            }

            //acostadas

            if (casillas[1] == 2 && casillas[4] == 2 && casillas[7] == 2)
            {
                pintaONeg(1);
                pintaONeg(4);
                pintaONeg(7);
                return H;
            }

            if (casillas[2] == 2 && casillas[5] == 2 && casillas[8] == 2)
            {
                pintaONeg(2);
                pintaONeg(5);
                pintaONeg(8);
                return H;
            }

            if (casillas[3] == 2 && casillas[6] == 2 && casillas[9] == 2)
            {
                pintaONeg(3);
                pintaONeg(6);
                pintaONeg(9);
                return H;
            }

            //diagonales

            if (casillas[3] == 2 && casillas[5] == 2 && casillas[7] == 2)
            {
                pintaONeg(3);
                pintaONeg(5);
                pintaONeg(7);
                return H;
            }

            if (casillas[1] == 2 && casillas[5] == 2 && casillas[9] == 2)
            {
                pintaONeg(1);
                pintaONeg(5);
                pintaONeg(9);
                return H;
            };

                //nadie gana
                if (casillas[1] >= 1 && casillas[2] >= 1 && casillas[3] >= 1 && casillas[4] >= 1 && casillas[5] >= 1 && casillas[6] >= 1 && casillas[7] >= 1 && casillas[8] >= 1 && casillas[9] >= 1) return 3;

                return -1;


            
        }
    }
}

