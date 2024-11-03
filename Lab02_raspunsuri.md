#  Laborator #02

1. Ce este un *viewport*?

    *Viewport-ul* definește zona din fereastră în care este desenat conținutul grafic generat de OpenGL. 

2. Ce reprezintă conceptul de *frames per seconds* din punctul de vedere al bibliotecii OpenGL?

    *Frames per second* (*FPS*) reprezintă numărul de cadre redate pe secundă, adică cât de des sunt actualizate imaginile pe ecran în fiecare secundă. În OpenGL, un FPS mai mare indică o redare mai rapidă și mai fluidă, ceea ce este esențial pentru aplicațiile grafice interactive.

3. Când este rulată metoda *OnUpdateFrame()*?

    Metoda *OnUpdateFrame()* este rulată înainte de fiecare ciclu de randare și se ocupă de actualizarea logicii aplicației, cum ar fi mișcarea obiectelor, gestionarea intrărilor utilizatorului și actualizarea fizicii. Aceasta este executată la o frecvență constantă, independent de randarea grafică.

4. Ce este modul imediat de randare?

    *Modul imediat de randare* este o tehnică de desenare utilizată în 
    primele versiuni de OpenGL. În acest mod, fiecare obiect grafic este desenat direct folosind funcții precum glBegin() și glEnd(), unde fiecare vertex și fiecare primitivă sunt procesate imediat ce sunt definite. Acest mod este considerat ineficient, deoarece se face o trecere frecventă între CPU și GPU pentru fiecare vertex, ceea ce duce la performanțe scăzute.

5. Care este ultima versiune de OpenGL care acceptă modul imediat?

    Ultima versiune de OpenGL care acceptă modul imediat este *OpenGL 3.2*.

6. Când este rulată metoda *OnRenderFrame()*?

    Metoda *OnRenderFrame()* este apelată de fiecare dată când un cadru trebuie redat pe ecran. În această metodă se realizează toate apelurile OpenGL pentru desenarea obiectelor.

7. De ce este nevoie ca metoda *OnResize()* să fie executată cel puțin
o dată?

    Metoda *OnResize()* trebuie executată cel puțin o dată pentru a asigura inițializarea corectă a viewport-ului și a matricei de proiecție chiar și atunci când fereastra nu a fost redimensionată. Fără acest apel inițial, randarea ar putea fi incorectă sau distorsionată, deoarece OpenGL nu ar avea informații precise despre dimensiunea ferestrei și nu ar putea afișa corect scena.

8. Ce reprezintă parametrii metodei *CreatePerspectiveFieldOfView()* și care este domeniul de valori pentru aceștia?

    Metoda *CreatePerspectiveFieldOfView(float fovy, float aspect, float zNear, float zFar)* este utilizată pentru a crea o matrice de proiecție de tip perspectivă în OpenGL. 

    * *fovy* (Field Of View):
        
        * Reprezintă unghiul de deschidere al camerei pe axa verticală, exprimat de obicei în radiani.

        * Domeniul tipic de valori este între 30 și 90 de grade (în radiani: ~0.5 până la ~1.57 radiani).

    * *aspect* (Aspect Ratio):

        * Reprezintă raportul dintre lățimea și înălțimea viewport-ului.
        
        * Valoarea este de obicei lățimea ferestrei împărțită la înălțimea ferestrei.

        * Are valori pozitive, unde un raport de 1.0 indică un viewport pătratic.

    * *zNear* (Near Plane Distance):

        * Reprezintă distanța până la planul de tăiere apropiat.

        * Trebuie să fie un număr pozitiv, de obicei, o valoare mică (ex. 0.1 sau 0.01).

    * *zFar* (Far Plane Distance):

        * Reprezintă distanța până la planul de tăiere îndepărtat.
        
        * Trebuie să fie un număr pozitiv și mai mare decât zNear (ex. 1000 sau mai mult).

