using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EncriptacionRS2048
{
    class Entidades
    {
        static void Main(string[] args)
        {
            string texto = "P/2bquc0BBR0YJYEF+D9SIfkCpLkDvV7IumCHB06V1dL3iPRlLB0dmU2jInV1+ZjmxWwHWGrhwnmYLlnTK44AtE+qioQQqVPjhzO4D3NLJpMhI+rqB1jb2jut2w8AcmBxErtohMAgoJEoLFALPy4IDIU8AXDWu/X/QRi1Oo9nKPaAcbAqwVz4NSGg7Zm8YT3Aa/SAcEoriEePnipCV9Lxitn0983XfBxg3vxupReVMscr5+rYztJZebKLpwvPTGUFcSwOdFtVPTD6tgO1viVyffWRbt5xkQQ9/i+HgeDnkMc9ovO/PbYiVFO1kkuqgteSIcLsKIdAyP3ysgZG63HsA==";
            string privateKeyXml = System.IO.File.ReadAllText("privateKey.xml");

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(privateKeyXml);

                    // Desencripta el texto con la clave privada
                    Console.Write(DesencriptarTexto(texto, rsa));
                    var asds = DesencriptarTexto(texto, rsa);
                    var a = "";
                }
                catch (Exception ex)
                {
                    throw new Exception("El campo no se pudo desencriptar ya que es un tipo de encriptacion no valida.");
                }
                finally
                {
                    // Asegúrate de liberar los recursos del objeto RSA
                    rsa.PersistKeyInCsp = false;
                }
            }


            //// Crear una instancia de la clase Documento
            //Entidades.Documento documento = new Entidades.Documento();

            //// Asignar valores a las propiedades
            //documento.idEstructuraArchivo = 1;
            //documento.scoringNumeroContrato = "ABC123";
            //documento.fechaAprobacion = "2023-09-22";
            //documento.tipoIdentificacionT1 = "1";
            //documento.numeroIdentificacionT1 = "123456789";
            //documento.nombreCompletoT1 = "Juan Pérez";
            //documento.celularClienteT1 = "555 123 4567";
            //documento.direccionResidenciaT1 = "123 Calle Principal";
            //documento.telefonoResidenteT1 = "555 987 6543";
            //documento.emailT1 = "juan@example.com";
            //documento.tipoIdentificacionT2 = "Cédula";
            //documento.numeroIdentificacionT2 = "987654321";
            //documento.nombreCompletoT2 = "María González";
            //documento.celularClienteT2 = "555-567-8901";
            //documento.direccionResidenciaT2 = "456 Avenida Secundaria";
            //documento.telefonoResidenteT2 = "555-234-5678";
            //documento.emailT2 = "maria@example.com";
            //documento.tipoIdentificacionT3 = "Pasaporte";
            //documento.numeroIdentificacionT3 = "ASDF1234";
            //documento.nombreCompletoT3 = "Carlos Rodriguez";
            //documento.celularClienteT3 = "555-345-6789";
            //documento.direccionResidenciaT3 = "789 Calle Terciaria";
            //documento.telefonoResidenteT3 = "555-876-5432";
            //documento.emailT3 = "carlos@example.com";
            //documento.codigoOficina = "123";
            //documento.descripcionOficina = "Oficina Principal";
            //documento.codigoAsesor = "ASE456";
            //documento.subproducto = "1234";
            //documento.montoOtorgaVivienda = 100000.00;
            //documento.plazoMesesHipotecario = "240";
            //documento.proyecto = "Proyecto ABC";
            //documento.descripcionProyecto = "Descripción del Proyecto ABC";
            //documento.estadoDelInmueble = "Nuevo";
            //documento.descripcionEstadoInmueble = "Inmueble en perfecto estado";
            //documento.tasa = 4.5;
            //documento.condicionesOrganismoDecisor = "Aprobado por Comité de Crédito";





            //Type objeto = documento.GetType();
            //PropertyInfo[] propiedades = objeto.GetProperties();

            //    foreach (PropertyInfo propiedad in propiedades)
            //    {
            //        // Validar si la propiedad es de tipo string y no es nula
            //        if (propiedad.PropertyType == typeof(string))
            //        {
            //            string valor = (string)propiedad.GetValue(documento);

            //        // Validar que la propiedad sea alfanumérica y tenga una longitud igual o menor a 18
            //        bool esCorrecto = Validacion(propiedad.Name, valor);
            //        if (!esCorrecto)
            //        {
            //            Console.WriteLine("es correcto "+ esCorrecto.ToString());
            //        }

            //        }
            //    }


            //PropertyInfo pidEstructuraArchivo = objeto.GetProperty("idEstructuraArchivo"); 
            //string valorEstructuraArchivo = pidEstructuraArchivo.GetValue(documento).ToString();

            //PropertyInfo pscoringNumeroContrato = objeto.GetProperty("scoringNumeroContrato");
            //string valorNumeroContrato = pscoringNumeroContrato.GetValue(documento).ToString();


        }

        static string DesencriptarTexto(string textoEncriptado, RSACryptoServiceProvider rsa)
        {
            // Convierte la representación legible (Base64) en bytes
            byte[] bytesEncriptados = Convert.FromBase64String(textoEncriptado);

            // Desencripta los bytes con la clave privada
            byte[] bytesDesencriptados = rsa.Decrypt(bytesEncriptados, false);

            // Convierte los bytes desencriptados nuevamente en texto
            return Encoding.UTF8.GetString(bytesDesencriptados);
        }
        public static bool EsAlfanumerico(string input)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            return regex.IsMatch(input);
        }
        public static bool EsFechaValida(string input)
        {
            DateTime fecha;
            return DateTime.TryParse(input, out fecha);
        }
        public static bool EsNumerico(string input)
        {
            return input.All(char.IsDigit);
        }
        public static bool EsEmail(string email)
        {
            // Expresión regular para validar direcciones de correo electrónico
            string patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(patron);

            return regex.IsMatch(email);
        }
        public static bool EsoDecimal(string input, int maxEnteros, int maxDecimales)
        {
            // Expresión regular para validar números decimales positivos
            string patron = $"^\\d{{1,{maxEnteros}}}(\\.\\d{{1,{maxDecimales}}})?$";
            Regex regex = new Regex(patron);

            return regex.IsMatch(input);
        }

        public static bool Validacion(string propiedad, string valor)
        {
            switch (propiedad)
            {
                case "scoringNumeroContrato":
                    return (EsAlfanumerico(valor) && valor.Length <= 18);
                case "fechaAprobacion":
                    return (EsFechaValida(valor) && valor.Length <= 10);
                case "tipoIdentificacionT1":
                    return (EsNumerico(valor) && valor.Length <= 1);
                case "numeroIdentificacionT1":
                    return (EsAlfanumerico(valor) && valor.Length <= 15);
                case "nombreCompletoT1":
                    return (EsAlfanumerico(valor) && valor.Length <= 50);
                case "celularClienteT1":
                    return (EsAlfanumerico(valor) && valor.Length <= 10);
                case "direccionResidenciaT1":
                    return (EsAlfanumerico(valor) && valor.Length <= 53);
                case "telefonoResidenteT1":
                    return (EsAlfanumerico(valor) && valor.Length <= 10);
                case "emailT1":
                    return (EsEmail(valor) && valor.Length <= 60);
                case "tipoIdentificacionT2":
                    return (EsNumerico(valor) && valor.Length <= 1);
                case "numeroIdentificacionT2":
                    return (EsAlfanumerico(valor) && valor.Length <= 15);
                case "nombreCompletoT2":
                    return (EsAlfanumerico(valor) && valor.Length <= 50);
                case "celularClienteT2":
                    return (EsAlfanumerico(valor) && valor.Length <= 10);
                case "direccionResidenciaT2":
                    return (EsAlfanumerico(valor) && valor.Length <= 53);
                case "telefonoResidenteT2":
                    return (EsAlfanumerico(valor) && valor.Length <= 10);
                case "emailT2":
                    return (EsEmail(valor) && valor.Length <= 60);
                case "tipoIdentificacionT3":
                    return (EsNumerico(valor) && valor.Length <= 1);
                case "numeroIdentificacion31":
                    return (EsAlfanumerico(valor) && valor.Length <= 15);
                case "nombreCompletoT3":
                    return (EsAlfanumerico(valor) && valor.Length <= 50);
                case "celularClienteT3":
                    return (EsAlfanumerico(valor) && valor.Length <= 10);
                case "direccionResidenciaT3":
                    return (EsAlfanumerico(valor) && valor.Length <= 53);
                case "telefonoResidenteT3":
                    return (EsAlfanumerico(valor) && valor.Length <= 10);
                case "emailT3":
                    return (EsEmail(valor) && valor.Length <= 60); 
                case "codigoOficina":
                    return (EsAlfanumerico(valor) && valor.Length <= 4);
                case "descripcionOficina":
                    return (EsAlfanumerico(valor) && valor.Length <= 30);
                case "codigoAsesor":
                    return (EsAlfanumerico(valor) && valor.Length <= 8);
                case "subproducto":
                    return (EsAlfanumerico(valor) && valor.Length <= 4);
                case "montoOtorgaVivienda":
                    return (EsoDecimal(valor, maxEnteros: 15, maxDecimales: 2));
                case "plazoMesesHipotecario":
                    return (EsAlfanumerico(valor) && valor.Length <= 4);
                case "proyecto":
                    return (EsAlfanumerico(valor) && valor.Length <= 6);
                case "descripcionProyecto":
                    return (EsAlfanumerico(valor) && valor.Length <= 30);
                case "estadoDelInmueble":
                    return (EsNumerico(valor) && valor.Length <= 1);
                case "descripcionEstadoInmueble":
                    return (EsAlfanumerico(valor) && valor.Length <= 25);
                case "tasa":
                    return (EsoDecimal(valor, maxEnteros: 4, maxDecimales: 2));
                case "condicionesOrganismoDecisor":
                    return (EsAlfanumerico(valor) && valor.Length <= 375);
            }
            return false;
        }

        public class Documento
        {
            public int idEstructuraArchivo { get; set; }
            public string scoringNumeroContrato { get; set; }
            public string fechaAprobacion { get; set; }
            public string tipoIdentificacionT1 { get; set; }
            public string numeroIdentificacionT1 { get; set; }
            public string nombreCompletoT1 { get; set; }
            public string celularClienteT1 { get; set; }
            public string direccionResidenciaT1 { get; set; }
            public string telefonoResidenteT1 { get; set; }
            public string emailT1 { get; set; }
            public string tipoIdentificacionT2 { get; set; }
            public string numeroIdentificacionT2 { get; set; }
            public string nombreCompletoT2 { get; set; }
            public string celularClienteT2 { get; set; }
            public string direccionResidenciaT2 { get; set; }
            public string telefonoResidenteT2 { get; set; }
            public string emailT2 { get; set; }
            public string tipoIdentificacionT3 { get; set; }
            public string numeroIdentificacionT3 { get; set; }
            public string nombreCompletoT3 { get; set; }
            public string celularClienteT3 { get; set; }
            public string direccionResidenciaT3 { get; set; }
            public string telefonoResidenteT3 { get; set; }
            public string emailT3 { get; set; }
            public string codigoOficina { get; set; }
            public string descripcionOficina { get; set; }
            public string codigoAsesor { get; set; }
            public string subproducto { get; set; }
            public double montoOtorgaVivienda { get; set; }
            public string plazoMesesHipotecario { get; set; }
            public string proyecto { get; set; }
            public string descripcionProyecto { get; set; }
            public string estadoDelInmueble { get; set; }
            public string descripcionEstadoInmueble { get; set; }
            public double tasa { get; set; }
            public string condicionesOrganismoDecisor { get; set; }

        }
    }
}
