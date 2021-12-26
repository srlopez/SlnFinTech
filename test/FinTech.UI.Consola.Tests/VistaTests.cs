using System;
using System.IO;
using System.Collections.Generic;
using Xunit;

namespace FinTech.UI.Consola
{
    /*
 
    */
    public class VistaTests
    {

        [Fact]
        public void TryObtenerDatoDeTipo_default_Test()
        {
            // Given
            var input = new StringReader("\n");//Enter
            Console.SetIn(input);

            var vista = new Vista();
            int esperado = 16;
            // When
            int resultado = vista.TryObtenerDatoDeTipo<int>("int", "16");
            // Then
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void TryObtenerDatoDeTipo_Int_Test()
        {
            // Given
            var input = new StringReader("16");
            Console.SetIn(input);

            var vista = new Vista();
            int esperado = 16;
            // When
            int resultado = vista.TryObtenerDatoDeTipo<int>("int");
            // Then
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void TryObtenerDatoDeTipo_Exception_Test()
        {
            // Given
            var input = new StringReader("abc");
            Console.SetIn(input);
            var vista = new Vista();
            Action testCode = () => vista.TryObtenerDatoDeTipo<int>("int");
            // When
            var ex = Record.Exception(testCode);
            // Then
            Assert.NotNull(ex);
            Assert.IsType<NullReferenceException>(ex);
            //Assert.Throws<NullReferenceException>(testCode);
        }

        [Fact]
        public void TryObtenerElementoDeLista_String_Test()
        {
            // Given
            var idx = 2;
            var datos = new List<string> { "dato1", "dato2", "dato3" };

            var input = new StringReader(idx.ToString());
            Console.SetIn(input);

            var vista = new Vista();
            var esperado = datos[idx - 1];
            // When
            var resultado = vista.TryObtenerElementoDeLista<string>("tit", datos, "prompt");
            // Then
            Assert.Equal(esperado, resultado);
        }
    }
}
