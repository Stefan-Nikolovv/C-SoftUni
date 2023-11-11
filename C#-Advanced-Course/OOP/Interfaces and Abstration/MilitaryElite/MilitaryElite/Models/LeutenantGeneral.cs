﻿using MilitaryElite.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Models
{
    public class LeutenantGeneral : Private, ILieutenantGeneral
    {
        public LeutenantGeneral(int id, string firstName, string lastName, 
            decimal salary, IReadOnlyCollection<IPrivate> privates)
            : base(id, firstName, lastName, salary)
        {
            Privates = privates;
        }

        public IReadOnlyCollection<IPrivate> Privates { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("Privates:");
            foreach ( IPrivate priv in Privates )
            {
                sb.AppendLine($"  {priv.ToString()}");
            }
            return sb.ToString().TrimEnd(); 

        }
    }
}
