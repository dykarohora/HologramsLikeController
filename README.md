# HologramsLikeController
HoloLensアプリケーションにて、プリインアプリ「Holograms」のようにオブジェクトをコントロールできるようにするアセットです。

## 導入
* [HoloToolKit-Unity](https://github.com/Microsoft/HoloToolkit-Unity)をプロジェクトにインポートする
* HologramsLikeController.unitypackageをプロジェクトにインポートする
* HoloToolKit-UnityのInputManager.prefabとカーソル系のprefab(e.g. DefaultCursor.prefab)をシーンに追加する
* Prefabs/TransformControllerManager.prefabをシーンに追加する
* ワイヤーフレーム表示できるマテリアルをプロジェクトに追加する([参考サイト](http://qiita.com/amano-kiyoyuki/items/01c2be92010e1a08f4eb))
* TransformControllerManagerのインスペクタから、Position Cube Materialに追加したマテリアルを設定する

## 使い方
* 操作したい3Dモデルの配下にPrefabs/TransformController.prefabを追加する
* 操作したい3Dモデル(TransformController.prefabの親)にColliderがない場合は追加してください  

## 注意点
* Scale(1,1,1)の場合に立方体におさまる3Dモデル(e.g. Cube, Sphere)は上記の手順で利用可能です
* おさまらない場合は、TransformControllerのインスペクタから、Obj Scale Correct X/Y/Zの値を変更して調整してください(testシーン参照)

## 調整用パラメータ
TransformControllerManagerのプロパティから、各種調整が可能です
* Distance Scale  
3Dモデルの位置を変更するとき、手を前後に動かしたときのオブジェクトの移動量にかかる係数
* Base Position Cube Scale  
ワイヤーフレーム形状のCubeのローカルScale
* Position Cube Material  
3Dモデルを覆うCubeのマテリアル  
* Rotation Speed  
3Dモデルを回転させるとき、回転量にかかる係数  
* Scale Magnification  
3Dモデルの大きさを変更するとき、拡大/縮小量にかかる係数  
* Scale Lower Limit  
3Dモデルを縮小するときの、Scaleの下限値
* Controller Scale  
Cubeの頂点上、辺上の3DモデルのローカルScale
* Complete Panel Position Y  
操作完了パネルボタンの位置  
0の場合はワイヤーフレームの天面に位置する

## 今後
* Editorスクリプトを組んで調整用パラメータをわかりやすくする
* READMEの英語化

## 参考
* コードの解説は[ブログ記事](http://blog.d-yama7.com/archives/481)をご参照ください
* ご意見等ございましたらissueや[@dy_karous](https://twitter.com/dy_karous)までメンションいただけるとありがたいです

## ライセンス
* MIT License  