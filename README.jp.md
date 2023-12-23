# [CocoTools](https://github.com/coco1337/CocoTools)
[![license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/coco1337/CocoTools/blob/main/LICENSE)
[![EN](https://img.shields.io/badge/Lang-EN-green.svg)](https://github.com/coco1337/CocoTools/blob/main/README.md)
[![KR](https://img.shields.io/badge/Lang-KR-green.svg)](https://github.com/coco1337/CocoTools/blob/main/README.kr.md)
[![JP](https://img.shields.io/badge/Lang-JP-green.svg)](https://github.com/coco1337/CocoTools/blob/main/README.jp.md)
[![Release](https://img.shields.io/badge/Release-Beta-red.svg?logo=unity&logoColor=#000000)](https://github.com/coco1337/CocoTools/releases/tag/Beta)

# Introduction to CocoTools
**CocoTools** はUnityとVRChat関連コンテンツのためにSisong's (https://github.com/sisong/HDiffPatch) HDiffPatch toolをC#に変換したツールです。このツールは使用者が元のFBXデータを再配布しなくてもコンテンツの追加、修理、復旧等、FBXの編集が出来るようにサポートします。一連の過程で元のデータが損傷する可能性がある為、注意が必要です。

**CocoTools** プロジェクトプラグイン内のhpatchz.dllとhdiffz.dllは、SisongのHdiffPatch機能のほとんとを持ち合わせていますが、現在はPatchのための基本機能のみ使用出来ます。βバージョンな為、いつでも更新される可能性があります。

## Instructions 
For Content Creators
**CocoTools** は、使用者にGUI(Graphical User Interface)を提供することによって手軽に使えるよう制作されています。使用者は元のFBXとHDiffファイルをドラッグアンドドロップすることでPatchを行えることが出来ます。

## Creating Patch Files (Content Creators)
HDiff ファイルを生成するにはUnity画面上部の **"coco"** からDiffツールを開いてください。

![image](https://github.com/coco1337/CocoTools/assets/91550600/4f744032-c4c5-4a23-be6c-33f0f07ca0f3)

**"Diff Tool".** という名前の画面が現れます。そこから元のFBXファイルと修正したFBXファイル、HDiffファイルの名前をそれぞれ各場所に登録します。

![image](https://github.com/coco1337/CocoTools/assets/91550600/3ff61c61-d65a-4fb2-98ba-f1a37946d496)

Original fileとDiff target fileを追加し、出力したいHDiffの名前を入力し **"Start Diff!"** ボタンを押せば、HDiffファイルが生成されます。当ファイルは他のユーザーがFBXのPatchを行うために使用されます。

![image](https://github.com/coco1337/CocoTools/assets/91550600/aec3ccea-c454-4248-aa6b-0048c6ee278d)

このファイルは最終使用者が当ツールに含まれたPatchツールでFBXの更新を行うために使われます。HDiffファイルはPatch作業のためにHDiffファイル生成に使われた元のFBXファイルを必要とする為、元のFBXファイルを持っていないと使用出来ません。このHDiffファイルには修正前と修正後のFBXファイルの違いが保存されています。詳しい内容はSisongのHDiffPatchの方を参考お願いします。

## Patching Files (End User) 

Unity上部 **"coco"** メニューからPatchツールを開いてください。

![image](https://github.com/coco1337/CocoTools/assets/91550600/0ad90325-c35c-48fc-922c-c671af2d9d73)

**"Patch Tool".** という名前の画面が開かれます。そこから元のFBXファイルとHDiffファイルを、それぞれ該当する場所に登録してください。

![image](https://github.com/coco1337/CocoTools/assets/91550600/bdd83f01-1413-4d55-9a4f-4be52ed30c83)

元のFBXファイルとHDiffファイルを登録した後、 **"Start Patch".** ボタンを押してください。他のHDiffオプションが必要な場合、Custom HDIFF Commandsを使ってお好みのオプションが入れられますが、デフォルト状態のまま使用することをお勧めします。

![image](https://github.com/coco1337/CocoTools/assets/91550600/68bbc7f0-3e0e-41af-b559-946f0dd4ed28)

## Credits

Original tool created by Sisong. The creation of this tool would not have been possible without his HDiffPatch tool. You can find the original tool at the link below. 

https://github.com/sisong/HDiffPatch
