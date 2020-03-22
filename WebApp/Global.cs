using Models;
using System.Collections.Generic;

namespace WebApp
{
    public static class Global
    {
        public static Dictionary<string, InitialForm> UserInitialForms { get; set; } = new Dictionary<string, InitialForm>();

        public static Dictionary<string, DetailedForm> UserDetailedForms { get; set; } = new Dictionary<string, DetailedForm>();
    }
}
