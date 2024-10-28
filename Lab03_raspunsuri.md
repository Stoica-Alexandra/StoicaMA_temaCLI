#  Laborator #03

*1.* Care este ordinea de desenare a vertexurilor pentru aceste metode (*orar* sau *anti-orar*)?

&nbsp;&nbsp;&nbsp;&nbsp;În OpenGL, vertexurile sunt desenate în ordinea în care sunt specificate.
De obicei ordinea este anti-orar, deoarece indică fața frontală a obiectului.  

*2.* Ce este *anti-aliasing*? Prezentați această tehnică pe scurt.

&nbsp;&nbsp;&nbsp;&nbsp;*Anti-aliasing* este o tehnică grafică utilizată pentru a netezi marginile zimțate ale obiectelor digitale (linii curbe, diagonale), creând tranziții de culoare mai line la margini.

&nbsp;&nbsp;&nbsp;&nbsp;Activarea anti-aliasing-ului în OpenGL se poate face cu: 
GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

*3.* Care este efectul rulării comenzii *GL.LineWidth(float)*? Dar pentru *GL.PointSize(float)*? Funcționează în interiorul unei zone GL.Begin()?

&nbsp;&nbsp;&nbsp;&nbsp;*GL.LineWidth(float)* setează grosimea liniilor.

&nbsp;&nbsp;&nbsp;&nbsp;*GL.PointSize(float)* setează dimensiunea punctelor.

&nbsp;&nbsp;&nbsp;&nbsp;Acestea nu funcționează  în interiorul unei zone *GL.Begin()*.

*4.* Răspundeți la următoarele întrebări (utilizați ca referință eventual și
tutorii OpenGL Nate Robbins):

* Care este efectul utilizării directivei *LineLoop* atunci când desenate segmente de dreaptă multiple în OpenGL?

* Care este efectul utilizării directivei *LineStrip* atunci când desenate segmente de dreaptă multiple în OpenGL?

* Care este efectul utilizării directivei *TriangleFan* atunci când desenate segmente de dreaptă multiple în OpenGL?

* Care este efectul utilizării directivei *TriangleStrip* atunci când
    desenate segmente de dreaptă multiple în OpenGL?


&nbsp;&nbsp;&nbsp;&nbsp;*LineLoop* conectează un set de puncte într-o buclă închisă, legând ultimul vertex de primul.
    
&nbsp;&nbsp;&nbsp;&nbsp;*LineStrip* conectează un set de puncte într-o linie continuă, fără a închide bucla.
    
&nbsp;&nbsp;&nbsp;&nbsp;*TriangleFan* creează un evantai de triunghiuri care pornesc de la un punct central.

&nbsp;&nbsp;&nbsp;&nbsp;*TriangleStrip* creează o panglică de triunghiuri, fiecare triunghi împărțind o latură cu cel anterior.

*6.* Urmăriți aplicația „shapes.exe” din tutorii OpenGL Nate Robbins. De ce este importantă utilizarea de culori diferite (în gradient sau culori selectate per suprafață) în desenarea obiectelor 3D? Care este avantajul?

&nbsp;&nbsp;&nbsp;&nbsp;Utilizarea culorilor diferite în obiectele 3D permite evidențierea detaliilor prin crearea unei iluzii de adâncime și volum. Aceasta îmbunătățește percepția vizuală și ajută la simularea efectelor de iluminare.

*7.* Ce reprezintă un gradient de culoare? Cum se obține acesta în OpenGL?

&nbsp;&nbsp;&nbsp;&nbsp;Un *gradient de culoare* este o tranziție lină între două sau mai multe culori. În OpenGL, acesta se obține setând culori diferite pentru vertexurile unui obiect.

*10.* Ce efect are utilizarea unei culori diferite pentru fiecare vertex atunci când desenați o linie sau un triunghi în modul strip?

&nbsp;&nbsp;&nbsp;&nbsp;Atunci când fiecare vertex are o culoare diferită, OpenGL realizează un gradient între aceste culori.