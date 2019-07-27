Unity Fast Paced Multiplayer
===

*A Unity project to build the base functionality for a Authoritative Server, with Lag Compensation, Client side Prediction/Server side Reconciliation and Entity Interpolation*

**WARNING:** Unity has announced they are working on a [new multiplayer framework](https://blogs.unity3d.com/2018/08/02/evolving-multiplayer-games-beyond-unet/) and deprecating UNet on Unity 2018.4 (LTS). I should **not work** on this anymore since it will be deprecated. There are still alternatives to develop your multiplayer project, [read here](https://blogs.unity3d.com/2019/06/13/navigating-unitys-multiplayer-netcode-transition/) for official Unity support. Otherwise, I'd also recommend you to check out [Bolt](https://www.photonengine.com/bolt), a Multiplayer Framework by [Photon](https://www.photonengine.com/en-US/Photon). Thanks to all the supporters that helped me through the development of this repository!

Based on the references:
---
- [Unity Networking Tutorial Part 1 - Introducing the HLAPI](http://gamasutra.com/blogs/ChristianArellano/20150922/254218/UNET_Unity_5_Networking_Tutorial_Part_1_of_3__Introducing_the_HLAPI.php)
- [Unity Networking Tutorial Part 2 - Client Side Prediction and Server Reconciliation](http://gamasutra.com/blogs/ChristianArellano/20151009/255873/UNET_Unity_5_Networking_Tutorial_Part_2_of_3__Client_Side_Prediction_and_Server_Reconciliation.php)
- [Unity Netowrking Entity Interpolation Part 1 - Break Into Components](http://www.gamasutra.com/blogs/ChristianArellano/20160329/269065/UNET_Unity_5_Networking_Entity_Interpolation_Part_1_of_6__Break_Into_Components.php)
- [Unity Networking Entity Interpolation Part 2 - Clientside Input Buffering](http://www.gamasutra.com/blogs/ChristianArellano/20161104/284933/UNET_Unity_5_Networking_Entity_Interpolation_Part_2_of_6__Clientside_Input_Buffering.php)
- [Fast-Paced Multiplayer by Gabriel Gambetta](http://www.gabrielgambetta.com/fpm1.html)
- [Networked Physics by Glenn Fiedler](http://gafferongames.com/networked-physics/introduction-to-networked-physics/)
- [Valve Developer Multiplayer Networking](https://developer.valvesoftware.com/wiki/Source_Multiplayer_Networking)
- [Valve Developer Latency Compensation](https://developer.valvesoftware.com/wiki/Latency_Compensating_Methods_in_Client/Server_In-game_Protocol_Design_and_Optimization)
- [Unity Networking Official Repository](https://bitbucket.org/Unity-Technologies/networking/overview)

I'm currently working on a multiplayer project, and I use this repository for testing, but later on, I hope to create a strong foundation with this to use as a substitute to the Network Transform component on server-authoritative games.

### What's this about?

This is a work in progress framework to help developing server authoritative games on Unity. It is recommended for **fast paced multiplayer games** such as shooters or racing games. This does **not provide** any type of server or database solution, as it only acts on the **sync** methods of the entities. Head over to the [wiki](https://github.com/JoaoBorks/unity-fastpacedmultiplayer/wiki) for further knowledge and usage guides.

### How to test

1. On the top menu, click Tools>Quick Build
2. Choose a folder to deploy your test build
3. Click Tools>Run Server to run the authoritative server
4. Click Tools>Run Client to run a simulated client (You can press F1 to toggle movement simulation)
5. Run the game on the editor then select Client on the Network HUD
6. Move around and watch the sync between the different game windows
