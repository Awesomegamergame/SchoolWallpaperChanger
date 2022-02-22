using System.Windows.Forms;

namespace SchoolWallpaperChanger.Functions
{
    internal class GetAspect
    {
        public static int ratio1;
        public static int ratio2;
        public static void Ratio()
        {
            int nGCD = GetGreatestCommonDivisor(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            string str = string.Format("{0}:{1}", Screen.PrimaryScreen.Bounds.Width / nGCD, Screen.PrimaryScreen.Bounds.Height / nGCD);
            ratio1 = Screen.PrimaryScreen.Bounds.Width / nGCD;
            ratio2 = Screen.PrimaryScreen.Bounds.Height / nGCD;
        }
        static int GetGreatestCommonDivisor(int a, int b)
        {
            return b == 0 ? a : GetGreatestCommonDivisor(b, a % b);
        }
    }
}
