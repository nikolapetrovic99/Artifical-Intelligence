using IksOks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksOksLab
{
    class HashObject
    {
        Potez value;
        int[,] mat;
        public int prom;
        public HashObject()
        {
            
            this.Value = null;
            Mat = new int[3, 3];
        }
        public HashObject(Potez v, Tabla t, int a)
        {
            prom = a;
            this.Value = v;
            this.mat = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.Mat[i, j] = t.Polje(i, j);
                }
            }
        }
        public HashObject(Tabla t)
        {
            this.mat = new int[3, 3];
            
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    this.Mat[i, j] = t.Polje(i, j);
                }
            }
        }
        public bool checktable(int[,] t)
        {

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (this.Mat[i, j] != t[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public Potez Value { get => value; set => this.value = value; }
        public int[,] Mat { get => mat; set => mat = value; }
        public int returnkey()
        { 
            int a=0;
            for (int i = 83; i < 86; i++)
            {
                for (int j = 147; j < 150; j++)
                {
                    a += Mat[i-83,j-147]*(i*13)*j+(i*j+j*4)*100;
                }
            }
            return a;
        }
    }
}
