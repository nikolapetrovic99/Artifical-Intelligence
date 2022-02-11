using IksOks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksOksLab
{
	class HashTable
	{
		int length = 4096;
		HashObject[] table;
		public int count = 0;
		public HashTable()
		{
			table = new HashObject[length];
		}
		int h1(HashObject obj)
		{
			return ((int)((f1(obj.returnkey())) % length));
		}
		int h2(int a)
		{
			return (a+1)%length;
		}
		uint f1(int key)
		{
			uint pom = 2654435769;
			pom = pom * (uint)key;
			return pom >> (32 - 12);
		}

		public void insert(HashObject obj)
		{
			if (count == length - 1)
			{
				throw new Exception("izuzetak");
			}
			else
			{
				if (table[h1(obj)] == null)
				{
					table[h1(obj)] = obj;
					count++;
				}
				else 
				{
					
					int a = h2(h1(obj));
					while (table[a] != null)
					{
						
						a = h2(a);
					}
					if (table[a] == null)
					{
						table[a] = obj;
						count++;
					}
					else { throw new Exception("izuzetak"); }
				}
			}
		}
	    public Potez find(HashObject obj)
		{ 
				int hash = (h1(obj));
				int a = hash;
				if(table[a] != null &&  table[a].checktable(obj.Mat))
				{
					return table[a].Value;
				}
				else
				{
					while (table[a]!=null && !table[a].checktable(obj.Mat))
					{
						a = h2(a);
					}
				if (table[a] == null)
				{ return null; }
                
				return table[a].Value;
				}
		}

		public HashObject prekinirekurziju(HashObject obj)
		{
			int hash = (h1(obj));
			int a = hash;
			if (table[a] != null && table[a].checktable(obj.Mat))
			{
				if (table[a].Value.x == obj.Value.x && table[a].Value.y == obj.Value.y)
				{
					return table[a];
				}
			}
			
			while (table[a] != null && (table[a].checktable(obj.Mat)==false || table[a].Value.x == obj.Value.x || table[a].Value.y == obj.Value.y)) 
			{
				a = h2(a);
			}
			if (table[a] == null)
			{
				return null;
			}
			else 
			{
				return table[a];
			}
		}
	};
}
