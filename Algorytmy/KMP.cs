using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorytmy
{
    class KMP
    {
        static int patern_L;
      
        private int txt_length;
        private string patern;
        private string txt;
        private int [] sufix;

        private void sufix_fill ()
        {
            sufix = new int[patern.Length];
            sufix[0] = -1;
            for (int i = 1; i < patern.Length; i++)
            {
                int k = sufix[i - 1];
                while (patern[k + 1] != patern[i] && k >= 0) k = sufix[k];
                if (k == -1 && patern[0] != patern[i]) sufix[i] = -1;
                else sufix[i] = k + 1;
            }
            
            

        }  
        
        public KMP ( string PATERN, int txt_L, string TXT)
        {
            patern = PATERN;
            txt_length = txt_L;
            txt = TXT;
            sufix_fill();
            patern_L = patern.Length;
        }



    }
}
