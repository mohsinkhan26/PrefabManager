# Overview Prefab Manager #

### Introduction ###
After spending some years in Game Industry, I realize managing prefabs and make them pool is a major and necessary module of each game. 

As, instantiating and destroying calls are really expensive at runtime. So, due to this, it is better to pool instantiated objects and recycle them after use, for future.

### Purpose ###
In Unity projects, programmers have to instantiate and destroy various GameObjects at runtime like enemies, particles, powerups, health boxes etc. So, it is better to pool them and recycle them.

### Unity Asset Store Link ###

[https://www.assetstore.unity3d.com/#!/content/96299](https://www.assetstore.unity3d.com/#!/content/96299)

### Features ###
* Easily pool GameObjects in your game
* Promotes the use of pooling concept
* Auto scale your pool
* Prefab manager to spawn and recycle
* Improve the performance of your game
* Place prefabs anywhere in Assets/ and just place a reference in PrefabManager
* Maintains hierarchy under PrefabManager
* Select when you want to create your pool on Awake, Start or Manually
* Still you can update the data of the newly spawned objects
* Easy to use, Plug n play
* Open Source code without any DLL

### Usage ###
* Import plugin
* Add enum against your prefab in PrefabType.cs
* Add respective prefab reference in PrefabManager.prefab
* Now, just drag PrefabManager.prefab to hierarchy view and call its public functions

![HowToUse-01.png](https://bytebucket.org/unbounded-eagle/prefab_manager/raw/64552c4af65ebcfcbabad4bb1cf8dc012acd3776/Screenshots/HowToUse-01.png)

![HowToUse-02.png](https://bytebucket.org/unbounded-eagle/prefab_manager/raw/64552c4af65ebcfcbabad4bb1cf8dc012acd3776/Screenshots/HowToUse-02.png)

![HowToUse-03.png](https://bytebucket.org/unbounded-eagle/prefab_manager/raw/64552c4af65ebcfcbabad4bb1cf8dc012acd3776/Screenshots/HowToUse-03.png)

![HowToUse-04.png](https://bytebucket.org/unbounded-eagle/prefab_manager/raw/64552c4af65ebcfcbabad4bb1cf8dc012acd3776/Screenshots/HowToUse-04.png)

![HowToUse-05.png](https://bytebucket.org/unbounded-eagle/prefab_manager/raw/64552c4af65ebcfcbabad4bb1cf8dc012acd3776/Screenshots/HowToUse-05.png)

### Remember ###
* Drag PrefabManager.prefab in the first scene and it will persist

### TODO ###
* Show count of pool in editor
* Check for duplicate entries in list and highlight with red background

### Special Thanks ###

* All the users who provide feedback and suggestions to improve
* All the users who gave reviews on Asset store


## Thanks for your support! ##