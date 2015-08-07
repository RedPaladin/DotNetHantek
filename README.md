I made this managed C# library in order to bring you a better abstraction level of the native library HTMarch.dll provided by Hantek.

Note my C# library is using unsafe code to call the low level functions of the original library.

I bring this project to the entire world because I haven't found any example in C# with this oscilloscope. I hope sincertly I will help you and save you a lot of times to understand the way this library works.

To build this project, you need Visual Studio 2012 (the express version should be sufficient) and, of course, a Hantek USB oscilloscope.

Nothing better than an example to understand how my library is working, I have included into the solution a protype which is displaying the signal from Channel 1. All you need is to connect the probe to 1Khz/2V connector located on the side of device and run the application.

My material is a model Hantek6022BE but I think it is compatible with other model but I haven't tried yet.

If you have question concerning this project, you can contact me through this page.
