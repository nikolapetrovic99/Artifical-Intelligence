using IksOks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksOksLab
{
    public class GameLogic : IGameLogic
    {
        //List<HashObject> Objects=new List<HashObject>();
        HashTable t;//= new HashTable();
        public Potez VratiPotez(ContextIksOks c, int dubina)
        {            
            List<Potez> lista = c.GetListaMogucihPoteza();
            if ((lista.Count == 8 && c.NaPotezu==2) || (lista.Count == 9 && c.NaPotezu == 1))
            {
                t = new HashTable();//posto je dubina ukljucena u igru, na svaku novu igru popunjavam tabelu iznova, jer ako to ne bih radio onda bi u slucaju da je npr prvi put nakon pokretanja izabrana dubina 1, na sledecoj novoj igri, ako izaberemo neku drugu dubinu, ponovo se vrednosti iz tabele uzimale za dubinu 1(naravno sa trenutnim kodom)
                //bez ovog if-a ce se samo dodavati potezi koji nisu izracunati u tabelu(zaa x, za oks je svejedno, jer se svi ubace odjednom) u zavisnosti od prvog poteza korisnika kada klikne nova igra, medjutim razmisljam o optimalnosti i eventualnoj prepravci najboljih poteza u odnosu na dubinu i ne vidim nesto optimalnije resenje, jer svakako bih za racunanje boljeg poteza ponovo morao da ulazim u rekurzije i da vrsim neke provere i plus problem kada se predje sa vece dubine na manju(to vodi ka praznjenju tabele), tako da mi ovo sa ifom deluje ok
            }
            int best=int.MinValue;
            Potez p = new Potez();
            int a = 0;int aa=0;
            List<Potez> aaa=new List<Potez>();
            if (t.count != 0)
            {
                p = t.find(new HashObject(c.TrenutnoStanje));
                if (p != null)
                {
                    return p;
                }
            }

            Potez k = new Potez();
            for (int i = 0; i < lista.Count; i++)
            {
                int b = minimax(lista[i].NarednoStanje, false, c.NaPotezu,dubina);
                if (b > best)
                {
                    best = b;
                    k.x = lista[i].x;
                    k.y = lista[i].y;
                }
            }
            t.insert(new HashObject(k, c.TrenutnoStanje, best));
            return k;
            // throw new NotImplementedException();
        }
        public int Evaluate(ContextIksOks c)
        {
            int a;
            c.TrenutnoStanje.DaLiJeKraj(out a);

            switch (a)
            {
                /*case 0:
                    return 0;*/
                case 1:
                    return (c.NaPotezu == 1) ? 10 : -10;
                case 2:
                    return (c.NaPotezu == 2) ? 10 : -10;
            }

            return 0;
           // throw new NotImplementedException();
        }
        int l=0;
        public int minimax(ContextIksOks c, bool Manute, int h, int dubina)
        {
            l++;
            List<Potez> potezi = c.GetListaMogucihPoteza();
            c.NaPotezu = h;
            int a;
            c.TrenutnoStanje.DaLiJeKraj(out a);

            if (a == 1)
            {   return Evaluate(c); }
            else if (a == 2)
            {   return Evaluate(c); }
            else if (dubina==0 || (a==0 && potezi.Count == 0))
            {   return Evaluate(c); }

            if (Manute)
            {
                Potez p = new Potez();
                int best = int.MinValue;
                for (int i = 0; i < potezi.Count; i++)
                {
                    HashObject n=null;
                    if ((n=t.prekinirekurziju(new HashObject(potezi[i], potezi[i].PrethodnoStanje.TrenutnoStanje,0))) == null)//proveravam da li sam vec uneo najbolji potez, ako jesam nema potrebe da ga trazim ponovo
                    {
                        int b = minimax(potezi[i].NarednoStanje, false, c.NaPotezu, dubina - 1);
                        if (b > best)
                        {
                            p = potezi[i];
                            best = b;
                        }
                    }
                    else 
                    {
                        return n.prom;
                    }
                }
                //if (p != null)
                 if (t.find(new HashObject(p.PrethodnoStanje.TrenutnoStanje)) == null)
                    {
                        t.insert(new HashObject(p, p.PrethodnoStanje.TrenutnoStanje,best));
                    }
                return best;
            }
            else
            {
                Potez p = new Potez();
                int best = int.MaxValue;
                for (int i = 0; i < potezi.Count; i++)
                {
                    int b = minimax(potezi[i].NarednoStanje, true, c.NaPotezu, dubina - 1);
                    if (b < best)
                    {
                        p = potezi[i];
                        best = b;
                    }
                }
                //Objects.Add(new HashObject(p, p.PrethodnoStanje.TrenutnoStanje));
                return best;
            }  
        }
    }
}
