using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreDrugs.Models
{
    // Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
    // TetraChemicalItem
    using System.Collections;
    using GameNetcodeStuff;
    using Unity.Netcode;
    using UnityEngine;

    public class TestDrug : TetraChemicalItem
    {
        protected override string __getTypeName()
        {
            return "TestDrug";
        }

    }
}
