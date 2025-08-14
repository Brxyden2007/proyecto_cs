using proyecto_cs.src.modules.administradores.application;

namespace proyecto_cs.src.modules.administradores.ui
{
    public class MenuAdministradores
    {
        private readonly AdministradorService _adminService;

        public MenuAdministradores(AdministradorService adminService)
        {
            _adminService = adminService;
        }

        public async Task RegistrarAdministradorAsync()
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine() ?? "";

            Console.Write("Edad: ");
            int edad = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Nacionalidad: ");
            string nacionalidad = Console.ReadLine() ?? "";

            Console.Write("Documento de identidad: ");
            int documento = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Género: ");
            string genero = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Contraseña: ");
            string password = LeerPassword();

            await _adminService.RegistrarAdministradorAsync(
                nombre, apellido, edad, nacionalidad, documento, genero, email, password
            );

            Console.WriteLine("✅ Administrador registrado correctamente.");
        }

        private string LeerPassword()
        {
            string pass = "";
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass[0..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    pass += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
            return pass;
        }
        public async Task LoginAdministradorAsync()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Contraseña: ");
            string password = LeerPassword();

            bool loginOk = await _adminService.LoginAdministradorAsync(email, password);

            if (loginOk)
                Console.WriteLine("✅ Login exitoso, bienvenido administrador!");
            else
                Console.WriteLine("❌ Credenciales incorrectas.");
        }

    }
}
