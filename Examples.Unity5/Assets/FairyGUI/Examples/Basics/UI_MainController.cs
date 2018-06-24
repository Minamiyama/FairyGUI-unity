using FairyGUI;

namespace Basics
{
    public partial class UI_Main
    {
        public class m_c1Controller : Controller
        {
            public enum Index
            {
                _0 = 0,
                _1 = 1
            }

            public new Index selectedIndex
            {
                get { return (Index)base.selectedIndex; }
                set { base.selectedIndex = (int)value; }
            }

            public void SetSelectedIndex(Index index)
            {
                base.SetSelectedIndex((int)index);
            }
        }


    }
}