using System;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos
{
    public class RateOptionsDto
    {
        /// <summary>
        /// Ставка
        /// </summary>
        public double Rate;

        /// <summary>
        /// Валюта
        /// </summary>
        public String Currency;

        /// <summary>
        /// Ед. изм.
        /// </summary>
        public String RemunerationUnit;

    }
}
