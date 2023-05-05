using Com.IronicEntertainment.TobisTimeOdyssey.Elements.Characters;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Godot;
using System;

namespace Com.IronicEntertainment.TobisTimeOdyssey.Managers
{

    public class PlayerManager : Manager
    {
        #region singleton
        static private PlayerManager instance;

		static public PlayerManager GetInstance() 
        {
            if (instance == null) instance = new PlayerManager ();
            return instance;

        }

        static private PlayerManager Instance { get {return GetInstance(); } }
        #endregion

        private PlayerManager (): base() { }

        [Export]
        private PackedScene
            playerFactory;

        [Export]
        private NodePath
            playerContainerPath;

        private Node2D
            playerContainer;

        private Player _player;

        public Player Player { get { return CreatePlayer(); } }

        protected override void Init()
        {
            base.Init();
            playerContainer = GetNode<Node2D>(playerContainerPath);
        }

        public override void _Ready()
        {
            base._Ready();
            #region singleton
            if (instance != null){  
                QueueFree();
                GD.Print(nameof(PlayerManager) + " Instance already exist, destroying the last added.");
                return;
            }
            
            instance = this;
            #endregion
        }

        public void ResetPlayer()
        {
            Player.SetPlayerModeStatic();
        }

        public Player CreatePlayer()
        {
            if (_player == null)
            {
                playerContainer = GetNode<Node2D>(playerContainerPath);
                _player = playerFactory.Instance<Player>();
                playerContainer.AddChild(_player);
            }
            return _player;
        }

        protected override void Dispose(bool pDisposing)
        {
            #region singleton
            if (pDisposing && instance == this) instance = null;
            #endregion
            base.Dispose(pDisposing);
        }
    }
}