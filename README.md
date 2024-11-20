#### Credits
Original script by dogzerx2 which is no longer available on the unity asset store and I was unable to find it on github. 


#### LightIK 
A simple two bone ik for unity. The problem with unity's built in TwoBoneIK system is that you can't control when it runs and so if you need to do any sort of pre or post processing to the positions of the ik target or pole, you can't do it with full control.  

With this script you can choose to update the solver manually, for example: 

1. Apply procedural recoil to ik target
2. Update IK now with new position of the ik target 
3. Do something else with the new solved positions of the bones


Other IK solutions like FastIK/SimpleIK and EasyIK do not correctly rotate the elbow and you will find that they create undesired twists between the forearm and upperarm. However, they are more flexible in terms of the length of the IK chain, with this script you can only have 3 bones.

There are no plans to add more features to this as the whole point is to keep it exceptionally simple but I will fix any problems users may face.




#### Installation 

Just copy the script and put it in your project or add it as a git module if you prefer: 


In your unity project root, run the following

```
git submodule add https://github.com/pheeya/LightIK Assets/Modules/LightIK
```