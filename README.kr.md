# [CocoTools](https://github.com/coco1337/CocoTools)
[![license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/coco1337/CocoTools/blob/main/LICENSE)
[![EN](https://img.shields.io/badge/Lang-EN-green.svg)](https://github.com/coco1337/CocoTools/blob/main/README.md)
[![KR](https://img.shields.io/badge/Lang-KR-green.svg)](https://github.com/coco1337/CocoTools/blob/main/README.kr.md)
[![JP](https://img.shields.io/badge/Lang-JP-green.svg)](https://github.com/coco1337/CocoTools/blob/main/README.jp.md)
[![Release](https://img.shields.io/badge/Release-Beta-red.svg?logo=unity&logoColor=#000000)](https://github.com/coco1337/CocoTools/releases/tag/Beta)

# Introduction to CocoTools
**CocoTools** 은 Unity와 VRChat 관련 콘텐츠를 위해 Sisong's (https://github.com/sisong/HDiffPatch) HDiffPatch tool을 C#으로 변환한 툴입니다. 이 툴은 사용자가 원본 FBX 데이터를 재배포 하지 않고 콘텐츠를 추가하거나, 수리, 복구하는 등 FBX를 편집할 수 있도록 지원합니다. 일련의 과정으로 원본 데이터가 손상 될 수 있기 때문에 주의가 필요합니다.

**CocoTools** 프로젝트 플러그인 내의 hpatchz.dll과 hdiffz.dll은 Sisong의 HdiffPatch 기능을 대부분 가지고 있지만 현재는 Patch를 위한 기본 기능만 사용할 수 있습니다. 현재 이 툴은 BETA 버전이기 때문에 언제나 변경 될 수 있습니다.

## Instructions 
For Content Creators
**CocoTools** 은 사용자에게 GUI(Graphical User Interface)를 제공하여 쉽게 사용할 수 있도록 제작되었습니다. 사용자는 원본 FBX와 HDiff파일을 드래그 앤 드랍을 통해 Patch를 시작 할 수 있습니다.

## Creating HDiff Files (Content Creators)
HDiff 파일을 생성하려면 Unity 화면 상단의 **"coco"** 탭으로 이동한 다음 드롭다운 메뉴에서 Diff 툴을 엽니다

![image](https://github.com/coco1337/CocoTools/assets/91550600/4f744032-c4c5-4a23-be6c-33f0f07ca0f3)

**"Diff Tool".** 이라는 이름의 창이 나타납니다. 해당 창에서 원본 FBX 파일과 편집한 FBX 파일, HDiff파일 이름을 각각의 필드에 등록합니다.
![image](https://github.com/coco1337/CocoTools/assets/91550600/3ff61c61-d65a-4fb2-98ba-f1a37946d496)
Original file과 Diff target file을 추가하고, 출력을 원하는 HDiff의 이름을 입련 한 뒤  **"Start Diff!"** 버튼을 누르면 HDiff 파일이 생성됩니다. 해당 파일은 다른 사용자가 FBX를 Patch 하는 데 사용됩니다.

![image](https://github.com/coco1337/CocoTools/assets/91550600/aec3ccea-c454-4248-aa6b-0048c6ee278d)

이 파일은 최종 사용자가 해당 툴에 포함된 Patch툴로 FBX를 Patch 하는데 사용됩니다. HDiff 파일은 생성에 사용했던 원본 FBX 파일이 필요합니다. HDiff파일은 Patch 작업을 위해 HDiff 파일 생성에 사용된 원본 FBX 파일을 사용하기 때문에 원본 FBX 파일이 없다면 사용 할 수 없습니다. 해당 HDiff파일은 원본과의 차이점이 저장되어 있습니다. 자세한 내용은 Sisong의 HDiffPatch를 참고 바랍니다.

## Patching Files (End User) 

Unity 화면 상단의 **"coco"** 탭으로 이동한 다음 드롭다운 메뉴에서 Patch 툴을 엽니다.

![image](https://github.com/coco1337/CocoTools/assets/91550600/0ad90325-c35c-48fc-922c-c671af2d9d73)

 **"Patch Tool".** 이라는 이름의 창이 나타납니다. 해당 창에서 원본 FBX 파일과 HDiff파일을 각각의 필드에 등록합니다.

![image](https://github.com/coco1337/CocoTools/assets/91550600/bdd83f01-1413-4d55-9a4f-4be52ed30c83)

원본 FBX 파일과 HDiff파일을 등록한 후 **"Start Patch".** 버튼을 누릅니다. 다른 HDiff 옵션이 필요하다면 Custom HDIFF Commands를 사용해서 원하는 옵션을 넣을 수 있지만 이미 등록되어 있는 기본값 사용을 권장합니다.

![image](https://github.com/coco1337/CocoTools/assets/91550600/68bbc7f0-3e0e-41af-b559-946f0dd4ed28)

# Credits

Original tool created by Sisong. The creation of this tool would not have been possible without his HDiffPatch tool. You can find the original tool at the link below. 

https://github.com/sisong/HDiffPatch
