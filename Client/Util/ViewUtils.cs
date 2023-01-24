using RestaurantSystem.Core.Enums;

namespace RestaurantSystem.Client.Util
{
    public static class ViewUtils
    {
        public static string TranslateOrderStatus(string statusEnum)
        {
            switch (statusEnum)
            {
                case "PENDING":
                    return "Pendiente";
                case "IN_PROGRESS":
                    return "En Preparación";
                case "DONE":
                    return "Terminada";
                default:
                    return "Desconocido";
            }
        }
    }
}
