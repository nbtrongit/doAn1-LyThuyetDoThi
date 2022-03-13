using System;
using System.IO;

namespace DoAn1_LTDT
{
    public struct maTranKe
    {
        public int n;
        public int[,] maTran;
        public int start;
        public int goal;
    }

    public struct danhSachKe
    {
        public int n;
        public int[,] danhSach;
    }
    class Program
    {
        public static danhSachKe danhSach(string filename)
        {
            string[] s = File.ReadAllLines(filename);
            danhSachKe AL;
            AL.n = int.Parse(s[0]);
            AL.danhSach = new int[AL.n, AL.n];
            for (int i = 0; i < AL.danhSach.GetLength(0); i++)
            {
                for (int j = 0; j < AL.danhSach.GetLength(1); j++)
                {
                    AL.danhSach[i, j] = -1;
                }
            }
            int a = 0;
            for (int i = 1; i < s.Length; i++)
            {
                string[] c = s[i].Split(' ');
                for (int j = 0; j < c.Length; j++)
                {
                    AL.danhSach[a, j] = int.Parse(c[j]);
                }
                a++;
            }
            return AL;
        }
        public static maTranKe ChuyenDoi(danhSachKe AL)
        {
            maTranKe AM;
            AM.n = AL.n;
            AM.maTran = new int[AM.n, AM.n];
            for (int i = 0; i < AL.danhSach.GetLength(0); i++)
            {
                for (int j = 1; j < AL.danhSach.GetLength(1); j++)
                {
                    if (AL.danhSach[i, j] != -1)
                    {
                        AM.maTran[i, AL.danhSach[i, j]] = 1;
                    }
                }
            }
            AM.start = 0;
            AM.goal = 0;
            return AM;
        }
        public static void DFS(int[,] maTranKe, bool[] viengTham, int u)
        {
            if(viengTham[u] == false)
            {
                viengTham[u] = true;
            }
            for(int i = 0; i < maTranKe.GetLength(0); i++)
            {
                if(viengTham[i] == false && maTranKe[u, i] == 1)
                {
                    DFS(maTranKe, viengTham, i);
                }    
            }
        }
        public static bool duongDi(maTranKe AM, bool[] viengTham)
        {
            if(AM.maTran[AM.start,AM.goal] == 1)
            {
                return true;
            }
            for(int i = 0; i < AM.n; i++)
            {
                if(viengTham[i] && i != AM.start)
                {
                    if (AM.maTran[i,AM.goal] == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static int[,] XuLyMaTranDuongDi(maTranKe AM)
        {
            int[,] duongdi = new int[AM.n, AM.n];
            for(int i = 0; i < AM.n; i++)
            {
                AM.start = i;
                for (int j = 0; j < AM.n; j++)
                {
                    AM.goal = j;
                    bool[] viengTham = new bool[AM.n];
                    DFS(AM.maTran, viengTham, AM.start);
                    if (duongDi(AM, viengTham))
                    {
                        duongdi[i, j] = 1;
                    }
                }    
            }    
            return duongdi;
        }
        public static bool LienThongManh(int[,] MaTranDuongDi)
        {
            for (int i = 0; i < MaTranDuongDi.GetLength(0); i++)
            {
                for (int j = 0; j < MaTranDuongDi.GetLength(1); j++)
                {
                    if(MaTranDuongDi[i,j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool LienThongTungPhan(int[,] MaTranDuongDi)
        {
            for (int i = 0; i < MaTranDuongDi.GetLength(0); i++)
            {
                for (int j = 0; j < MaTranDuongDi.GetLength(1); j++)
                {
                    if (i != j && MaTranDuongDi[i, j] == 0 && MaTranDuongDi[j, i] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool LienThongYeu(int[,] maTranKe)
        {
            int[,] maTranNen = maTranKe;
            for (int i = 0; i < maTranNen.GetLength(0); i++)
            {
                for (int j = 0; j < maTranNen.GetLength(1); j++)
                {
                    if(maTranNen[i,j] == 1)
                    {
                        maTranNen[i, j] = 1;
                        maTranNen[j, i] = 1;
                    }
                }
            }
            bool[] viengTham = new bool[maTranNen.GetLength(0)];
            DFS(maTranNen, viengTham, 0);
            for(int i = 0; i < viengTham.Length; i++)
            { 
                if(viengTham[i] == false)
                {
                    return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Nhập vào đường dẫn file/tên file");
            string filename = Console.ReadLine();
            if (!File.Exists(filename))
            {
                Console.WriteLine("File không tồn tại");
            }
            else
            {
                danhSachKe AL;
                AL = danhSach(filename);
                maTranKe AM;
                AM = ChuyenDoi(AL);
                int[,] MaTranDuongDi = new int[AM.n, AM.n];
                MaTranDuongDi = XuLyMaTranDuongDi(AM);
                if (LienThongManh(MaTranDuongDi))
                {
                    Console.WriteLine("Đồ thị liên thông mạnh");
                }
                else if (LienThongTungPhan(MaTranDuongDi))
                {
                    Console.WriteLine("Đồ thị liên thông từng phần");
                }
                else if (LienThongYeu(AM.maTran))
                {
                    Console.WriteLine("Đồ thị liên thông yếu");
                }
                else
                {
                    Console.WriteLine("Đồ thị không liên thông");
                }
            }
        }
    }
}
