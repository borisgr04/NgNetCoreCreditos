using Microsoft.AspNetCore.Identity;
using NgNetCore.Data;
using NgNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgNetCore.Config.Seguridad
{
    public interface IConfigSeguridad 
    {
        void InicializarAsync();
    }
    public class ConfigSeguridad: IConfigSeguridad
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public ConfigSeguridad
            (
            RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async void InicializarAsync()
        {
            var roles = new List<string>();
            roles.Add("Administrador");
            roles.Add("RegistrarCreditos");
            roles.Add("AprobarCreditos");
            ApplicationUser userAdmin = _userManager.FindByNameAsync("admin@hotmail.com").Result;
            if (userAdmin == null)
            {
                userAdmin = new ApplicationUser() {  UserName= "admin@hotmail.com" };
                var result=await _userManager.CreateAsync(userAdmin, "Admin2019.");
                if (!result.Succeeded) 
                {
                    throw new Exception("Error creando usuario Admin");
                }
            }
            foreach (var item in roles)
            {
                var identityResult = await _roleManager.CreateAsync(new IdentityRole() { Name = item });
                var isAdminInRole = await _userManager.IsInRoleAsync(userAdmin, item);
                if (!isAdminInRole)
                {
                    var result= await _userManager.AddToRoleAsync(userAdmin, item);
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Error creando usuario Role {item}");
                    }
                }
            }

            ApplicationUser userConsulta = _userManager.FindByNameAsync("consulta@hotmail.com").Result;
            if (userConsulta == null)
            {
                userConsulta = new ApplicationUser() { UserName = "consulta@hotmail.com" };
                var result = await _userManager.CreateAsync(userConsulta, "Consulta2019.");
                if (!result.Succeeded)
                {
                    throw new Exception("Error creando usuario Consulta");
                }
            }

        }
    }
}
