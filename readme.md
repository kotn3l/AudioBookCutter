## Mi is az AudioBookCutter?
Röviden egy olyan program, amely képes hangoskönyveket (igazából minden mp3 fájformátumú) fájlt elvágni a felhasználó által megjelölt helyen. Jelöléseket tehetünk a megnyitott audiofájl kívánt pontjaiba, majd a program ezen markereken elvágja azt.

Egy C#-ban írott, és Windows Forms-al implementált szoftver, amely az ffmpeg könyvtár segítségével végzi el a vágásokat.

Több infó a dokumentumokban.


## What is AudioBookCutter?
It can be used to cut any audiobooks (any mp3 format file, for that matter) at one, or at multiple user-placed timestamps. We can place markers at any given point in the opened audio file, and it will be cut there.

It's written in C# using Windows Forms, while for cutting it is using the ffmpeg library.