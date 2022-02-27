using System;
using System.Collections.Generic;
using System.Text;

namespace GorselProje5
{
    class RandomSayiEkle
    {
        Random rnd = new Random();
        int sayi = 0;

        public RandomSayiEkle(int aralik1, int aralik2)
        {
            this.sayi = rnd.Next(aralik1, aralik2);
        }

        public int sayiyiAl()
        {
            return sayi;
        }
    }
}
