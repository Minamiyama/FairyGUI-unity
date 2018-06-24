/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Basics
{
    public partial class UI_Main : GComponent
	{
		public Controller m_c1;
		public GComponent m_container;
		public GButton m_btn_Back;
		public GButton m_btn_Button;
		public GButton m_btn_Image;
		public GButton m_btn_Graph;
		public GButton m_btn_MovieClip;
		public GButton m_btn_Depth;
		public GButton m_btn_Loader;
		public GButton m_btn_List;
		public GButton m_btn_ProgressBar;
		public GButton m_btn_Slider;
		public GButton m_btn_ComboBox;
		public GButton m_btn_Clip_Scroll;
		public GButton m_btn_Controller;
		public GButton m_btn_Relation;
		public GButton m_btn_Label;
		public GButton m_btn_Popup;
		public GButton m_btn_Window;
		public GButton m_btn_Drag_Drop;
		public GButton m_btn_Component;
		public GButton m_btn_Grid;
		public GButton m_btn_Text;
		public GGroup m_btns;

		public const string URL = "ui://9leh0eyfrpmb1c";

		public static UI_Main CreateInstance()
		{
			return (UI_Main)UIPackage.CreateObject("Basics","Main");
		}

		public UI_Main()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_c1 = this.GetControllerAt(0);
			m_container = (GComponent)this.GetChildAt(1);
			m_btn_Back = (GButton)this.GetChildAt(3);
			m_btn_Button = (GButton)this.GetChildAt(4);
			m_btn_Image = (GButton)this.GetChildAt(5);
			m_btn_Graph = (GButton)this.GetChildAt(6);
			m_btn_MovieClip = (GButton)this.GetChildAt(7);
			m_btn_Depth = (GButton)this.GetChildAt(8);
			m_btn_Loader = (GButton)this.GetChildAt(9);
			m_btn_List = (GButton)this.GetChildAt(10);
			m_btn_ProgressBar = (GButton)this.GetChildAt(11);
			m_btn_Slider = (GButton)this.GetChildAt(12);
			m_btn_ComboBox = (GButton)this.GetChildAt(13);
			m_btn_Clip_Scroll = (GButton)this.GetChildAt(14);
			m_btn_Controller = (GButton)this.GetChildAt(15);
			m_btn_Relation = (GButton)this.GetChildAt(16);
			m_btn_Label = (GButton)this.GetChildAt(17);
			m_btn_Popup = (GButton)this.GetChildAt(18);
			m_btn_Window = (GButton)this.GetChildAt(19);
			m_btn_Drag_Drop = (GButton)this.GetChildAt(20);
			m_btn_Component = (GButton)this.GetChildAt(21);
			m_btn_Grid = (GButton)this.GetChildAt(22);
			m_btn_Text = (GButton)this.GetChildAt(23);
			m_btns = (GGroup)this.GetChildAt(24);
		}
	}
}