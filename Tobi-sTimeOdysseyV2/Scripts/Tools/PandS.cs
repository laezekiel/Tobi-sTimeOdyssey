using Godot;
using System;


// Author : Ironee

namespace Com.BeerAndDev.TobisTimeOdyssey.Tools
{
	static public class PandS
	{
		public struct NODE_P
		{
			public NODE_P()
			{
				_script = "script";
				_priority = "process_priority";
				_mode = "process_mode";
			}


			private string
				_script,
				_priority,
				_mode;


			public string Script { get { return _script; } }
			public string Priority { get { return _priority; } }
			public string Mode { get { return _mode; } }
		}



		public struct CANVAS_ITEM_P
		{
			public CANVAS_ITEM_P()
			{
				_material = "material";
				_useParentMaterial = "use_parent_material";
				_filter = "texture_filter";
				_repeat = "texture_repeat";
				_ZIndex = "z_index";
				_ZAsRelative = "z_as_relative";
				_YSortEnabled = "y_sort_enabled";
				_visible = "visible";
				_modulate = "modulate";
				_selfModulate = "self_modulate";
				_showBehindParent = "show_behind_parent";
				_topLevel = "top_level";
				_lightMask = "light_mask";
				_visibilityLayer = "visibility_layer";
			}


			private string
				_material,
				_useParentMaterial,
				_filter,
				_repeat,
				_ZIndex,
				_ZAsRelative,
				_YSortEnabled,
				_visible,
				_modulate,
				_selfModulate,
				_showBehindParent,
				_topLevel,
				_lightMask,
				_visibilityLayer;


			public string Material { get { return _material; } }
			public string Use_Parent_Material { get { return _useParentMaterial; } }
			public string Filter { get { return _filter; } }
			public string Repeat { get { return _repeat; } }
			public string Z_Index { get { return _ZIndex; } }
			public string Z_As_Relative { get { return _ZAsRelative; } }
			public string Y_Sort_Enabled { get { return _YSortEnabled; } }
			public string Visible { get { return _visible; } }
			public string Modulate { get { return _modulate; } }
			public string Self_Modulate { get { return _selfModulate; } }
			public string Show_Behind_Parent { get { return _showBehindParent; } }
			public string Top_Level { get { return _topLevel; } }
			public string Light_Mask { get { return _lightMask; } }
			public string Visibility_Layer { get { return _visibilityLayer; } }
		}



		public struct NODE_2D_P
		{
			public NODE_2D_P()
			{
				_position = "position";
				_rotation = "rotation";
				_scale = "scale";
				_skew = "skew";
			}


			private string
				_position,
				_rotation, 
				_scale, 
				_skew;


			public string Position { get { return _position;} }
			public string Rotation { get { return _rotation;} }
			public string Scale { get { return _scale;} }
			public string Skew { get { return _skew;} }
		}



		public struct COLLISION_OBJECT_2D_P
		{
			public COLLISION_OBJECT_2D_P()
			{
				_disableMode = "disable_mode";
				_pickable = "input_pickable";
			}


			private string
				_pickable,
				_disableMode;


			public string DisableMode { get { return _disableMode;} }
			public string Pickable { get { return _pickable;} }
		}



		public struct CHARACTER_BODY_2D_P
		{
			public CHARACTER_BODY_2D_P()
			{
				_safeMargin = "safe_margin";
				_layer = "collision_layer";
				_mask = "collision_mask";
				_priority = "collision_priority";
				_onLeave = "platform_on_leave";
				_floorLayers = "platform_floor_layers";
				_wallLayers = "platform_wall_layers";
				_stopOnSlope = "floor_stop_on_slope";
				_constantSpeed = "floor_constant_speed";
				_blockOnWall = "floor_block_on_wall";
				_maxAngle = "floor_max_angle";
				_snapLength = "floor_snap_length";
				_motionMode = "motion_mode";
				_upDirection = "up_direction";
				_slideOnCeiling = "slide_on_ceiling";
			}


			private string
				_safeMargin,
				_layer,
				_mask,
				_priority,
				_onLeave,
				_floorLayers,
				_wallLayers,
				_stopOnSlope,
				_constantSpeed,
				_blockOnWall,
				_maxAngle,
				_snapLength,
				_motionMode,
				_upDirection,
				_slideOnCeiling;


			public string Safe_Margin { get { return _safeMargin;} }
			public string Layer { get { return _layer;} }
			public string Mask { get { return _mask;} }
			public string Priority { get { return _priority;} }
			public string On_Leave { get { return _onLeave;} }
			public string Floor_Layers { get { return _floorLayers;} }
			public string Wall_Layers { get { return _wallLayers;} }
			public string Stop_On_Slope { get { return _stopOnSlope;} }
			public string Constant_Speed { get { return _constantSpeed;} }
			public string Block_On_Wall { get {  return _blockOnWall;} }
			public string Max_Angle { get { return _maxAngle;} }
			public string Snap_Length { get { return _snapLength;} }
			public string Motion_Mode { get { return _motionMode;} }
			public string Up_Direction { get { return _upDirection;} }
			public string Slide_On_Ceiling { get { return _slideOnCeiling;} }
		}



		public struct OBJECT_S
		{
			public OBJECT_S()
			{
				_scriptChanged = "script_changed";
				_propertyListChanged = "property_list_changed";
			}


			private string
				_scriptChanged,
				_propertyListChanged;


			public string Script_Changed { get { return _scriptChanged;} }
			public string Property_List_Changed { get { return _propertyListChanged;} }
		}



		public struct NODE_S
		{
			public NODE_S()
			{
				_treeExiting = "tree_exiting";
				_treeExited = "tree_exited";
				_treeEntered = "tree_entered";
				_renamed = "renamed";
				_ready = "ready";
				_childExitingTree = "child_exiting_tree";
				_childEnteredTree = "child_entered_tree";
			}


			private string
				_treeExiting,
				_treeExited, 
				_treeEntered, 
				_renamed, 
				_ready, 
				_childExitingTree, 
				_childEnteredTree;


			public string Tree_Exiting { get { return _treeExiting;} }
			public string Tree_Exited { get { return _treeExited;} }
			public string Tree_Entered { get { return _treeEntered;} }
			public string Renamed { get { return _renamed;} }
			public string Ready { get { return _ready;} }
			public string Child_Exiting_Tree { get { return _childExitingTree;} }
			public string Child_Entered_Tree { get { return _childEnteredTree;} }
		}



		public struct CANVAS_ITEM_S
		{
			public CANVAS_ITEM_S()
			{
				_draw = "draw";
				_hidden = "hidden";
				_itemRectChanged = "item_rect_changed";
				_visibilityChanged = "visibility_changed";
			}


			private string
				_draw,
				_hidden,
				_itemRectChanged,
				_visibilityChanged;


			public string Draw { get { return _draw;} }
			public string Hidden { get { return _hidden;} }
			public string Item_Rect_Changed { get { return _itemRectChanged;} }
			public string Visibility_Changed { get { return _visibilityChanged;} }
		}



		public struct COLLISION_OBJECT_2D_S
		{
			public COLLISION_OBJECT_2D_S() 
			{
				_inputEvent = "input_event";
				_mouseEntered = "mouse_entered";
				_mouseExited = "mouse_exited";
				_mouseShapeEntered = "mouse_shape_entered";
				_mouseShapeExited = "mouse_shape_exited";
			}


			private string
				_inputEvent,
				_mouseEntered,
				_mouseExited,
				_mouseShapeEntered,
				_mouseShapeExited;


			public string Input_Event { get { return _inputEvent; } }
			public string Mouse_Entered { get { return _mouseEntered; } }
			public string Mouse_Exited { get { return _mouseExited; } }
			public string Mouse_Shape_Entered { get { return _mouseShapeEntered; } }
			public string Mouse_Shape_Exited { get { return _mouseShapeExited;} }
		}



		public struct PROPERTIES
		{
			public PROPERTIES()
			{
				_node = new NODE_P();

				_canvasItem = new CANVAS_ITEM_P();

				_node2D = new NODE_2D_P();

				_collisionObject2D = new COLLISION_OBJECT_2D_P();

				_characterBody2D = new CHARACTER_BODY_2D_P();
			}


			private NODE_P _node;

			private CANVAS_ITEM_P _canvasItem;

			private NODE_2D_P _node2D;

			private COLLISION_OBJECT_2D_P _collisionObject2D;

			private CHARACTER_BODY_2D_P _characterBody2D;


			public NODE_P Node { get { return _node; } }

			public CANVAS_ITEM_P Canvas_Item { get { return _canvasItem; } }
		
			public NODE_2D_P Node_2D { get { return _node2D; } }

			public COLLISION_OBJECT_2D_P Collision_Object_2D { get { return _collisionObject2D; } }
	
			public CHARACTER_BODY_2D_P Character_Body_2D { get { return _characterBody2D; } }
		}



		public struct SIGNALS
		{
			public SIGNALS()
			{
				_objects = new OBJECT_S();

				_node = new NODE_S();

				_canvasItem = new CANVAS_ITEM_S() { };

				_collisionObject2D = new COLLISION_OBJECT_2D_S();
			}


			private OBJECT_S _objects;

			private NODE_S _node;

			private CANVAS_ITEM_S _canvasItem;

			private COLLISION_OBJECT_2D_S _collisionObject2D;


			public OBJECT_S Objects { get { return _objects;} }

			public NODE_S Node { get { return _node; } }

			public CANVAS_ITEM_S Canvas_Item { get { return _canvasItem; } }

			public COLLISION_OBJECT_2D_S Collision_Object_2D { get { return _collisionObject2D; }}
		}



		private static PROPERTIES _properties = new PROPERTIES();

		private static SIGNALS _signals = new SIGNALS();



		public static PROPERTIES Properties { get { return _properties; } }

		public static SIGNALS Signals { get { return _signals; } }
	}
}

