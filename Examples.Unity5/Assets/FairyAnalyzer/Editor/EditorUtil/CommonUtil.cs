namespace FairyAnalyzer
{
    public class CommonUtil
    {
        public bool IsDefaultName(string _name)
        {
            if (true == _name.StartsWith("n") || true == _name.StartsWith("c") || true == _name.StartsWith("t"))
            {
                return true;
            }
            return false;
        }
    }
}