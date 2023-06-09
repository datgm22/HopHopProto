using UnityEngine;
using UnityEngine.Events;

namespace HopHop
{
    /// <summary>
    /// プレイヤーオブジェクトを制御するクラス
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        /// <summary>
        /// 壁に接触した時の処理の登録場所
        /// </summary>
        public readonly UnityEvent onCollisionWall = new UnityEvent();

        /// <summary>
        /// PlayerMoverのインスタンス
        /// </summary>
        public PlayerMover Mover { get; private set; }

        /// <summary>
        /// クリック待ち状態
        /// </summary>
        public PlayerStateWaitStart StateWaitStart { get; private set; } = new();

        /// <summary>
        /// インジケーター操作状態
        /// </summary>
        public Indicator IndicatorInstance { get; private set; }

        /// <summary>
        /// 切り替え予定の状態
        /// </summary>
        IPlayerState nextState;
        /// <summary>
        /// 現在の状態
        /// </summary>
        IPlayerState currentState;

        /// <summary>
        /// シーンが開始した時の座標
        /// </summary>
        public Vector3 StartPosition { get; private set; }

        private void Awake()
        {
            Instance = this;
            Mover = GetComponent<PlayerMover>();
            IndicatorInstance = GetComponentInChildren<Indicator>();
            ChangeState(StateWaitStart, true);
            StartPosition = transform.position;
        }

        private void Update()
        {
            // 状態切り替え
            if (nextState != null)
            {
                // 終了処理
                currentState?.Terminate();

                // 状態切り替え
                currentState = nextState;
                nextState = null;

                // 初期化処理
                currentState.Init();
            }

            // 更新処理
            currentState?.UpdateState();            
        }

        /// <summary>
        /// プレイヤーの次の状態を設定する。
        /// デフォルトでは、すでにほかの呼び出しがある時は受付をキャンセルする。
        /// forceにtrueを指定すると、強制的に次の状態要求を設定する。
        /// </summary>
        /// <param name="state">切り替えたいIPlayerStateのインスタンス</param>
        /// <param name="force">省略するとfalse。trueで強制設定</param>
        public void ChangeState(IPlayerState state, bool force=false)
        {
            if (force || (nextState == null))
            {
                nextState = state;
            }
        }

        /// <summary>
        /// 重なり判定
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gimmick"))
            {
                OnHitGimmick(other);
            }    
        }

        /// <summary>
        /// ギミックとの接触処理。
        /// すでに状態切り替え要求がある時は、新規の接触は無効
        /// </summary>
        /// <param name="other">接触時の情報</param>
        void OnHitGimmick(Collider other)
        {
            var target = other.GetComponent<IGimmick>();
            if (target == null) return;
            if (nextState != null) return;

            var next = target.Hit(this);
            if (next != null)
            {
                ChangeState(next);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            OnCollisionEnter(collision);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                onCollisionWall.Invoke();
            }
        }
    }
}
