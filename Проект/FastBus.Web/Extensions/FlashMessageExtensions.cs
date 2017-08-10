using System.Collections.Generic;
using System.Web.Mvc;
using FastBus.Web.Models.Enums;

namespace FastBus.Web.Extensions
{
    public static class FlashMessageExtensions
    {
        public static void FlashSuccess(this Controller controller, string message)
        {
            AddMessage(controller, message, FlashLevel.Success);
        }

        public static void FlashAlert(this Controller controller, string message)
        {
            AddMessage(controller, message, FlashLevel.Alert);
        }

        public static void FlashWarning(this Controller controller, string message)
        {
            AddMessage(controller, message, FlashLevel.Warning);
        }

        public static void FlashInfo(this Controller controller, string message)
        {
            AddMessage(controller, message, FlashLevel.Info);
        }
       
        private static void AddMessage(Controller controller, string message, FlashLevel level)
        {
            var key = $"flash-{level.ToString().ToLower()}";
            var messages = controller.TempData.ContainsKey(key)
                ? (IList<string>)controller.TempData[key]
                : new List<string>();

            messages.Add(message);

            controller.TempData[key] = messages;
        }
    }
}