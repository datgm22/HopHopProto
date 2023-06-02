using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    public interface IGimmick
    {
        /// <summary>
        /// 触れた時に呼び出す
        /// 戻り値がnullならプレイヤーは操作を継続
        /// 戻り値がnull以外ならその状態に切り替え
        /// </summary>
        /// <param name="pcon">プレイヤーコントローラーのインスタンス</param>
        /// <returns>プレイヤーの状態変更があれば返す。なければnull</returns>
        IPlayerState Hit(PlayerController pcon);
    }
}
