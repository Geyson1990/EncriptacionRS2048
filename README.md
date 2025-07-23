# 🔐 EncriptacionRS2048

Este proyecto en C# implementa un sistema de **desencriptación de datos usando RSA con claves de 2048 bits**, junto con un módulo de validación estructural de datos para entidades tipo `Documento`. Es útil para manejar información sensible como datos personales o financieros en un flujo seguro y validado.

## 🧠 ¿Qué hace este proyecto?

- **Desencripta textos encriptados con una clave pública**, utilizando la clave privada almacenada en `privateKey.xml`.
- **Valida propiedades de objetos tipo `Documento`**, como formatos de correo, fechas, campos numéricos, alfanuméricos y decimales.
- Facilita el procesamiento de registros con datos estructurados y protegidos.

## 🧪 Tecnologías y librerías usadas

- .NET (C#)
- System.Security.Cryptography (RSA)
- Expresiones regulares para validaciones

## 📂 Estructura principal

- `DesencriptarTexto(...)`: Método que convierte un texto encriptado en Base64 de vuelta a texto plano usando RSA 2048.
- `Validacion(...)`: Método que revisa múltiples propiedades del objeto `Documento` asegurando que cumplen con formato y longitud establecidos.
- `Documento`: Clase con más de 30 propiedades que representan información de identificación, contacto y detalles de solicitud.

## 🧾 Ejemplo de uso

```csharp
string textoEncriptado = "...";
string privateKeyXml = System.IO.File.ReadAllText("privateKey.xml");

using (var rsa = new RSACryptoServiceProvider(2048))
{
    rsa.FromXmlString(privateKeyXml);
    string textoPlano = DesencriptarTexto(textoEncriptado, rsa);
    Console.WriteLine(textoPlano);
}
