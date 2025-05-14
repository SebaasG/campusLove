using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.dto
{
    public class DtoRegisterUser
    {
        //Atributos de usuario
        public string doc { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public int genderId { get; set; }
        public int cityId { get; set; }
        public int careerId { get; set; }

        //Atributos de credenciales
        public string username { get; set; }
        public string password { get; set; }
    }
}