using System;
using System.Collections.Generic;
using System.Text;
using TestProjrct.infrastructure;

namespace TestProjrct.Models
{
    public class VendingMachines
    {
        public VendingMachines()
        {
            Snacks = DataSeed.Snacks;
            Money = 10;// initial amount ot return back the change
        }
        
        public List<Snack> Snacks { get; set; }
        public double Money { get; set; }
    }
}
