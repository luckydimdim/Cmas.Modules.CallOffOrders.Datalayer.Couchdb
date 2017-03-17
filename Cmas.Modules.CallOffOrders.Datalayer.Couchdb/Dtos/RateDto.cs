using System;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos
{
    public class RateDto
    {
        public int Id;

        public String Name;

        public bool IsRate;

        /// <summary>
        /// Ставка
        /// </summary>
        public double Amount;

        /// <summary>
        /// Валюта
        /// </summary>
        public String Currency;

        /// <summary>
        /// Ед. изм.
        /// </summary>
        public String UnitName;

    }
}
