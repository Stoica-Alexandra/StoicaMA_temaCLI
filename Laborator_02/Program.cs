//Stoica Maria Alexandra 3131B
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

/**
    Aplicația utilizează biblioteca OpenTK v2.0.0 (stable) oficială și OpenTK. GLControl v2.0.0
    (unstable) neoficială. Aplicația fiind scrisă în modul consolă nu va utiliza controlul WinForms
    oferit de OpenTK!
    Tipul de ferestră utilizat: GAMEWINDOW. Se demmonstrează modul imediat de randare (vezi comentariul!)...
**/
namespace StoicaMA_temaCLI
{
    class SimpleWindow3D : GameWindow
    {

        const float rotation_speed = 180.0f;

        float angleKey = 0.0f;

        float positionX = 0.0f;
        float positionY = 0.0f;



        // Constructor.
        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }

        /**Setare mediu OpenGL și încarcarea resurselor (dacă e necesar) - de exemplu culoarea de
           fundal a ferestrei 3D.
           Atenție! Acest cod se execută înainte de desenarea efectivă a scenei 3D. */
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Blue);
            //GL.Enable(EnableCap.DepthTest);
        }

        /**Inițierea afișării și setarea viewport-ului grafic. Metoda este invocată la redimensionarea
           ferestrei. Va fi invocată o dată și imediat după metoda ONLOAD()!
           Viewport-ul va fi dimensionat conform mărimii ferestrei active (cele 2 obiecte pot avea și mărimi 
           diferite). */
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        /** Secțiunea pentru "game logic"/"business logic". Tot ce se execută în această secțiune va fi randat
            automat pe ecran în pasul următor - control utilizator, actualizarea poziției obiectelor, etc. */
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();

            // Se utilizeaza mecanismul de control input oferit de OpenTK (include perifcerice multiple, mai ales pentru gaming - gamepads, joysticks, etc.).
            if (keyboard[OpenTK.Input.Key.Escape])
            {
                Exit();
                return;
            }


            ///Rotire obiect in functie de tastele UP si DOWN
            else if (keyboard[Key.Up])
                angleKey += rotation_speed / 3 * (float)e.Time;

            else if (keyboard[Key.Down])
                angleKey -= rotation_speed / 3 * (float)e.Time;

            ///Mutare cub cand se face click stanga sau dreapta
            if (mouse[OpenTK.Input.MouseButton.Left])
            {
                if (mouse.X >= 0 && mouse.X <= Width)
                {
                    positionX -= 1.0f;
                    positionY -= 1.0f;
                }
            }
            if (mouse[OpenTK.Input.MouseButton.Right])
            {
                if (mouse.X >= 0 && mouse.X <= Width)
                {
                    positionX += 1.0f;
                    positionY += 1.0f;
                }
            }

        }

        /** Secțiunea pentru randarea scenei 3D. Controlată de modulul logic din metoda ONUPDATEFRAME().
            Parametrul de intrare "e" conține informatii de timing pentru randare. */
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);



            GL.Translate(positionX, positionY, 0.0f);//mutare obiect

            GL.Rotate(angleKey, 0.0f, 0.0f, 1.0f);//rotire obiect



            DrawCube();
            DrawAxes_OLD();

            SwapBuffers();
            //Thread.Sleep(1);
        }

        private void DrawAxes_OLD()
        {
            GL.Begin(PrimitiveType.Lines);

            // X
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(20, 0, 0);

            // Y
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 20, 0);

            // Z
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 20);


            GL.End();
        }

        // Utilizăm modul imediat!!!
        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {

            /**Utilizarea cuvântului-cheie "using" va permite dealocarea memoriei o dată ce obiectul nu mai este
               în uz (vezi metoda "Dispose()").
               Metoda "Run()" specifică cerința noastră de a avea 30 de evenimente de tip UpdateFrame per secundă
               și un număr nelimitat de evenimente de tip randare 3D per secundă (maximul suportat de subsistemul
               grafic). Asta nu înseamnă că vor primi garantat respectivele valori!!!
               Ideal ar fi ca după fiecare UpdateFrame să avem si un RenderFrame astfel încât toate obiectele generate
               în scena 3D să fie actualizate fără pierderi (desincronizări între logica aplicației și imaginea randată
               în final pe ecran). */
            using (SimpleWindow3D example = new SimpleWindow3D())
            {

                // Verificați semnătura funcției în documentația inline oferită de IntelliSense!
                example.Run(30.0, 0.0);
            }
        }
    }
}
