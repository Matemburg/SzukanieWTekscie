using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMSearch;

namespace Algorytmy
{
    class Tekst
    {
        public string txt;
        private BoyerMoore BM;
        List<int> suffixes = new List<int>();
        List<int> move = new List<int>();


        public Tekst(string text)
        {

            txt = text;

        }


        private List<int> pre_KMP(string Pattern)
        {
            List<int> MP_pi = new List<int>();
            int m = Pattern.Length;
            int i = 0;
            int j = -1;
            MP_pi.Add(j);
            while (i < m)
            {
                while (j > -1 && Pattern[i] != Pattern[j])
                {
                    j = MP_pi[j];
                }
                j++;
                i++;
                MP_pi.Add(j);
            }
            return MP_pi;

        }


        public int Wystapienie(string Pattern)
        {
            List<int> patern = pre_KMP(Pattern);
            int i = 0;
            int k = 0;
            int w = 0;
            while (i < txt.Length)
            {
                if (k == -1)
                {
                    i++;
                    k = 0;
                }
                else if (txt[i] == Pattern[k])
                {
                    i++;
                    k++;
                    if (k == Pattern.Length - 1)
                    {
                        w++;
                        k = -1;
                    }

                }
                else
                    k = patern[k];
            }
            return w - 1;
        }


        public int KMPSearch(string pattern)
        {
            int w = 0;
            string text = txt;
            int N = text.Length;
            int M = pattern.Length;

            if (N < M) return -1;
            if (N == M && text == pattern) return 0;
            if (M == 0) return 0;

            int[] lpsArray = new int[M];
            List<int> matchedIndex = new List<int>();

            LongestPrefixSuffix(pattern, ref lpsArray);

            int i = 0, j = 0;
            while (i < N)
            {
                if (text[i] == pattern[j])
                {
                    i++;
                    j++;
                }

                // match found at i-j
                if (j == M)
                {

                    w++;
                    j = lpsArray[j - 1];
                }
                else if (i < N && text[i] != pattern[j])
                {
                    if (j != 0)
                    {
                        j = lpsArray[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            return w;
        }

        public void LongestPrefixSuffix(string pattern, ref int[] lpsArray)
        {
            int M = pattern.Length;
            int len = 0;
            lpsArray[0] = 0;
            int i = 1;

            while (i < M)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lpsArray[i] = len;
                    i++;
                }
                else
                {
                    if (len == 0)
                    {
                        lpsArray[i] = 0;
                        i++;
                    }
                    else
                    {
                        len = lpsArray[len - 1];
                    }
                }
            }
        }


        public int SundaySeartch(string pattern)
        {
            

            int[] moveTable = new int[122];

            for (int l = 0; l < 122; l++)
            {
                moveTable[l] = -1;
            }

            for (int l = 0; l < pattern.Length; l++)
            {
                moveTable[pattern[l] - 32] = l;
            }

       

            int ilosc_wystapien = 0;

            int i = 0;
            int j = 0;
            int k = 0;
      
            while (i < txt.Length - pattern.Length)
            {
                j = 0;
                while(txt[i]>'z')
                {
                    i++;
                }
                if (txt[i] == pattern[j])
                {
                    k = i + 1;
                    j++;
                    while (txt[k] == pattern[j])
                    {
                        k++;
                        j++;
                        if (j == pattern.Length)
                        {
                            ilosc_wystapien++;
                            break;
                        }
                    }
                }
       
                i = i + pattern.Length;
                while (txt[i] > 122 || txt[i] < 32)
                {
                    i++;
                    if (i >= txt.Length)
                        break;
                }
                i -= moveTable[txt[i]-32];
            }
            k = txt.Length - pattern.Length;
            if(k<0)
            {
                return ilosc_wystapien;
            }
            j = 0;
            while (txt[k] == pattern[j])
            {
                k++;
                j++;
                if (j == pattern.Length)
                {
                    ilosc_wystapien++;
                    break;
                }
            }

            return ilosc_wystapien;
        }

        public int BMSeartch()
        {
            return BM.Search(txt);
        }
        public void preBM(string pattern)
        {
             BM = new BoyerMoore(pattern);
     
        }


  public int SundayMeta ( string _wzorzec)
        {

                string wzorzec = _wzorzec;
                int dlugoscWzorca = wzorzec.Length;

                int przesuniecia = 127 - 32;

                List<int> tablicaPrzesuniec = new List<int>();
                for(int o=0;o<przesuniecia+1;o++)
            {
                tablicaPrzesuniec.Add(0);
            }

                bool czyByl = false;
                char a;
                int licznik = -1;
                for (int o = 0; o < dlugoscWzorca; o++)
                {
                    a = wzorzec[o];
                    licznik++;
                    if (a == '?')
                    {
                        czyByl = true;
                    }
                }

                if (czyByl == true)
                {
                    for (int o = 0;o < przesuniecia; o++)
                    {
                        tablicaPrzesuniec[o] = licznik;
                    }
                }
                else
                {
                    for (int o = 0; o < przesuniecia; o++)
                    {
                        tablicaPrzesuniec[o] = -1;
                    }
                }

                for (int o = 0; o < dlugoscWzorca; o++)
                {
                    tablicaPrzesuniec[wzorzec[o] - 32] = o;
                }

 


                string tekst = txt;
                int dlugoscTekstu = txt.Length;


                int ilosc_wystapien = 0;
                int i = 0;
                int j = 0;

                while (i < dlugoscTekstu - dlugoscWzorca)
                {
                    j = 0;
                    if (tekst[i] == wzorzec[j] || wzorzec[j] == '?')
                    {
                        j++;
                        while ((tekst[i + j] == wzorzec[j-1] || wzorzec[j-1] == '?') && j < dlugoscWzorca)
                        {
                            j++;
                        }
                        if (j == dlugoscWzorca)
                        {
                            ilosc_wystapien++;
                        }
                    }
                    i = i + dlugoscWzorca;
                    i -= tablicaPrzesuniec[tekst[i] - 32];
                }

                i = dlugoscTekstu - dlugoscWzorca;
                j = 0;
                while (tekst[i + j] == wzorzec[j] || wzorzec[j] == '?')
                {
                    j++;
                    if (j == dlugoscWzorca)
                    {
                        ilosc_wystapien++;
                        break;
                    }
                }
            return ilosc_wystapien;
            }
    }







}


