using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMSearch
{
    class UnicodeSkipArray
    {
        // Pattern length used for default byte value
        private byte _patternLength;
        // Default byte array (filled with default value)
        private byte[] _default;
        // Array to hold byte arrays
        private byte[][] _skipTable;
        // Size of each block
        private const int BlockSize = 0x100;

        /// <summary>
        /// Initializes this UnicodeSkipTable instance
        /// </summary>
        /// <param name="patternLength">Length of BM pattern</param>
        public UnicodeSkipArray(int patternLength)
        {
            // Default value (length of pattern being searched)
            _patternLength = (byte)patternLength;
            // Default table (filled with default value)
            _default = new byte[BlockSize];
            InitializeBlock(_default);
            // Master table (array of arrays)
            _skipTable = new byte[BlockSize][];
            for (int i = 0; i < BlockSize; i++)
                _skipTable[i] = _default;
        }


        public byte this[int index]
        {
            get
            {
                // Return value
                return _skipTable[index / BlockSize][index % BlockSize];
            }
            set
            {
                // Get array that contains value to set
                int i = (index / BlockSize);
                // Does it reference the default table?
                if (_skipTable[i] == _default)
                {
                    // Yes, value goes in a new table
                    _skipTable[i] = new byte[BlockSize];
                    InitializeBlock(_skipTable[i]);
                }
                // Set value
                _skipTable[i][index % BlockSize] = value;
            }
        }

        /// <summary>
        /// Initializes a block to hold the current "nomatch" value.
        /// </summary>
        /// <param name="block">Block to be initialized</param>
        private void InitializeBlock(byte[] block)
        {
            for (int i = 0; i < BlockSize; i++)
                block[i] = _patternLength;
        }
    }


   




    class BoyerMoore
    {
        private string _pattern;
        private UnicodeSkipArray _skipArray;
        private List<int> suffixes = new List<int>();
        private List<int> move = new List<int>();

        void bmPreprocess1(string p)
        {
            int m = p.Length;
            int i = m, j = m + 1;
            suffixes[i] = j;
            while (i > 0)
            {
                while (j <= m && p[i - 1] != p[j - 1])
                {
                    if (move[j] == 0) move[j] = j - i;
                    j = suffixes[j];
                }
                i--; j--;
                suffixes[i] = j;
            }
        }

        void bmPreprocess2()
        {
            int j = suffixes[0];
            for (int i = 0; i < suffixes.Capacity; i++)
            {
                if (move[i] == 0) move[i] = j;
                if (i == j) j = suffixes[j];
            }
        }

       public void find_table( string pattern)
    {
        int m = pattern.Length;
          
       

    }
    public int bmSearch(string p, string t)
    {
        suffixes = new List<int>();
        move = new List<int>();
            for (int o = 0; o < p.Length+1; o++)
            {
                move.Add(0);
                suffixes.Add(0);
            }
            bmPreprocess1(p);
            bmPreprocess2();
            int wys = 0;
        List<int> occ= new List<int>();
            for(int o=0; o<256; o++)
            {
                occ.Add(-1);
            }
        for (int u = 0; u < p.Length; u++)
            occ[p[u]] = u;
        int m = p.Length;
        int n = t.Length;
        int i = 0, j;
        while (i <= n - m)
        {
            j = m - 1;
            while (j >= 0 && p[j] == t[i + j]) j--;
            if (j < 0)
            {
                i += move[0];
                    wys++;
                
            }
            else
                i += Math.Max(move[j + 1], j - occ[t[i + j]]);
        }
            return wys;
    }


    public BoyerMoore(string pattern)
        {
            Initialize(pattern);
        }



        public void Initialize(string pattern)
        {
            _pattern = pattern;

            _skipArray = new UnicodeSkipArray(_pattern.Length);

                for (int i = 0; i < _pattern.Length - 1; i++)
                    _skipArray[_pattern[i]] = (byte)(_pattern.Length - i - 1);
            
        }


        public int Search(string text)
        {
            int i = 0;
            int w=0;

            while (i <= (text.Length - _pattern.Length))
            {
                int j = _pattern.Length - 1;

                while (j >= 0 && _pattern[j] == text[i + j])
                    j--;


                if (j < 0)
                {

                    w++;
                    j = _pattern.Length - 1;
                }


                i += Math.Max(_skipArray[text[i + j]] - _pattern.Length + 1 + j, 1);
            }
            return w;
        }

    
    }
}
