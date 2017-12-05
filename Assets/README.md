# Robo-sumo-unity
A 2-player Android/touch-based game built in Unity3D

Google Play store link:
https://play.google.com/store/apps/details?id=com.DavidDickBall.RoboSumo
When building from Unity, call this BotSumo  and make sure checkbox on PaidVersion in scene "menu" is checked. 
Package has to be com.DavidDickBall.RoboSumo


Google Play store link of ad-free version:
https://play.google.com/store/apps/details?id=com.DavidDickBall.BotSumoBattleArena
When building from Unity, call this BotSumo BattleArena
Package has to be com.DavidDickBall.BotSumoBattleArena


# note to self - how to add new bot
Copy an existing bot, replace the VFX. Change the "Player character" name. Save in "Resources" folder. 
In PlayerSelectScript, create new elseif in showOnlyP1Character() & showOnlyP2Character()
Create new isXUnlocked variable, and set to PlayerPref in Start()
In unlockSelectedBot() create new elseif for new variable
In playerSelect scene, place p1 & p1 in place. Increment the array on "SceneController" and drag the object from the scene into the array. 
Add entry to public var playerCharacters at top of playerSelectScript
You can test the character by setting the default character in GameControllerScript by changing var defaultPlayer, and then in LoadPlayer(), change "playerToLoad" too.