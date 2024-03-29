# [CocoTools](https://github.com/coco1337/CocoTools)
[![license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/coco1337/CocoTools/blob/main/LICENSE)
[![EN](https://img.shields.io/badge/Lang-EN-green.svg)](https://github.com/coco1337/CocoTools/blob/main/README.md)
[![KR](https://img.shields.io/badge/Lang-KR-green.svg)](https://github.com/coco1337/CocoTools/blob/main/README.kr.md)
[![JP](https://img.shields.io/badge/Lang-JP-green.svg)](https://github.com/coco1337/CocoTools/blob/main/README.jp.md)
[![Release](https://img.shields.io/badge/Release-Beta-red.svg?logo=unity&logoColor=#000000)](https://github.com/coco1337/CocoTools/releases/tag/Beta)

# Introduction to CocoTools
**CocoTools** is a C# conversion of Sisong's (https://github.com/sisong/HDiffPatch) HDiffPatch tool for use in Unity and VRChat related content. The tool aims to allow users to safely edit FBX's for additional content, repairs, and added functionality to avatars without redistributing original FBX data. The entire process is destructive so please be aware of this as an end user when using this tool. 

**CocoTools** maintains the functionality of the original HDiffPatch tool from Sisong through the conversion of its DLL file. However, at this point in time, only basic features are currently available for general use. The tool is still currently in a BETA state and fuctionality is always subject to change. Depending on user necessity, more original features will be opened up for end users and content creators. 

# Instructions 
For Content Creators
**CocoTools** is made to be extremely easy to use by bringing a GUI (Graphical User Interface) to the user. Ease of use is a necessary feature for general users to properly use the tool. For end users they only need to drag in their original FBX and the HDiff file to commence the patching routine. Advance users still have some freedom to use their own commands for patching purposes. Please check Sisong's HDiffPatch github page for additional information regarding commands. 

# Creating Patch Files (Content Creators)
To create a patch file please navigate to the **"coco"** tab at the top left of your Unity screen and then open the Diff tool from the dropdown menu.

![image](https://github.com/coco1337/CocoTools/assets/91550600/4f744032-c4c5-4a23-be6c-33f0f07ca0f3)

A window should appear named **"Diff Tool".** From the new window input the original FBX and the edited FBX in the fields named, Original file and Diff Target File. 

![image](https://github.com/coco1337/CocoTools/assets/91550600/3ff61c61-d65a-4fb2-98ba-f1a37946d496)

Once you add your original file and Diff target file press **"Start Diff!"** to create the HDiff file. This file will be used by other users to patch their FBX. 

![image](https://github.com/coco1337/CocoTools/assets/91550600/aec3ccea-c454-4248-aa6b-0048c6ee278d)

This file is used by end users to patch their FBX with the Patch tool included with CocoTools. Please be aware that HDiff files require the Original FBX to work. The HDiff file itself cannot be used in its own environment and requires the use of the original file to complete its patching process. The file only contains non-original data. For more information please check Sisong's HDiffPatch tool. 

# Patching Files (End User) 

For end users the entire process is simple. Navigate to the coco tab at the top left of your Unity screen and then open the Patch Tool. 

![image](https://github.com/coco1337/CocoTools/assets/91550600/0ad90325-c35c-48fc-922c-c671af2d9d73)

A window should appear named **"Patch Tool".** From the new window input the original FBX and the HDiff file in the fields named,  Original File (FBX) and HDiff file (HDIFF).

![image](https://github.com/coco1337/CocoTools/assets/91550600/bdd83f01-1413-4d55-9a4f-4be52ed30c83)

Once you add your original file and Diff file press **"Start Patch".** It's recommended to have overwrite enabled but please check recommended installation instructions listed by the content creator. 

![image](https://github.com/coco1337/CocoTools/assets/91550600/68bbc7f0-3e0e-41af-b559-946f0dd4ed28)

# Credits

Original tool created by Sisong. The creation of this tool would not have been possible without his HDiffPatch tool. You can find the original tool at the link below. 

https://github.com/sisong/HDiffPatch
