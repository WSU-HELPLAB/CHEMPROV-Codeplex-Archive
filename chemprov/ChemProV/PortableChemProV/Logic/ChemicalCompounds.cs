﻿using System;
using System.Net;
using System.ComponentModel;

namespace ChemProV.Logic
{
    public enum ChemicalCompounds : byte
    { 
        AceticAcid = 0,
        Ammonia,
        Benzene,
        CarbonDioxide,
        CarbonMonoxide,
        Cyclohexane,
        Ethane,
        Ethanol,
        Ethylene,
        Ethylene_Oxide,
        Formaldehyde,
        HydrochloricAcid,
        Hydrogen,
        Methane,
        Methanol,
        n_butane,
        n_hexane,
        n_octane,
        Nitrogen,
        Oxygen,
        PhosphoricAcid,
        Propane,
        SodiumHydroxide,
        SulfuricAcid,
        Toluene,
        Water,
        Xylene
    }

    public static class ChemicalCompoundOptions
    {
        // There were problems with upper case
        //public static readonly string[] All = new string[]{
        //    "Acetic Acid",
        //    "Ammonia",
        //    "Benzene",
        //    "Carbon Dioxide",
        //    "Carbon Monoxide",
        //    "Cyclohexane",
        //    "Ethane",
        //    "Ethanol",
        //    "Ethylene",
        //    "Hydrochloric Acid",
        //    "Hydrogen",
        //    "Methane",
        //    "Methanol",
        //    "n_butane",
        //    "n_hexane",
        //    "n_octane",
        //    "Nitrogen",
        //    "Oxygen",
        //    "Phosphoric Acid",
        //    "Propane",
        //    "Sodium Hydroxide",
        //    "Sulfuric Acid",
        //    "Toluene",
        //    "Water",
        //    "Xylene" };

        public static readonly string[] All = new string[]{
            "acetic acid",
            "ammonia",
            "benzene",
            "carbon dioxide",
            "carbon monoxide",
            "cyclohexane",
            "ethane",
            "ethanol",
            "ethylene",
            "ethylene oxide",
            "formaldehyde",
            "hydrochloric acid",
            "hydrogen",
            "methane",
            "methanol",
            "n_butane",
            "n_hexane",
            "n_octane",
            "nitrogen",
            "oxygen",
            "phosphoric acid",
            "propane",
            "sodium hydroxide",
            "sulfuric acid",
            "toluene",
            "water",
            "xylene"};
    }

    /// <summary>
    /// Used to format ChemicalCompounds into "pretty" strings 
    /// </summary>
    public static class ChemicalCompoundsFormatter
    {
        /// <summary>
        /// Breaks apart the string name of the AssignmentType based on upper camel casing.
        /// Will also convert underscores "_" into dashes "-".
        /// EX: SomeEnum_1 will get returned as "Some Enum-1".
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ToPrettyString(ChemicalCompounds compound)
        {
            string rawEnumValue = compound.ToString();
            rawEnumValue = rawEnumValue.Replace('_', '-');

            char[] characters = rawEnumValue.ToCharArray();

            string formattedValue = characters[0].ToString();
            for (int i = 1; i < characters.Length; i++)
            {
                if (char.IsUpper(characters[i]))
                {
                    formattedValue += " ";
                }
                formattedValue += characters[i].ToString();
            }
            return formattedValue.ToLower();
        }
    }
}
