using Cars;
using System;

namespace CarPlateApp
{
    /// <summary>
    /// Main application class responsible for coordinating plate management and sorting.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Executes the main application workflow: retrieves plates, sorts them, and outputs results.
        /// </summary>
        public void Run()
        {
            var manager = new CarPlateManager();
            var sorter = new PlateSorter();
            
            var plates = manager.GetCarPlates();
            var sorted = sorter.SortByBrandFirstLetter(plates);

            foreach (var plate in sorted)
                Console.WriteLine(plate);
        }
    }
}