using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>※OncollisonEnterは子オブジェクトがコライダーにぶつかった時も判定されるっぽい？</para>
/// <para>データと挙動部分は分ける</para>
/// <para>1クラス 1機能</para>
/// <para>移動部は、rigidbodyのMoveメソッドを使う</para>
/// <para>今までと違い、ゲームがVRかPCかを選択して遊べる</para>
/// <para>VRとPCの共通部分は、別クラスとして実装する</para>
/// <para>Control + Status + Move + Belong +Authority のABCMSアーキテクチャを採用</para>
/// <para>Control:(制御部) Status,Move,Authority の情報を閲覧し,艦の挙動や、攻撃の命令を出す</para>
/// <para>Status:(データ部) 艦のMHPや、攻撃力, 武装のリスト(List(IWeapon)) </para>
/// <para>Move: (移動部)rigidbodyと密接に連携し、Controlによって指定された座標に向かう処理を請け負う</para>
/// <para>Belong: (勢力部) 自身の所属勢力を管理する、このクラスの存在により、layerも制御される</para>
/// <para>Authority:(指揮部・披指揮部) 自身の随伴状況を示す FlagShip か 護衛艦か 、  自身の麾下にいる艦隊はListで管理される</para>
/// <para>IShip インターフェースは、CSMAの全ての情報が閲覧できる窓口として作用する</para>
/// <para>ここでは艦の沈没は、Destroyでなく、非アクティブ化である</para>
/// <para>どうやら、インターフェースを介してのMonoBehavior継承オブジェクトのフィールドアクセスはNullRefurenceになるらしい</para>
/// <para></para>
/// </summary>
public class Memo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //クラス設計
    //https://www.yayoibloglife.work/entry/2019/07/28/164941
    //ステータス部分は
    //UML
    //https://dev.classmethod.jp/articles/plantuml-server-on-docker/
    //alt + D でプレビュー

    //dcokerのcompose : docker run -d -p 8080:8080 plantuml/plantuml-server:jetty

    //カメラにアタッチするテクスチャはRenderTextureと呼ばれるもので、
    //Assets→create→RenderTextureから生成出来る

    /// <summary>
    /// <para>Profiler、Unityのデバッグを補助してくれる　 Window > Analysis > Profiler の順に選択します。</para>
    /// <para>printf デバッグ: 標準出力に内部状態を出力しデバッグする手法</para>
    /// <para></para>
    /// </summary>
    public void YOUGO()
    {

    }
}
