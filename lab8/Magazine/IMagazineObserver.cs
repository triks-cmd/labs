namespace MagazineApp
{
    /// <summary>
    /// Интерфейс наблюдателя за изменениями журнала.
    /// </summary>
    public interface IMagazineObserver
    {
        /// <summary>
        /// Вызывается при изменении названия журнала.
        /// </summary>
        void OnMagazineNameChanged(string oldName, string newName);

        /// <summary>
        /// Вызывается при изменении стоимости подписки на журнал.
        /// </summary>
        void OnMagazinePriceChanged(string magazineName, decimal newPrice);
    }
}
