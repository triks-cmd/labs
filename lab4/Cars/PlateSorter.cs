using System;

namespace Cars
{
    /// <summary>
    /// Provides sorting functionality for car plates.
    /// </summary>
    public class PlateSorter
    {
        /// <summary>
        /// Sorts car plates alphabetically by the first letter of the vehicle brand.
        /// </summary>
        /// <param name="plates">An array of car plates in "Brand Number" format.</param>
        /// <returns>A new array sorted by the initial letter of the brand.</returns>
        public string[] SortByBrandFirstLetter(string[] plates)
        {
            string[] sortedPlates = (string[])plates.Clone();
            Array.Sort(sortedPlates, CompareByBrandInitial);
            return sortedPlates;
        }

        /// <summary>
        /// Comparison method for sorting car plates by the first letter of the brand.
        /// </summary>
        /// <param name="plate1">The first plate to compare.</param>
        /// <param name="plate2">The second plate to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative order of plate1 and plate2:
        /// less than zero if plate1 precedes plate2, zero if they are equal, 
        /// or greater than zero if plate1 follows plate2.
        /// </returns>
        private int CompareByBrandInitial(string plate1, string plate2)
        {
            string brand1 = plate1.Split(' ')[0];
            string brand2 = plate2.Split(' ')[0];

            return brand1[0].CompareTo(brand2[0]);
        }
    }
}