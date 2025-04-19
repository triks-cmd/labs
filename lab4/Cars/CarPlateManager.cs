namespace CarPlateApp
{
    /// <summary>
    /// Provides access to predefined car plate data.
    /// </summary>
    public class CarPlateManager
    {
        /// <summary>
        /// Retrieves an array of car plates with associated brands and numbers.
        /// </summary>
        /// <returns>An array of formatted car plate strings.</returns>
        public string[] GetCarPlates() => new[] 
        {
            "Toyota 1234 AB-5",
            "Lada 3417 CZ-3",
            "Audi 4567 XY-9",
            "Volvo 9876 QW-7",
            "Chevrolet 1357 RT-2"
        };
    }
}