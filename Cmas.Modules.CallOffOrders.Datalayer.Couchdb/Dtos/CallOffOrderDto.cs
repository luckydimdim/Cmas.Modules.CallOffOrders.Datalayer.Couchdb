using Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos.Remuneration;
using System;
using System.Collections.Generic;

namespace Cmas.Modules.CallOffOrders.Datalayer.Couchdb.Dtos
{
    /// <summary>
    /// Наряд заказ
    /// </summary>
    public class CallOffOrderDto
    {
        /// <summary>
        /// Уникальный внутренний идентификатор
        /// </summary>
        public String Id;

        /// <summary>
        /// Идентификатор договора
        /// </summary>
        public String ContractId;

        /// <summary>
        /// Номер наряд заказа
        /// </summary>
        public String Number;

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime CreatedAt;

        /// <summary>
        /// Дата и время обновления
        /// </summary>
        public DateTime UpdatedAt;

        /// <summary>
        /// Наименование заказа (по сути - работы)
        /// </summary>
        public String Name;

        /// <summary>
        /// Вознаграждения
        /// </summary>
        public ICollection<BaseRemunerationDto> Remunerations;

        public CallOffOrderDto()
        {
            Remunerations = new List<BaseRemunerationDto>();
        }

        public String Status;

    }
}
